using FRIO.MAR.APPLICATION.CORE.Constants;
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
        public string Fecha { get; set; }
        public long ClienteId { get; set; }
        public long SucursalId { get; set; }
        public List<DetalleFacturaModel> Detalle { get; set; }
        public List<FormaPagoFacturaModel> FormaPago { get; set; }
        public TotalesFacturaModel Totales { get; set; }
        public EstadoFactura EstadoFactura { get; set; }

        public FacturaModel()
        {

        }

        public FacturaModel(Factura factura)
        {
            //Fecha = factura.FechaEmision.Value
            ClienteId = factura.ClienteId;
            SucursalId = factura.SucursalId;
            Detalle = factura.FacturaDetalle.Select(c => new DetalleFacturaModel(c)).ToList();
            Totales = new TotalesFacturaModel(Detalle);
            EstadoFactura = factura.Estado;
        }

    }

    public class DetalleFacturaModel
    {
        public string Id { get; set; }
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

        public DetalleFacturaModel()
        {

        }

        public DetalleFacturaModel(FacturaDetalle detalle)
        {
            Id = Guid.NewGuid().ToString();
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
        }
    }

    public class FormaPagoFacturaModel
    {
        public string Id { get; set; }
        public string FormaPago { get; set; }
        public string Valor { get; set; }
        public string Descripcion { get; set; }
    }

    public class TotalesFacturaModel
    {
        public TotalesFacturaModel(List<DetalleFacturaModel> detalles)
        {
            Subtotal = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.SubTotalDec), 2);
            ImporteIva = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.IvaValorDec), 2);
            //TotalAbono = Utilidades.DoubleToString_FrontCO(detalles.Sum(x => x.IvaValorDec), 2),
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
