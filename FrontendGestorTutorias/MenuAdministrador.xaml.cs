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
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public partial class MenuAdministrador : Window
    {
        private int idProgramaEducativo;
        public MenuAdministrador(int idProgramaEducativo)
        {
            InitializeComponent();
            this.idProgramaEducativo = idProgramaEducativo;
        }

        private void clicCerrarSesion(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cerrar sesión?","Cerrar sesión", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                MainWindow ventanaLogin = new MainWindow();
                ventanaLogin.Show();
                this.Close();
            }
        }

        private void clicRegistrarEstudiante(object sender, RoutedEventArgs e)
        {
            RegistrarEstudiante ventanaRegistrarAcademico = new RegistrarEstudiante(idProgramaEducativo);
            ventanaRegistrarAcademico.Show();
            this.Close();
        }

        private void clicRegistrarTutor(object sender, RoutedEventArgs e)
        {
            RegistrarTutor ventanaRegistrarTutor = new RegistrarTutor();
            ventanaRegistrarTutor.Show();
            this.Close();
        }

        private void clicAsignarTutorAEstudiante(object sender, RoutedEventArgs e)
        {
            AsignarTutorAcademicoAEstudiante ventanaAsignacionTutor = new AsignarTutorAcademicoAEstudiante(idProgramaEducativo, null);
            ventanaAsignacionTutor.Show();
            this.Close();
        }

        private void clicModificarAsignacion(object sender, RoutedEventArgs e)
        {
            EditarAsignacionTutor ventanaModificarAsignacion = new EditarAsignacionTutor (idProgramaEducativo);
            ventanaModificarAsignacion.Show();
            this.Close();
        }

    }
}
