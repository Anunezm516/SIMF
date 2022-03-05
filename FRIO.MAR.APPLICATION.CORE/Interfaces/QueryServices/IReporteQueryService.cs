using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices
{
    public interface IReporteQueryService
    {
        List<VentasDomainServiceResultDto> GetFacturas(EstadoFactura estadoFactura, DateTime fechaInicio, DateTime fechaFin);
    }
}
