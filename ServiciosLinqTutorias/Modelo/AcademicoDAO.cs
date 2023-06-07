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
                var usuarioIniciado = from usuarioEncontrado in conexionBD.Usuarios
                                      join academico in conexionBD.Academicos on usuarioEncontrado.academico_idAcademico
                                      equals academico.idAcademico select academico;

                if (usuarioIniciado != null) 
                {
                    resultado.Error = false;
                    resultado.Mensaje = "Bienvenido " + usuarioIniciado;
                }
            }
            catch   (Exception)
            {
                
            }
            return null;
        }
    }
}