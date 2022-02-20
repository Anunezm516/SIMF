using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public sealed class Factura
    {
        [Key]
        public long FacturaId { get; set; }

        public long IdUsuario { get; set; }
        public long ClienteId { get; set; }

        public string NumeroDocumento { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaEmision { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string Ip { get; set; }
        public int Estado { get; set; }


    }
}
