using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class CFacturaFormaPago
    {
        [Key]
        public long FacturaFormaPagoId { get; set; }

        public long FormaPagoId { get; set; }

        [Column(TypeName = "varchar(20)")]
        public string CodigoFormaPago { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string DescripcionFormaPago { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Valor { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Observacion { get; set; }
        public virtual CFactura Factura { get; set; }
    }
}
