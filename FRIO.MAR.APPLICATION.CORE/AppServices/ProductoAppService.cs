using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class ProductoAppService : IProductoAppService
    {
        private readonly IProductoRepository _ProductoRepository;

        public ProductoAppService(IProductoRepository ProductoRepository)
        {
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

                Producto Producto = _ProductoRepository.Get(Id);

                responseDto.Data = new ProductoModel(Producto);

                responseDto.Estado = true;
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

                Producto = new Producto
                {
                    Codigo = model.Codigo,
                    //IVA = model.IVA,
                    UnidadMedida = model.UnidadMedida,
                    Descripcion = model.Descripcion,

                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                _ProductoRepository.Add(Producto);
                responseDto.Estado = _ProductoRepository.Save() > 0;
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

                Producto Producto = _ProductoRepository.Get(Id);
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

                Producto.Codigo = model.Codigo;
                //IVA = model.IVA,
                Producto.UnidadMedida = model.UnidadMedida;
                Producto.Descripcion = model.Descripcion;

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
                Producto Producto = _ProductoRepository.Get(Id);
                if (Producto == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PRODUCTO_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Producto.Ip = Ip;
                Producto.UsuarioEliminacion = Usuario;
                Producto.FechaCreacion = Utilities.Utilidades.GetHoraActual();
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
