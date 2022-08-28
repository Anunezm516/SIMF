using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using FRIO.MAR.APPLICATION.CORE.Utilities;
using FRIO.MAR.CROSSCUTTING.Interfaces;
using FRIO.MAR.UI.WEB.SITE.Constants;
using GS.TOOLS;
using Luilliarcec.Identification.Ecuador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.UI.WEB.SITE.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Filters.MenuFilter(Constants.VentanasSoporte.Clientes)]
    public class ClientesController : BaseController
    {
        private readonly IProductoClienteAppService _productoClienteAppService;
        private readonly IUtilidadRepository _utilidadRepository;
        private readonly IClienteAppService _clienteAppService;

        public ClientesController(
            IProductoClienteAppService productoClienteAppService,
            IUtilidadRepository utilidadRepository, 
            ILogInfraServices logInfraServices,
            IClienteAppService clienteAppService) : base(logInfraServices)
        {
            _productoClienteAppService = productoClienteAppService;
            _utilidadRepository = utilidadRepository;
            _clienteAppService = clienteAppService;
        }

        public IActionResult Index()
        {
            List<ClienteModel> clientes = new List<ClienteModel>();
            try
            {
                ViewData["tipoIdentificacion"] = _utilidadRepository.GetTipoIdentificaciones();
                var result = _clienteAppService.ConsultarClientes();
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    clientes = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(clientes);
        }

        [HttpGet]
        public IActionResult Registrar(string Id)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);

            ClienteModel model = new ClienteModel();
            try
            {
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones().ToList(), "Codigo", "Nombre");

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _clienteAppService.ConsultarCliente(Id);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        model = result.Data;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Mensaje);
                    }
                }
            }
            catch (System.Exception ex)
            {
                model = new ClienteModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Registrar(ClienteModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                ViewData["tipoIdentificacion"] = new SelectList(_utilidadRepository.GetTipoIdentificaciones(), "Codigo", "Nombre");

                if (Identification.ValidateAllTypeIdentification(model.Identificacion) != model.TipoIdentificacion)
                {
                    ModelState.AddModelError("Identificacion", "La identificación ingresada no corresponde al tipo de identificación seleccionado");
                    return View(model);
                }

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;

                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _clienteAppService.CrearCliente(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Cliente registrado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _clienteAppService.EditarCliente(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Cliente actualizado con éxito");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult Eliminar(string Id)
        {
            try
            {
                var usr = GetUserLogin();
                var result = _clienteAppService.EliminarCliente(Id, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Cliente eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = "Error al eliminar el Cliente" });
                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        [ActionName("Productos")]
        public IActionResult Productos(string Id)
        {
            List<ProductoClienteModel> Productoes = new List<ProductoClienteModel>();
            try
            {
                ViewBag.ClienteId = Id;

                //ViewData["unidadMeida"] = _utilidadRepository.GetUnidadesMedida();

                long ClienteId = long.Parse(Crypto.DescifrarId(Id));

                var result = _productoClienteAppService.ConsultarProductos(ClienteId);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    Productoes = result.Data;
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = WebSiteConstants.MENSAJE_SWEET_ALERT_ERROR.Replace("{Mensaje_Respuesta}", DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(Productoes);
        }

        [HttpGet]
        public IActionResult ProductosRegistrar(string Id, string Cliente)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(Id);
            ViewBag.ClienteId = Cliente;

            ProductoClienteModel model = new ProductoClienteModel();
            try
            {
                model.ClienteId = ViewBag.ClienteId;
                //CargarDatos();

                if (!string.IsNullOrEmpty(Id))
                {
                    var result = _productoClienteAppService.ConsultarProducto(Id, Cliente);
                    if (result.TieneErrores) throw new Exception(result.MensajeError);
                    if (result.Estado)
                    {
                        model = result.Data;
                        model.ClienteId = ViewBag.ClienteId;
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, result.Mensaje);
                    }
                }
            }
            catch (System.Exception ex)
            {
                model = new ProductoClienteModel();
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult ProductosRegistrar(ProductoClienteModel model)
        {
            ViewBag.EsNuevo = string.IsNullOrEmpty(model.Id);
            try
            {
                //CargarDatos();

                if (ModelState.IsValid)
                {
                    var usr = GetUserLogin();
                    model.Ip = usr.IPLogin;
                    model.Usuario = usr.IdUsuario;
                    
                    if (string.IsNullOrEmpty(model.Id))
                    {
                        var result = _productoClienteAppService.CrearProducto(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Producto registrado con éxito");

                            return RedirectToAction("Productos", new { Id = model.ClienteId });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                    else
                    {
                        var result = _productoClienteAppService.EditarProducto(model);
                        if (result.TieneErrores) throw new Exception(result.MensajeError);
                        if (result.Estado)
                        {
                            TempData["msg"] = WebSiteConstants.MENSAJE_TOAST_ALERT_SUCCESS.Replace("{Mensaje_Respuesta}", "Producto actualizado con éxito");
                            return RedirectToAction("Productos", new { Id = model.ClienteId });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, result.Mensaje);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex));
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult EliminarProductoCliente(string Id, string Cliente)
        {
            try
            {
                var usr = GetUserLogin();
                var result = _productoClienteAppService.EliminarProducto(Id, Cliente, usr.IPLogin, usr.IdUsuario);
                if (result.TieneErrores) throw new Exception(result.MensajeError);
                if (result.Estado)
                {
                    return Json(new ResponseToViewDto { Estado = true, Mensaje = "Producto eliminado con éxito" });
                }
                else
                {
                    return Json(new ResponseToViewDto { Estado = false, Mensaje = (string.IsNullOrEmpty(result.Mensaje) ? "Error al eliminar el Producto" : result.Mensaje) });

                }
            }
            catch (Exception ex)
            {
                return Json(new ResponseToViewDto { Estado = false, Mensaje = DomainConstants.ObtenerDescripcionError(DomainConstants.ERROR_GENERAL) + RegistrarLogError(this.GetCaller(), ex) });
            }
        }

        //private void CargarDatos()
        //{
        //    List<SelectListItem> items = new List<SelectListItem>();
        //    foreach (var item in _utilidadRepository.GetUnidadesMedida())
        //    {
        //        items.Add(new SelectListItem
        //        {
        //            Text = item.Simbolo + " - " + item.Comentario,
        //            Value = item.Simbolo,
        //        });
        //    }

        //    ViewData["unidadMeida"] = new SelectList(items, "Value", "Text");

        //}
    }
}
