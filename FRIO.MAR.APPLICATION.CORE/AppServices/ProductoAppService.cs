using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.Services;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class ProductoAppService : IProductoAppService
    {
        private readonly IStorageService _storageService;
        private readonly IProductoRepository _ProductoRepository;
        private readonly string Tipo = "Productos";
        public ProductoAppService(IStorageService storageService, IProductoRepository ProductoRepository)
        {
            _storageService = storageService;
            _ProductoRepository = ProductoRepository;
        }

        public MethodResponseDto ConsultarProductos()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _ProductoRepository.GetProductos();

                responseDto.Data = result.Select(Producto => new ProductoModel(Producto)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarProducto(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));

                Producto Producto = _ProductoRepository.GetProducto(Id);
                if (Producto != null)
                {
                    var prod = new ProductoModel(Producto);
                    string imagen = _storageService.ObtenerImagenBase64(GlobalSettings.TipoAlmacenamiento, prod.ImagenRuta, "");
                    prod.ImagenBase64 = string.IsNullOrEmpty(imagen) ? AppConstants.SinImagen : imagen;

                    responseDto.Data = prod;

                    responseDto.Estado = true;
                }
                else
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearProducto(ProductoModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Producto Producto = _ProductoRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado);
                if (Producto != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_REGISTRADO_CODIGO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                List<ArchivoServiceDto> imagenes =_storageService.GuardarImagenes(model.Imagen, this.Tipo);

                Producto = new Producto
                {
                    Codigo = model.Codigo,
                    IvaCodigo = model.IVA.Split("|")[0],
                    IvaPorcentaje = decimal.Parse(model.IVA.Split("|")[1]),
                    //UnidadMedida = model.UnidadMedida,
                    Descripcion = model.Descripcion,
                    PrecioUnitario = decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.PrecioUnitarioStr)),
                    TipoProducto = model.TipoProducto,

                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true,
                    ProductoImagen = imagenes.Select(c => new ProductoImagen
                    {
                        Nombre = c.Nombre,
                        Ruta = c.Ruta,
                        Estado = true,
                    }).ToList()
                };

                _ProductoRepository.Add(Producto);
                responseDto.Estado = _ProductoRepository.Save() > 0;
                model.ProductoId = Producto.ProductoId;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarProducto(ProductoModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Producto Producto = _ProductoRepository.GetProducto(Id);
                if (Producto == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Producto.Codigo != model.Codigo)
                {
                    var existe = _ProductoRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado && x.ProductoId != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_REGISTRADO_CODIGO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                List<ArchivoServiceDto> imagenes = new List<ArchivoServiceDto>();

                if (model.Imagen != null && model.Imagen.Any())
                {
                    imagenes = _storageService.GuardarImagenes(model.Imagen, this.Tipo);
                    Producto.ProductoImagen.Clear();
                    _ProductoRepository.Save();

                    Producto.ProductoImagen = imagenes.Select(c => new ProductoImagen
                    {
                        Nombre = c.Nombre,
                        Ruta = c.Ruta,
                        Estado = true,
                    }).ToList();
                }

                Producto.Codigo = model.Codigo;
                Producto.IvaCodigo = model.IVA.Split("|")[0];
                Producto.IvaPorcentaje = decimal.Parse(model.IVA.Split("|")[1]);
                //Producto.UnidadMedida = model.UnidadMedida;
                Producto.Descripcion = model.Descripcion;
                Producto.PrecioUnitario = decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.PrecioUnitarioStr));
                Producto.TipoProducto = model.TipoProducto;

                Producto.Ip = model.Ip;
                Producto.UsuarioModificacion = model.Usuario;
                Producto.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _ProductoRepository.Update(Producto);
                responseDto.Estado = _ProductoRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarProducto(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));
                Producto Producto = _ProductoRepository.GetProducto(Id);
                if (Producto == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Producto.InventarioVenta.Any() || Producto.InventarioProveedor.Any())
                {
                    if (Producto.InventarioVenta.Sum(x => x.StockActual) > 0 || Producto.InventarioProveedor.Sum(x => x.StockActual) > 0)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_REGISTRADO_BODEGA;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Producto.Ip = Ip;
                Producto.UsuarioEliminacion = Usuario;
                Producto.FechaEliminacion = Utilities.Utilidades.GetHoraActual();
                Producto.Estado = false;

                _ProductoRepository.Update(Producto);
                responseDto.Estado = _ProductoRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

    }
}
