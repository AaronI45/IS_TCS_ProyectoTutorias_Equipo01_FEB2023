using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ServiciosLinqTutorias.Modelo
{
    public static class AcademicoDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();
        private static int TUTOR_ACADEMICO = 3;

        public static ResultadoLogin iniciarSesion(string usuario, string password)
        {
            ResultadoLogin resultado = new ResultadoLogin();
            resultado.Error = false;
            try
            {
                var encontrarUsuario = conexionBD.Academicos.FirstOrDefault(usuarioEncontrado => usuarioEncontrado.username == usuario
                && usuarioEncontrado.password == ConvertidorSHA256.Convertir(password));
                if (encontrarUsuario != null) 
                {
                    Academico academico = new Academico()
                    {
                        rol_idRol = encontrarUsuario.rol_idRol,
                        programa_educativo_idPrograma_educativo = encontrarUsuario.programa_educativo_idPrograma_educativo,
                        numerPersonal = encontrarUsuario.numerPersonal,
                        correoInstitucional = encontrarUsuario.correoInstitucional,
                        nombre = encontrarUsuario.nombre,
                        apellidoPaterno = encontrarUsuario.apellidoPaterno,
                        apellidoMaterno = encontrarUsuario.apellidoMaterno,
                        telefono = encontrarUsuario.telefono,
                    };
                    resultado.AcademicoEncontrado = academico;
                    resultado.Error = false;
                    resultado.Mensaje = "Bienvenido " + academico.nombre + " al sistema";
                    return resultado;
                }
                else
                {
                    resultado.Mensaje = "Usuario o contraseña incorrectos";
                }
            }
            catch   (Exception e)
            {
                resultado.Mensaje = e.Message;
            }
            return resultado;
        }

        public static ResultadoOperacion registrarTutorAcademico(Academico nuevoTutor)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;

            try
            {
                var tutor = new Academico()
                {
                    rol_idRol = TUTOR_ACADEMICO,
                    numerPersonal = nuevoTutor.numerPersonal,
                    correoInstitucional = nuevoTutor.correoInstitucional,
                    nombre = nuevoTutor.nombre,
                    apellidoPaterno = nuevoTutor.apellidoPaterno,
                    apellidoMaterno = nuevoTutor.apellidoMaterno,
                    telefono = nuevoTutor.telefono,
                    programa_educativo_idPrograma_educativo = nuevoTutor.programa_educativo_idPrograma_educativo,
                    username = nuevoTutor.username,
                    password = ConvertidorSHA256.Convertir(nuevoTutor.password)
                };
                conexionBD.Academicos.InsertOnSubmit(tutor);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "El tutor se registró correctamente";

            }
            catch (Exception e) 
            {
                resultado.Mensaje = e.Message;
                Console.WriteLine(e.Message);
            }
            return resultado;
        }

        public static List<Academico> RecuperarTutoresPorProgramaEducativo(int idProgramaEducativo)
        {
            List<Academico> tutores = new List<Academico>();
            try
            {
                var tutoresEncontrados = from tutor in conexionBD.Academicos
                                         where tutor.programa_educativo_idPrograma_educativo == idProgramaEducativo && tutor.rol_idRol == TUTOR_ACADEMICO
                                         select tutor;
                foreach (var tutor in tutoresEncontrados)
                {
                    Academico tutorEncontrado = new Academico()
                    {
                        idAcademico = tutor.idAcademico,
                        rol_idRol = tutor.rol_idRol,
                        numerPersonal = tutor.numerPersonal,
                        correoInstitucional = tutor.correoInstitucional,
                        nombre = tutor.nombre,
                        apellidoPaterno = tutor.apellidoPaterno,
                        apellidoMaterno = tutor.apellidoMaterno,
                        telefono = tutor.telefono,
                        programa_educativo_idPrograma_educativo = tutor.programa_educativo_idPrograma_educativo
                    };
                    tutores.Add(tutorEncontrado);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return tutores;
        }

        public static bool validarUsername (string username)
        {
            bool usernameValido = false;
            try
            {
                var encontrarUsername = conexionBD.Academicos.FirstOrDefault(usernameEncontrado => usernameEncontrado.username == username);
                if (encontrarUsername == null)
                {
                    usernameValido = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return usernameValido;
        }
    }
}