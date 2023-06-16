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

namespace FrontendGestorTutorias.VentanasTutor
{
    /// <summary>
    /// Interaction logic for FormularioProblematica.xaml
    /// </summary>
    public partial class FormularioProblematica : Window
    {
        Academico tutorIniciado;
        ReporteTutoria reporteTutoria;
        ExperienciaEducativa[] experienciasEducativas;
        ClasificacionProblematica[] clasificacionesProblematicas;
        public FormularioProblematica(Academico tutorIniciado, ReporteTutoria reporteTutoria)
        {
            InitializeComponent();
            this.tutorIniciado = tutorIniciado;
            this.reporteTutoria = reporteTutoria;
            EstudiantesAsistentesViewModel modelo = new EstudiantesAsistentesViewModel(reporteTutoria.tutoria_idTutoria,tutorIniciado.idAcademico);
            dgEstudiante.ItemsSource = modelo.EstudiantesAsistentesBd;
            cbTipoProblematica.Items.Insert(0, "Seleccione una clasificación");
            cbTipoProblematica.SelectedIndex = 0;
            cbExperiencia.Items.Insert(0, "Seleccione una experiencia educativa");
            cbExperiencia.SelectedIndex = 0;
            llenarComboBoxes();
            cbTipoProblematica.SelectionChanged += cbTipoProblematicaChanged;
        }

        private void clicVolver(object sender, RoutedEventArgs e)
        {
            var confirmacion = MessageBox.Show("¿Está seguro de querer cancelar la operacion?", "Cancelar",
                               MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (confirmacion == MessageBoxResult.Yes)
            {
                ReporteTutoriaAcademica ventanaReporteTutoriaAcademica = new ReporteTutoriaAcademica(tutorIniciado, reporteTutoria);
                ventanaReporteTutoriaAcademica.Show();
                this.Close();
            }
        }

        private void clicRegistrar(object sender, RoutedEventArgs e)
        {
            int idEE = 0;
            Estudiante estudianteConProblematica = verificarSeleccion();
            if (checarCamposVacios())
            {
                if (validarSeleccionClasificacion() && estudianteConProblematica != null)
                {
                    if(cbTipoProblematica.SelectedIndex == 1)
                    {
                        cbExperiencia.Visibility = Visibility.Visible;
                        lbExperiencia.Visibility = Visibility.Visible;
                        if (validarEE())
                        {
                            idEE = cbExperiencia.SelectedIndex;
                            RegistroProblematica nuevaProblematica = new RegistroProblematica()
                            {
                                clasificacionProblematica = clasificacionesProblematicas.ElementAt(cbTipoProblematica.SelectedIndex -1).idClasificacion_problematica,
                                idReporteTutoria = reporteTutoria.idReporte_Tutoria,
                                titulo = txtTitulo.Text,
                                descripcion = txtDescripcion.Text,
                                idEstudiante = estudianteConProblematica.idEstudiante,
                                idExperienciaEducativa = idEE
                            };
                            registrarProblematica(nuevaProblematica);
                        }
                    }
                    else
                    {
                        RegistroProblematica nuevaProblematica = new RegistroProblematica()
                        {
                            clasificacionProblematica = clasificacionesProblematicas.ElementAt(cbTipoProblematica.SelectedIndex).idClasificacion_problematica,
                            idReporteTutoria = reporteTutoria.idReporte_Tutoria,
                            titulo = txtTitulo.Text,
                            descripcion = txtDescripcion.Text,
                            idEstudiante = estudianteConProblematica.idEstudiante,
                            idExperienciaEducativa = idEE
                        };
                        registrarProblematica(nuevaProblematica);
                        cbExperiencia.Visibility = Visibility.Hidden;
                        lbExperiencia.Visibility = Visibility.Hidden;
                    }

                }
                else
                {

                }
            }
        }

        private bool checarCamposVacios()
        {
            if (txtDescripcion.Text == "" && txtTitulo.Text == "")
            {
                MessageBox.Show("Debe llenar todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private Estudiante verificarSeleccion()
        {
            Estudiante estudianteSeleccion;
            estudianteSeleccion = dgEstudiante.SelectedItem as Estudiante;
            if (estudianteSeleccion == null)
            {
                MessageBox.Show("Por favor seleccione un estudiante que presente la problemática", "Error de selección", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return estudianteSeleccion;
        }

        private bool validarEE()
        {
            if (cbExperiencia.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar una experiencia educativa", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool validarSeleccionClasificacion()
        {
            if (cbTipoProblematica.SelectedIndex == 0)
            {
                MessageBox.Show("Debe seleccionar una clasificación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }   

        private async void llenarComboBoxes()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if(conexionServicios != null)
            {
                experienciasEducativas = await conexionServicios.recuperarExperienciasEducativasAsync();
                clasificacionesProblematicas = await conexionServicios.recuperarClasificacionesAsync();
                foreach (ExperienciaEducativa experiencia in experienciasEducativas)
                {
                    cbExperiencia.Items.Add(experiencia.nombre);
                }
                foreach (ClasificacionProblematica clasificacion in clasificacionesProblematicas)
                {
                    cbTipoProblematica.Items.Add(clasificacion.clasificacion);
                }
            }   
        }

        private async void registrarProblematica(RegistroProblematica problematicaPresentada)
        {
            var conexionServicios = new Service1Client();
            if (conexionServicios != null)
            {
                ResultadoOperacion resultadoRegistro = await conexionServicios.registrarProblematicaAsync(problematicaPresentada);
                if (!resultadoRegistro.Error)
                {
                    MessageBox.Show(resultadoRegistro.Mensaje, "Éxito de registro de problemática", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReporteTutoriaAcademica ventanaReporteTutoria = new ReporteTutoriaAcademica(tutorIniciado, reporteTutoria);
                    ventanaReporteTutoria.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(resultadoRegistro.Mensaje, "Error en el registro de problemática", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void cbTipoProblematicaChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbTipoProblematica.SelectedIndex == 1)
                {
                    cbExperiencia.Visibility = Visibility.Visible;
                    lbExperiencia.Visibility = Visibility.Visible;
                }
                else
                {
                    cbExperiencia.Visibility = Visibility.Hidden;
                    lbExperiencia.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            };
        }
    }
}
