using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public class TutoriaDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        public static ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;

            try
            {
                var periodo = conexionBD.PeriodoEscolars.FirstOrDefault(periodoEncontrado 
                            => periodoEncontrado.idPeriodo_escolar == periodoFechas.idPeriodo_escolar);
                if(periodo != null)
                {
                    periodo.primeraFechaTutoria = periodoFechas.primeraFechaTutoria;
                    periodo.segundaFechaTutoria = periodoFechas.segundaFechaTutoria;
                    periodo.terceraFechaTutoria = periodoFechas.terceraFechaTutoria;
                    conexionBD.SubmitChanges();
                    resultado.Error = false;
                    resultado.Mensaje = "Fechas registradas con exito";
                }
                else { resultado.Mensaje = "Periodo escolar no encontrado";}
            }
            catch(Exception ex)
            {
                resultado.Mensaje = "Error de conexion";
            }
            return resultado;
        }
    }
}