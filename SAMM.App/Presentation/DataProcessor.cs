using System.Collections.Generic;
using System.Windows.Forms;

namespace SAMM.App.Presentation
{
    public static class DataProcessor
    {
        public static void MostrarDatosEnGridView(DataGridView dgv, Dictionary<string, object> datos)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            dgv.Columns.Add("Campo", "Campo");
            dgv.Columns.Add("Valor", "Valor");

            foreach (var dato in datos)
            {
                dgv.Rows.Add(dato.Key, dato.Value);
            }
        }
    }
}