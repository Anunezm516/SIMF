using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IVentasDomainService
    {
        MethodResponseDto ListarFacturas(EstadoFactura[] Estados);
        MethodResponseDto ConsultarFactura(long Id, EstadoFactura Estado);
    }
}
