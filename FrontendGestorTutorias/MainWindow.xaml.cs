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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrontendGestorTutorias
{
    public partial class MainWindow : Window
    {
        private const int LOGIN_ADMINISTRADOR = 5;
        private const int LOGIN_TUTOR = 3;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            string username = tbUsername.Text;
            string password = pbPassword.Password;
            if(username.Length > 0  && password.Length > 0)
            {
                verificarInicioSesion(username, password);
            }
            else
            {
                MessageBox.Show("Usuario y/o contraseña requeridos para el inicio de sesion", "Error al iniciar sesion");
            }

        }

        private async void verificarInicioSesion(string username, string password)
        {
            var conexionServicios = new Service1Client();
            if (conexionServicios != null)
            {
                ResultadoLogin resultado = await conexionServicios.iniciarSesionAsync(username, password);
                if (resultado.Error)
                {
                    MessageBox.Show(resultado.Mensaje, "Credenciales incorrectas");
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Usuario verificado");
                    switch (resultado.AcademicoEncontrado.rol_idRol)
                    {
                        case LOGIN_ADMINISTRADOR:
                            MenuAdministrador ventanaAdministrador = new MenuAdministrador();
                            ventanaAdministrador.Show();
                            ventanaAdministrador.setAcademico(resultado.AcademicoEncontrado);
                            this.Close();
                            break;
                        case LOGIN_TUTOR:
                            MenuTutor ventanaTutor = new MenuTutor();
                            ventanaTutor.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Rol no reconocido", "Error al iniciar sesion");
                            break;
                    }
                }  
            }
            
                
            
        }
    }
}
