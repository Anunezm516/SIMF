using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices.ApexCharts;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class DashboardAppService : IDashboardAppService
    {
        private readonly IVentasRepository _ventasRepository;
        private readonly IComprasRepository _comprasRepository;

        public DashboardAppService(IVentasRepository ventasRepository, IComprasRepository comprasRepository)
        {
            _ventasRepository = ventasRepository;
            _comprasRepository = comprasRepository;
        }

        public MethodResponseDto GetGraficoLineaComportamientoLineaVentasCompras(int anio)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var ventas = _ventasRepository.Find(x => x.Estado == Constants.EstadoFactura.Facturado && x.FechaEmision.Value.Year == anio);
                var compras = _comprasRepository.Find(x => x.Estado == Constants.EstadoFactura.Facturado && x.FechaEmision.Value.Year == anio);

                List<GraficoMes> grupoFacturasPorMesVentas = ventas
                    .GroupBy(x => new
                    {
                        Mes = x.FechaEmision.Value.Month // se realiza la agrupacion por mes
                    })
                    .Select(g => new GraficoMes
                    {
                        NombreCorto = string.Empty,
                        NumeroMes = g.Key.Mes,
                        ValorTotalDecimal = g.Sum(x => x.ValorTotal)
                    }).ToList();

                List<GraficoMes> grupoFacturasPorMesCompras = compras
                    .GroupBy(x => new
                    {
                        Mes = x.FechaEmision.Value.Month // se realiza la agrupacion por mes
                    })
                    .Select(g => new GraficoMes
                    {
                        NombreCorto = string.Empty,
                        NumeroMes = g.Key.Mes,
                        ValorTotalDecimal = g.Sum(x => x.ValorTotal)
                    }).ToList();

                var dataJoinGraficoMesFacturaVentas = new JoinGraficoMes().LeftJoinDecimal(grupoFacturasPorMesVentas);
                var dataJoinGraficoMesNoDebitosCompras = new JoinGraficoMes().LeftJoinDecimal(grupoFacturasPorMesCompras);

                List<SerieApexChart> dataGrafico = new List<SerieApexChart>();

                var dataGraficoFacturasVentas = new SerieApexChart();
                dataGraficoFacturasVentas.Name = "Ventas";
                dataGraficoFacturasVentas.Data = dataJoinGraficoMesFacturaVentas.Select(x => x.ValorTotalDecimal).ToList();
                dataGrafico.Add(dataGraficoFacturasVentas);

                var dataGraficoNotasCreditosCompras = new SerieApexChart();
                dataGraficoNotasCreditosCompras.Name = "Compras";
                dataGraficoNotasCreditosCompras.Data = dataJoinGraficoMesNoDebitosCompras.Select(x => x.ValorTotalDecimal).ToList();
                dataGrafico.Add(dataGraficoNotasCreditosCompras);

                var categories = dataJoinGraficoMesFacturaVentas.Select(x => x.NombreCorto).ToList();

                var xaxis = new XAxisApexChart
                {
                    Categories = categories
                };

                var response = new GraficoDTO
                {
                    Series = dataGrafico,
                    Xaxis = xaxis
                };

                responseDto.Data = response;
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
    }
}
