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
    public class ProveedorAppService : IProveedorAppService
    {
        private readonly IProveedorRepository _proveedorRepository;

        public ProveedorAppService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public MethodResponseDto ConsultarProveedores()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _proveedorRepository.GetProveedores();

                responseDto.Data = result.Select(Proveedor => new ProveedorModel(Proveedor)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarProveedor(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));

                Proveedor Proveedor = _proveedorRepository.Get(Id);

                responseDto.Data = new ProveedorModel(Proveedor);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto CrearProveedor(ProveedorModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Proveedor Proveedor = _proveedorRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado);
                if (Proveedor != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Proveedor = new Proveedor
                {
                    TipoPersona = model.TipoPersona,
                    TipoIdentificacion = model.TipoIdentificacion,
                    Identificacion = model.Identificacion,
                    RazonSocial = model.RazonSocial,
                    NombreComercial = model.NombreComercial,
                    Direccion = model.Direccion,
                    CorreoElectronico = model.CorreoElectronico,
                    Telefono = model.Telefono,
                    Ip = model.Ip,
                    UsuarioCreacion = model.Usuario,
                    FechaCreacion = Utilities.Utilidades.GetHoraActual(),
                    Estado = true
                };

                _proveedorRepository.Add(Proveedor);
                responseDto.Estado = _proveedorRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarProveedor(ProveedorModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Proveedor Proveedor = _proveedorRepository.Get(Id);
                if (Proveedor == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PROVEEDOR_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (Proveedor.Identificacion != model.Identificacion)
                {
                    var existe = _proveedorRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado && x.ProveedorId != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_PROVEEDOR_REGISTRADO_IDENTIFICACION;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                Proveedor.TipoPersona = model.TipoPersona;
                Proveedor.TipoIdentificacion = model.TipoIdentificacion;
                Proveedor.Identificacion = model.Identificacion;
                Proveedor.RazonSocial = model.RazonSocial;
                Proveedor.NombreComercial = model.NombreComercial;
                Proveedor.Direccion = model.Direccion;
                Proveedor.CorreoElectronico = model.CorreoElectronico;
                Proveedor.Telefono = model.Telefono;
                Proveedor.Ip = model.Ip;
                Proveedor.UsuarioModificacion = model.Usuario;
                Proveedor.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _proveedorRepository.Update(Proveedor);
                responseDto.Estado = _proveedorRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EliminarProveedor(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));
                Proveedor Proveedor = _proveedorRepository.Get(Id);
                if (Proveedor == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_PROVEEDOR_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Proveedor.Ip = Ip;
                Proveedor.UsuarioEliminacion = Usuario;
                Proveedor.FechaEliminacion = Utilities.Utilidades.GetHoraActual();
                Proveedor.Estado = false;

                _proveedorRepository.Update(Proveedor);
                responseDto.Estado = _proveedorRepository.Save() > 0;
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
