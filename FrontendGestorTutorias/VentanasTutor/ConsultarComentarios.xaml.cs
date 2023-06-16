using FrontendGestorTutorias.modelo;
using FrontendGestorTutorias.VentanasTutor;
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
    /// Lógica de interacción para ConsultarComentarios.xaml
    /// </summary>
    public partial class ConsultarComentarios : Window
    {
        Academico tutorIniciado;
        ReporteTutoria reporteTutoria;
        public ConsultarComentarios(Academico tutorIniciado, ReporteTutoria reporteTutoria)
        {
            this.reporteTutoria = reporteTutoria;
            this.tutorIniciado = tutorIniciado;
            InitializeComponent();
            ComentarioGeneralViewModel modelo = new ComentarioGeneralViewModel(reporteTutoria.idReporte_Tutoria);
            dgComentarios.ItemsSource = modelo.ComentariosBD;
        }

        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            ReporteTutoriaAcademica ventanaReporte = new ReporteTutoriaAcademica(tutorIniciado, reporteTutoria);
            ventanaReporte.Show();
            this.Close();
        }

        private void Button_ClickConsultar(object sender, RoutedEventArgs e)
        {
            Comentario comentarioSeleccionado = validarSeleccion();
            if (comentarioSeleccionado != null)
            {
                ModificarComentarioGeneral ventanaModificarComentario = new ModificarComentarioGeneral(tutorIniciado, reporteTutoria, comentarioSeleccionado);
                ventanaModificarComentario.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private Comentario validarSeleccion()
        {
            Comentario comentarioSeleccionado = dgComentarios.SelectedItem as Comentario;
            if (comentarioSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un comentario", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return comentarioSeleccionado;
        }
    }
}
