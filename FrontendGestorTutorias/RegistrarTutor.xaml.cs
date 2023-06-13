using FrontendGestorTutorias.Utilidades;
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
            Validacion validacion = hayCamposVacios();
            if (!validacion.Error)
            {
                validacion = validarCampos();
                if (!validacion.Error)
                {
                    registrarTutorAsync();
                }
                else
                {
                    MessageBox.Show(validacion.Mensaje, "Error de campos inválidos", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show(validacion.Mensaje,"Error de campos vacíos", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    RegistroTutor nuevoTutor= new RegistroTutor()
                    {
                        numeroPersonal = int.Parse(tbNumeroPersonal.Text),
                        nombre = tbNombre.Text,
                        apellidoPaterno = tbApellidoPaterno.Text,
                        apellidoMaterno = tbApellidoMaterno.Text,
                        correoInstitucional = tbCorreoInstitucional.Text,
                        telefono = long.Parse(tbNumeroTelefonico.Text),
                        username = tbNombreUsuario.Text,
                        password = pbPassword.Password,
                        idProgramaEducativo = this.idProgramaEducativo
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

        private Validacion validarCampos()
        {
            Validacion camposValidos = new Validacion();
            camposValidos.Mensaje = "Los siguientes campos no son válidos: ";
            return camposValidos;
        }
        private Validacion hayCamposVacios()
        {
            Validacion camposVacios = new Validacion();
            camposVacios.Mensaje = "Los siguientes campos no pueden estar vacíos: ";
            camposVacios.Error = false;
            if (tbNombre.Text == "")
            {
                tbNombre.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Nombre"; 
                camposVacios.Error = true;
            }
            else
            { 
                tbNombre.BorderBrush = Brushes.Black;
            }
            if (tbApellidoPaterno.Text == "")
            {
                tbApellidoPaterno.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Apellido paterno";
                camposVacios.Error = true;
            }
            else
            {
                tbApellidoPaterno.BorderBrush = Brushes.Black;
            }
            if (tbApellidoMaterno.Text == "")
            {
                tbApellidoMaterno.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Apellido materno";
                camposVacios.Error = true;
            }
            else
            {
                tbApellidoMaterno.BorderBrush = Brushes.Black;
            }
            if (tbCorreoInstitucional.Text == "")
            {
                tbCorreoInstitucional.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Correo institucional";
                camposVacios.Error = true;
            }
            else
            {
                tbCorreoInstitucional.BorderBrush = Brushes.Black;
            }
            if(tbNumeroTelefonico.Text == "")
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Número telefónico";
                camposVacios.Error = true;
            }
            else
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Black;
            }
            if(tbNumeroPersonal.Text == "")
            {
                tbNumeroPersonal.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Número de personal";
                camposVacios.Error = true;
            }
            else
            {
                tbNumeroPersonal.BorderBrush = Brushes.Black;
            }
            if(tbNombreUsuario.Text == "")
            {
                tbNombreUsuario.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Nombre de usuario";
                camposVacios.Error = true;
            }
            else
            {
                tbNombreUsuario.BorderBrush = Brushes.Black;
            }
            if(pbPassword.Password == "")
            {
                pbPassword.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Contraseña";
                camposVacios.Error = true;
            }
            else
            {
                pbPassword.BorderBrush = Brushes.Black;
            }
            if (pbCopiaPassword.Password == "")
            {
                pbCopiaPassword.BorderBrush = Brushes.Red;
                camposVacios.Mensaje += "\n-Copia de contraseña";
                camposVacios.Error= true;
            }
            else
            {
                pbCopiaPassword.BorderBrush = Brushes.Black;
            }
            return camposVacios;
        }

        //TODO : Validar que el correo institucional sea válido
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
        private Validacion validarNumeroPersonal()
        {
            Validacion numeroPersonalValido = new Validacion();
            numeroPersonalValido.Mensaje = "El número personal no es válido: ";
            numeroPersonalValido.Error = false;
            if (tbNumeroPersonal.Text.All(char.IsDigit))
            {
                if (tbNumeroPersonal.Text.Length == 5)
                {
                    tbNumeroPersonal.BorderBrush = Brushes.Black;
                }
                else
                {
                    tbNumeroPersonal.BorderBrush = Brushes.Red;
                    numeroPersonalValido.Error = true;
                    numeroPersonalValido.Mensaje += "\n-El número personal debe contener 5 dígitos";
                }
            }
            else
            {
                tbNumeroPersonal.BorderBrush = Brushes.Red;
                numeroPersonalValido.Error = true;
                numeroPersonalValido.Mensaje += "\n-El número personal debe contener solo números";
            }
            return numeroPersonalValido;
        }

        private Validacion validarTelefono()
        {
            Validacion telefonoValido = new Validacion();
            telefonoValido.Mensaje = "El número telefónico no es válido: ";
            telefonoValido.Error = false;
            if (tbNumeroTelefonico.Text.All(char.IsDigit))
            {
                if (tbNumeroTelefonico.Text.Length == 10)
                {
                    tbNumeroTelefonico.BorderBrush = Brushes.Black;

                }
                else
                {
                    tbNumeroTelefonico.BorderBrush = Brushes.Red;
                    telefonoValido.Error = true;
                    telefonoValido.Mensaje += "\n-El número telefónico debe contener 10 dígitos";
                }
            }
            else
            {
                tbNumeroTelefonico.BorderBrush = Brushes.Red;
                telefonoValido.Error = true;
                telefonoValido.Mensaje += "\n-El número telefónico debe contener solo números";
            }
            return telefonoValido;
        }

        //validar que la password sea de al menos 8 caracteres, contenga caracteres especiales y numeros y que coincida con la copia
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
