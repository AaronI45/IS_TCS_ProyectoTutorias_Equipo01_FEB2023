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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        ResultadoLogin iniciarSesion(string username, string password);

        [OperationContract]
        ResultadoOperacion registrarEstudiante(Estudiante nuevoEstudiante);

        [OperationContract]
        ResultadoOperacion registrarTutorAcademico(Academico nuevoTutor);

        [OperationContract]
        ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas);

        [OperationContract]
        ResultadoOperacion asignacionTutorAEstudiante(Estudiante estudianteAsignacion);

        [OperationContract]
        ResultadoOperacion registrarProblematica(RegistroProblematica problematicaPresentada);

        [OperationContract]
        List<Problematica> obtenerProblematicas();

        [OperationContract]
        ResultadoProblematica consultarProblematicaPorId(int idProblematica);

        [OperationContract]
        ResultadoOperacion registrarComentariosGenerales(Comentario comentarioPresentado);

        [OperationContract]
        ResultadoOperacion editarComentariosGenerales(string nuevosComentarios, int idTutoria);

        [OperationContract]
        List<ReporteTutoria> recuperarReportesPorTutor (int idTutor);

        [OperationContract]
        List<Estudiante> recuperarEstudiantesPorTutor(int idTutor);

        [OperationContract]
        Estudiante recuperarEstudiantePorMatricula(string matriculaEstudiante);
    }

    [DataContract]
    public class ResultadoOperacion
    {
        [DataMember]
        public string Mensaje { get; set; }
        [DataMember]
        public bool Error { get; set; }
        public ResultadoOperacion() { }
    }
    [DataContract]
    public class ResultadoLogin : ResultadoOperacion
    {
        [DataMember]
        public Academico AcademicoEncontrado { get; set; }
    }
}
