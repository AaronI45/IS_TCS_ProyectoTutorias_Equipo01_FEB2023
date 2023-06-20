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

namespace FrontendGestorTutorias.VentanasTutor
{
    /// <summary>
    /// Lógica de interacción para ModificarComentarioGeneral.xaml
    /// </summary>
    public partial class ModificarComentarioGeneral : Window
    {
        Academico tutorIniciado;
        ReporteTutoria reporteTutoria;
        Comentario comentarioSeleccionado;
        public ModificarComentarioGeneral(Academico tutorIniciado, ReporteTutoria reporteTutoria, Comentario comentarioSeleccionado)
        {
            this.reporteTutoria = reporteTutoria;
            this.tutorIniciado = tutorIniciado;
            this.comentarioSeleccionado = comentarioSeleccionado;
            InitializeComponent();
            cargarComentario();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarComentarios ventanaListaComentarios = new ConsultarComentarios(tutorIniciado, reporteTutoria);
            ventanaListaComentarios.Show();
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            modificarComentario();
        }

        private async void cargarComentario()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if (conexionServicios != null)
            {
                var comentarioGeneral = await conexionServicios.obtenerComentarioPorIdAsync(comentarioSeleccionado.idComentario);
                if(comentarioGeneral != null)
                {
                    tbComentarioGeneral.Text = comentarioGeneral.comentarios;
                }
                else
                {
                    MessageBox.Show("No se encontró el comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void modificarComentario()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if(conexionServicios != null)
            {
                string comentarios = tbComentarioGeneral.Text;
                ResultadoOperacion resultado = await conexionServicios.editarComentariosGeneralesAsync(comentarios, comentarioSeleccionado.idComentario);
                if (!resultado.Error)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito en la edición de comentario", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReporteTutoriaAcademica ventanaReporteTutoria = new ReporteTutoriaAcademica(this.tutorIniciado, this.reporteTutoria);
                    ventanaReporteTutoria.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error en la edición de comentario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
