using System;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Data.Common;
using SAMM.DataAccess.Infrastructure;

namespace SAMM.Decoder.Domain
{
    public class InputMessage
    {
        public InputMessage()
        {
        }

        // Methods
        public static bool MsgFormatting(string message)
        {
            string clean = Regex.Replace(message, @"\s+", "");
            
            var header = HeaderAnalizer(clean.ToUpper());
            var body = BodyAnalizer(clean.ToUpper());
            if (header.Item1 && body.Item1)
            {
                string server = EnvLoader.GetEnvVariable("DB_SERVER");
                string database = EnvLoader.GetEnvVariable("DB_DATABASE");
                string user = EnvLoader.GetEnvVariable("DB_USER");
                string password = EnvLoader.GetEnvVariable("DB_PASSWORD");

                string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
                Infraestructure.DatabaseConnection db = new Infraestructure.DatabaseConnection(connectionString);



                MySqlParameter[] headerParameters = new MySqlParameter[8]
                {
                    new MySqlParameter("@p_octante" , header.Item4),
                    new MySqlParameter("@p_latitud" , header.Item2),
                    new MySqlParameter("@p_longitud", header.Item3),
                    new MySqlParameter("@p_fecha"   , header.Item5),
                    new MySqlParameter("@p_hora"    , header.Item6),
                    new MySqlParameter("@p_validez" , header.Item7),
                    new MySqlParameter("@p_altitud" , header.Item8),
                    new MySqlParameter("@p_presion" , header.Item9)
                };
                db.ExecuteStoredProcedure("InsertMetb", headerParameters);
                int lastMessageId = db.GetLastInsertedId();
                
                foreach (var bodyData in body.Item2)
                {
                    MySqlParameter[] bodyParameters = new MySqlParameter[6]
                    {
                        new MySqlParameter("@p_id_mensaje",   lastMessageId ),
                        new MySqlParameter("@p_id_capa"   ,   bodyData.Item1),
                        new MySqlParameter("@p_dir_viento",   bodyData.Item2),
                        new MySqlParameter("@p_vel_viento",   bodyData.Item3),
                        new MySqlParameter("@p_temp_air"  ,   bodyData.Item4),
                        new MySqlParameter("@p_dens_air"  ,   bodyData.Item5)
                    };

                    db.ExecuteStoredProcedure("InsertCuerpoMetb", bodyParameters);
                }

                return true;
            }
            else
            {
                return false;
            }
        }


        internal static (bool, double, double, string, string, string, int, int, float) HeaderAnalizer(string message)
        {
            List<string> groups = new List<string>();

            if (message.Length < 36)
            {
                return (false, 0, 0, "", "", "", 0, 0, 0);
            }

            string header = message[..24];

            for (int i = 0; i < header.Length; i += 6)
            {
                groups.Add(header.Substring(i, 6));
            }

            if (groups.Count < 4)
            {
                return (false, 0, 0, "", "", "", 0, 0, 0);
            }

            string firstGroup = groups[0];
            string secondGroup = groups[1];
            string thirdGroup = groups[2];
            string fourthGroup = groups[3];
            char octant = firstGroup[5];

            if (!(firstGroup.StartsWith("METB3") || firstGroup.StartsWith("METCM")))
            {
                return (false, 0, 0, "", "", "", 0, 0, 0);
            }

            if (!int.TryParse(secondGroup[..3], out int rawLatitude) ||
                !int.TryParse(secondGroup.Substring(3, 3), out int rawLongitude))
            {
                return (false, 0, 0, "", "", "", 0, 0, 0);
            }
            
            bool valid = true;
            var Time = Decode.ProcessTime(thirdGroup);
            var Coords = Decode.ProcessCoordinates(octant, rawLatitude, rawLongitude);
            var Alt = Decode.ProcessAltitude(fourthGroup);

            if (!Time.Item1)
            {
                valid = false;
            }
            else if (!Coords.Item1)
            {
                valid = false;

            }

            else if (!Alt.Item1)
            {
                valid = false;

            }
            return (valid, Coords.Item2, Coords.Item3, Coords.Item4, Time.Item2, Time.Item3, Time.Item4, Alt.Item2, Alt.Item3);
        }

        public static (bool, List<(int, int, int, float, float)>) BodyAnalizer(string message)
        {
            List<string> bodyGroups = new List<string>();

            if (message.Length <= 36)
            {;
                return (false, new List<(int, int, int, float, float)>());
            }

            string body = message[24..];

            if (body.Length % 12 != 0)
            {;
                return (false, new List<(int, int, int, float, float)>());
            }

            for (int i = 0; i < body.Length; i += 12)
            {
                if (i + 12 <= body.Length)
                {
                    bodyGroups.Add(body.Substring(i, 12));
                }
                else
                {
                    bodyGroups.Add(body[i..]);
                }
            }
            
            List<(int, int, int, float, float)> bodyData = new List<( int, int, int, float, float)>();

            for (int i = 0; i < bodyGroups.Count; i++)
            {   
                var data = Decode.ProcessBody(bodyGroups[i]);
                if (!data.Item1)
                {
                    return (false, new List<(int, int, int, float, float)>());
                }
                else
                {
                    bodyData.Add((data.Item2, data.Item3, data.Item4, data.Item5, data.Item6));
                }
    
            }      

            return (true, bodyData);
        }
    }
}