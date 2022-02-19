using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Parameters;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using FRIO.MAR.UI.WEB.SITE.Models;
using FRIO.MAR.UI.WEB.SITE.Parameters;
using GS.TOOLS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountAppService _accountAppService;

        public AccountController(
            IAccountAppService accountAppService,
            ILogInfraServices logInfraServices) : base(logInfraServices)
        {
            _accountAppService = accountAppService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UsuarioLoginPageModel usr, string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!User.Identity.IsAuthenticated)
                    {
                        var google = new GSReCaptchaGoogleDto
                        {
                            ReCaptchaClaveSitioWeb = WebSiteParameters.WebReCaptchaClaveSitioWeb,
                            ReCaptchaClaveComGoogle = WebSiteParameters.WebReCaptchaClaveComGoogle,
                            ValorReCaptcha = null,
                            ReCaptchaCont = 0,
                            EncodedResponse = usr.Captcha
                        };

                        string mensaje = null;
                        var resultGoogle = GSRecaptchaGoogle.Validar(ref google, ref mensaje);

                        if (!resultGoogle)
                        {
                            RegistrarLogError(this.GetCaller(), mensaje);
                            throw new Exception(AppParameters.ExcepcionGenerica);
                        }

                        var IPLogin = HttpContext.Connection.RemoteIpAddress.ToString();

                        var resultLogin = _accountAppService.Login(usr.Usuario, usr.Clave, IPLogin);
                        if (resultLogin.TieneErrores) throw new Exception(resultLogin.MensajeError);

                        if (!resultLogin.Estado)
                        {
                            ModelState.AddModelError("", resultLogin.Mensaje);
                            return View(usr);
                        }

                        LoginAppResultDto loginAppResult = resultLogin.Data;

                        HttpContext.Session.SetString("Menu", JsonConvert.SerializeObject(loginAppResult.Menu));
                        HttpContext.Session.SetInt32("CantidadNotificaciones", loginAppResult.CantidadNotificaciones);

                        loginAppResult.Menu = null;

                        string RolStr = "";
                        if (loginAppResult.Rol > 4 && loginAppResult.Rol < 1)
                        {
                            RolStr = Roles.Estandar.ToString();
                        }
                        else
                        {
                            RolStr = ((Roles)loginAppResult.Rol).ToString();
                        }

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, loginAppResult.Nombre + " " + loginAppResult.Apellido),
                            new Claim(ClaimTypes.Role, RolStr),
                            new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(loginAppResult))
                        };

                        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                        if (loginAppResult.ForzarCambioClave)
                            return RedirectToAction("ChangePassword");
                        else
                        {
                            if (WebSiteParameters.WebLimiteConsulta == "") 
                            { 
                                WebSiteParameters.WebLimiteConsulta = "3[MM]"; 
                            }
                            WebSiteParameters.WebLimiteConsulta = WebSiteParameters.WebLimiteConsulta.ToUpper();
                        }
                    }

                    if (Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    return RedirectToAction("Index", "Dashboard");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(usr);
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Menu");
            HttpContext.Session.Remove("CantidadNotificaciones");
            HttpContext.Session.Clear();
            GS.TOOLS.GSUtilities.ClearMemory();
            return RedirectToAction("Login");
        }

        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UsuarioCambioClave model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogin = GetUserLogin();
                    var result = _accountAppService.CambiarPassword(userLogin.IdUsuario, model.ClaveActualConfirma, model.ClaveNueva, model.ClaveNuevaConfirma, true );
                    if (result.TieneErrores) throw new Exception(result.MensajeError);

                    if (!result.Estado)
                    {
                        ModelState.AddModelError("", result.Mensaje);
                    }
                    else
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", $"Se ha cambiado la contraseña con exito. Vuelva a iniciar sesión");

                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return RedirectToAction("Login");
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult RecuperarClave()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RecuperarClave(UsuarioRecuperaClave user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _accountAppService.RecuperarPassword(user.Usuario, user.CorreoElectronico);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", $"Se ha enviado un correo electrónico al correo {APPLICATION.CORE.Utilities.Utilidades.OcultarCaracteres(user.CorreoElectronico, user.CorreoElectronico.Split("@")[0].Length)} registrado para poder recuperar su contraseña. Gracias");
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("", result.Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(user);
        }

    }
}
