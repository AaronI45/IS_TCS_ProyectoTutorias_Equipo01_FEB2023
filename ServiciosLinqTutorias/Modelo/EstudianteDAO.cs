﻿using ServiciosLinqTutorias.AdministracionApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiciosLinqTutorias.Modelo
{
    public static class EstudianteDAO
    {
        private static DataClassesTutoriasUVDataContext conexionBD = ConexionBD.Instancia.ObtenerConexion();

        public static ResultadoOperacion registrarEstudiante (Estudiante nuevoEstudiante)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            resultado.Error = true;
            try
            {
                var studiante = new Estudiante()
                {
                    academico_idAcademico = 1,
                    estado_estudiante_idEstado_estudiante = 1,
                    nombre = nuevoEstudiante.nombre,
                    matricula = nuevoEstudiante.matricula,
                    apellidoPaterno = nuevoEstudiante.apellidoPaterno,
                    apellidoMaterno = nuevoEstudiante.apellidoMaterno,
                    correoElectronico = nuevoEstudiante.correoElectronico,
                    SemestreCursando = nuevoEstudiante.SemestreCursando,
                    telefono = nuevoEstudiante.telefono,
                };
                conexionBD.Estudiantes.InsertOnSubmit(studiante);
                conexionBD.SubmitChanges();
                resultado.Error = false;
                resultado.Mensaje = "El estudiante se registró correctamente, ¿Desea asignarle un tutor académico?";
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