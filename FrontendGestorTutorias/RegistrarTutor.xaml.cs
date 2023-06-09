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
    /// Lógica de interacción para RegistrarTutor.xaml
    /// </summary>
    public partial class RegistrarTutor : Window
    {
        int idProgramaEducativo;
        public RegistrarTutor(int idProgramaEducativo)
        {
            this.idProgramaEducativo = idProgramaEducativo;
            InitializeComponent();
            tbNombre.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            tbApellidoPaterno.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            tbApellidoMaterno.PreviewTextInput += new TextCompositionEventHandler(soloLetras);
            tbNumeroPersonal.PreviewTextInput += new TextCompositionEventHandler(soloNumeros);
            tbNumeroTelefonico.PreviewTextInput += new TextCompositionEventHandler(soloNumeros);
        }

        private void clicRegistrar(object sender, RoutedEventArgs e)
        {
            if (!hayCamposVacios())
            {
                if (validarCampos())
                {
                    registrarTutorAsync();
                }
            }
            else
            {
                MessageBox.Show("Favor de llenar todos los campos");
            }
        }

        private async void registrarTutorAsync()
        {
            var conexionServicios = new Service1Client();
            if (conexionServicios != null)
            {
                bool validarUsername = await conexionServicios.validarUsernameAsync(tbNombreUsuario.Text);
                if (validarUsername)
                {
                    Academico academico = new Academico()
                    {
                        numerPersonal = int.Parse(tbNumeroPersonal.Text),
                        nombre = tbNombre.Text,
                        apellidoPaterno = tbApellidoPaterno.Text,
                        apellidoMaterno = tbApellidoMaterno.Text,
                        correoInstitucional = tbCorreoInstitucional.Text,
                        telefono = long.Parse(tbNumeroTelefonico.Text),
                        username = tbNombreUsuario.Text,
                        password = pbPassword.Password,
                        programa_educativo_idPrograma_educativo = this.idProgramaEducativo
                    };
                    MessageBox.Show("Tutor registrado exitosamente", "Registro exitoso");
                    MenuAdministrador menuAdministrador = new MenuAdministrador(this.idProgramaEducativo);
                    menuAdministrador.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El nombre de usuario ya existe, por favor ingrese uno nuevo", "Error");
                    tbNombreUsuario.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                MessageBox.Show("No se pudo conectar con el servidor", "Error");
            }
        }

        private void clicCancelar(object sender, RoutedEventArgs e)
        {
            MenuAdministrador menuAdministrador = new MenuAdministrador(this.idProgramaEducativo);
            menuAdministrador.Show();
            this.Close();
        }

        private bool validarCampos()
        {
            bool camposValidos = true;
            if(!validarCorreoInstitucional())
            {
                camposValidos = false;
            }
            if(!validarNumeroPersonal())
            {
                camposValidos = false;
            }
            if(!validarTelefono())
            {
                camposValidos = false;
            }
            if(!validarNumeroPersonal())
            {
                camposValidos = false;
            }
            if(!validarPassword())
            {
                camposValidos = false;
            }
            return camposValidos;
        }
        private bool hayCamposVacios()
        {
            bool camposVacios = false;
            if (tbNombre.Text == "")
            {
                tbNombre.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            { 
                tbNombre.BorderBrush = Brushes.Black;
            }
            if (tbApellidoPaterno.Text == "")
            {
                tbApellidoPaterno.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbApellidoPaterno.BorderBrush = Brushes.Black;
            }
            if (tbApellidoMaterno.Text == "")
            {
                tbApellidoMaterno.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbApellidoMaterno.BorderBrush = Brushes.Black;
            }
            if (tbCorreoInstitucional.Text == "")
            {
                tbCorreoInstitucional.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbCorreoInstitucional.BorderBrush = Brushes.Black;
            }
            if(tbNumeroTelefonico.Text == "")
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Black;
            }
            if(tbNumeroPersonal.Text == "")
            {
                tbNumeroPersonal.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbNumeroPersonal.BorderBrush = Brushes.Black;
            }
            if(tbNombreUsuario.Text == "")
            {
                tbNombreUsuario.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                tbNombreUsuario.BorderBrush = Brushes.Black;
            }
            if(pbPassword.Password == "")
            {
                pbPassword.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                pbPassword.BorderBrush = Brushes.Black;
            }
            if (pbCopiaPassword.Password == "")
            {
                pbCopiaPassword.BorderBrush = Brushes.Red;
                camposVacios = true;
            }
            else
            {
                pbCopiaPassword.BorderBrush = Brushes.Black;
            }
            return camposVacios;
        }

        private bool validarCorreoInstitucional()
        {
            bool correoValido = false;
            if (tbCorreoInstitucional.Text.Contains("@"))
            {
                tbCorreoInstitucional.BorderBrush = Brushes.Black;
                correoValido = true;
            }
            else
            {
               tbCorreoInstitucional.BorderBrush = Brushes.Red;
            }
            return correoValido;
        }
        private bool validarNumeroPersonal()
        {
            bool numeroPersonalValido = false;
            if (tbNumeroPersonal.Text.All(char.IsDigit))
            {
                if (tbNumeroPersonal.Text.Length == 5)
                {
                    tbNumeroPersonal.BorderBrush = Brushes.Black;
                    numeroPersonalValido = true;
                }
                else
                {
                    tbNumeroPersonal.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tbNumeroPersonal.BorderBrush = Brushes.Red;
            }
            return numeroPersonalValido;
        }

        private bool validarTelefono()
        {
            bool telefonoValido = false;
            if (tbNumeroTelefonico.Text.All(char.IsDigit))
            {
                if (tbNumeroTelefonico.Text.Length == 10)
                {
                    tbNumeroTelefonico.BorderBrush = Brushes.Black;
                    telefonoValido = true;

                }
                else
                {
                    tbNumeroTelefonico.BorderBrush = Brushes.Red;
                }
            }
            else
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Red;
            }
            return telefonoValido;
        }


        private bool validarPassword()
        {
            bool passwordValida = false;
            string password = pbPassword.Password;
            if (password.Length >= 8)
            {
                if (pbPassword.Password == pbCopiaPassword.Password)
                {
                    pbPassword.BorderBrush = Brushes.Black;
                    pbCopiaPassword.BorderBrush = Brushes.Black;
                    passwordValida = true;
                }
                else
                {
                    pbPassword.BorderBrush = Brushes.Red;
                    pbCopiaPassword.BorderBrush = Brushes.Red;
                    MessageBox.Show("Las contraseñas no coinciden, por favor vuelva a intentarlo");
                }
            }
            else
            {
                MessageBox.Show("La contraseña debe tener al menos 8 caracteres");
                pbPassword.BorderBrush = Brushes.Red;
            }
            return passwordValida;
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
