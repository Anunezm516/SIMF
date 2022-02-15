using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using FRIO.MAR.UI.WEB.SITE.Models;
using GS.TOOLS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Permisos)]
    public class PermisosController : BaseController
    {
        private readonly IPermisoAppService permisoAppService;

        public PermisosController(ILogInfraServices logInfraServices, IPermisoAppService permisoAppService) : base(logInfraServices)
        {
            this.permisoAppService = permisoAppService;
        }

        public IActionResult Index()
        {
            try
            {
                (var result, string error) = permisoAppService.ListarPermisos();
                if (!string.IsNullOrEmpty(error)) throw new Exception(error);
                return View(result);
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
                return View(new List<APPLICATION.CORE.DTOs.AppServices.PermisoAppResultDto>());
            }
        }

        public IActionResult Registrar(string Id)
        {
            PermisoModel model = new PermisoModel();
            try
            {
                (var permisos, string errorGet) = permisoAppService.VerPermisos();
                if (!string.IsNullOrEmpty(errorGet)) throw new Exception(errorGet);

                ViewBag.VentanasPadre = new SelectList(permisos.Select(c => new { IdPadre = c.Item1, Nombre = c.Item2 } ), "IdPadre", "Nombre");
                if (!string.IsNullOrEmpty(Id))
                {
                    (var result, string error) = permisoAppService.VerPermiso(Id);
                    if (!string.IsNullOrEmpty(error))
                    {
                        throw new Exception(error);
                    }
                    else
                    {
                        model = new PermisoModel
                        {
                            Codigo = result.Codigo,
                            Descripcion = result.Descripcion,
                            Icono = result.Icono,
                            IdPadre = result.IdPadre,
                            Id = result.Id,
                            NombreAbreviado = result.NombreAbreviado,
                            Url = result.Url,
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registrar(PermisoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    
                    if (!string.IsNullOrEmpty(model.Id))
                    {
                        (var result, string error) = permisoAppService.EditarPermiso(
                            model.Id,
                            model.NombreAbreviado,
                            model.Codigo,
                            model.Icono,
                            model.Descripcion,
                            model.Url,
                            model.IdPadre,
                            usr.IdUsuario,
                            usr.IPLogin
                            );
                        if (!string.IsNullOrEmpty(error))
                        {
                            throw new Exception(error);
                        }

                        if (result)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Permiso actualizado con éxito");
                            return RedirectToAction("Index");
                        }
                    }
                    else
                    {
                        (var result, string error) = permisoAppService.AgregarPermiso(
                            model.NombreAbreviado,
                            model.Codigo,
                            model.Icono,
                            model.Descripcion,
                            model.Url,
                            model.IdPadre,
                            usr.IdUsuario,
                            usr.IPLogin
                            );
                        if (!string.IsNullOrEmpty(error))
                        {
                            throw new Exception(error);
                        }
                        if (result)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Permiso registrado con éxito");
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL ) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                (var result, string error) = permisoAppService.EliminarPermiso(Id, usr.IdUsuario, usr.IPLogin);
                if (!string.IsNullOrEmpty(error)) throw new Exception(error);

                if (result)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Permiso eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el permiso" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }
    }
}
