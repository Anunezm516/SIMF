using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
   public class InventarioViewModels
    {

    }

    public class ListaInventarioViewModel
    {
        public ListaInventarioViewModel()
        {
             ListaInventario = new List<ItemInventarioViewModel>();
        }
        public List<ItemInventarioViewModel> ListaInventario { get; set; }
        public List<ItemInventarioProvedorViewModel> ListaInventarioProvedor { get; set; }

        public bool Descontar { get; set; }
        public bool ControlSucursal { get; set; }
        public bool ControlEmision { get; set; }
    }

    public class ItemInventarioViewModel
    {
        public ItemInventarioViewModel(InventarioVenta Inventario)
        {
            this.UnidadMedida = Inventario.UnidadMedida;
            this.CantidadDescripcion = Inventario.CantidadDescripcion;
            this.Producto = Inventario.IdProductoNavigation?.Descripcion ?? "";
            this.StockActual = Inventario.StockActual;
            this.IdInventario = Inventario.IdInventarioVenta;
            this.IdProducto = Inventario.IdProductoNavigation.ProductoId;
            BodegaCodigo =/* (Inventario.IdInventarioBodegaNavigation?.Codigo ?? "") + "-" +*/ (Inventario.Bodega?.Nombre ?? "");
            PrecioUnitario = Inventario.IdProductoNavigation.PrecioUnitario;
            Sucursal = Inventario.IdSucursalNavigation?.Nombre ?? "";
            IdBodega = Inventario.IdInventarioBodega ?? 0;
            IdSucursal = Inventario.IdSucursal ?? 0;

        }
        public string UnidadMedida { get; set; }
        public string CantidadDescripcion { get; set; }
        public decimal? StockActual { get; set; }
        public string Producto { get; set; }
        public long IdInventario { get; set; }
        public long IdProducto { get; set; }
        public string BodegaCodigo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public long IdSucursal { get; set; }
        public long IdBodega { get; set; }
        public string Sucursal { get; set; }

    }

    public class ListaInventarioProveedorViewModel
    {
        public ListaInventarioProveedorViewModel()
        {
            ListaInventarioProvedor = new List<ItemInventarioProvedorViewModel>();
        }
        public List<ItemInventarioProvedorViewModel> ListaInventarioProvedor { get; set; }
        public string ProductoNombre { get; set; }

    }

    public class ItemInventarioProvedorViewModel
    {
        public ItemInventarioProvedorViewModel(InventarioProveedor Inventario)
        {
            this.UnidadMedida = Inventario.UnidadMedida;
            this.CantidadDescripcion = Inventario.CantidadDescripcion;
            this.Producto = Inventario.IdProductoNavigation?.Descripcion ?? "";
            this.StockActual = Inventario.StockActual;
            this.IdInventario = Inventario.IdInventarioProveedor;
            this.IdProducto = Inventario.IdProductoNavigation.ProductoId;
            BodegaCodigo = (Inventario.Bodega?.Codigo ?? "") + "-" + (Inventario.Bodega?.Nombre ?? "");
            PrecioUnitario = Inventario.IdProductoNavigation.PrecioUnitario;
            Sucursal = Inventario.IdSucursalNavigation?.Nombre ?? "";
            IdBodega = Inventario.IdInventarioBodega ?? 0;
            IdSucursal = Inventario.IdSucursal ?? 0;

        }
        public string UnidadMedida { get; set; }
        public string CantidadDescripcion { get; set; }
        public decimal? StockActual { get; set; }
        public string Producto { get; set; }
        public long IdInventario { get; set; }
        public long IdProducto { get; set; }
        public string BodegaCodigo { get; set; }
        public decimal PrecioUnitario { get; set; }
        public long IdSucursal { get; set; }
        public long IdBodega { get; set; }
        public string Sucursal { get; set; }
    }

}
