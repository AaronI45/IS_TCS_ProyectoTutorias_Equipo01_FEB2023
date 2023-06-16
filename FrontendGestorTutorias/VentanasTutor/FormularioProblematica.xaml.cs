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
            if (checarCamposVacios())
            {

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

        private async void llenarComboBoxes()
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            try
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
            catch
            {
                MessageBox.Show("No se pudo conectar con la base de datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
