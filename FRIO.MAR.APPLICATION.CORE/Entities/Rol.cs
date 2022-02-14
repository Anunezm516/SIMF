﻿using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class Rol
    {
        public Rol()
        {
            SprolPermiso = new HashSet<RolPermiso>();
            SpusuarioRol = new HashSet<UsuarioRol>();
        }

        public long IdRol { get; set; }
        public string Nombre { get; set; }
        public string Ip { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<RolPermiso> SprolPermiso { get; set; }
        public virtual ICollection<UsuarioRol> SpusuarioRol { get; set; }
    }
}
