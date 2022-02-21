
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATIONCORE_GSEDOCPYME.Interfaces.Inventario
{
    public interface IInventarioRepository
    {
        bool QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdCompania, long IdUsuario, string IP, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError);
        bool QryInventarioTransferencia(long IdCompania, long IdUsuario, string IP, InventarioTransferenciaDto transferencia, ref string mensaje, ref string mensajeError);
        List<ProductoBodegaDto> SelProductosBodega(long IdBodega, int Sucursal, int TipoInventario, long IdCompania);
        List<Producto> SelProductos(int cantidad, long IdProducto, long IdCompania, DateTime fechaInicio, DateTime fechaFin);
        List<Bodega> GetBodegas(long IdCompania);

    }
}
