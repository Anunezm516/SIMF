using System;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class InventarioVenta : Auditoria
    {
        public long IdInventarioVenta { get; set; }
        public long? IdProducto { get; set; }
        public long? IdInventarioBodega { get; set; }
        public string UnidadMedida { get; set; }
        public string CantidadDescripcion { get; set; }
        public decimal? StockActual { get; set; }
        public long? IdSucursal { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
        public virtual Bodega Bodega { get; set; }
        public virtual Producto IdProductoNavigation { get; set; }
    }
}
