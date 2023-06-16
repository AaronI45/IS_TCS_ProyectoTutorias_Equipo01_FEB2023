using FrontendGestorTutorias.modelo;
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
    /// Interaction logic for ListadoReportes.xaml
    /// </summary>
    public partial class ListadoReportes : Window
    {
        Academico tutorIniciado;
        public ListadoReportes(Academico tutorIniciado)
        {
            this.tutorIniciado = tutorIniciado;
            InitializeComponent();
            ReporteTutoriaViewModel modelo = new ReporteTutoriaViewModel(tutorIniciado.idAcademico);
            dgReportes.ItemsSource = modelo.TutoriasBD;
        }

        private void clicRegresar(object sender, RoutedEventArgs e)
        {
            ListadoReportes ventanaListadoReportes= new ListadoReportes(tutorIniciado);
            ventanaListadoReportes.Show();
            this.Close();
        }

        private ReporteTutoria validarSeleccion()
        {
            ReporteTutoria reporteSeleccionado = dgReportes.SelectedItem as ReporteTutoria;
            if (reporteSeleccionado == null)
            {
                MessageBox.Show("Debe seleccionar un reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return reporteSeleccionado;
        }
        private void clicConsultar(object sender, RoutedEventArgs e)
        {
            ReporteTutoria reporteSeleccionado = validarSeleccion();
            if (reporteSeleccionado != null)
            {
                ReporteTutoriaAcademica ventanaReporte = new ReporteTutoriaAcademica(tutorIniciado, reporteSeleccionado);
                ventanaReporte.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un reporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
