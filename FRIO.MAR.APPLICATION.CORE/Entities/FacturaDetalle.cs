using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class FacturaDetalle
    {
        [Key]
        public long FacturaDetalleId { get; set; }

        public long ProductoId { get; set; }
        public long SucursalId { get; set; }
        public long BodegaId { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string Codigo { get; set; }

        [Column(TypeName = "varchar(30)")]
        public string CodigoSeguimiento { get; set; }

        [Column(TypeName = "varchar(300)")]
        public string Descripcion { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal PrecioUnitario { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Cantidad { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal IvaPorcentaje { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal IvaValor { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,6)")]
        public decimal Total { get; set; }

        public int MesesGarantia { get; set; }
        public virtual Factura Factura { get; set; }
    }
}
