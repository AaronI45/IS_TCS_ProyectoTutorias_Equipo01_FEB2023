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
    /// Interaction logic for ReporteTutoriaAcademica.xaml
    /// </summary>
    public partial class ReporteTutoriaAcademica : Window
    {
        public ReporteTutoriaAcademica()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operacion?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                MenuTutor ventanaMenuTutor = new MenuTutor();
                ventanaMenuTutor.Show();
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProblematicasAcademicas ventanaProblematicas = new ProblematicasAcademicas();
            ventanaProblematicas.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ComentariosGenerales ventanaComentario = new ComentariosGenerales();
            ventanaComentario.Show();
            this.Close();
        }
    }
}
