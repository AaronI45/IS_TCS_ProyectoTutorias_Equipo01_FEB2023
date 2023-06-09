using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public static class EstudianteDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();

        public static ResultadoOperacion registrarEstudiante (Estudiante nuevoEstudiante)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var studiante = new Estudiante()
                {
                    enRiesgo = 0,
                    nombre = nuevoEstudiante.nombre,
                    matricula = nuevoEstudiante.matricula,
                    apellidoPaterno = nuevoEstudiante.apellidoPaterno,
                    apellidoMaterno = nuevoEstudiante.apellidoMaterno,
                    correoElectronico = nuevoEstudiante.correoElectronico,
                    semestreCursando = nuevoEstudiante.semestreCursando,
                    telefono = nuevoEstudiante.telefono,
                };
                conexionBD.Estudiantes.InsertOnSubmit(studiante);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "El estudiante se registró correctamente, ¿Desea asignarle un tutor académico?";
            }
            catch (Exception e)
            {
                resultado.Mensaje = e.Message;
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static ResultadoOperacion asignacionTutorAEstudiante(Estudiante estudianteAsignacion)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;

            try
            {
                var estudiante = conexionBD.Estudiantes.FirstOrDefault(estudianteEncontrado
                => estudianteEncontrado.idEstudiante == estudianteAsignacion.idEstudiante);
                if (estudiante != null) 
                {
                    estudiante.academico_idAcademico = estudianteAsignacion.academico_idAcademico;
                    conexionBD.SubmitChanges();
                    resultado.Error = false;
                    resultado.Mensaje = "La asignacion fue realizada con exito";
                }
                else { resultado.Mensaje = "Estudiante no encontrado"; }
            }
            catch(Exception ex)
            {
                resultado.Mensaje = "Error de conexion";
            }
            return resultado;
        }

        public static List<Estudiante> obtenerEstudiantesPorTutor(int idTutor)
        {
            List<Estudiante> estudiantes = new List<Estudiante>();
            var estudiantesAsignados = conexionBD.Estudiantes.Where(estudiante => estudiante.academico_idAcademico == idTutor);
            foreach (var estudiante in estudiantesAsignados)
            {
                var estudianteEncontrado = new Estudiante()
                {
                    idEstudiante = estudiante.idEstudiante,
                    academico_idAcademico = estudiante.academico_idAcademico,
                    enRiesgo = estudiante.enRiesgo,
                    matricula = estudiante.matricula,
                    nombre = estudiante.nombre,
                    apellidoPaterno = estudiante.apellidoPaterno,
                    apellidoMaterno = estudiante.apellidoMaterno,
                    correoElectronico = estudiante.correoElectronico,
                    semestreCursando = estudiante.semestreCursando,
                    telefono = estudiante.telefono
                };
                estudiantes.Add(estudianteEncontrado);
            }
            return estudiantes;
        }

        public static Estudiante recuperarEstudiantePorMatricula(string matricula)
        {
            var estudiante = conexionBD.Estudiantes.FirstOrDefault(estudianteEncontrado => estudianteEncontrado.matricula == matricula);
            if (estudiante != null)
            {
                var estudianteEncontrado = new Estudiante()
                {
                    idEstudiante = estudiante.idEstudiante,
                    academico_idAcademico = estudiante.academico_idAcademico,
                    enRiesgo = estudiante.enRiesgo,
                    matricula = estudiante.matricula,
                    nombre = estudiante.nombre,
                    apellidoPaterno = estudiante.apellidoPaterno,
                    apellidoMaterno = estudiante.apellidoMaterno,
                    correoElectronico = estudiante.correoElectronico,
                    semestreCursando = estudiante.semestreCursando,
                    telefono = estudiante.telefono
                };
                return estudianteEncontrado;
            }
            else
            {
                return null;
            }
        }
    }
}