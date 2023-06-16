using ServiciosLinqTutorias.AdministracionApp;
using ServiciosLinqTutorias.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServiciosLinqTutorias
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public ResultadoLogin iniciarSesion(string username, string password)
        {
            return AcademicoDAO.iniciarSesion(username, password);
        }

        public ResultadoOperacion registrarEstudiante(Estudiante nuevoEstudiante)
        {
            return EstudianteDAO.registrarEstudiante(nuevoEstudiante);
        }

        public ResultadoOperacion registrarTutorAcademico(RegistroTutor nuevoTutor)
        {
            return AcademicoDAO.registrarTutorAcademico(nuevoTutor);
        }

        public ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas)
        {
            return PeriodoDAO.registrarFechaSesiontutoria(periodoFechas);
        }

        public ResultadoOperacion asignacionTutorAEstudiante(int idEstudiante, int idTutor)
        {
            return EstudianteDAO.asignacionTutorAEstudiante(idEstudiante, idTutor);
        }

        public ResultadoOperacion registrarProblematica(RegistroProblematica problematicaPresentada)
        {
            return TutoriaDAO.registrarProblematica(problematicaPresentada);
        }

        public List<Problematica> obtenerProblematicas()
        {
            return TutoriaDAO.consultarProblematicas();
        }

        public List<PeriodoEscolar> obtenerPeriodosEscolares() 
        {
            return PeriodoDAO.obtenerPeriodosEscolares();
        }

        public ResultadoProblematica consultarProblematicaPorId(int idProblematica)
        {
            return TutoriaDAO.consultarProblematicaPorID(idProblematica);
        }

        public ResultadoOperacion registrarComentariosGenerales(int idReporte, string comentarios)
        {
            return TutoriaDAO.registrarComentariosGenerales(idReporte, comentarios);
        }

        public ResultadoOperacion editarComentariosGenerales(string nuevosComentarios, int idTutoria)
        {
            return TutoriaDAO.editarComentariosGenerales(nuevosComentarios, idTutoria);
        }

        public List<ReporteTutoria> recuperarReportesPorTutor(int idTutor)
        {
            return TutoriaDAO.recuperarReportesPorTutor(idTutor);
        }

        public List<Estudiante> recuperarEstudiantesPorTutor(int idTutor)
        {
            return EstudianteDAO.obtenerEstudiantesPorTutor(idTutor);
        }

        public Estudiante recuperarEstudiantePorMatricula(string matriculaEstudiante)
        {
            return EstudianteDAO.recuperarEstudiantePorMatricula(matriculaEstudiante);
        }

        public List<Academico> recuperarTutoresPorProgramaEducativo(int idProgramaEducativo)
        {
            return AcademicoDAO.RecuperarTutoresPorProgramaEducativo(idProgramaEducativo);
        }

        public bool validarUsername(string username)
        {
            return AcademicoDAO.validarUsername(username);
        }

        public List<Estudiante> recuperarEstudiantes()
        {
            return EstudianteDAO.obtenerEstudiantes();
        }

        public Academico recuperarAcademicoPorId(int idAcademico)
        {
            return AcademicoDAO.recuperarAcademicoPorId(idAcademico);
        }

        public List<Comentario> recuperarComentariosPorIdTutoria(int idReporteTutoria)
        {
            return TutoriaDAO.recuperarComentariosPorIdTutoria(idReporteTutoria);
        }

        public bool validarMatricula(string matricula)
        {
            return EstudianteDAO.validarMatricula(matricula);
        }

        public List<Estudiante> recuperarEstudiantesAsistentes(int idTutoria)
        {
            return EstudianteDAO.recuperarAsistencias(idTutoria);
        }

        public Tutoria recuperarTutoriaPorId(int idTutoria)
        {
            return TutoriaDAO.recuperarTutoriaPorId(idTutoria);
        }

        public ProgramaEducativo recuperarProgramaEducativoPorId(int idProgramaEducativo)
        {
            return ProgramaEducativoDAO.recuperarProgramaEducativoPorId(idProgramaEducativo);
        }

        public PeriodoEscolar recuperarPeriodoEscolarPorId(int idPeriodoEscolar)
        {
            return PeriodoDAO.obtenerPeriodoEscolarPorId(idPeriodoEscolar);
        }

        public List<ClasificacionProblematica> recuperarClasificaciones()
        {
            return TutoriaDAO.recuperarClasificaciones();
        }

        public List<ExperienciaEducativa> recuperarExperienciasEducativas()
        {
            return ExperienciaEducativaDAO.recuperarExperienciasEducativas();   
        }
        
        public Comentario obtenerComentarioPorId(int idComentario)
        {
            return TutoriaDAO.recuperarComentarioPorId(idComentario);
        }
    }
}
