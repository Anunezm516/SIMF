using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using System;
using GS.TOOLS;
using System.Linq;
using FRIO.MAR.APPLICATION.CORE.Interfaces.QueryServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Contants;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public sealed class UsuarioAppService : BaseAppService, IUsuarioAppService
    {
        private readonly IMailDomainService _envioMailService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioQueryService _usuarioQueryService;


        public UsuarioAppService(
            IMailDomainService envioMailService, 
            IUsuarioRepository usuarioRepository,
            IUsuarioQueryService usuarioQueryService
            )
        {
            this._envioMailService = envioMailService;
            this._usuarioRepository = usuarioRepository;
            this._usuarioQueryService = usuarioQueryService;
        }

        public MethodResponseDto CrearUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                Usuario usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Username == usuariosApp.Usuario && (x.Estado == ((int)EstadoUsuario.Activo) || x.Estado == ((int)EstadoUsuario.Bloqueado)));
                if (usuarioExiste != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.CorreoElectronico == usuariosApp.CorreoElectronico && (x.Estado == ((int)EstadoUsuario.Activo) || x.Estado == ((int)EstadoUsuario.Bloqueado)));
                if (usuarioExiste != null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                string Password = APPLICATION.CORE.Utilities.Utilidades.RandomString();

                Usuario usuario = new Usuario
                {
                    Nombre = usuariosApp.Nombre,
                    Apellido = usuariosApp.Apellido,

                    Username = usuariosApp.Usuario,
                    Password = GSCrypto.ComputeHashV1(Password),

                    CorreoElectronico = usuariosApp.CorreoElectronico,
                    Telefono = usuariosApp.Telefono,
                    FechaActualizarPassword = Utilidades.GetHoraActual().AddDays(GlobalSettings.LoginAppDiasForzarCambioPassword),
                    
                    Ip = Ip,
                    FechaCreacion = Utilidades.GetHoraActual(),
                    UsuarioCreacion = IdUsuarioCreacion,
                    Estado = ((int)EstadoUsuario.Activo),
                };

                _usuarioRepository.Add(usuario);
                responseDto.Estado = _usuarioRepository.Save() > 0;

                if (responseDto.Estado)
                {
                    _envioMailService.EnviarMailBienvenida(
                        usuario.CorreoElectronico,
                        usuario.Nombre + " " + usuario.Apellido,
                        usuario.Username,
                        Password
                        );

                    //_envioMailService.EnviarMail(new MailDto
                    //{
                    //    Asunto = usuario.Username,
                    //    Mensaje = Password,
                    //    Correos = usuario.CorreoElectronico,
                    //    FechaIngreso = Utilidades.GetHoraActual(),
                    //    Tipo = TipoMail.Bienvenida,
                    //    Copias = "",
                    //    CopiasOcultas = "",

                    //    //Servicio = Constants.Servicio.
                    //});
                }

                UsuarioRol rol = new UsuarioRol
                {
                    IdRol = usuariosApp.IdRol,
                    IdUsuario = usuario.IdUsuario,
                    Estado = true
                };

                _usuarioRepository.GuardarUsuarioRol(rol);

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto EditarUsuario(UsuariosAppResultDto usuariosApp, long IdUsuarioCreacion, string Ip)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var usuario = _usuarioRepository.Get(long.Parse(Crypto.DescifrarId(usuariosApp.IdUsuario)));
                if (usuario == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_USUARIO_NO_REGISTRADO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                Usuario usuarioExiste = null;
                if (usuario.Username != usuariosApp.Usuario)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.Username == usuariosApp.Usuario && (x.Estado == ((int)EstadoUsuario.Activo) || x.Estado == ((int)EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_USERNAME;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                if (usuario.CorreoElectronico != usuariosApp.CorreoElectronico)
                {
                    usuarioExiste = _usuarioRepository.GetFirstOrDefault(x => x.CorreoElectronico == usuariosApp.CorreoElectronico && (x.Estado == ((int)EstadoUsuario.Activo) || x.Estado == ((int)EstadoUsuario.Bloqueado)));
                    if (usuarioExiste != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_USUARIO_REGISTRADO_MAIL;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }
                }

                usuario.Nombre = usuariosApp.Nombre;
                usuario.Apellido = usuariosApp.Apellido;
                
                usuario.Username = usuariosApp.Usuario;

                usuario.CorreoElectronico = usuariosApp.CorreoElectronico;
                usuario.Telefono = usuariosApp.Telefono;

                usuario.Ip = Ip;
                usuario.UsuarioModificacion = IdUsuarioCreacion;
                usuario.FechaModificacion = Utilidades.GetHoraActual();

                _usuarioRepository.Update(usuario);
                responseDto.Estado = _usuarioRepository.Save() > 0;

                UsuarioRol rol = new UsuarioRol
                {
                    IdRol = usuariosApp.IdRol,
                    IdUsuario = usuario.IdUsuario,
                    Estado = true
                };

                _usuarioRepository.GuardarUsuarioRol(rol);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto ConsultarUsuarios()
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _usuarioRepository.ConsultarUsuarios();
                if (result != null)
                {
                    responseDto.Estado = true;
                    responseDto.Data = result.Select(c => new UsuariosAppResultDto(c, c.UsuarioRol.ToList())).ToList();
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public (bool, string) EliminarUsuario(string IdCifrado, long IdUsuario, string Ip)
        {
            try
            {
                long Id = long.Parse(IdCifrado);
                var result = _usuarioRepository.GetFirstOrDefault(x =>x.IdUsuario==Id && x.Estado==1) ;
                if (result == null)
                {
                    return (false, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_USUARIO_NO_REGISTRADO));
                }

                result.FechaEliminacion = FRIO.MAR.APPLICATION.CORE.Utilities.Utilidades.GetHoraActual();
                result.UsuarioEliminacion = IdUsuario;
                result.Ip = Ip;
                result.Estado = 0;
                _usuarioRepository.Update(result);

                //return true;
                return (_usuarioRepository.Save() > 0, null);
            }
            catch (Exception ex)
            {
                //return false;
                return (false, string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex)));
            }
        }

        public MethodResponseDto ObtenerUsuario(long idUsuario)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _usuarioRepository.ObtenerUsuario(idUsuario);
                if (result != null)
                {
                    responseDto.Estado = true;
                    responseDto.Data =  new UsuariosAppResultDto(result, result.UsuarioRol.ToList());
                }

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
