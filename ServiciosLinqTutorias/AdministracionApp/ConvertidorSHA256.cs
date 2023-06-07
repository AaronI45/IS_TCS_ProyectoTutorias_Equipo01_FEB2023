using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ServiciosLinqTutorias.AdministracionApp
{
    public static class ConvertidorSHA256
    {
        public static string Convertir(string cadena)
        {
            SHA256Managed encriptador = new SHA256Managed();
            byte[] cadenaEncriptada = encriptador.ComputeHash(Encoding.UTF8.GetBytes(cadena));
            StringBuilder cadenaEncriptadaSB = new StringBuilder();
            foreach (byte caracter in cadenaEncriptada)
            {
                cadenaEncriptadaSB.Append(caracter.ToString("x2"));
            }
            return cadenaEncriptadaSB.ToString();
        }

        public static bool Comparar(string cadena, string cadenaEncriptada)
        {
            string cadenaEncriptadaCadena = Convertir(cadena);
            StringComparer comparador = StringComparer.CurrentCulture;
            return comparador.Compare(cadenaEncriptadaCadena, cadenaEncriptada) == 0;
        }
    }
}