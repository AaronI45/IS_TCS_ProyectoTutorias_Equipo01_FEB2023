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
    /// Interaction logic for EditarAsignacionTutor.xaml
    /// </summary>
    public partial class EditarAsignacionTutor : Window
    {
        int idProgramaEducativo;
        public EditarAsignacionTutor(int idProgramaEducativo)
        {
            InitializeComponent();
            this.idProgramaEducativo = idProgramaEducativo;
            EstudianteTutorViewModel modelo = new EstudianteTutorViewModel();
            dgEstudiantes.ItemsSource = modelo.EstudiantesBd;
        }

        private void clicAsignarNuevoTutor(object sender, RoutedEventArgs e)
        {
            Estudiante estudianteSeleccionado = verificarSeleccion();
            if(estudianteSeleccionado != null)
            {
                AsignarTutorAcademicoAEstudiante ventanaAsignarTutorAcademicoEstudiantes =
                    new AsignarTutorAcademicoAEstudiante(this.idProgramaEducativo, estudianteSeleccionado.matricula);
                ventanaAsignarTutorAcademicoEstudiantes.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor seleccione un tutorado para realizar la nueva asignación", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clicVolver(object sender, RoutedEventArgs e)
        {
            MenuAdministrador menuAdministrador = new MenuAdministrador(this.idProgramaEducativo);
            menuAdministrador.Show();
            this.Close();
        }

        private Estudiante verificarSeleccion()
        {
            Estudiante estudianteSeleccionado = null;
            if(dgEstudiantes.SelectedItem != null)
            {
                estudianteSeleccionado = (Estudiante)dgEstudiantes.SelectedItem;
            }
            return estudianteSeleccionado;
        }

    }
}
