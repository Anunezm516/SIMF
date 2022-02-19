using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IClienteAppService
    {
        MethodResponseDto ConsultarClientes();
        MethodResponseDto ConsultarCliente(string ID);
        MethodResponseDto CrearCliente(ClienteModel model);
        MethodResponseDto EditarCliente(ClienteModel model);
        MethodResponseDto EliminarCliente(string ID, string Ip, long Usuario);
    }
}
