using System;
using System.Data;
using MySql.Data.MySqlClient;
using SAMM.DataAccess.Infrastructure;

namespace SAMM.DataAccess.Application
{
    public class DataService
    {
        private readonly DatabaseConnection _db;

        public DataService()
        {
            string server = EnvLoader.GetEnvVariable("DB_SERVER");
            string database = EnvLoader.GetEnvVariable("DB_DATABASE");
            string user = EnvLoader.GetEnvVariable("DB_USER");
            string password = EnvLoader.GetEnvVariable("DB_PASSWORD");

            string connectionString = $"Server={server};Database={database};Uid={user};Pwd={password};";
            _db = new DatabaseConnection(connectionString);
        }

        private object ExecuteSp(int altura, string campo)
        {
            string procedureName = "ObtenerDatoPorAltura";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@altura", altura),
                new MySqlParameter("@campo", campo)
            };

            DataTable result = _db.ExecuteStoredProcedure(procedureName, parameters);

            if (result.Rows.Count > 0)
            {
                return result.Rows[0][0];
            }

            return null;
        }

        public (int? dirViento, int? velViento, float? tempAir, float? densAir, int? octante, float? latitud, float? longitud, string fecha, string hora, int? validez, float? altitud, float? presion) ObtenerInfoPorAltura(int altura)
        {
            string procedureName = "ObtenerInfoPorAltura";

            MySqlParameter[] parameters = new MySqlParameter[]
            {
                new MySqlParameter("@altura", altura)
            };

            DataTable result = _db.ExecuteStoredProcedure(procedureName, parameters);

            if (result.Rows.Count > 0)
            {
                DataRow row = result.Rows[0];
                return (
                    dirViento: ConvertToNullableInt(row["dir_viento"]),
                    velViento: ConvertToNullableInt(row["vel_viento"]),
                    tempAir: ConvertToNullableFloat(row["temp_air"]),
                    densAir: ConvertToNullableFloat(row["dens_air"]),
                    octante: ConvertToNullableInt(row["octante"]),
                    latitud: ConvertToNullableFloat(row["latitud"]),
                    longitud: ConvertToNullableFloat(row["longitud"]),
                    fecha: row["fecha"] as string,
                    hora: row["hora"] as string,
                    validez: ConvertToNullableInt(row["validez"]),
                    altitud: ConvertToNullableFloat(row["altitud"]),
                    presion: ConvertToNullableFloat(row["presion"])
                );
            }

            return (null, null, null, null, null, null, null, null, null, null, null, null);
        }

        public int? ObtenerDirVientoPorAltura(int altura)
        {
            return ConvertToNullableInt(ExecuteSp(altura, "dir_viento"));
        }

        public int? ObtenerVelVientoPorAltura(int altura)
        {
            return ConvertToNullableInt(ExecuteSp(altura, "vel_viento"));
        }

        public float? ObtenerTempAirPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "temp_air"));
        }

        public float? ObtenerDensAirPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "dens_air"));
        }

        public int? ObtenerOctantePorAltura(int altura)
        {
            return ConvertToNullableInt(ExecuteSp(altura, "octante"));
        }

        public float? ObtenerLatitudPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "latitud"));
        }

        public float? ObtenerLongitudPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "longitud"));
        }

        public string ObtenerFechaPorAltura(int altura)
        {
            return ExecuteSp(altura, "fecha") as string;
        }

        public string ObtenerHoraPorAltura(int altura)
        {
            return ExecuteSp(altura, "hora") as string;
        }

        public int? ObtenerValidezPorAltura(int altura)
        {
            return ConvertToNullableInt(ExecuteSp(altura, "validez"));
        }

        public float? ObtenerAltitudPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "altitud"));
        }

        public float? ObtenerPresionPorAltura(int altura)
        {
            return ConvertToNullableFloat(ExecuteSp(altura, "presion"));
        }

        private int? ConvertToNullableInt(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (value is short)
                return (short)value;

            return Convert.ToInt32(value);
        }

        private float? ConvertToNullableFloat(object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            return Convert.ToSingle(value);
        }
    }
}