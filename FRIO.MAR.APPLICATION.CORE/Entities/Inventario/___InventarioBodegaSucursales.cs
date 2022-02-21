using System;
using System.Collections.Generic;


namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class __InventarioBodegaSucursales
    {
        public long IdInventarioBadegaSucursales { get; set; }
        public long? IdBodega { get; set; }
        public int? IdSucursal { get; set; }
        public long? IdCompania { get; set; }
        public int? Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public long IdUsuarioCreacion { get; set; }
        public long? IdUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? IdUsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public virtual Bodega Bodega { get; set; }
        public virtual Sucursal IdSucursalNavigation { get; set; }
    }
}
