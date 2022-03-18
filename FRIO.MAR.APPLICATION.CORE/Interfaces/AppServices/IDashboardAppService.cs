using FRIO.MAR.APPLICATION.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IDashboardAppService
    {
        MethodResponseDto GetGraficoLineaComportamientoLineaVentasCompras(int anio);
    }
}
