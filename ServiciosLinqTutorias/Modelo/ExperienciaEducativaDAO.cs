using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public static class ExperienciaEducativaDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();

        public static List<ExperienciaEducativa> recuperarExperienciasEducativas()
        {
            List<ExperienciaEducativa> listaEE = new List<ExperienciaEducativa>();
            var listaEEBd = conexionBD.ExperienciaEducativas;
            foreach (ExperienciaEducativa experienciaEducativa in listaEEBd)
            {
                ExperienciaEducativa experienciaAAgregar = new ExperienciaEducativa()
                {
                    idExperiencia_educativa = experienciaEducativa.idExperiencia_educativa,
                    academico_idAcademico = experienciaEducativa.academico_idAcademico,
                    periodo_escolar_idPeriodo_escolar = experienciaEducativa.periodo_escolar_idPeriodo_escolar,
                    nombre = experienciaEducativa.nombre
                };
                listaEE.Add(experienciaAAgregar);
            }
            return listaEE;
        }
    }
}