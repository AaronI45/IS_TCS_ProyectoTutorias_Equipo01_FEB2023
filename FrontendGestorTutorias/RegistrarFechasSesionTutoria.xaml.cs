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
    /// Lógica de interacción para RegistrarFechasSesionTutoria.xaml
    /// </summary>
    public partial class RegistrarFechasSesionTutoria : Window
    {
        public RegistrarFechasSesionTutoria()
        {
            InitializeComponent();
            cargarCbPeriodosEscolares();
        }

        private async void cargarCbPeriodosEscolares()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if(conexionServicios != null)
            {
                var periodosEscolares = await conexionServicios.obtenerPeriodosEscolaresAsync();
                if(periodosEscolares != null)
                {
                    foreach(PeriodoEscolar periodoEscolar in periodosEscolares)
                    {
                        cbPeriodoEscolar.Items.Add(periodoEscolar.inicioPeriodo + " " + periodoEscolar.finPeriodo);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron periodos escolares", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operación?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                MenuTutor ventanaMenuTutor = new MenuTutor();
                ventanaMenuTutor.Show();
                this.Close();
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
