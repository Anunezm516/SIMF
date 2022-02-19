﻿using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Auditoria
    {
        public string Ip { get; set; }
        public long UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool Estado { get; set; }
    }
}
