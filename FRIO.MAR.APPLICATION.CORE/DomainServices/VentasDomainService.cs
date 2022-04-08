using APLICATIONCORE_GSEDOCPYME.Interfaces.General;
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.DTOs.Services;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class VentasDomainService : IVentasDomainService
    {
        private readonly IEscribirArchivoService _escribirArchivoService;
        private readonly IAccountRepository _accountRepository;
        private readonly IStorageService _storageService;
        private readonly IClienteRepository _clienteRepository;
        private readonly IVentasRepository _ventasRepository;

        public VentasDomainService(
            IEscribirArchivoService escribirArchivoService,
            IAccountRepository accountRepository,
            IStorageService storageService,
            IClienteRepository clienteRepository,
            IVentasRepository ventasRepository)
        {
            _escribirArchivoService = escribirArchivoService;
            _accountRepository = accountRepository;
            _storageService = storageService;
            _clienteRepository = clienteRepository;
            _ventasRepository = ventasRepository;
        }

        public MethodResponseDto ConsultarFactura(long Id, EstadoFactura Estado)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _ventasRepository.GetFactura(Id, (Estado));
                if (result != null)
                {
                    responseDto.Data = new FacturaModel(result);
                    responseDto.Estado = true;
                }
                else
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_ANONIMA;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                }
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;

        }

        public MethodResponseDto ListarFacturas(EstadoFactura[] Estados)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _ventasRepository.GetFacturas(Estados);

                responseDto.Data = result.Select(c => new VentasDomainServiceResultDto(c)).ToList();
                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
            
        }

        public MethodResponseDto GuardarFactura(FacturaModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                bool esNuevo = false;
                Factura factura = null;

                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));
                if (model.EstadoFactura == EstadoFactura.Facturado)
                {
                    factura = _ventasRepository.GetFacturaNoIgualEstado(Id, EstadoFactura.Facturado);
                }
                else
                {
                    factura = _ventasRepository.GetFactura(Id, model.EstadoFactura);
                }

                if (factura is null)
                {
                    esNuevo = true;
                    factura = new Factura();
                }

                var cliente = _clienteRepository.Get(model.Cliente.ClienteId);
                if (cliente == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_CLIENTE_ANONIMO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                var facturador = _accountRepository.GetFacturador();
                if (facturador == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_ANONIMA;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (factura.FacturaDetalle != null && factura.FacturaDetalle.Any())
                {
                    factura.FacturaDetalle.Clear();
                    _ventasRepository.Save();
                }

                if (factura.FacturaFormaPago != null && factura.FacturaFormaPago.Any())
                {
                    factura.FacturaFormaPago.Clear();
                    _ventasRepository.Save();
                }

                if (factura.FacturaAdjunto != null && factura.FacturaAdjunto.Any())
                {
                    factura.FacturaAdjunto.Clear();
                }
                else
                {
                    factura.FacturaAdjunto = new List<FacturaAdjunto>();
                }

                if (model.Adjunto != null && model.Adjunto.Any())
                {
                    model.Adjunto.ForEach(c =>
                    {
                        string ruta = Path.Combine("wwwroot", "Ventas", "Factura", c.Identificador + "-" + c.Nombre);
                        string mensaje = "";
                        if (c.Adjunto != null)
                        {
                            _storageService.GuardarArchivo(new System.IO.MemoryStream(c.Adjunto ?? new byte[] { }), ruta, ref mensaje);
                        }
                        else
                        {
                            ruta = c.Ruta;
                        }

                        factura.FacturaAdjunto.Add(new FacturaAdjunto
                        {
                            Estado = true,
                            Nombre = c.Nombre,
                            Ruta = ruta,
                            ImagenBase64 = c.Identificador
                        });
                    });
                }

                factura.SucursalId = model.SucursalId;
                factura.ClienteId = model.Cliente.ClienteId;
                factura.Identificacion = model.Cliente.Identificacion;
                factura.CorreoElectronico = model.Cliente.CorreoCliente;
                factura.RazonSocial = model.Cliente.RazonSocial;
                factura.NombreComercial = model.Cliente.NombreComercial;
                factura.Telefono = model.Cliente.Telefono;
                factura.Direccion = cliente.Direccion;

                factura.FechaModificacion = Utilities.Utilidades.GetHoraActual();
                factura.FechaEmision = new DateTime(model.FechaEmision.Year, model.FechaEmision.Month, model.FechaEmision.Day, factura.FechaModificacion.Hour, factura.FechaModificacion.Minute, factura.FechaModificacion.Second);
                factura.FechaEntrega = model.FechaEntrega;

                factura.PuntoEmision = facturador.PuntoEmision;
                factura.Sucursal = facturador.Sucursal;
                factura.Secuencial = model.Secuencial;

                factura.NumeroDocumento = $"{factura.PuntoEmision}-{factura.Sucursal}-{factura.Secuencial.ToString().PadLeft(10, '0')}";
                factura.ValorTotal = model.Detalle.Sum(c => c.TotalDec);

                factura.FacturaDetalle = model.Detalle.Select(c => new FacturaDetalle
                {
                    Codigo = c.Codigo,
                    CodigoSeguimiento = c.CodigoSeguimiento,
                    Descripcion = c.Descripcion,
                    Cantidad = c.CantidadDec,
                    IvaPorcentaje = c.IvaPorcentajeDec,
                    IvaValor = c.IvaValorDec,
                    ProductoId = c.ProductoId,
                    Subtotal = c.SubTotalDec,
                    Total =c.TotalDec,
                    PrecioUnitario = c.PrecioUnitarioDec,
                    SucursalId = c.SucursalId,
                    BodegaId = c.BodegaId,
                    MesesGarantia = c.MesesGarantia,
                    UnidadMedida = c.UnidadMedida,
                    ProductoClienteId = c.ProductoClienteId,
                    TipoProducto = c.TipoProducto
                }).ToList();

                factura.FacturaFormaPago = model.FormaPago.Select(c => new FacturaFormaPago
                {
                    FormaPagoId = 0,

                    CodigoFormaPago = c.FormaPagoCodigo,
                    DescripcionFormaPago = c.FormaPagoDescripcion,
                    Observacion = c.Observacion,
                    Valor = c.ValorDec,
                }).ToList();

                factura.Ip = model.Ip;
                factura.IdUsuario = model.Usuario;
                factura.Estado = model.EstadoFactura;

                if (esNuevo)
                {
                    factura.FechaCreacion = Utilities.Utilidades.GetHoraActual();
                }

                if (esNuevo)
                {
                    
                    _ventasRepository.Add(factura);
                }
                else
                {
                    _ventasRepository.Update(factura);
                }

                responseDto.Data = factura;
                responseDto.Estado = _ventasRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto EliminarFactura(long Id)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var factura = _ventasRepository.GetFacturaNoIgualEstado(Id, EstadoFactura.Facturado);
                if (factura == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_ANONIMA;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                factura.Estado = EstadoFactura.Eliminado;
                factura.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _ventasRepository.Update(factura);

                responseDto.Estado = _ventasRepository.Save() > 0;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
        
        public MethodResponseDto ValidarFactura(FacturaModel model)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                if (model.Detalle == null || !model.Detalle.Any())
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_DETALLE;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (model.FormaPago == null || !model.FormaPago.Any())
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_FORMA_PAGO;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                if (model.EstadoFactura == EstadoFactura.Facturado)
                {
                    if (decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.Totales.TotalAbono)) < decimal.Parse(Utilities.Utilidades.DepuraStrConvertNum(model.Totales.Total)))
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_FACTURA_MONTO_PAGAR;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }

                    if (model.Secuencial <= 0)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_FACTURA_SECUENCIAL;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }

                    var facturador = _accountRepository.GetFacturador();
                    if (facturador == null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_FACTURA_ANONIMA;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        return responseDto;
                    }

                    var factura = _ventasRepository.GetFirstOrDefault(x => x.Sucursal == facturador.Sucursal && x.PuntoEmision == facturador.PuntoEmision && x.Secuencial == model.Secuencial);
                    if (factura != null)
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_FACTURA_REGISTRADA;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                        responseDto.Mensaje += $" El número de documento {factura.NumeroDocumento} fue registrado en la fecha {factura.FechaEmision}";
                        return responseDto;
                    }
                }

                responseDto.Estado = true;
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public MethodResponseDto DescargarAdjuntos(long FacturaId)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                List<ArchivoResponseDTO> archivos = new List<ArchivoResponseDTO>();
                string NombreArchivo = Guid.NewGuid().ToString();
                var adjuntos = _ventasRepository.GetAdjuntosFactura(FacturaId);
                if (adjuntos != null && adjuntos.Any())
                {
                    foreach (var item in adjuntos)
                    {
                        if (!string.IsNullOrEmpty(item.Ruta))
                        {
                            try
                            {
                                var fileStream = System.IO.File.ReadAllBytes(item.Ruta);
                                archivos.Add(new ArchivoResponseDTO { Archivo = fileStream, NombreArchivo = item.Nombre });

                            }
                            catch (Exception ex)
                            {

                            }
                        }

                    }

                    string directorioDocumentosPyme = @"wwwroot\reportes\";
                    string subdirectorioAdjuntosRecibidos = @"adjuntos_ventas\";
                    string carpetaAzipear = NombreArchivo;
                    EscribirZipResponseDTO responseEscribirDTO = _escribirArchivoService.ResponseEscribirZip(archivos, directorioDocumentosPyme, subdirectorioAdjuntosRecibidos, carpetaAzipear);
                    if (responseEscribirDTO.EstadoSolicitud && responseEscribirDTO.TieneZip)
                    {
                        if (responseEscribirDTO.LenghtBytes < 30)
                        {
                            responseDto.Mensaje = "--ZIP_VACIO";
                            return responseDto;
                            //return Json("--ZIP_VACIO");
                        }

                        string file = responseEscribirDTO.RutaLocal;
                        responseDto.Estado = true;
                        responseDto.Data = file;
                        return responseDto;
                    }
                    else
                    {
                        responseDto.Mensaje = "--NO_ZIP";
                        return responseDto;
                    }
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
