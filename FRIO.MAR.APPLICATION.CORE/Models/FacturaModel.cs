using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Models
{
    public class FacturaModel
    {
        public string Id { get; set; }
        public string Fecha { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public ClienteFacturaModel Cliente { get; set; }
        public long SucursalId { get; set; }
        public long BodegaId { get; set; }
        public List<DetalleFacturaModel> Detalle { get; set; }
        public List<FormaPagoFacturaModel> FormaPago { get; set; }
        public List<AdjuntoDto> Adjunto { get; set; }
        public TotalesFacturaModel Totales { get; set; }
        public EstadoFactura EstadoFactura { get; set; }
        public long Secuencial { get; set; }
        public string NumeroDocumento { get; set; }

        public FacturaModel()
        {
            FechaEmision = Utilidades.GetHoraActual();
            Fecha = FechaEmision.ToString();
        }

        public FacturaModel(Factura factura)
        {
            Id = Crypto.CifrarId(factura.FacturaId);
            Fecha = factura.FechaEmision.HasValue ? factura.FechaEmision.ToString() : "";
            FechaEmision = factura.FechaEmision.HasValue ? factura.FechaEmision.Value : Utilidades.GetHoraActual();
            Cliente = new ClienteFacturaModel
            {
                ClienteId = factura.ClienteId,
                CorreoCliente = factura.CorreoElectronico,
                Identificacion = factura.Identificacion,
                NombreComercial = factura.NombreComercial,
                RazonSocial = factura.RazonSocial,
                Telefono = factura.Telefono
            };
            
            SucursalId = factura.SucursalId;
            Detalle = factura.FacturaDetalle.Select(c => new DetalleFacturaModel(c)).ToList();
            FormaPago = factura.FacturaFormaPago.Select(c => new FormaPagoFacturaModel(c)).ToList();
            Adjunto = factura.FacturaAdjunto.Select(c => new AdjuntoDto { Identificador = c.ImagenBase64, Nombre = c.Nombre, Ruta = c.Ruta }).ToList();
            Totales = new TotalesFacturaModel(Detalle, FormaPago);
            EstadoFactura = factura.Estado;
            NumeroDocumento = factura.NumeroDocumento;
            Secuencial = factura.Secuencial;
            FechaEntrega = factura.FechaEntrega;
        }

        public FacturaModel(CFactura factura)
        {
            Id = Crypto.CifrarId(factura.FacturaId);
            Fecha = factura.FechaEmision.HasValue ? factura.FechaEmision.ToString() : "";
            FechaEmision = factura.FechaEmision.HasValue ? factura.FechaEmision.Value : Utilidades.GetHoraActual();
            Cliente = new ClienteFacturaModel
            {
                ClienteId = factura.ProveedorId,
                CorreoCliente = factura.CorreoElectronico,
                Identificacion = factura.Identificacion,
                NombreComercial = factura.NombreComercial,
                RazonSocial = factura.RazonSocial,
                Telefono = factura.Telefono
            };

            SucursalId = factura.SucursalId;
            Detalle = factura.FacturaDetalle.Select(c => new DetalleFacturaModel(c)).ToList();
            FormaPago = factura.FacturaFormaPago.Select(c => new FormaPagoFacturaModel(c)).ToList();
            Adjunto = factura.FacturaAdjunto.Select(c => new AdjuntoDto { Identificador = c.ImagenBase64, Nombre = c.Nombre, Ruta = c.Ruta }).ToList();
            Totales = new TotalesFacturaModel(Detalle, FormaPago);
            EstadoFactura = factura.Estado;
            NumeroDocumento = factura.NumeroDocumento;
        }

        public string Ip { get; set; }
        public long Usuario { get; set; }

    }

    public class ClienteFacturaModel
    {
        public long ClienteId { get; set; }
        public string CorreoCliente { get; set; }
        public string Identificacion { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Telefono { get; set; }
    }

    public class DetalleFacturaModel
    {
        public string Id { get; set; }

        public long ProductoId { get; set; }
        public string Codigo { get; set; }
        public string CodigoSeguimiento { get; set; }
        public string Descripcion { get; set; }

        public decimal PrecioUnitarioDec { get; set; }
        public string PrecioUnitario { get; set; }

        public decimal CantidadDec { get; set; }
        public string Cantidad { get; set; }

        public decimal IvaValorDec { get; set; }
        public string IvaValor { get; set; }

        public decimal IvaPorcentajeDec { get; set; }
        public string IvaPorcentaje { get; set; }

        public decimal SubTotalDec { get; set; }
        public string SubTotal { get; set; }

        public decimal TotalDec { get; set; }
        public string Total { get; set; }
        public int MesesGarantia { get; set; }
        public long BodegaId { get; set; }
        public long SucursalId { get; set; }
        public string UnidadMedida { get; set; }
        public TipoProducto TipoProducto { get; set; }
        public long ProductoClienteId { get; set; }

        public DetalleFacturaModel()
        {

        }

        public DetalleFacturaModel(FacturaDetalle detalle)
        {
            Id = Guid.NewGuid().ToString();
            ProductoId = detalle.ProductoId;

            Codigo = detalle.Codigo;
            CodigoSeguimiento = detalle.CodigoSeguimiento;
            Descripcion = detalle.Descripcion;

            PrecioUnitario = Utilities.Utilidades.DoubleToString_FrontCO(detalle.PrecioUnitario, 2);
            PrecioUnitarioDec = detalle.PrecioUnitario;

            Cantidad = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Cantidad, 2);
            CantidadDec = detalle.Cantidad;

            IvaValor = Utilities.Utilidades.DoubleToString_FrontCO(detalle.IvaValor, 2);
            IvaValorDec = detalle.IvaValor;

            IvaPorcentaje = Utilities.Utilidades.DoubleToString_FrontCO(detalle.IvaPorcentaje, 2);
            IvaPorcentajeDec = detalle.IvaPorcentaje;

            SubTotal = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Subtotal, 2);
            SubTotalDec = detalle.Subtotal;

            Total = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Total, 2);
            TotalDec = detalle.Total;

            MesesGarantia = detalle.MesesGarantia;
            BodegaId = detalle.BodegaId;
            SucursalId = detalle.SucursalId;
            UnidadMedida = detalle.UnidadMedida;
            TipoProducto = detalle.TipoProducto;
            ProductoClienteId = detalle.ProductoClienteId;
        }

        public DetalleFacturaModel(CFacturaDetalle detalle)
        {
            Id = Guid.NewGuid().ToString();
            ProductoId = detalle.ProductoId;

            Codigo = detalle.Codigo;
            CodigoSeguimiento = detalle.CodigoSeguimiento;
            Descripcion = detalle.Descripcion;

            PrecioUnitario = Utilities.Utilidades.DoubleToString_FrontCO(detalle.PrecioUnitario, 2);
            PrecioUnitarioDec = detalle.PrecioUnitario;

            Cantidad = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Cantidad, 2);
            CantidadDec = detalle.Cantidad;

            IvaValor = Utilities.Utilidades.DoubleToString_FrontCO(detalle.IvaValor, 2);
            IvaValorDec = detalle.IvaValor;

            IvaPorcentaje = Utilities.Utilidades.DoubleToString_FrontCO(detalle.IvaPorcentaje, 2);
            IvaPorcentajeDec = detalle.IvaPorcentaje;

            SubTotal = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Subtotal, 2);
            SubTotalDec = detalle.Subtotal;

            Total = Utilities.Utilidades.DoubleToString_FrontCO(detalle.Total, 2);
            TotalDec = detalle.Total;

            TipoProducto = detalle.TipoProducto;

        }
    }

    public class FormaPagoFacturaModel
    {
        public string Id { get; set; }
        public long FormaPagoId { get; set; }
        public string FormaPagoCodigo { get; set; }
        public string FormaPagoDescripcion { get; set; }
        public string Valor { get; set; }
        public decimal ValorDec { get; set; }
        public string Observacion { get; set; }

        public FormaPagoFacturaModel()
        {

        }

        public FormaPagoFacturaModel(FacturaFormaPago facturaFormaPago)
        {
            Id = Guid.NewGuid().ToString();
            FormaPagoId = facturaFormaPago.FormaPagoId;
            FormaPagoCodigo = facturaFormaPago.CodigoFormaPago;
            FormaPagoDescripcion = facturaFormaPago.DescripcionFormaPago;
            Valor = Utilidades.DoubleToString_FrontCO(facturaFormaPago.Valor, 2);
            ValorDec = facturaFormaPago.Valor;
            Observacion = facturaFormaPago.Observacion;
        }
        
        public FormaPagoFacturaModel(CFacturaFormaPago facturaFormaPago)
        {
            Id = Guid.NewGuid().ToString();
            FormaPagoId = facturaFormaPago.FormaPagoId;
            FormaPagoCodigo = facturaFormaPago.CodigoFormaPago;
            FormaPagoDescripcion = facturaFormaPago.DescripcionFormaPago;
            Valor = Utilidades.DoubleToString_FrontCO(facturaFormaPago.Valor, 2);
            ValorDec = facturaFormaPago.Valor;
            Observacion = facturaFormaPago.Observacion;
        }

    }

    public class TotalesFacturaModel
    {
        public TotalesFacturaModel(List<DetalleFacturaModel> detalles, List<FormaPagoFacturaModel> formasPago)
        {
            Subtotal = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.SubTotalDec), 2);
            ImporteIva = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.IvaValorDec), 2);
            TotalAbono = Utilidades.DoubleToString_FrontCO(formasPago.Sum(x => x.ValorDec), 2);
            Total = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.TotalDec), 2);
        }

        public TotalesFacturaModel()
        {

        }

        public string Subtotal { get; set; }
        public string ImporteIva { get; set; }
        public string TotalAbono { get; set; }
        public string Total { get; set; }
    }
}
