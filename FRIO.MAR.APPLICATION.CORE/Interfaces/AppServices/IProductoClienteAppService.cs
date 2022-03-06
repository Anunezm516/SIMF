using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IProductoClienteAppService
    {
        MethodResponseDto ConsultarProductos(long ClienteId);
        MethodResponseDto ConsultarProducto(string ProductoClienteId, string ClienteId);
        MethodResponseDto CrearProducto(ProductoClienteModel model);
        MethodResponseDto EditarProducto(ProductoClienteModel model);
        MethodResponseDto EliminarProducto(string ProductoClienteId, string ClienteId, string Ip, long Usuario);
    }
}
