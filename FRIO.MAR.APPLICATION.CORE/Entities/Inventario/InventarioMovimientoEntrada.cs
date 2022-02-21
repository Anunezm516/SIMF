using System;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class InventarioMovimientoEntrada : Auditoria
    {
        public long IdInventarioMovimiento { get; set; }
        public long? IdProducto { get; set; }
        public long? IdInventarioBodega { get; set; }
        public long? IdSucursal { get; set; }
        public long? IdProveedor { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public string NumeroFactura { get; set; }
        public string Cufe { get; set; }
        public string Motivo { get; set; }
        public string CodigoTransferencia { get; set; }
        public int? TipoInventario { get; set; }
        public int? TipoMovimiento { get; set; }
        

        public virtual Bodega Bodega { get; set; } 
        public virtual Producto IdProductoNavigation { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
