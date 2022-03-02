using FRIO.MAR.APPLICATION.CORE.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Factura
    {
        [Key]
        public long FacturaId { get; set; }

        public long IdUsuario { get; set; }
        public long ClienteId { get; set; }
        public long SucursalId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string NumeroDocumento { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Identificacion { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string RazonSocial { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string NombreComercial { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string CorreoElectronico { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime FechaModificacion { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal ValorTotal { get; set; }


        public DateTime FechaCreacion { get; set; }

        [Column(TypeName = "varchar(25)")] 
        public string Ip { get; set; }
        public EstadoFactura Estado { get; set; }

        public virtual ICollection<FacturaDetalle> FacturaDetalle { get; set; }
        public virtual ICollection<FacturaFormaPago> FacturaFormaPago { get; set; }

    }
}
