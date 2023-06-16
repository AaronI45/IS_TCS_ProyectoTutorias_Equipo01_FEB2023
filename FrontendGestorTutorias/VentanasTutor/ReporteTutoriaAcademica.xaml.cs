using FrontendGestorTutorias.modelo;
using FrontendGestorTutorias.VentanasTutor;
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
    /// Interaction logic for ReporteTutoriaAcademica.xaml
    /// </summary>
    public partial class ReporteTutoriaAcademica : Window
    {
        Academico tutorIniciado;
        ReporteTutoria reporte;
        public ReporteTutoriaAcademica(Academico tutorIniciado, ReporteTutoria reporte)
        {
            this.tutorIniciado = tutorIniciado;
            this.reporte = reporte;
            InitializeComponent();
            EstudiantesAsistentesViewModel modelo = new EstudiantesAsistentesViewModel(reporte.Tutoria.idTutoria, tutorIniciado.idAcademico);
            dgEstudiantes.ItemsSource = modelo.EstudiantesAsistentesBd;
            inicializarDatos();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
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

        private void clicRegistrarProblematicaAcademica(object sender, RoutedEventArgs e)
        {
            FormularioProblematica ventanaRegistroProblematica= new FormularioProblematica(tutorIniciado, reporte);
            ventanaRegistroProblematica.Show();
            this.Close();
        }

        private void clicRegistrarComentariosGenerales(object sender, RoutedEventArgs e)
        {
            ComentariosGenerales ventanaComentario = new ComentariosGenerales(tutorIniciado, reporte);
            ventanaComentario.Show();
            this.Close();
        }

        private void clicVolver(object sender, RoutedEventArgs e)
        {
            ListadoReportes ventanaListadoReportes= new ListadoReportes(tutorIniciado);
            ventanaListadoReportes.Show();
            this.Close();
        }

        private void inicializarDatos()
        {
            lbPeriodo.Content = lbPeriodo.Content + reporte.Tutoria.PeriodoEscolar.ToString();
            lbProgramaEducativo.Content = lbProgramaEducativo.Content + reporte.ProgramaEducativo.nombre;
            lbNumeroTutoria.Content = lbNumeroTutoria.Content + reporte.Tutoria.numeroTutoria.ToString();
            lbFecha.Content = lbFecha.Content + reporte.Tutoria.fechaTutoria.ToString();
        }
    }
}
