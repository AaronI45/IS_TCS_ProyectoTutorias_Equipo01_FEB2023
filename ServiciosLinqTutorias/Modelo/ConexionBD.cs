using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public sealed class ConexionBD
    {
        private ConexionBD() { }
        private static ConexionBD instancia;
        private static DataClassesTutoriasUVDataContext Conexion { get; set; }

        public static ConexionBD Instancia
        {
            get
            {
                if (instancia == null)
                    Conexion = new DataClassesTutoriasUVDataContext(global::System.Configuration.
                        ConfigurationManager.ConnectionStrings["ConexionBDTutorias"].ConnectionString); 
                    instancia = new ConexionBD();
                return instancia;
            }
        }

        public DataClassesTutoriasUVDataContext ObtenerConexion()
        {
            return Conexion;
        }
    }
}