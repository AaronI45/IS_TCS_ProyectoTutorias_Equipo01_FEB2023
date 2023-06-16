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
        PeriodoEscolar periodoSeleccionado = null;
        public ModificarFechasSesionTutoria(Academico tutorIniciado)
        {
            InitializeComponent();
            cargarCbPeriodosEscolares();
            this.tutorIniciado = tutorIniciado;
            cbPeriodosEscolares.SelectionChanged += cbPeriodosEscolares_SelectionChanged;
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
            if(cbPeriodosEscolares_SelectionChanged != null)
            {
                if (evaluarFechasVacias())
                {
                    MessageBox.Show("Favor de seleccionar todas las fechas", "Fechas vacías", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DateTime? primeraFecha = dpPrimeraFechaEdicion.SelectedDate;
                    DateTime? segundaFecha = dpSegundaFechaEdicion.SelectedDate;
                    DateTime? terceraFecha = dpTerceraFechaEdicion.SelectedDate;
                    var confirmacion = MessageBox.Show("¿Está seguro de querer modificar estas fechas?", "Modificar fechas",
                                               MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmacion == MessageBoxResult.Yes)
                    {
                        PeriodoEscolar periodoEscolar = periodoSeleccionado;
                        periodoEscolar.primeraFechaTutoria = primeraFecha.Value;
                        periodoEscolar.segundaFechaTutoria = segundaFecha.Value;
                        periodoEscolar.terceraFechaTutoria = terceraFecha.Value;
                        modificarFechasTutorias(periodoEscolar);
                    }
                }
            }
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
                        cbPeriodosEscolares.Items.Add(periodoEscolar);
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

        private void cbPeriodosEscolares_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                periodoSeleccionado = (PeriodoEscolar)cbPeriodosEscolares.SelectedItem;
                DateTime? primeraFecha = periodoSeleccionado.primeraFechaTutoria;
                DateTime? segundaFecha = periodoSeleccionado.segundaFechaTutoria;
                DateTime? terceraFecha = periodoSeleccionado.terceraFechaTutoria;

                tbPrimeraFecha.Text = primeraFecha.ToString();
                tbSegundaFecha.Text = segundaFecha.ToString();
                tbTerceraFecha.Text = terceraFecha.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private bool evaluarFechasVacias()
        {
            bool fechasVacias = false;
            if (dpPrimeraFechaEdicion.Text == "")
            {
                fechasVacias = true;
            }
            if (dpSegundaFechaEdicion.Text == "")
            {
                fechasVacias = true;
            }
            if (dpTerceraFechaEdicion.Text == "")
            {
                fechasVacias = true;
            }
            return fechasVacias;
        }

        private async void modificarFechasTutorias(PeriodoEscolar periodoEscolar)
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if (conexionServicio != null)
            {
                ResultadoOperacion resultado = await conexionServicio.registrarFechaSesiontutoriaAsync(periodoEscolar);
                if (resultado.Error == false)
                {
                    MessageBox.Show("Modificacion de fechas exitosa", "Modificacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    MenuTutor ventanaTutor = new MenuTutor(tutorIniciado);
                    ventanaTutor.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Modificacion de fechas fallida", "Error en la modificacion", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Modificacion de fechas fallida", "Error en la modificacion", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
