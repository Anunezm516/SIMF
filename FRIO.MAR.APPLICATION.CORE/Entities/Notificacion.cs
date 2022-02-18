using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class Notificacion
    {
        public long IdNotificacion { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public int TipoNotificacion { get; set; }
        public int TipoCriticidad { get; set; }
        public bool EsNotificacionLeida { get; set; }
        public DateTime? FechaNotificacionLeida { get; set; }
        public long IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int Estado { get; set; }
    }
}
