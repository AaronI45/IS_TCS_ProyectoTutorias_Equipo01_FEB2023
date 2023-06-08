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

        public ResultadoOperacion iniciarSesion(string username, string password)
        {
            return AcademicoDAO.iniciarSesion(username, password);
        }

        public ResultadoOperacion registrarEstudiante(Estudiante nuevoEstudiante)
        {
            return EstudianteDAO.registrarEstudiante(nuevoEstudiante);
        }

        public ResultadoOperacion registrarTutorAcademico(Academico nuevoTutor)
        {
            return AcademicoDAO.registrarTutorAcademico(nuevoTutor);
        }

        public ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas)
        {
            return PeriodoDAO.registrarFechaSesiontutoria(periodoFechas);
        }

        public ResultadoOperacion asignacionTutorAEstudiante(Estudiante estudianteAsignacion)
        {
            return EstudianteDAO.asignacionTutorAEstudiante(estudianteAsignacion);
        }

        public ResultadoOperacion registrarProblematica(Problematica problematicaPresentada)
        {
            return TutoriaDAO.registrarProblematica(problematicaPresentada);
        }

        public List<Problematica> obtenerProblematicas()
        {
            return TutoriaDAO.consultarProblematicas();
        }

        public Problematica consultarProblematicaPorId(int idProblematica)
        {
            return TutoriaDAO.consultarProblematicaPorID(idProblematica);
        }

        public ResultadoOperacion registrarComentariosGenerales(Comentario comentarioPresentado)
        {
            return TutoriaDAO.registrarComentariosGenerales(comentarioPresentado);
        }

        public ResultadoOperacion editarComentariosGenerales(Comentario comentarioPresentado)
        {
            return TutoriaDAO.editarComentariosGenerales(comentarioPresentado);
        }
    }
}
