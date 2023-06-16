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
    /// Lógica de interacción para ModificarFechasSesionTutoria.xaml
    /// </summary>
    public partial class ModificarFechasSesionTutoria : Window
    {
        Academico tutorIniciado;
        public ModificarFechasSesionTutoria(Academico tutorIniciado)
        {
            InitializeComponent();
            cargarCbPeriodosEscolares();
            this.tutorIniciado = tutorIniciado;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operacion?", "Cancelar",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                MenuTutor ventanaMenuTutor = new MenuTutor(tutorIniciado);
                ventanaMenuTutor.Show();
                this.Close();
            }
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void cargarCbPeriodosEscolares()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if (conexionServicios != null)
            {
                var periodosEscolares = await conexionServicios.obtenerPeriodosEscolaresAsync();
                if (periodosEscolares != null)
                {
                    foreach (PeriodoEscolar periodoEscolar in periodosEscolares)
                    {
                        cbPeriodosEscolares.Items.Add(periodoEscolar.inicioPeriodo + " " + periodoEscolar.finPeriodo);
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
    }
}
