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
    public class SucursalAppService : ISucursalAppService
    {
        private readonly ISucursalRepository _SucursalRepository;

        public SucursalAppService(ISucursalRepository SucursalRepository)
        {
            _SucursalRepository = SucursalRepository;
        }

        public MethodResponseDto ConsultarSucursales()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _SucursalRepository.GetSucursales();

                responseDto.Data = result.Select(Sucursal => new SucursalModel(Sucursal)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarSucursal(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));

                Sucursal Sucursal = _SucursalRepository.Get(Id);

                responseDto.Data = new SucursalModel(Sucursal);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearSucursal(SucursalModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Sucursal Sucursal = _SucursalRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado);
                if (Sucursal != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_SUCURSAL_REGISTRADO_CODIGO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Sucursal = new Sucursal
                {
                    Codigo = model.Codigo,
                    Nombre = model.Nombre,

                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                _SucursalRepository.Add(Sucursal);
                responseDto.Estado = _SucursalRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarSucursal(SucursalModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Sucursal Sucursal = _SucursalRepository.Get(Id);
                if (Sucursal == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_SUCURSAL_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Sucursal.Codigo != model.Codigo)
                {
                    var existe = _SucursalRepository.GetFirstOrDefault(x => x.Codigo == model.Codigo && x.Estado && x.SucursalId != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_SUCURSAL_REGISTRADO_CODIGO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Sucursal.Codigo = model.Codigo;
                Sucursal.Nombre = model.Nombre;
                
                Sucursal.Ip = model.Ip;
                Sucursal.UsuarioModificacion = model.Usuario;
                Sucursal.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _SucursalRepository.Update(Sucursal);
                responseDto.Estado = _SucursalRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarSucursal(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));
                Sucursal Sucursal = _SucursalRepository.Get(Id);
                if (Sucursal == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_SUCURSAL_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Sucursal.Ip = Ip;
                Sucursal.UsuarioEliminacion = Usuario;
                Sucursal.FechaEliminacion = Utilities.Utilidades.GetHoraActual();
                Sucursal.Estado = false;

                _SucursalRepository.Update(Sucursal);
                responseDto.Estado = _SucursalRepository.Save() > 0;
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
