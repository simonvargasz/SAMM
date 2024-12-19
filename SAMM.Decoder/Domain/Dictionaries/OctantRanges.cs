using System.Collections.Generic;

namespace SAMM.Decoder.Domain.Dictionaries
{
    public static class OctantRanges
    {
        private static readonly Dictionary<char, (double latMin, double latMax, double lonMin, double lonMax, string latDir, string lonDir)> Values =
            new Dictionary<char, (double, double, double, double, string, string)>
            {
                { '0', (0, 90,  0,  90, "N", "W")},
                { '1', (0, 90, 90, 180, "N", "W")},
                { '2', (0, 90, 90, 180, "N", "E")},
                { '3', (0, 90,  0,  90, "N", "E")},
                { '5', (0, 90,  0,  90, "S", "W")},
                { '6', (0, 90, 90, 180, "S", "W")},
                { '7', (0, 90, 90, 180, "S", "E")},
                { '8', (0, 90,  0,  90, "S", "E")},
                { '9', (0,  0,  0,   0, "X", "X")}
            };

        public static bool TryGetValue(char key, out (double latMin, double latMax, double lonMin, double lonMax, string latDir, string lonDir) value)
        {
            return Values.TryGetValue(key, out value);
        }
    }
}