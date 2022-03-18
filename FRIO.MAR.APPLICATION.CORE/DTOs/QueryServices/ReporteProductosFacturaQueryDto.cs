using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices
{
    public class ReporteProductosFacturaQueryDto
    {
        public string Id { get; set; }
        public string Fecha { get; set; }
        public string Identificacion { get; set; }
        public string Adquiriente { get; set; }
        public string NumeroDocumento { get; set; }
        public string ProductoCodigoSeguimiento { get; set; }
        public string ProductoCodigo { get; set; }
        public string ProductoNombre { get; set; }
        public string ProductoClienteCodigo { get; set; }
        public string ProductoClienteNombre { get; set; }
        public int MesesGarantia { get; set; }

        public ReporteProductosFacturaQueryDto(Factura factura, FacturaDetalle facturaDetalle, ProductoCliente productoCliente, Producto producto)
        {
            Id = Crypto.CifrarId(factura.FacturaId);
            Fecha = factura.FechaModificacion.ToString("yyyy-MM-dd HH:mm:ss");
            Identificacion = factura.Identificacion;
            Adquiriente = factura.RazonSocial;
            NumeroDocumento = factura.NumeroDocumento;
            ProductoClienteCodigo = productoCliente.Codigo;
            ProductoClienteNombre = productoCliente.Nombre;
            ProductoCodigoSeguimiento = facturaDetalle.CodigoSeguimiento;
            ProductoCodigo = producto.Codigo;
            ProductoNombre = producto.Descripcion;
            MesesGarantia = facturaDetalle.MesesGarantia;
        }
    }
}
