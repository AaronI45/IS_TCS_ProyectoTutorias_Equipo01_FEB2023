using ServiciosTutorias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FrontendGestorTutorias
{
    /// <summary>
    /// Lógica de interacción para ComentariosGenerales.xaml
    /// </summary>
    public partial class ComentariosGenerales : Window
    {
        public ComentariosGenerales()
        {
            InitializeComponent();
            tbComentarioGeneral.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operación?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                ReporteTutoriaAcademica ventanaReporte = new ReporteTutoriaAcademica();
                ventanaReporte.Show();
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (!hayCamposVacios())
            {
                registrarComentariosGeneralesAsync();
            }
        }

        private async void registrarComentariosGeneralesAsync()
        {
            var conexionServicios = new Service1Client();
            if (conexionServicios != null)
            {
                Comentario comentarioGeneral = new Comentario()
                {
                    comentarios = tbComentarioGeneral.Text
                };
                MessageBox.Show("Comentario registrado exitosamente", "Registro exitoso");
                ReporteTutoriaAcademica ventanaReporteTutoria = new ReporteTutoriaAcademica();
                ventanaReporteTutoria.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error");
            }
        }

        private void soloLetras(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(char.IsLetter);
        }

        private bool hayCamposVacios()
        {
            bool camposVacios = false;
            if (tbComentarioGeneral.Text == "")
            {
                tbComentarioGeneral.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbComentarioGeneral.BorderBrush = Brushes.Black;
            }
            return camposVacios;
        }
    }
}
