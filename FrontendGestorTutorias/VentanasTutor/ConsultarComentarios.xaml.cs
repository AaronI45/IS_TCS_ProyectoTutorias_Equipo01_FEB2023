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

namespace FrontendGestorTutorias
{
    /// <summary>
    /// Lógica de interacción para ConsultarComentarios.xaml
    /// </summary>
    public partial class ConsultarComentarios : Window
    {
        Academico tutorIniciado;
        public ConsultarComentarios(Academico tutorIniciado)
        {
            this.tutorIniciado = tutorIniciado;
            InitializeComponent();
            ComentarioGeneralViewModel modelo = new ComentarioGeneralViewModel();
            dgComentarios.ItemsSource = modelo.ComentariosBD;
        }

        private void Button_ClickCancelar(object sender, RoutedEventArgs e)
        {
            MenuTutor ventanaTutor = new MenuTutor(tutorIniciado);
            ventanaTutor.Show();
            this.Close();
        }

        private void Button_ClickConsultar(object sender, RoutedEventArgs e)
        {

        }
    }
}
