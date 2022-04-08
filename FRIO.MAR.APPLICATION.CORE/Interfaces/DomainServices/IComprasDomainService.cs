using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IComprasDomainService
    {
        MethodResponseDto ListarFacturas(EstadoFactura[] Estados);
        MethodResponseDto ConsultarFactura(long Id, EstadoFactura Estado);
        MethodResponseDto GuardarFactura(FacturaModel model);
        MethodResponseDto EliminarFactura(long Id);
        MethodResponseDto ValidarFactura(FacturaModel model);
        MethodResponseDto DescargarAdjuntos(long FacturaId);
    }
}
