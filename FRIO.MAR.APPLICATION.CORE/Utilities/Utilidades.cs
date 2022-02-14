
using GS.TOOLS;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Utilities
{
    public static class Utilidades
    {
        public static string RandomString(int longitudContrasena = 10)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890.!$@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = longitudContrasena;
            StringBuilder contraseniaAleatoria = new StringBuilder();
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria.Append(letra.ToString());
            }

            return contraseniaAleatoria.ToString();
        }

        public static string GenerarCodigoSoporte(string CodigoPais, string CodigoCategoria, long serieSoporte, int? AnioOperacion = null)
        {
            DateTime Fecharegistro = Utilidades.GetHoraActual();

            var codigoBase = CodigoPais + "-" + CodigoCategoria;

            return $"{codigoBase}-{Fecharegistro.ToString("ddMMyy")}-{serieSoporte.ToString().PadLeft(6, '0')}";
        }

        public static DateTime GetHoraActual(string TimeZone = "")
        {
            if (string.IsNullOrEmpty(TimeZone))
            {
                return DateTime.Now;
            }
            else
            {
                return TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(TimeZone));
            }
        }

        public static bool ValidarClave(string ClaveActual, string ClaveActualHash, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion, ref string mensaje)
        {
            if (!GSCrypto.ConfirmHashV1(ClaveActual, ClaveActualHash))
            {
                if (EsCorreoRecuperacion)
                    mensaje = "La contraseña temporal es incorrecta. Verifique su correo";
                else
                    mensaje = "La contraseña actual es incorrecta";
                return false;
            }

            if (ClaveNueva.Length < 8)
            {
                mensaje = "La nueva contraseña debe tener minimo 8 caracteres";
                return false;
            }

            string caracteresPermitidos = "._-*$#%&?!";
            int contMAYUS = 0;
            int contMINUS = 0;
            int contNUM = 0;
            int contESP = 0;

            foreach (char c in ClaveNueva)
            {
                if (!char.IsDigit(c) & !char.IsLetter(c) & !caracteresPermitidos.Contains(c))
                {
                    mensaje = "La nueva contraseña solo puede contener letras, números y los caracteres especiales: ._-*$#%&?!";
                    return false;
                }
                if (char.IsUpper(c))
                    contMAYUS += 1;
                if (char.IsLower(c))
                    contMINUS += 1;
                if (char.IsDigit(c))
                    contNUM += 1;
                if (caracteresPermitidos.Contains(c))
                    contESP += 1;
            }

            if (contMAYUS == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos una letra mayúscula";
                return false;
            }
            if (contMINUS == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos una letra minúscula";
                return false;
            }
            if (contNUM == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos un número";
                return false;
            }
            if (contESP == 0)
            {
                mensaje = "La nueva contraseña debe contener al menos uno de los siguientes caracteres especiales: ._-*$#%&?!";
                return false;
            }

            if (ClaveNueva != ClaveNuevaConfirma)
            {
                mensaje = "La nueva contraseña no coincide con la confirmación";
                return false;
            }

            if (GSCrypto.ConfirmHashV1(ClaveNueva, ClaveActualHash))
            {
                if (EsCorreoRecuperacion)
                    mensaje = "La nueva contraseña no puede ser igual a la recibida por correo";
                else
                    mensaje = "La nueva contraseña no puede ser igual a la anterior";
                return false;
            }

            return true;
        }

        public static string OcultarCaracteres(string text, int count)
        {
            string TextoEnx = "";
            for (int i = 0; i < count; i++)
            {
                TextoEnx = TextoEnx + "X";
            }

            int tamanoString = text.Length;
            string SubStringCorreosInicial = text.Substring(count, tamanoString - count);
            return TextoEnx + SubStringCorreosInicial;
        }

        public static string ToReverseString(this string value)
        {
            return string.Join("", value.Reverse());
        }

        public static string SubstringReverse(this string value, int indexFromEnd, int length)
        {
            return value.ToReverseString().Substring(indexFromEnd, length).ToReverseString();
        }

        public static string GenerarTokenEncuesta(string Codigo, string IdentificacionEmpresa, long IdEncuestaTicket)
        {
            string token = $"{IdEncuestaTicket};{Codigo};{IdentificacionEmpresa}";

            return Utilities.Crypto.CifrarId(token);
        }

        public static string DoubleToString_FrontCO(decimal? valor, int DigitoDecimal)
        {
            if (valor is null)
            {
                valor = 0;
            }
            string resp = string.Empty;
            try
            {
                resp = Strings.FormatNumber(valor, DigitoDecimal);
                if ((System.Convert.ToDecimal(123.456).ToString()).Contains(","))
                {
                    resp = resp.Replace(".", "|");
                    resp = resp.Replace(",", ".");
                    resp = resp.Replace("|", ",");
                }
            }
            catch (Exception ex)
            {
            }
            return resp;
        }

        public static string DepuraStrConvertNum(string cadena)
        {
            if (string.IsNullOrEmpty(cadena))
                cadena = "0";
            if (cadena == "undefined")
                cadena = "0";
            cadena = cadena.Replace("$", "").Replace(" ", "").Replace("%", "").Replace(",", "").Replace(".", ",");
            if (cadena.Trim() == "")
                cadena = "0";
            return cadena;
        }

        public static string ToTimeSpanString(this TimeSpan? obj)
        {
            return (obj.HasValue ? obj : new TimeSpan()).ToString();
        }

        public static string ToTimeSpanString(this TimeSpan obj)
        {
            return (obj).ToString();
        }

        public static TimeSpan FromTimeSpanString(this string obj)
        {
            TimeSpan ts = new TimeSpan();

            if (!string.IsNullOrEmpty(obj))
            {
                return TimeSpan.Parse(obj);
            }

            return ts;
        }

        public static string ToReadableString(this TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} día{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hora{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minuto{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} segundo{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 segundos";

            return formatted;
        }

        public static string ToReadableString(this string ts)
        {
            TimeSpan span = ts.FromTimeSpanString();
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} día{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hora{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minuto{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} segundo{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 segundos";

            return formatted;
        }

        public static string ConcatenarValores(this string[] valores)
        {
            if (valores is null) return "";

            int cont = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var item in valores)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    if (cont == 0)
                    {
                        sb.Append(item);
                    }
                    else
                    {
                        sb.Append(";");
                        sb.Append(item);
                    }
                    cont++;
                }
            }

            return sb.ToString();
        }
    }
}
