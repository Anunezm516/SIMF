using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
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
            List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
            using var context = new SIFMContext(GlobalSettings.ConnectionString);

            return (from factura in context.Set<Factura>().AsNoTracking()
                          where (estadoFactura == EstadoFactura.Todos || factura.Estado == estadoFactura)
                          && (factura.FechaEmision >= fechaInicio && factura.FechaEmision <= fechaFin)
                          let fac = factura
                          select new VentasDomainServiceResultDto(fac)
                          ).ToList();
            /*
            return context.Set<Factura>().AsNoTracking().Where(x => (estadoFactura == EstadoFactura.Todos || x.Estado == estadoFactura) 
                        && (x.FechaEmision >= fechaInicio && x.FechaEmision <= fechaFin)).ToList();
            */
        }

        public List<ComprasDomainServiceResultDto> GetFacturasCompras(long ProveedorId, DateTime fechaInicio, DateTime fechaFin)
        {
            List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
            using var context = new SIFMContext(GlobalSettings.ConnectionString);

            return (from factura in context.Set<CFactura>().AsNoTracking()
                    where 
                        factura.Estado == EstadoFactura.Facturado 
                        && (ProveedorId == 99 || factura.ProveedorId == ProveedorId)
                        && (factura.FechaEmision >= fechaInicio && factura.FechaEmision <= fechaFin)
                        let fac = factura
                    select new ComprasDomainServiceResultDto(fac)
                    ).ToList();
            /*
            return context.Set<Factura>().AsNoTracking().Where(x => (estadoFactura == EstadoFactura.Todos || x.Estado == estadoFactura) 
                        && (x.FechaEmision >= fechaInicio && x.FechaEmision <= fechaFin)).ToList();
            */
        }
    }
}
