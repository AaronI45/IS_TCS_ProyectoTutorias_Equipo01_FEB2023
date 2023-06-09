﻿using ServiciosLinqTutorias.AdministracionApp;
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
    }
}