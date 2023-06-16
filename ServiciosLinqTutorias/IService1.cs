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
        ResultadoOperacion registrarTutorAcademico(RegistroTutor nuevoTutor);

        [OperationContract]
        ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas);

        [OperationContract]
        ResultadoOperacion asignacionTutorAEstudiante(int idEstudiante, int idTutor);

        [OperationContract]
        ResultadoOperacion registrarProblematica(RegistroProblematica problematicaPresentada);

        [OperationContract]
        List<Problematica> obtenerProblematicas();

        [OperationContract]
        List<PeriodoEscolar> obtenerPeriodosEscolares();

        [OperationContract]
        ResultadoProblematica consultarProblematicaPorId(int idProblematica);

        [OperationContract]
        ResultadoOperacion registrarComentariosGenerales(int idReporteTutoria, string comentarios);

        [OperationContract]
        ResultadoOperacion editarComentariosGenerales(string nuevosComentarios, int idTutoria);

        [OperationContract]
        List<ReporteTutoria> recuperarReportesPorTutor (int idTutor);

        [OperationContract]
        List<Estudiante> recuperarEstudiantesPorTutor(int idTutor);

        [OperationContract]
        Estudiante recuperarEstudiantePorMatricula(string matriculaEstudiante);

        [OperationContract]
        List<Academico> recuperarTutoresPorProgramaEducativo(int idProgramaEducativo);
        [OperationContract]
        bool validarUsername(string username);
        [OperationContract]
        List<Estudiante> recuperarEstudiantes();
        [OperationContract]
        Academico recuperarAcademicoPorId(int idAcademico);

        [OperationContract]
        bool validarMatricula(string matricula);
        [OperationContract]
        List<Estudiante> recuperarEstudiantesAsistentes(int idTutoria);
        [OperationContract]
        Tutoria recuperarTutoriaPorId(int idTutoria);
        [OperationContract]
        ProgramaEducativo recuperarProgramaEducativoPorId(int idProgramaEducativo);
        [OperationContract]
        PeriodoEscolar recuperarPeriodoEscolarPorId(int idPeriodoEscolar);
        [OperationContract]
        List<ClasificacionProblematica> recuperarClasificaciones();
        [OperationContract]
        List<ExperienciaEducativa> recuperarExperienciasEducativas();

        [OperationContract]
        List<Comentario> recuperarComentariosPorIdTutoria(int idReporteTutoria);

        [OperationContract]
        Comentario obtenerComentarioPorId(int idComentario);
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
    [DataContract]
    public class RegistroTutor
    {
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string apellidoPaterno { get; set; }
        [DataMember]
        public string apellidoMaterno { get; set; }
        [DataMember]
        public int numeroPersonal { get; set; }
        [DataMember]
        public string correoInstitucional { get; set; }
        [DataMember]
        public long telefono { get; set; }
        [DataMember]
        public string username { get; set; }
        [DataMember]
        public string password { get; set; }
        [DataMember]
        public int idProgramaEducativo { get; set; }
    }
    [DataContract]
    public class RegistroProblematica
    {
        [DataMember]
        public int clasificacionProblematica { get; set; }
        [DataMember]
        public int idReporteTutoria { get; set; }
        [DataMember]
        public string titulo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int idEstudiante { get; set; }
        [DataMember]
        public int idExperienciaEducativa { get; set; }
    }
}
