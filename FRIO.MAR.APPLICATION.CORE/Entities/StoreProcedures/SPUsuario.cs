using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities.StoreProcedures
{
    public abstract class SPUsuario
    {
        internal SPUsuario()
        {
        }

        public long IdUsuario { get; set; }
        public long IdCompania { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioAuditoria { get; set; }

        public static bool ValidarCorreo(string Correo, ref string mensaje)
        {
            try
            {
                var result = new System.Net.Mail.MailAddress(Correo);
                return true;
            }
            catch (Exception)
            {
                mensaje = "Correo electrónico no válido";
                return false;
            }
        }

        
    }
}
