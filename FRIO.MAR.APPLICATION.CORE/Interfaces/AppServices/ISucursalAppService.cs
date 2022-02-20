using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface ISucursalAppService
    {
        MethodResponseDto ConsultarSucursales();
        MethodResponseDto ConsultarSucursal(string ID);
        MethodResponseDto CrearSucursal(SucursalModel model);
        MethodResponseDto EditarSucursal(SucursalModel model);
        MethodResponseDto EliminarSucursal(string ID, string Ip, long Usuario);
    }
}
