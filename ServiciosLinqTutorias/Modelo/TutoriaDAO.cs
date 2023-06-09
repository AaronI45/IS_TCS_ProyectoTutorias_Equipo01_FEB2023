using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public class TutoriaDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        private static readonly int NO_SOLUCIONADO = 1;
        public static ResultadoOperacion registrarProblematica (Problematica problematicaPresentada)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var problematica = new Problematica()
                {
                    clasificacion_problematica_idClasificacion_problematica = 
                    problematicaPresentada.clasificacion_problematica_idClasificacion_problematica,
                    estado_problematica_idestado_problematica = NO_SOLUCIONADO,
                    reporte_Tutoria_idReporte_Tutoria   = problematicaPresentada.reporte_Tutoria_idReporte_Tutoria,
                    titulo = problematicaPresentada.titulo,
                    descripcion = problematicaPresentada.descripcion,
                };
                conexionBD.Problematicas.InsertOnSubmit(problematica);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "La problematica se registró correctamente";
            }
            catch (Exception e)
            {
                resultado.Mensaje = "Error al registrar la problematica";
            }
            return resultado;
        }

        public static List<Problematica> consultarProblematicas()
        {
            var listaProblematicas = conexionBD.Problematicas;
            return listaProblematicas.ToList();
        }

        public static Problematica consultarProblematicaPorID(int idProblematica)
        {
            Problematica problematicaEncontrada = null;
            try
            {
                problematicaEncontrada = conexionBD.Problematicas.FirstOrDefault(problematica => problematica.idproblematica == idProblematica);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return problematicaEncontrada;
        }

        public static ResultadoOperacion registrarComentariosGenerales(Comentario nuevosComentarios)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var comentariosRegistro = new Comentario
                {
                    comentarios = nuevosComentarios.comentarios,
                    reporte_Tutoria_idReporte_Tutoria = nuevosComentarios.reporte_Tutoria_idReporte_Tutoria
                };
                conexionBD.Comentarios.InsertOnSubmit(comentariosRegistro);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "Los comentarios se registraron correctamente";
            }
            catch (Exception e)
            {
                resultado.Mensaje = "Error al registrar los comentarios";
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static ResultadoOperacion editarComentariosGenerales(Comentario comentarioEdicion)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var comentario = conexionBD.Comentarios.FirstOrDefault(comentarioEncontrado => comentarioEncontrado.idComentario == comentarioEdicion.idComentario);
                if(comentario != null)
                {
                    comentario.comentarios = comentarioEdicion.comentarios;
                    conexionBD.SubmitChanges();
                    resultado.Error = false;
                    resultado.Mensaje = "Los comentarios se editaron correctamente";
                }
                else
                {
                    resultado.Mensaje = "No se encontró el comentario";
                }
            }
            catch (Exception e)
            {
                resultado.Mensaje = "Error al editar los comentarios";
                Console.WriteLine(e.Message);
            }
            return resultado;
        }
    }
}