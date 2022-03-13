
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IInventarioRepository
    {
        bool QryInventarioMovimiento(InventarioMantenimientoDto mantenimientoDto, long IdUsuario, string IP, ref long IdInventarioMovimiento, ref string mensaje, ref string mensajeError);
        bool QryInventarioTransferencia(long IdUsuario, string IP, InventarioTransferenciaDto transferencia, ref string mensaje, ref string mensajeError);
        List<ProductoInventarioDto> GetProductosInventario(TipoInventario tipoInventario, long Sucursal);
        List<ProductoBodegaDto> SelProductosBodega(long IdBodega, int Sucursal, int TipoInventario);
        List<Producto> SelProductos(int cantidad, long IdProducto, DateTime fechaInicio, DateTime fechaFin);
        List<Bodega> GetBodegas(long IdCompania);
        List<InventarioProveedor> GetInventarioProveedor(long BodegaId);
        List<InventarioVenta> GetInventarioVenta(long BodegaId);
        InventarioConfiguracionesGenerales GetConfiguracionesGenerales();
    }
}
