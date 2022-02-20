using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IBodegaAppService
    {
        MethodResponseDto ConsultarBodegas();
        MethodResponseDto ConsultarBodega(string ID);
        MethodResponseDto CrearBodega(BodegaModel model);
        MethodResponseDto EditarBodega(BodegaModel model);
        MethodResponseDto EliminarBodega(string ID, string Ip, long Usuario);

    }
}
