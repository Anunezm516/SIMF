using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities.StoreProcedures
{
    public partial class SPUsuarios
    {
        //public SPUsuarios(Spusuario usr)
        //{
        //    IdUsuario = usr.IdUsuario;
        //    Usuario = usr.Usuario;
        //    Nombre = usr.Nombre;
        //    Apellido = usr.Apellido;
        //    CorreoElectronico = usr.CorreoElectronico;
        //    Telefono = usr.Telefono;
        //    IdTelegram = usr.IdTelegram;
        //}

        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string IdTelegram { get; set; }
    }
}
