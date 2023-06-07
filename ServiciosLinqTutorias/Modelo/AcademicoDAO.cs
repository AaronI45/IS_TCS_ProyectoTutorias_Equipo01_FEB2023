using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ServiciosLinqTutorias.Modelo
{
    public static class AcademicoDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        public static ResultadoOperacion iniciarSesion(string usuario, string password)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = false;
            try
            {
                var encontrarUsuario = conexionBD.Usuarios.FirstOrDefault(usuarioEncontrado => usuarioEncontrado.username == usuario
                && ConvertidorSHA256.Comparar(password,usuarioEncontrado.password));

                if (encontrarUsuario != null) 
                {
                    resultado.Error = false;
                    resultado.Mensaje = "Bienvenido " + encontrarUsuario.Academico.nombre + " al sistema";
                }
            }
            catch   (Exception)
            {
                
            }
            return null;
        }
    }
}