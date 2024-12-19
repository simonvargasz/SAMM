using System.Collections.Generic;


namespace SAMM.Decoder.Domain.Dictionaries
{
    public static class FieldDescriptions
    {

        private static readonly Dictionary<string, string> Values = new Dictionary<string, string>
            {
                { "dir_viento", "Dirección del viento" },
                { "vel_viento", "Velocidad del viento" },
                { "temp_air", "Temperatura balística" },
                { "dens_air", "Densidad balística" },
                { "altitud", "Altitud estación" },
                { "presion", "Presión atmosférica" },
                { "fecha", "Fecha" },
                { "hora", "Hora" },
                { "validez", "Validez" },
                { "octante", "Octante" },
                { "latitud", "Latitud" },
                { "longitud", "Longitud" },
                { "capa_altura", "Altitud" }
            };

        public static string GetDescription(string field)
        {
            return Values.TryGetValue(field, out var description) ? description : field;
        }

    }
}