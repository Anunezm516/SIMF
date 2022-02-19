using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IProveedorAppService
    {
        MethodResponseDto ConsultarProveedores();
        MethodResponseDto ConsultarProveedor(string ID);
        MethodResponseDto CrearProveedor(ProveedorModel model);
        MethodResponseDto EditarProveedor(ProveedorModel model);
        MethodResponseDto EliminarProveedor(string ID, string Ip, long Usuario);
    }
}
