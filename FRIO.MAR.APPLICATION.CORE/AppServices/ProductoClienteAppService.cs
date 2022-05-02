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
    public class ProductoClienteAppService : IProductoClienteAppService
    {
        private readonly IStorageService _storageService;
        private readonly IProductoClienteRepository _ProductoRepository;
        private readonly string Tipo = "Clientes";
        public ProductoClienteAppService(IStorageService storageService, IProductoClienteRepository ProductoRepository)
        {
            _storageService = storageService;
            _ProductoRepository = ProductoRepository;
        }

        public MethodResponseDto ConsultarProductos(long ClienteId)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _ProductoRepository.GetProductos(ClienteId);

                responseDto.Data = result.Select(Producto => new ProductoClienteModel(Producto)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarProducto(string ProductoClienteId, string ClienteId)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long ProductoCliente = long.Parse(Utilities.Crypto.DescifrarId(ProductoClienteId));
                long Cliente = long.Parse(Utilities.Crypto.DescifrarId(ClienteId));

                var Producto = _ProductoRepository.GetProducto(Cliente, ProductoCliente);
                if (Producto != null)
                {
                    var prod = new ProductoClienteModel(Producto);
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

        public MethodResponseDto CrearProducto(ProductoClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Cliente = long.Parse(Utilities.Crypto.DescifrarId(model.ClienteId));

                var Producto = _ProductoRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado);
                if (Producto != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_REGISTRADO_CODIGO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                List<ArchivoServiceDto> imagenes =_storageService.GuardarImagenes(model.Imagen, this.Tipo);

                Producto = new ProductoCliente
                {
                    Codigo = model.Codigo,
                    //UnidadMedida = model.UnidadMedida,
                    Nombre = model.Nombre,
                    Descripcion = model.Descripcion,
                    Marca = model.Marca,
                    Modelo = model.Modelo,
                    
                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true,

                    ProductoClienteImagen = imagenes.Select(c => new ProductoClienteImagen
                    {
                        Nombre = c.Nombre,
                        Ruta = c.Ruta,
                        Estado = true,
                    }).ToList(),

                    ClienteId = Cliente
                };

                _ProductoRepository.Add(Producto);
                responseDto.Estado = _ProductoRepository.Save() > 0;
                model.ProductoClienteId = Producto.ProductoClienteId;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarProducto(ProductoClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));
                long Cliente = long.Parse(Utilities.Crypto.DescifrarId(model.ClienteId));

                var Producto = _ProductoRepository.GetProducto(Cliente, Id);
                if (Producto == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Producto.Codigo != model.Codigo)
                {
                    var existe = _ProductoRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado && x.ProductoClienteId != Id);
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
                    Producto.ProductoClienteImagen.Clear();
                    _ProductoRepository.Save();

                    Producto.ProductoClienteImagen = imagenes.Select(c => new ProductoClienteImagen
                    {
                        Nombre = c.Nombre,
                        Ruta = c.Ruta,
                        Estado = true,
                    }).ToList();
                }

                Producto.Codigo = model.Codigo;
                //Producto.UnidadMedida = model.UnidadMedida;
                Producto.Nombre = model.Nombre;
                Producto.Descripcion = model.Descripcion;
                Producto.Marca = model.Marca;
                Producto.Modelo = model.Modelo;

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

        public MethodResponseDto EliminarProducto(string ProductoClienteId, string ClienteId, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long ProductoCliente = long.Parse(Utilities.Crypto.DescifrarId(ProductoClienteId));
                long Cliente = long.Parse(Utilities.Crypto.DescifrarId(ClienteId));

                var Producto = _ProductoRepository.GetProducto(Cliente, ProductoCliente);
                if (Producto == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
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
