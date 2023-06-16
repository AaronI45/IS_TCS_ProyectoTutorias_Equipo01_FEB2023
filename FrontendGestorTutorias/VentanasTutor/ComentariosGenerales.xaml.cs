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
        Academico tutorIniciado;
        ReporteTutoria reporte;
        public ComentariosGenerales(Academico tutorIniciado, ReporteTutoria reporte)
        {
            this.tutorIniciado = tutorIniciado;
            InitializeComponent();
            tbComentarioGeneral.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            this.reporte = reporte;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operación?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                ReporteTutoriaAcademica ventanaReporte = new ReporteTutoriaAcademica(tutorIniciado, reporte);
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
                    comentarios = tbComentarioGeneral.Text,
                    reporte_Tutoria_idReporte_Tutoria = reporte.idReporte_Tutoria
                };
                ResultadoOperacion resultadoRegistroComentario = await conexionServicios.registrarComentariosGeneralesAsync(reporte.idReporte_Tutoria, tbComentarioGeneral.Text);
                if (!resultadoRegistroComentario.Error)
                {
                    MessageBox.Show(resultadoRegistroComentario.Mensaje, "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(resultadoRegistroComentario.Mensaje, "Error en el registro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ReporteTutoriaAcademica ventanaReporteTutoria = new ReporteTutoriaAcademica(tutorIniciado, reporte);
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
