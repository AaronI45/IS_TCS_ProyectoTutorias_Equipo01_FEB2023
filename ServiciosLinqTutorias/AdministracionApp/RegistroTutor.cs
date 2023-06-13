using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.AdministracionApp
{
    public class RegistroTutor
    {
        public string nombre { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public int numeroPersonal { get; set; }
        public string correoInstitucional { get; set; }
        public long telefono { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int idProgramaEducativo { get; set; }
        public RegistroTutor() { }
    }
}