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
    /// Lógica de interacción para ConsultarProblematicas.xaml
    /// </summary>
    public partial class ConsultarProblematicas : Window
    {
        Academico tutorIniciado;
        public ConsultarProblematicas(Academico tutorIniciado)
        {
            InitializeComponent();
            this.tutorIniciado = tutorIniciado;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuTutor ventanaTutor = new MenuTutor(tutorIniciado);
            ventanaTutor.Show();
            this.Close();
        }

        private async void cargarProblematicas()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if (conexionServicios != null)
            {
                var problematicas = await conexionServicios.obtenerProblematicasAsync();
                if(problematicas != null)
                {
                    foreach (Problematica problematica in problematicas)
                    {
                        dgProblematicasConsulta.Items.Add(problematica.titulo);
                        dgProblematicasConsulta.Items.Add(problematica.descripcion);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron problematicas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
