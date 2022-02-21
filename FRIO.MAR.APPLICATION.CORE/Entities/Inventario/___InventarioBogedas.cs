using System;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public partial class __InventarioBogedas
    {
        //public __InventarioBogedas()
        //{
        //    InventarioMovimientoEntrada = new HashSet<InventarioMovimientoEntrada>();
        //    InventarioMovimientoSalida = new HashSet<InventarioMovimientoSalida>();
        //    InventarioProveedor = new HashSet<InventarioProveedor>();
        //    InventarioVenta = new HashSet<InventarioVenta>(); 
        //    InventarioBodegaSucursales = new HashSet<InventarioBodegaSucursales>();
        //}
       
        public long IdInventarioBodega { get; set; }
        public int? IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public long? IdCompania { get; set; }
        public string Ip { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool? Estado { get; set; }

        public virtual Sucursal IdSucursalNavigation { get; set; }
        public virtual ICollection<InventarioMovimientoEntrada> InventarioMovimientoEntrada { get; set; }
        public virtual ICollection<InventarioMovimientoSalida> InventarioMovimientoSalida { get; set; }
        public virtual ICollection<InventarioProveedor> InventarioProveedor { get; set; }
        public virtual ICollection<InventarioVenta> InventarioVenta { get; set; }
        //public virtual ICollection<InventarioBodegaSucursales> InventarioBodegaSucursales { get; set; }
    }
}
