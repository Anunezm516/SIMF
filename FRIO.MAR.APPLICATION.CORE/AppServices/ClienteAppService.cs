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
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteAppService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public MethodResponseDto ConsultarClientes()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _clienteRepository.GetClientes();

                responseDto.Data = result.Select(cliente => new ClienteModel(cliente)).ToList();

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto ConsultarCliente(string ID)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));

                Cliente cliente = _clienteRepository.Get(Id);

                responseDto.Data = new ClienteModel(cliente);

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }


        public MethodResponseDto CrearCliente(ClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Cliente cliente = _clienteRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado);
                if (cliente != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_REGISTRADO_IDENTIFICACION;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                cliente = new Cliente
                {
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

                _clienteRepository.Add(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }

        public MethodResponseDto EditarCliente(ClienteModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));

                Cliente cliente = _clienteRepository.Get(Id);
                if (cliente == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (cliente.Identificacion != model.Identificacion)
                {
                    var existe = _clienteRepository.GetFirstOrDefault(x => x.Identificacion == model.Identificacion && x.Estado && x.ClienteId != Id);
                    if (existe != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_REGISTRADO_IDENTIFICACION;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                cliente.TipoIdentificacion = model.TipoIdentificacion;
                cliente.Identificacion = model.Identificacion;
                cliente.RazonSocial = model.RazonSocial;
                cliente.NombreComercial = model.NombreComercial;
                cliente.Direccion = model.Direccion;
                cliente.CorreoElectronico = model.CorreoElectronico;
                cliente.Telefono = model.Telefono;
                cliente.Ip = model.Ip;
                cliente.UsuarioModificacion = model.Usuario;
                cliente.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _clienteRepository.Update(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }
            return responseDto;
        }


        public MethodResponseDto EliminarCliente(string ID, string Ip, long Usuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                long Id = long.Parse(Utilities.Crypto.DescifrarId(ID));
                Cliente cliente = _clienteRepository.Get(Id);
                if (cliente == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                cliente.Ip = Ip;
                cliente.UsuarioEliminacion = Usuario;
                cliente.FechaCreacion = Utilities.Utilidades.GetHoraActual();
                cliente.Estado = false;

                _clienteRepository.Update(cliente);
                responseDto.Estado = _clienteRepository.Save() > 0;
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
