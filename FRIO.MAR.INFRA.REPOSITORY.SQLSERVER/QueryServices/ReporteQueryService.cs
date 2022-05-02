using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.QueryServices
{
    public class ReporteQueryService : IReporteQueryService
    {
        public List<VentasDomainServiceResultDto> GetFacturasVentas(EstadoFactura estadoFactura, DateTime fechaInicio, DateTime fechaFin)
        {
            fechaFin = fechaFin.AddDays(1);
            List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
            using var context = new SIFMContext(GlobalSettings.ConnectionString);

            //return (from factura in context.Set<Factura>().AsNoTracking()
            //        join adjuntos in context.FacturaAdjunto on factura.FacturaId equals adjuntos.FacturaId
            //        where factura.Estado != EstadoFactura.Eliminado && factura.Estado != EstadoFactura.Borrador
            //        where (estadoFactura == EstadoFactura.Todos || factura.Estado == estadoFactura)
            //              && (factura.FechaEmision >= fechaInicio && factura.FechaEmision <= fechaFin)
            //              let fac = factura
            //              select new VentasDomainServiceResultDto(fac, adjuntos)
            //              ).ToList();
            
            return context.Set<Factura>().AsNoTracking()
                .Include(x => x.FacturaAdjunto)
                .Where(x => 
                (x.Estado != EstadoFactura.Eliminado && x.Estado != EstadoFactura.Borrador) &&
                (estadoFactura == EstadoFactura.Todos || x.Estado == estadoFactura) &&
                 (x.FechaEmision >= fechaInicio && x.FechaEmision <= fechaFin))
                .Select(c => new VentasDomainServiceResultDto(c))
                .ToList();
            
        }

        public List<ComprasDomainServiceResultDto> GetFacturasCompras(long ProveedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            fechaFin = fechaFin.AddDays(1);
            List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
            using var context = new SIFMContext(GlobalSettings.ConnectionString);

            return context.CFactura.Include(x => x.FacturaAdjunto)
                .Where(x =>
                    x.Estado == EstadoFactura.Facturado
                        && (ProveedorId == 99 || x.ProveedorId == ProveedorId)
                        && (x.FechaEmision >= fechaInicio && x.FechaEmision <= fechaFin)
                )
                .Select(c => new ComprasDomainServiceResultDto(c))
                .ToList();
            //return (from factura in context.Set<CFactura>().AsNoTracking()
            //        join adjuntos in context.FacturaAdjunto on factura.FacturaId equals adjuntos.FacturaId
            //        where 
            //            factura.Estado == EstadoFactura.Facturado 
            //            && (ProveedorId == 99 || factura.ProveedorId == ProveedorId)
            //            && (factura.FechaEmision >= fechaInicio && factura.FechaEmision <= fechaFin)
            //            let fac = factura
            //        select new ComprasDomainServiceResultDto(fac)
            //        ).ToList();
        }

        public List<ReporteProductosFacturaQueryDto> GetProductosFactura(long ClienteId, DateTime fechaInicio, DateTime fechaFin)
        {
            fechaFin = fechaFin.AddDays(1);

            using var context = new SIFMContext(GlobalSettings.ConnectionString);

            List<ReporteProductosFacturaQueryDto> results = new List<ReporteProductosFacturaQueryDto>();

            //var r1 = context.Factura.Include(x => x.FacturaDetalle)
            //    .Where(x => x.Estado == EstadoFactura.Facturado && (x.FechaEmision >= fechaInicio && x.FechaEmision <= fechaFin)).Select(c => c.FacturaDetalle).ToList();

            var r = (from factura in context.Set<Factura>().AsNoTracking()
                     join detalle in context.Set<FacturaDetalle>().AsNoTracking() on factura.FacturaId equals detalle.FacturaId
                     join productoCliente in context.Set<ProductoCliente>().AsNoTracking() on detalle.ProductoClienteId equals productoCliente.ProductoClienteId
                     join producto in context.Set<Producto>().AsNoTracking() on detalle.ProductoId equals producto.ProductoId
                     where factura.Estado == EstadoFactura.Facturado
                          && (factura.FechaEmision >= fechaInicio && factura.FechaEmision <= fechaFin)
                          && factura.ClienteId == ClienteId
                     let fac = factura
                     let det = detalle
                     let proCli = productoCliente
                     let pro = producto
                     select new ReporteProductosFacturaQueryDto(fac, det, proCli, pro)
                          ).ToList();

            return r;
        }
    }
}
