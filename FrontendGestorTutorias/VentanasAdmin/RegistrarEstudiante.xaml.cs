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
    /// Lógica de interacción para RegistrarEstudiante.xaml
    /// </summary>
    public partial class RegistrarEstudiante : Window
    {
        private int idProgramaEducativo;
        public RegistrarEstudiante(int idProgramaEducativo)
        {
            InitializeComponent();
            tbTelefono.PreviewTextInput += new TextCompositionEventHandler(soloNumeros);
            tbSemestre.PreviewTextInput += new TextCompositionEventHandler(soloNumeros);
            tbNombre.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            tbApellidoPaterno.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            tbApellidoMaterno.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            this.idProgramaEducativo = idProgramaEducativo;
        }

        private void clicRegistrar (object sender, RoutedEventArgs e)
        {
            if (validarCamposVacios())
            {
                MessageBox.Show("Favor de llenar todos los campos", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (validarCampos())
                {
                    string nombre = tbNombre.Text;
                    string apellidoPaterno = tbApellidoPaterno.Text;
                    string apellidoMaterno = tbApellidoMaterno.Text;
                    string correoElectronico = tbCorreoElectronico.Text;
                    string matricula = tbMatricula.Text;
                    int semestre = int.Parse(tbSemestre.Text);
                    long telefono = long.Parse(tbTelefono.Text);
                    var confirmacion = MessageBox.Show("¿Está seguro de querer registrar al estudiante?", "Registrar estudiante",
                                               MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmacion == MessageBoxResult.Yes)
                    {
                        Estudiante estudiante = new Estudiante();
                        estudiante.nombre = nombre;
                        estudiante.apellidoPaterno = apellidoPaterno;
                        estudiante.apellidoMaterno = apellidoMaterno;
                        estudiante.correoElectronico = correoElectronico;
                        estudiante.matricula = matricula;
                        estudiante.semestreCursando = semestre;
                        estudiante.telefono = telefono;
                        registrarEstudiante(estudiante);
                    }
                }
                else
                {
                    MessageBox.Show("Favor de llenar los campos con el formato correcto", "Campos incorrectos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void registrarEstudiante(Estudiante estudiante)
        {
            var conexionServicio = new ServiciosTutorias.Service1Client();
            if(conexionServicio != null)
            {
                ResultadoOperacion resultado = await conexionServicio.registrarEstudianteAsync(estudiante);
                if (resultado.Error ==false)
                {
                    var irAAsignarTutor = MessageBox.Show(resultado.Mensaje, "Registro exitoso", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (irAAsignarTutor == MessageBoxResult.Yes)
                    {
                        AsignarTutorAcademicoAEstudiante ventanaAsignacionTutor = new AsignarTutorAcademicoAEstudiante(idProgramaEducativo, estudiante.matricula);
                        ventanaAsignacionTutor.Show();
                        ventanaAsignacionTutor.tbMatricula.Text = tbMatricula.Text;
                        ventanaAsignacionTutor.tbMatricula.IsReadOnly = true;
                        this.Close();
                    }
                    else
                    {
                        MenuAdministrador ventanaMenuAdministrador = new MenuAdministrador(idProgramaEducativo);
                        ventanaMenuAdministrador.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Registro fallido", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con la base de datos", "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Error);    
            }
        }

        private void clicCancelar(object sender, RoutedEventArgs e)
        {
            MenuAdministrador ventanaMenuAdministrador = new MenuAdministrador(idProgramaEducativo);
            ventanaMenuAdministrador.Show();
            this.Close();
        }

        private bool validarCamposVacios()
        {
            bool hayCamposVacios = false;
            if (tbNombre.Text == "")
            {
                tbNombre.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else 
            { 
                tbNombre.BorderBrush = Brushes.Black;
            }
            if (tbApellidoPaterno.Text == "")
            {
                tbApellidoPaterno.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbApellidoPaterno.BorderBrush = Brushes.Black;
            }
            if (tbApellidoMaterno.Text == "")
            {
                tbApellidoMaterno.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbApellidoMaterno.BorderBrush = Brushes.Black;
            }
            if (tbCorreoElectronico.Text == "")
            {
                tbCorreoElectronico.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbCorreoElectronico.BorderBrush = Brushes.Black;
            }
            if (tbMatricula.Text == "")
            {
                tbMatricula.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbMatricula.BorderBrush = Brushes.Black;
            }
            if (tbSemestre.Text == "")
            {
                tbSemestre.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbSemestre.BorderBrush = Brushes.Black;
            }
            if (tbTelefono.Text == "")
            {
                tbTelefono.BorderBrush = Brushes.Red;
                hayCamposVacios = true;
            }
            else
            {
                tbTelefono.BorderBrush = Brushes.Black;
            }
            return hayCamposVacios;
        }

        private bool validarCampos()
        {
            if (validarMatricula() && validarCorreo() && validarSemestre() && validarTelefono())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool validarCorreo()
        {
            bool correoValido = false;
            string correoIntroducido = tbCorreoElectronico.Text; 
            if (correoIntroducido.Contains("@"))
            {
                tbCorreoElectronico.BorderBrush = Brushes.Black;
                correoValido = true;
            }
            else
            {
                tbCorreoElectronico.BorderBrush = Brushes.Red;
            }
            return correoValido;
        }

        private bool validarSemestre()
        {
            bool semestreValido = false;
            if (tbSemestre.Text.All(char.IsDigit))
            {
                if (int.Parse(tbSemestre.Text) > 0 && int.Parse(tbSemestre.Text) < 13)
                {
                    tbSemestre.BorderBrush = Brushes.Black;
                    semestreValido = true;
                }
                else
                {
                    tbSemestre.BorderBrush = Brushes.Red;
                }
            }
            else{                 
                tbSemestre.BorderBrush = Brushes.Red;
            }
            return semestreValido;
        }

        private bool validarTelefono()
        {
            bool telefonoValido = false;
            if (tbTelefono.Text.All(char.IsDigit))
            {
                if (tbTelefono.Text.Length == 10)
                {
                    tbTelefono.BorderBrush = Brushes.Black;
                    telefonoValido = true;

                }
                else
                {
                    tbTelefono.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tbTelefono.BorderBrush = Brushes.Red;
            }
            return telefonoValido;
        }

        private bool validarMatricula()
        {
            bool matriculaValida = false;
            if (tbMatricula.Text.Length == 9)
            {
                if(tbMatricula.Text.StartsWith("S") || tbMatricula.Text.StartsWith("s"))
                {
                    if (tbMatricula.Text.Substring(1, 8).All(char.IsDigit))
                    {
                        tbMatricula.BorderBrush = Brushes.Black;
                        matriculaValida = true;
                    }
                    else
                    {
                        tbMatricula.BorderBrush = Brushes.Red;
                    }
                }
                else
                {
                    tbMatricula.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tbMatricula.BorderBrush = Brushes.Red;
            }
            return matriculaValida;
        }
        private void soloNumeros(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(char.IsDigit);
        }

        private void soloLetras(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.Any(char.IsLetter);
        }
    }

}
