using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public static class AcademicoDAO
    {
        public static ResultadoOperacion iniciarSesion(string usuario, string password)
        {
            DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerContexto();
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = false;

              return null;
        }
    }
}