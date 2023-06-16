using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public static class ProgramaEducativoDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        public static ProgramaEducativo recuperarProgramaEducativoPorId(int idProgramaEducativo)
        {
            var programaEncontrado = conexionBD.ProgramaEducativos.FirstOrDefault(programaResultado => 
            programaResultado.idPrograma_educativo == idProgramaEducativo);
            ProgramaEducativo programaEducativo = new ProgramaEducativo()
            {
                idPrograma_educativo = programaEncontrado.idPrograma_educativo,
                jefeCarrera = programaEncontrado.jefeCarrera,
                coordinadorTutor = programaEncontrado.coordinadorTutor,
                nombre = programaEncontrado.nombre,
                region = programaEncontrado.region,
                modalidad = programaEncontrado.modalidad
            };
            return programaEducativo;
        }
    }
}