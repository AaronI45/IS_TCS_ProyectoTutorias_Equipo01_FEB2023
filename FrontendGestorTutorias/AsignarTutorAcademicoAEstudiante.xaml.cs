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
    /// Lógica de interacción para AsignarTutorAcademicoAEstudiante.xaml
    /// </summary>
    public partial class AsignarTutorAcademicoAEstudiante : Window
    {
        private int idProgramaEducativo;
        private string matriculaIngresada = null;
        private Estudiante estudianteEncontrado = null;
        private Academico[] academicos = null;
        public AsignarTutorAcademicoAEstudiante(int idProgramaEducativo, string matriculaIngresada)
        {
            InitializeComponent();
            this.idProgramaEducativo = idProgramaEducativo;
            this.matriculaIngresada = matriculaIngresada;
            cargarCbTutores(this.idProgramaEducativo);
            cbTutorAcademico.Items.Insert(0, "Seleccionar Tutor académico");
            if (!string.IsNullOrEmpty(matriculaIngresada))
            {
                cargarNombre(matriculaIngresada);
            }
        }

        private void clicBuscar(object sender, RoutedEventArgs e)
        {
            cargarNombre(tbMatricula.Text);
        }

        private async void clicAsignarTutor(object sender, RoutedEventArgs e)
        {
            if (validarSelecciones())
            {
                var conexionServicios = new ServiciosTutorias.Service1Client();
                if (conexionServicios != null)
                {
                    int idAcademico = academicos[cbTutorAcademico.SelectedIndex - 1].idAcademico;
                    int idEstudiante = estudianteEncontrado.idEstudiante;
                    ResultadoOperacion resultadoOperacion = await conexionServicios.asignacionTutorAEstudianteAsync(idEstudiante, idAcademico);
                    if (!resultadoOperacion.Error)
                    {
                        MessageBox.Show(resultadoOperacion.Mensaje, "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        MenuAdministrador ventanaMenuAdministrador = new MenuAdministrador(this.idProgramaEducativo);
                        ventanaMenuAdministrador.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(resultadoOperacion.Mensaje, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void clicCancelar(object sender, RoutedEventArgs e)
        {
            MenuAdministrador ventanaMenuAdministrador = new MenuAdministrador(this.idProgramaEducativo);
            ventanaMenuAdministrador.Show();
            this.Close();
        }

        private async void cargarNombre(string matricula)
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if(conexionServicios != null)
            {
                Estudiante estudiante = await conexionServicios.recuperarEstudiantePorMatriculaAsync(matricula);
                if (estudiante != null)
                {
                    if (!string.IsNullOrEmpty(matriculaIngresada))
                    {
                        this.estudianteEncontrado = estudiante;
                        tbMatricula.Text = matricula;
                        tbMatricula.IsReadOnly = true;
                        tbEstudiante.Text = estudiante.nombre + " " + estudiante.apellidoPaterno + " " + estudiante.apellidoMaterno;
                    }
                    else
                    {
                        this.estudianteEncontrado = estudiante;
                        tbEstudiante.Text = estudiante.nombre + " " + estudiante.apellidoPaterno + " " + estudiante.apellidoMaterno;
                        MessageBox.Show("Estudiante encontrado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        tbMatricula.IsReadOnly = true;
                        matriculaIngresada = matricula;
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró un estudiante con la matrícula ingresada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void cargarCbTutores(int idProgramaEducativo)
        {
            var conexionServicios = new ServiciosTutorias.Service1Client();
            if (conexionServicios != null)
            {
                academicos = await conexionServicios.recuperarTutoresPorProgramaEducativoAsync(idProgramaEducativo);
                if (academicos != null)
                {
                    foreach (Academico tutor in academicos)
                    {
                        cbTutorAcademico.Items.Add(tutor.nombre + " " + tutor.apellidoPaterno + " " + tutor.apellidoMaterno);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró un estudiante con la matrícula ingresada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool validarSelecciones()
        {
            bool seleccionesValidas = true;
            if (cbTutorAcademico.SelectedIndex == 0)
            {
                MessageBox.Show("Seleccione un tutor académico", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                seleccionesValidas = false;
            }else if (string.IsNullOrEmpty(matriculaIngresada))
            {
                MessageBox.Show("Ingrese una matrícula", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                seleccionesValidas = false;
            }
            return seleccionesValidas;
        }
    }
}
