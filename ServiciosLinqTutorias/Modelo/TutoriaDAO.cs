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
        private static readonly int NO_SOLUCIONADO = 1;
        public static ResultadoOperacion registrarProblematica (RegistroProblematica problematicaPresentada)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var problematica = new Problematica()
                {
                    clasificacion_problematica_idClasificacion_problematica = 
                        problematicaPresentada.clasificacionProblematica,
                    estado_problematica_idestado_problematica = NO_SOLUCIONADO,
                    reporte_Tutoria_idReporte_Tutoria   = problematicaPresentada.idReporteTutoria,
                    titulo = problematicaPresentada.titulo,
                    descripcion = problematicaPresentada.descripcion,
                };
                conexionBD.Problematicas.InsertOnSubmit(problematica);
                conexionBD.SubmitChanges();
                if (problematicaPresentada.idExperienciaEducativa == 0)
                {
                    var problematicaEstudiante = new ProblematicaEstudiante()
                    {
                        estudiante_idEstudiante = problematicaPresentada.idEstudiante,
                        problematica_idproblematica = problematica.idproblematica
                    };
                    conexionBD.ProblematicaEstudiantes.InsertOnSubmit(problematicaEstudiante);
                }
                else
                {
                    var problematicaAcademica = new ProblematicaAcademica()
                    {
                        Problematica_idproblematica = problematica.idproblematica,
                        Estudiante_idEstudiante = problematicaPresentada.idEstudiante,
                        Experiencia_educativa_idExperiencia_educativa = problematicaPresentada.idExperienciaEducativa
                    };
                    conexionBD.ProblematicaAcademicas.InsertOnSubmit(problematicaAcademica);
                }
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "La problematica se registró correctamente";
            }
            catch (Exception e)
            {
                resultado.Mensaje = e.Message;
            }
            return resultado;
        }

        public static List<Problematica> consultarProblematicas()
        {
            var listaProblematicas = conexionBD.Problematicas;
            List<Problematica> problematicas = new List<Problematica>();
            foreach (Problematica problematicasRegistrada in listaProblematicas)
            {
                Problematica problematica = new Problematica()
                {
                    clasificacion_problematica_idClasificacion_problematica = problematicasRegistrada.clasificacion_problematica_idClasificacion_problematica,
                    estado_problematica_idestado_problematica = problematicasRegistrada.estado_problematica_idestado_problematica,
                    reporte_Tutoria_idReporte_Tutoria = problematicasRegistrada.reporte_Tutoria_idReporte_Tutoria,
                    titulo = problematicasRegistrada.titulo,
                    descripcion = problematicasRegistrada.descripcion
                };
                problematicas.Add(problematica);
            }
            return problematicas;
        }

        public static ResultadoProblematica consultarProblematicaPorID(int idProblematica)
        {
            ResultadoProblematica resultado = new ResultadoProblematica();
            resultado.Error = true;
            try
            {
                var problematicaEncontrada = conexionBD.Problematicas.FirstOrDefault(problematica => problematica.idproblematica == idProblematica);
                Problematica problematicaARegresar = new Problematica()
                {
                    idproblematica = problematicaEncontrada.idproblematica,
                    clasificacion_problematica_idClasificacion_problematica = problematicaEncontrada.clasificacion_problematica_idClasificacion_problematica,
                    estado_problematica_idestado_problematica = problematicaEncontrada.estado_problematica_idestado_problematica,
                    reporte_Tutoria_idReporte_Tutoria = problematicaEncontrada.reporte_Tutoria_idReporte_Tutoria,  
                    titulo = problematicaEncontrada.titulo, 
                    descripcion = problematicaEncontrada.descripcion
                };
                resultado.Error = false;
                resultado.Mensaje = "La problematica se encontró correctamente";
                resultado.Problematica = problematicaARegresar;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static ResultadoOperacion registrarComentariosGenerales(int idReporteTutoria, string comentarios)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var comentariosRegistro = new Comentario
                {
                    comentarios = comentarios,
                    reporte_Tutoria_idReporte_Tutoria = idReporteTutoria
                };
                conexionBD.Comentarios.InsertOnSubmit(comentariosRegistro);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "Los comentarios se registraron correctamente";
            }
            catch (Exception e)
            {
                resultado.Mensaje = "Error al registrar los comentarios";
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static ResultadoOperacion editarComentariosGenerales(string nuevosComentarios, int idTutoria)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var comentario = conexionBD.Comentarios.FirstOrDefault(comentarioEncontrado => comentarioEncontrado.reporte_Tutoria_idReporte_Tutoria == idTutoria);
                if(comentario != null)
                {
                    comentario.comentarios = nuevosComentarios;
                    conexionBD.SubmitChanges();
                    resultado.Error = false;
                    resultado.Mensaje = "Los comentarios se editaron correctamente";
                }
                else
                {
                    resultado.Mensaje = "No se encontró el comentario";
                }
            }
            catch (Exception e)
            {
                resultado.Mensaje = "Error al editar los comentarios";
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static List<Comentario> recuperarComentarios()
        {
            var listaComentarios = conexionBD.Comentarios;
            List<Comentario> comentariosGenerales = new List<Comentario>();
            foreach (Comentario comentarioRegistrado in listaComentarios)
            {
                Comentario comentarioGeneral = new Comentario()
                {
                    comentarios = comentarioRegistrado.comentarios
                };
                comentariosGenerales.Add(comentarioGeneral);

            }
            return comentariosGenerales;
        }

        public static List<ReporteTutoria> recuperarReportesPorTutor (int idTutorAcademico)
        {
            List<ReporteTutoria> reportes = new List<ReporteTutoria>();
            var listaReportes = conexionBD.ReporteTutorias.Where(reporte => reporte.academico_idAcademico == idTutorAcademico);
            foreach (ReporteTutoria reporte in listaReportes)
            {
                var reporteEncontrado = new ReporteTutoria()
                {
                    idReporte_Tutoria = reporte.idReporte_Tutoria,
                    descripcion = reporte.descripcion,
                    programa_educativo_idPrograma_educativo = reporte.programa_educativo_idPrograma_educativo,
                    tutoria_idTutoria = reporte.tutoria_idTutoria,
                    academico_idAcademico = reporte.academico_idAcademico,
                };
                reportes.Add(reporteEncontrado);
            }
            return reportes;
        }

        public static Tutoria recuperarTutoriaPorId(int idTutoria)
        {
            var tutoria = conexionBD.Tutorias.FirstOrDefault(tutoriaResultado => tutoriaResultado.idTutoria == idTutoria);
            Tutoria tutoriaEncontrada = new Tutoria()
            {
                idTutoria = tutoria.idTutoria,
                periodo_escolar_idPeriodo_escolar = tutoria.periodo_escolar_idPeriodo_escolar,
                numeroTutoria = tutoria.numeroTutoria,
                fechaTutoria = tutoria.fechaTutoria,
                fechaEntrega = tutoria.fechaEntrega,
            };
            return tutoriaEncontrada;
        }
        
        public static List<ClasificacionProblematica> recuperarClasificaciones()
        {
            List<ClasificacionProblematica> clasifiacionesEncontradas = new List<ClasificacionProblematica>();
            var clasificaciones = conexionBD.ClasificacionProblematicas;
            foreach (ClasificacionProblematica clasificacion in clasificaciones)
            {
                ClasificacionProblematica clasificacionEncontrada = new ClasificacionProblematica()
                {
                    idClasificacion_problematica = clasificacion.idClasificacion_problematica,
                    clasificacion = clasificacion.clasificacion
                };
                clasifiacionesEncontradas.Add(clasificacionEncontrada);
            }
                return clasifiacionesEncontradas;
        }
    }
}