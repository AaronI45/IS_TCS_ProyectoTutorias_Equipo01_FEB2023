using ServiciosLinqTutorias.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.AdministracionApp
{
    public class ResultadoProblematica : ResultadoOperacion
    {
        public Problematica Problematica { get; set; }
        public ResultadoProblematica() { }
    }
}