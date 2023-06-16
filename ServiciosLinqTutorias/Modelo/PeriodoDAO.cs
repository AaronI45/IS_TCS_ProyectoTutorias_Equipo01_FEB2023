using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public class PeriodoDAO
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

        public static List<PeriodoEscolar> obtenerPeriodosEscolares()
        {
            var listaPeriodosEscolares = conexionBD.PeriodoEscolars;
            List<PeriodoEscolar> periodosEscolares = new List<PeriodoEscolar>();
            foreach (PeriodoEscolar periodoEscolarRegistrado in listaPeriodosEscolares)
            {
                PeriodoEscolar periodoEscolar = new PeriodoEscolar()
                {
                    inicioPeriodo = periodoEscolarRegistrado.inicioPeriodo,
                    finPeriodo = periodoEscolarRegistrado.finPeriodo,
                    primeraFechaTutoria = periodoEscolarRegistrado.primeraFechaTutoria,
                    segundaFechaTutoria = periodoEscolarRegistrado.segundaFechaTutoria,
                    terceraFechaTutoria = periodoEscolarRegistrado.terceraFechaTutoria
                };
                periodosEscolares.Add(periodoEscolar);
            }
            return periodosEscolares;
        }

        public static PeriodoEscolar obtenerPeriodoEscolarPorId(int idPeriodoEscolar)
        {
            var periodoEscolarRegistrado = conexionBD.PeriodoEscolars.FirstOrDefault(periodoEscolarEncontrado
                               => periodoEscolarEncontrado.idPeriodo_escolar == idPeriodoEscolar);
            PeriodoEscolar periodoEscolar = new PeriodoEscolar()
            {
                idPeriodo_escolar = periodoEscolarRegistrado.idPeriodo_escolar,
                inicioPeriodo = periodoEscolarRegistrado.inicioPeriodo,
                finPeriodo = periodoEscolarRegistrado.finPeriodo,
                primeraFechaTutoria = periodoEscolarRegistrado.primeraFechaTutoria,
                segundaFechaTutoria = periodoEscolarRegistrado.segundaFechaTutoria,
                terceraFechaTutoria = periodoEscolarRegistrado.terceraFechaTutoria
            };
            return periodoEscolar;
        }
    }
}