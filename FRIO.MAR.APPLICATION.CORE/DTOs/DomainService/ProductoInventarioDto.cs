using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.DomainService
{
    public class ProductoInventarioDto
    {
        public string Stock { get; set; }
        public decimal StockDec { get; set; }
        public string NombreProducto { get; set; }
        public Producto Producto { get; set; }
        public Sucursal Sucursal { get; set; }
        public Bodega Bodega { get; set; }

        public ProductoInventarioDto(Producto producto, Sucursal sucursal, Bodega bodega, decimal stockDec)
        {
            Producto = producto;
            Sucursal = sucursal;
            Bodega = bodega;

            if (Sucursal != null && Bodega != null)
            {
                NombreProducto = $"{Sucursal.Nombre} - {Bodega.Nombre} - {Producto.Codigo} - {Producto.Descripcion}";
            }
            else
            {
                NombreProducto = $"{Producto.Codigo} - {Producto.Descripcion}";
            }

            StockDec = stockDec;
            Stock = Utilidades.DoubleToString_FrontCO(StockDec, 2);

            Sucursal.Bodega = null;
            Bodega.SucursalBodega = null;
        }
    }
}
