using SAMM.Decoder.Domain;
using SAMM.Decoder.Domain.Dictionaries;
using SAMM.DataAccess.Application;
using SAMM.App.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Drawing;
using System.Diagnostics;

namespace SAMM
{
    public partial class Inicio : Form
    {
        private readonly DataService _dataService;
        public Inicio()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {

            if (InputMessage.MsgFormatting(txtMessage.Text))
            {
                MessageBox.Show("Mensaje decodificado correctamente");
                txtMessage.Clear();
            }
            else
            {
                MessageBox.Show("Se han encontrado errores en el mensaje");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtMessage.Text, out int altura))
            {
                Dictionary<string, object> datos = new Dictionary<string, object>();
                string selectedType = cboType.SelectedItem.ToString();

                switch (selectedType)
                {
                    case "Todos":
                        var info = _dataService.ObtenerInfoPorAltura(altura);
                        datos.Add(FieldDescriptions.GetDescription("dir_viento"), info.dirViento);
                        datos.Add(FieldDescriptions.GetDescription("vel_viento"), info.velViento);
                        datos.Add(FieldDescriptions.GetDescription("temp_air"), info.tempAir);
                        datos.Add(FieldDescriptions.GetDescription("dens_air"), info.densAir);
                        datos.Add(FieldDescriptions.GetDescription("octante"), info.octante);
                        datos.Add(FieldDescriptions.GetDescription("latitud"), info.latitud);
                        datos.Add(FieldDescriptions.GetDescription("longitud"), info.longitud);
                        datos.Add(FieldDescriptions.GetDescription("fecha"), info.fecha);
                        datos.Add(FieldDescriptions.GetDescription("hora"), info.hora);
                        datos.Add(FieldDescriptions.GetDescription("validez"), info.validez);
                        datos.Add(FieldDescriptions.GetDescription("altitud"), info.altitud);
                        datos.Add(FieldDescriptions.GetDescription("presion"), info.presion);
                        break;
                    case "Direccion de viento":
                        datos.Add(FieldDescriptions.GetDescription("dir_viento"), _dataService.ObtenerDirVientoPorAltura(altura));
                        break;
                    case "Velocidad de viento":
                        datos.Add(FieldDescriptions.GetDescription("vel_viento"), _dataService.ObtenerVelVientoPorAltura(altura));
                        break;
                    case "Temperatura Balistica":
                        datos.Add(FieldDescriptions.GetDescription("temp_air"), _dataService.ObtenerTempAirPorAltura(altura));
                        break;
                    case "Densidad Balistica":
                        datos.Add(FieldDescriptions.GetDescription("dens_air"), _dataService.ObtenerDensAirPorAltura(altura));
                        break;
                }

                if (datos.Count > 0)
                {
                    DataProcessor.MostrarDatosEnGridView(dgvMsg, datos);
                }
                else
                {
                    MessageBox.Show("No se encontraron datos para la altitud especificada.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingrese una altitud válida.");
            }
        }


        //soon --->

        [DllImport("user32.dll")]
        static extern bool CreateCaret(IntPtr hWnd, IntPtr hBitmap, int nWidth, int nHeight);

        [DllImport("user32.dll")]
        static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool DestroyCaret();
        private void TxtMessage_Enter(object sender, EventArgs e)
        {
            txtMessage.Cursor = Cursors.Hand;
            CreateCustomCaret();
        }

        private void TxtMessage_Leave(object sender, EventArgs e)
        {
            txtMessage.Cursor = Cursors.Default;
            DestroyCaret();
        }

        private void TxtMessage_TextChanged(object sender, EventArgs e)
        {
            CreateCustomCaret();
        }

        private void CreateCustomCaret()
        {
            DestroyCaret();
            Bitmap caretBitmap = new Bitmap(2, txtMessage.Font.Height);
            using (Graphics g = Graphics.FromImage(caretBitmap))
            {
                g.Clear(Color.Red);
            }
            IntPtr hBitmap = caretBitmap.GetHbitmap();
            CreateCaret(txtMessage.Handle, hBitmap, 2, txtMessage.Font.Height);
            ShowCaret(txtMessage.Handle);
        }
    }
}