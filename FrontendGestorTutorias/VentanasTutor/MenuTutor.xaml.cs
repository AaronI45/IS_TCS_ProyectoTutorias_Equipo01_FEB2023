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
    /// Lógica de interacción para MenuTutor.xaml
    /// </summary>
    public partial class MenuTutor : Window
    {
        Academico tutorIniciado;
        public MenuTutor(Academico tutor)
        {
            this.tutorIniciado = tutor;
            InitializeComponent();
        }

        private void clicCerrarSesion(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cerrar sesión?", "Cerrar sesión",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                MainWindow ventanaLogin = new MainWindow();
                ventanaLogin.Show();
                this.Close();
            }
        }

        private void clicRegistrarFechasDeSesiones(object sender, RoutedEventArgs e)
        {
            RegistrarFechasSesionTutoria ventanaFechasTutorias = new RegistrarFechasSesionTutoria(tutorIniciado);
            ventanaFechasTutorias.Show();
            this.Close();
        }

        private void clicConsultarReporteDeTutoriaAcademica(object sender, RoutedEventArgs e)
        {
            ListadoReportes ventanaReporteTutoria = new ListadoReportes(tutorIniciado);
            ventanaReporteTutoria.Show();
            this.Close();
        }

        private void clicModificarFechasDeSesiones(object sender, RoutedEventArgs e)
        {
            ModificarFechasSesionTutoria ventanaModificarFechas = new ModificarFechasSesionTutoria(tutorIniciado);
            ventanaModificarFechas.Show();
            this.Close();
        }

        private void clicConsultarProblematicaAcademica(object sender, RoutedEventArgs e)
        {
            ConsultarProblematicas ventanaProblematicas = new ConsultarProblematicas(tutorIniciado);
            ventanaProblematicas.Show();
            this.Close();
        }
    }
}
