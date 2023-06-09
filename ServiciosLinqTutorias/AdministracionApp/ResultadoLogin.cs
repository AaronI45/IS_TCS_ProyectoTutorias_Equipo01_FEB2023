using ServiciosLinqTutorias.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.AdministracionApp
{
    public class ResultadoLogin : ResultadoOperacion
    {
        public Academico AcademicoEncontrado { get; set; }
    }
}