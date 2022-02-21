using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Entities
{
    public class Sucursal : Auditoria
    {
        public Sucursal()
        {
            SucursalBodega = new HashSet<SucursalBodega>();
            Bodega = new HashSet<Bodega>();
            InventarioMovimientoEntrada = new HashSet<InventarioMovimientoEntrada>();
            InventarioMovimientoSalida = new HashSet<InventarioMovimientoSalida>();
            //InventarioBodegaSucursales = new HashSet<InventarioBodegaSucursales>();
            InventarioProveedor = new HashSet<InventarioProveedor>();
            InventarioVenta = new HashSet<InventarioVenta>();
        }

        [Key]
        public long SucursalId { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        public virtual ICollection<SucursalBodega> SucursalBodega { get; set; }
        public virtual ICollection<Bodega> Bodega { get; set; }
        public virtual ICollection<InventarioMovimientoEntrada> InventarioMovimientoEntrada { get; set; }
        public virtual ICollection<InventarioMovimientoSalida> InventarioMovimientoSalida { get; set; }
        //public virtual ICollection<InventarioBodegaSucursales> InventarioBodegaSucursales { get; set; }
        public virtual ICollection<InventarioProveedor> InventarioProveedor { get; set; }
        public virtual ICollection<InventarioVenta> InventarioVenta { get; set; }
    }
}
