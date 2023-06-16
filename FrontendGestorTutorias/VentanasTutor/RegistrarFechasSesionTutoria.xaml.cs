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
        Academico tutorIniciado;
        public RegistrarFechasSesionTutoria(Academico tutorIniciado)
        {
            InitializeComponent();
            cargarCbPeriodosEscolares();
            this.tutorIniciado = tutorIniciado;
            cbPeriodoEscolar.SelectionChanged += cbPeriodoEscolar_SelectionChanged;
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
                MenuTutor ventanaMenuTutor = new MenuTutor(tutorIniciado);
                ventanaMenuTutor.Show();
                this.Close();
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (cbPeriodoEscolar_SelectionChanged != null)
            {
                if (evaluarFechasVacias())
                {
                    MessageBox.Show("Favor de seleccionar todas las fechas", "Fechas vacías", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DateTime? primeraFecha = dpPrimeraFecha.SelectedDate;
                    DateTime? segundaFecha = dpSegundaFecha.SelectedDate;
                    DateTime? terceraFecha = dpTerceraFecha.SelectedDate;
                    var confirmacion = MessageBox.Show("¿Está seguro de querer registrar estas fechas?", "Registrar fechas",
                                               MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmacion == MessageBoxResult.Yes)
                    {
                        PeriodoEscolar periodoEscolar = new PeriodoEscolar();
                        periodoEscolar.primeraFechaTutoria = primeraFecha;
                        periodoEscolar.segundaFechaTutoria = segundaFecha;
                        periodoEscolar.terceraFechaTutoria = terceraFecha;
                        registrarFechasTutorias(periodoEscolar);
                    }
                }
            }
        }

        private void cbPeriodoEscolar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PeriodoEscolar periodoSeleccionado = (PeriodoEscolar)cbPeriodoEscolar.SelectedItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private bool evaluarFechasVacias()
        {
            bool fechasVacias = false;
            if(dpPrimeraFecha.Text == "")
            {
                fechasVacias = true;
            }
            if(dpSegundaFecha.Text == "")
            {
                fechasVacias = true;
            }
            if(dpTerceraFecha.Text == "")
            {
                fechasVacias = true;
            }
            return fechasVacias;
        }

        private async void registrarFechasTutorias(PeriodoEscolar periodoEscolar)
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if (conexionServicio != null)
            {
                ResultadoOperacion resultado = await conexionServicio.registrarFechaSesiontutoriaAsync(periodoEscolar);
                if(resultado.Error == false)
                {
                    MessageBox.Show("Registro de fechas exitoso", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Registro de fechas fallido", "Error en el registro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Registro de fechas fallido", "Error en el registro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
