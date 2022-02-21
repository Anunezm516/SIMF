using FRIO.MAR.APPLICATION.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DTOs.DomainService
{
    public class InventarioDto
    {

    }

    public class InventarioTransferenciaDto
    {
        public int TipoMovimientoOrigen { get; set; }
        public int TipoInventarioOrigen { get; set; }
        public long BodegaOrigen { get; set; }
        public long Producto { get; set; }
        public int TipoMovimientoDestino { get; set; }
        public int TipoInventarioDestino { get; set; }
        public int SucursalOrigen { get; set; }
        public int SucursalDestino { get; set; }
        public long BodegaDestino { get; set; }
        public decimal Cantidad { get; set; }
        public string Motivo { get; set; }
        public decimal Precio { get; set; }
        public string PrecioStr { get; set; }
        public string UnidadMedida { get; set; }
        public string NumeroFactura { get; set; }
        public string Cufe { get; set; }
        public string CantidadDescripcion { get; set; }
        public long Proveedor { get; set; }
        public long Cliente { get; set; }
        public int SubTipoMovimiento { get; set; }

    }

    public class InventarioMantenimientoDto
    {
        public int tipoInventario { get; set; }
        public int tipoMovimiento { get; set; }
        public int subTipoMovimiento { get; set; }
        public long productos { get; set; }
        public decimal cantidad { get; set; }
        public string cantidadStr { get; set; }
        public long sucursal { get; set; }
        public long bodegas { get; set; }
        public long proveedor { get; set; }
        public long cliente { get; set; }
        public decimal precio { get; set; }
        public string precioStr { get; set; }
        public string numeroFactura { get; set; }
        public string cufeFactura { get; set; }
        public string unidadMedida { get; set; }
        public string cantidadDescripcion { get; set; }
        public string motivo { get; set; }
        public string nitProveedor { get; set; }
    }


    public class InventarioBogedaProductoDto
    {
        public long IdInventarioBp { get; set; }
        public long? IdProducto { get; set; }
        public long? IdInventarioBodega { get; set; }
        public decimal? Stock { get; set; }
        public long? IdCompania { get; set; }
        public string Ip { get; set; }
        public long? UsuarioCreacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? UsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? UsuarioEliminacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool? Estado { get; set; }
    }

    public class InventarioMovimientoDto : Auditoria
    {
        public long IdInventarioMovimiento { get; set; }
        public long? IdProducto { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public string NumeroFactura { get; set; }
        public string Cufe { get; set; }
        public long? IdClienteProveedor { get; set; }
        public string Motivo { get; set; }
        public long? IdInventarioBodega { get; set; }
        public long? IdCompania { get; set; }

    }

    public class ProductoBodegaDto
    {
        public long IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Stock { get; set; }
    }

    public class ERFactDto
    {
        public ERFactDto()
        {
            ERFactLine = new List<ERFactLineDto>();
        }
        public string NumeroDocumento { get; set; }
        public string Cufe { get; set; }
        public string NitProveedor { get; set; }
        public List<ERFactLineDto> ERFactLine { get; set; }
    }

    public class ERFactLineDto
    {
        public ERFactLineDto()
        {
            ERFactLineStandardItem = new List<ERFactLineStandardItemDto>();
            ERFactLineTaxtTotal = new List<ERFactLineTaxtTotal>();
        }

        public long IdFactLine { get; set; }
        public string ID { get; set; }
        public string BaseQuantity { get; set; }
        public string PriceAmountValue { get; set; }
        public string LineExtensionAmountValue { get; set; }
        public string UnitCode { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string TotalLinea { get; set; }
        public long? IdInventarioMovimiento { get; set; }
        public List<ERFactLineStandardItemDto> ERFactLineStandardItem { get; set; }
        public List<ERFactLineTaxtTotal> ERFactLineTaxtTotal { get; set; }
    }

    public class ERFactLineStandardItemDto
    {
        public string Code { get; set; }
        public string SchemeName { get; set; }
        public string ProductCode { get; set; }
    }

    public class ERFactLineTaxtTotal
    {
        public string Porcentaje { get; set; }
        public string TaxSchemeId { get; set; }
        public string TaxSchemeName { get; set; }
        public decimal TaxAmountValue { get; set; }
        public string TaxAmountValueStr { get; set; }
    }

    public class GetProductoInventarioDto
    {
        public GetProductoInventarioDto()
        {
            //EpinventarioProveedor = new List<FRIO.MAR.APPLICATION.CORE.Entities.EpinventarioProveedor>();
            //EpinventarioVenta = new List<FRIO.MAR.APPLICATION.CORE.Entities.EpinventarioVenta>();
        }
        public int TipoInventario { get; set; }
        //public List<FRIO.MAR.APPLICATION.CORE.Entities.EpinventarioProveedor> EpinventarioProveedor { get; set; }
        //public List<FRIO.MAR.APPLICATION.CORE.Entities.EpinventarioVenta> EpinventarioVenta { get; set; }
    }

    public class ProductoMovimientoDto
    {
        public long id { get; set; }
        public int tipoInventario { get; set; }
        public long inventario { get; set; }
        public int sucursal { get; set; }
        public long bodega { get; set; }
        public long producto { get; set; }
        public string cantidad { get; set; }
        public bool tieneError { get; set; }
        public string mensaje { get; set; }
        public string mensajeError { get; set; }
    }

}
