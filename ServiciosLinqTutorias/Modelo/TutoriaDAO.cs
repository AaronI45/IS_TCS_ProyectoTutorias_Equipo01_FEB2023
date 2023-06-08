using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public class TutoriaDAO
    {
        DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        public static ResultadoOperacion registrarProblematica (Problematica problematicaPresentada)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                
            }
            catch (Exception e)
            {
                
            }
            return resultado;
        }
    }
}