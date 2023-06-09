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
        public MenuTutor()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrarFechasSesionTutoria ventanaFechasTutorias = new RegistrarFechasSesionTutoria();
            ventanaFechasTutorias.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ReporteTutoriaAcademica ventanaReporteTutoria = new ReporteTutoriaAcademica();
            ventanaReporteTutoria.Show();
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ModificarFechasSesionTutoria ventanaModificarFechas = new ModificarFechasSesionTutoria();
            ventanaModificarFechas.Show();
            this.Close();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ConsultarProblematicas ventanaProblematicas = new ConsultarProblematicas();
            ventanaProblematicas.Show();
            this.Close();
        }
    }
}
