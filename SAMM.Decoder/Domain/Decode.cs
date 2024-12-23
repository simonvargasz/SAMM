using SAMM.Decoder.Domain.Dictionaries;
using SAMM.Decoder.Application;

namespace SAMM.Decoder.Domain
{

    class Decode
    {

        public Decode()
        {

        }
        public static (bool, double, double, string) ProcessCoordinates(char octant, int rawLatitude, int rawLongitude)
        {
            if (!OctantRanges.TryGetValue(octant, out var range))
            {
                return (false, 0, 0, "");
            }

            var (latMin, latMax, lonMin, lonMax, latDir, lonDir) = range;

            double latitude = rawLatitude / 10.0;
            double longitude = rawLongitude / 10.0;

            if (octant == '1' || octant == '2' || octant == '6' || octant == '7')
            {
                if (rawLongitude >= 0 && rawLongitude <= 800)
                {
                    longitude = (rawLongitude + 1000) / 10.0;
                }
            }

            bool isValid = latitude >= latMin && latitude <= latMax && longitude >= lonMin && longitude <= lonMax;

            if (!isValid)
            {
                return (false, 0, 0, "");
            }
            else
            {
                return (true, latitude, longitude, octant.ToString());
            }
        }

        public static (bool, string, string, int) ProcessTime(string message)
        {

            string day = message[..2];

            if (int.Parse(day) > 31 || int.Parse(day) < 1)
            {
                return (false, "", "", 0);
            }

            string time = message.Substring(2, 3);

            if (int.Parse(time) >= 239 || int.Parse(time) <= 000)
            {
                return (false, "", "", 0);

            }
            int hours = int.Parse(time[..2]);
            float minutes = float.Parse(time[2..]) / 10 * 60;


            int duration = int.Parse(message[5..]);
            if (duration == 9)
            {
                duration = 12;
            }

            DateTime date = DateTime.Now;
            string month = date.Month.ToString();

            string Mdate = day + "/" + month;
            string Mtime = hours + ":" + minutes;

            return (true, Mdate, Mtime, duration);
        }

        public static (bool, int, float) ProcessAltitude(string message)
        {
            int altitude = int.Parse(message[..3]);
            float rawPressure = int.Parse(message.Substring(3, 3));
            float pressure = rawPressure / 10;
            if (rawPressure < 500)
            {
                pressure = (rawPressure / 10) + 100;
            }

            pressure = (pressure / 100) * 1013.25f;

            return (true, altitude, pressure);
        }

        public static (bool, int, int, int, float, float) ProcessBody(string message)
        {
            int layer = int.Parse(message[..2]);
            int direction = int.Parse(message.Substring(2, 2));

            if (direction < 0 || direction > 64)
            {
                return (false, 0, 0, 0, 0, 0);
            }
            else
            {
                direction = direction * 100;
            }
            int speed = int.Parse(message.Substring(4, 2));
            float airTemp = float.Parse(message.Substring(6, 3));
            float airDensity = float.Parse(message.Substring(9, 3));
            airTemp /= 10;
            airDensity /= 10;

            if (airTemp < 50)
            {
                airTemp += 100;
            }
            if (airDensity < 50)
            {
                airDensity += 100;
            }

            airTemp = (airTemp / 100) * 15;
            airDensity = (airDensity / 100) * 1.225f;

            return (true, layer, direction, speed, airTemp, airDensity);

        }

    }
}