using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class Usuario
    {
        public Usuario()
        {
            SpusuarioRol = new HashSet<UsuarioRol>();
        }

        public long IdUsuario { get; set; }
        public string Username { get; set; }
        public string CorreoElectronico { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string IdTelegram { get; set; }
        public DateTime? FechaUltimaConexion { get; set; }
        public int? IntentosFallidos { get; set; }
        public DateTime? FechaActualizarPassword { get; set; }
        public string Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public int? Estado { get; set; }
        public bool? Bloqueado { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UsuarioRol> SpusuarioRol { get; set; }
    }
}
