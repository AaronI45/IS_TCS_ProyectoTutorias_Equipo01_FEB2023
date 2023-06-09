using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.AdministracionApp
{
    public class RegistroProblematica
    {
        public int clasificacionProblematica { get; set; }
        public int idReporteTutoria { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public int idEstudiante { get; set; }
        public int idExperienciaEducativa { get; set; }
    }
}