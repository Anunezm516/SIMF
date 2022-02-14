using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class AccountAppService : IAccountAppService
    {
        public MethodResponseDto CambiarPassword(long id, string ClaveActual, string ClaveNueva, string ClaveNuevaConfirma, bool EsCorreoRecuperacion)
        {
            throw new NotImplementedException();
        }

        public (List<Menu>, string) LoadMenu(long IdUsuario)
        {
            throw new NotImplementedException();
        }

        public MethodResponseDto Login(string username, string password, string ip, bool DirActivo = false, string NomUsrDirAct = null, string GrupoUsuarioDirActivo = null)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                DateTime fechaActual = Utilities.Utilidades.GetHoraActual();
                string error = null;
                responseDto.Mensaje = AppConstants.MensajeUsuarioLogin;

                /*
                Spusuario usuario = _accountRepository.GetFirstOrDefault(x => x.Usuario == username && (x.Estado == ((int)EstadoUsuario.Activo) || x.Estado == ((int)EstadoUsuario.Bloqueado)));
                if (usuario == null) return responseDto;

                if (usuario.Estado == ((int)EstadoUsuario.Bloqueado))
                {
                    responseDto.Mensaje = AppConstants.MensajeUsuarioBloqueado;
                    return responseDto;
                }

                if (!GSCrypto.ConfirmHashV1(password, usuario.Password))
                {
                    if (usuario.IntentosFallidos is null) usuario.IntentosFallidos = 0;
                    usuario.IntentosFallidos++;

                    if (usuario.IntentosFallidos >= GlobalSettings.LoginAppNumeroIntentoBloqueo)
                    {
                        usuario.Estado = ((int)EstadoUsuario.Bloqueado);

                        responseDto.Mensaje = AppConstants.MensajeUsuarioBloqueado;
                    }

                    _accountRepository.Update(usuario);
                    _accountRepository.Save();

                    return responseDto;
                }

                var resultVentana = _loginQueryService.ConsultarVentana(usuario.IdUsuario, ref error);
                var roles = _rolRepository.GetRolesUsuario(usuario.IdUsuario);

                var correoAdmin = _usuarioRepository.GetUsuariosAdministrador().Select(c => c.CorreoElectronico);
                
                var login = new LoginAppResultDto
                {
                    IdUsuario = usuario.IdUsuario,
                    Usuario = usuario.Usuario,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Correo = usuario.CorreoElectronico,
                    CorreosAdministrador = correoAdmin.ToArray().ConcatenarValores(),
                    ForzarCambioClave = false,
                    IPLogin = ip,
                    FechaUltimaConexion = usuario.FechaUltimaConexion,
                    Menu = GenerarVentanaHabilitadas(resultVentana, null),
                    VentanasActivasConcat = GenerarVentanaHabilitadasConcat(resultVentana),
                    Rol = roles?.FirstOrDefault()?.IdRol ?? 0,
                    //TimeZoneId
                };

                //primera vez
                if (usuario.FechaUltimaConexion is null) login.ForzarCambioClave = true;

                if (usuario.FechaActualizarPassword is null) usuario.FechaActualizarPassword = fechaActual.AddDays(GlobalSettings.LoginAppDiasForzarCambioPassword);
                if (usuario.FechaActualizarPassword <= fechaActual) login.ForzarCambioClave = true;

                usuario.FechaUltimaConexion = fechaActual;
                usuario.IntentosFallidos = 0;
                */

                var acceso = new AccesoUsuario
                {
                    //Fecha = usuario.FechaUltimaConexion ?? fechaActual,
                    //IdUsuario = usuario.IdUsuario,
                    Ip = ip,
                    SitioWeb = DomainConstants.COMPONENTE_NAME
                };

                //_accountRepository.Update(usuario);
                //_accountRepository.Save();

                var login = new LoginAppResultDto
                {
                    Nombre = "Bolivar",
                    Apellido = "Cardenas",
                    Correo = "bolivar.cardenas@gmail.com",
                    CorreosAdministrador = "",
                    FechaUltimaConexion = Utilities.Utilidades.GetHoraActual(),
                    ForzarCambioClave = false,
                    IdUsuario = 1,
                    IPLogin = "",
                    Menu = new List<Menu>(),
                    Rol =  ((int)Roles.SuperAdministrador),
                    TimeZoneId = "",
                    Usuario = "bolivar.cardenas",
                    VentanasActivasConcat = ""
                };

                responseDto.Data = login;
                responseDto.Estado = true; // _accountRepository.RegistrarAccesoUsuario(acceso);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto RecuperarPassword(string username, string email)
        {
            throw new NotImplementedException();
        }


        #region metodos privados
        private void GenerarSubMenu(List<VentanaLoginQueryDto> ventanasDto, long IdNivelSuperior, ref string MenuConcat)
        {
            var rows = ventanasDto.FindAll(x => x.IdPadre == IdNivelSuperior);
            if (rows != null)
            {
                foreach (var ItemNivelSub in rows)
                {
                    if (ItemNivelSub.Url != null)
                    {
                        string MenuHref = "<li><a href='../" + ItemNivelSub.Url + "'>" + ItemNivelSub.NombreAbreviado + "</a></li>";
                        MenuConcat += MenuHref;
                    }
                    else
                    {
                        string MenuSubInicio = "<li class='dropdown'>";
                        MenuSubInicio += "<a class='dropdown-toggle' href='#' role='button' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>" + ItemNivelSub.NombreAbreviado + "</a>";
                        MenuSubInicio += "<ul class='dropdown-menu' aria-labelledby='navbarDropdown'>";
                        MenuConcat += MenuSubInicio;
                        GenerarSubMenu(ventanasDto, ItemNivelSub.IdPermiso, ref MenuConcat);
                        string MenuSubFin = "</ul></li>";
                        MenuConcat += MenuSubFin;
                    }
                }
            }
        }

        private List<Menu> GenerarVentanaHabilitadas(List<VentanaLoginQueryDto> ventanasMaestra, long? IdPadre)
        {
            List<Menu> menu = new List<Menu>();
            if (ventanasMaestra != null && ventanasMaestra.Any())
            {
                var VentanaPadre = ventanasMaestra.Where(x => x.IdPadre == IdPadre).ToList();
                foreach (var i in VentanaPadre)
                {
                    Menu ventana = new Menu
                    {
                        Icon = i.Icono,
                        Label = i.NombreAbreviado,
                        Url = i.Url,
                        IdRol = i.IdRol,
                        Rol = i.Rol,
                        SubMenu = GenerarVentanaHabilitadas(ventanasMaestra, i.IdPermiso)
                    };

                    menu.Add(ventana);
                }
            }

            return menu;
        }

        private string GenerarVentanaHabilitadasConcat(List<VentanaLoginQueryDto> ventanasDto)
        {
            StringBuilder sb = new StringBuilder();

            if (ventanasDto != null && ventanasDto.Count > 0)
            {
                foreach (var i in ventanasDto)
                    sb.Append($"{i.Codigo};");
            }

            return sb.ToString();
        }
        #endregion

    }
}
