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
    public class BodegaAppService : IBodegaAppService
    {
        private readonly IBodegaRepository _bodegaRepository;

        public BodegaAppService(IBodegaRepository bodegaRepository)
        {
            _bodegaRepository = bodegaRepository;
        }

        public MethodResponseDto ConsultarBodegas()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _bodegaRepository.GetBodegas();

                responseDto.Data = result.Select(Bodega => new BodegaModel(Bodega)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarBodega(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));

                Bodega Bodega = _bodegaRepository.GetBodega(Id);

                responseDto.Data = new BodegaModel(Bodega);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearBodega(BodegaModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Bodega Bodega = _bodegaRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado);
                if (Bodega != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_BODEGA_REGISTRADO_CODIGO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Bodega = new Bodega
                {
                    Codigo = model.Codigo,
                    Nombre = model.Nombre,
                    
                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                Bodega.SucursalBodega = model.Sucursales.Select(c => new SucursalBodega 
                { 
                    SucursalId = c,
                    Estado = true,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    UsuarioCreacion = model.Usuario,
                    Ip = model.Ip

                }).ToList();

                _bodegaRepository.Add(Bodega);

                responseDto.Estado = _bodegaRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarBodega(BodegaModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Bodega Bodega = _bodegaRepository.GetBodega(Id);
                if (Bodega == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_BODEGA_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Bodega.Codigo != model.Codigo)
                {
                    var existe = _bodegaRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado && x.BodegaId != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_BODEGA_REGISTRADO_CODIGO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Bodega.Codigo = model.Codigo;
                Bodega.Nombre = model.Nombre;

                Bodega.Ip = model.Ip;
                Bodega.UsuarioModificacion = model.Usuario;
                Bodega.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                Bodega.SucursalBodega.Clear();
                Bodega.SucursalBodega = model.Sucursales.Select(c => new SucursalBodega 
                { 
                    SucursalId = c ,
                    Estado = true,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    UsuarioCreacion = model.Usuario,
                    Ip = model.Ip
                }).ToList();
                _bodegaRepository.Update(Bodega);
                responseDto.Estado = _bodegaRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarBodega(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));
                Bodega Bodega = _bodegaRepository.GetBodega(Id);
                if (Bodega == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_BODEGA_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Bodega.InventarioVenta.Any() || Bodega.InventarioProveedor.Any())
                {
                    if (Bodega.InventarioVenta.Sum(x => x.StockActual) > 0 || Bodega.InventarioProveedor.Sum(x => x.StockActual) > 0)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_BODEGA_REGISTRADO_PRODUCTO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Bodega.Ip = Ip;
                Bodega.UsuarioEliminacion = Usuario;
                Bodega.FechaCreacion = Utilities.Utilidades.GetHoraActual();
                Bodega.Estado = false;

                _bodegaRepository.Update(Bodega);
                responseDto.Estado = _bodegaRepository.Save() > 0;
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
