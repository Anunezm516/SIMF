using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface IProductoAppService
    {
        MethodResponseDto ConsultarProductos();
        MethodResponseDto ConsultarProducto(string ID);
        MethodResponseDto CrearProducto(ProductoModel model);
        MethodResponseDto EditarProducto(ProductoModel model);
        MethodResponseDto EliminarProducto(string ID, string Ip, long Usuario);
    }
}
