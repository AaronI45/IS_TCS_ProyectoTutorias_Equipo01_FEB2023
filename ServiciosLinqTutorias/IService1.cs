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
        ResultadoOperacion iniciarSesion(string username, string password);

        [OperationContract]
        ResultadoOperacion registrarEstudiante(Estudiante nuevoEstudiante);

        [OperationContract]
        ResultadoOperacion registrarTutorAcademico(Academico nuevoTutor);

        [OperationContract]
        ResultadoOperacion registrarFechaSesiontutoria(PeriodoEscolar periodoFechas);

        [OperationContract]
        ResultadoOperacion asignacionTutorAEstudiante(Estudiante estudianteAsignacion);

        [OperationContract]
        ResultadoOperacion registrarProblematica(Problematica problematicaPresentada);

        [OperationContract]
        List<Problematica> obtenerProblematicas();

        [OperationContract]
        Problematica consultarProblematicaPorId(int idProblematica);

        [OperationContract]
        ResultadoOperacion registrarComentariosGenerales(Comentario comentarioPresentado);

        [OperationContract]
        ResultadoOperacion editarComentariosGenerales(Comentario comentarioPresentado);
    }
        namespace Tipos
        {
            [DataContract]
            public class Problematica
            {
                [DataMember]
                public int idProblematica { get; set; }
                [DataMember]
                public string descripcionProblematica { get; set; }
                [DataMember]
                public string titulo { get; set; }
                [DataMember]
                public int clasificacion { get; set; }
                [DataMember]
                public int estado{ get; set; }
            }   
        }
    
}
