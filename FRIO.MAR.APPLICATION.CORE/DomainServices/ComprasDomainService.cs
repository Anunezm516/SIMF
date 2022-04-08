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
    public class ComprasDomainService : IComprasDomainService
    {
        private readonly IEscribirArchivoService _escribirArchivoService;
        private readonly IStorageService _storageService;
        private readonly IComprasRepository _comprasRepository;

        public ComprasDomainService(
            IEscribirArchivoService escribirArchivoService,
            IStorageService storageService,
            IComprasRepository comprasRepository
            )
        {
            _escribirArchivoService = escribirArchivoService;
            _storageService = storageService;
            _comprasRepository = comprasRepository;
        }

        public MethodResponseDto ConsultarFactura(long Id, EstadoFactura Estado)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                var result = _comprasRepository.GetFactura(Id, (Estado));
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
                var result = _comprasRepository.GetFacturas(Estados);

                responseDto.Data = result.Select(c => new ComprasDomainServiceResultDto(c)).ToList();
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
                CFactura factura = null;

                long Id = long.Parse(Utilities.Crypto.DescifrarId(model.Id));
                if (model.EstadoFactura == EstadoFactura.Facturado)
                {
                    factura = _comprasRepository.GetFacturaNoIgualEstado(Id, EstadoFactura.Facturado);
                }
                else
                {
                    factura = _comprasRepository.GetFactura(Id, model.EstadoFactura);
                }

                if (factura is null)
                {
                    esNuevo = true;
                    factura = new CFactura();
                }

                if (factura.FacturaDetalle != null && factura.FacturaDetalle.Any())
                {
                    factura.FacturaDetalle.Clear();
                    _comprasRepository.Save();
                }

                if (factura.FacturaFormaPago != null && factura.FacturaFormaPago.Any())
                {
                    factura.FacturaFormaPago.Clear();
                    _comprasRepository.Save();

                }

                factura.FechaEmision = model.FechaEmision;
                factura.SucursalId = model.SucursalId;

                factura.ProveedorId = model.Cliente.ClienteId;
                factura.Identificacion = model.Cliente.Identificacion;
                factura.CorreoElectronico = model.Cliente.CorreoCliente;
                factura.RazonSocial = model.Cliente.RazonSocial;
                factura.NombreComercial = model.Cliente.NombreComercial;
                factura.Telefono = model.Cliente.Telefono;

                factura.FechaModificacion = Utilities.Utilidades.GetHoraActual();
                factura.NumeroDocumento = model.NumeroDocumento;
                factura.ValorTotal = model.Detalle.Sum(c => c.TotalDec);

                factura.FacturaDetalle = model.Detalle.Select(c => new CFacturaDetalle
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
                    SucursalId = 0,
                    BodegaId = 0,
                }).ToList();

                factura.FacturaFormaPago = model.FormaPago.Select(c => new CFacturaFormaPago
                {
                    FormaPagoId = 0,

                    CodigoFormaPago = c.FormaPagoCodigo,
                    DescripcionFormaPago = c.FormaPagoDescripcion,
                    Observacion = c.Observacion,
                    Valor = c.ValorDec,
                }).ToList();

                if (factura.FacturaAdjunto != null && factura.FacturaAdjunto.Any())
                {
                    factura.FacturaAdjunto.Clear();
                }
                else
                {
                    factura.FacturaAdjunto = new List<CFacturaAdjunto>();
                }

                if (model.Adjunto != null && model.Adjunto.Any())
                {
                    model.Adjunto.ForEach(c =>
                    {
                        string ruta = Path.Combine("wwwroot", "Compras", "Factura", c.Identificador + "-" + c.Nombre);
                        string mensaje = "";
                        if (c.Adjunto != null)
                        {
                            _storageService.GuardarArchivo(new System.IO.MemoryStream(c.Adjunto ?? new byte[] { }), ruta, ref mensaje);
                        }
                        else
                        {
                            ruta = c.Ruta;
                        }

                        factura.FacturaAdjunto.Add(new CFacturaAdjunto
                        {
                            Estado = true,
                            Nombre = c.Nombre,
                            Ruta = ruta,
                            ImagenBase64 = c.Identificador
                        });
                    });
                }

                factura.Ip = model.Ip;
                factura.IdUsuario = model.Usuario;
                factura.Estado = model.EstadoFactura;

                if (esNuevo)
                {
                    factura.FechaCreacion = Utilities.Utilidades.GetHoraActual();
                }

                if (esNuevo)
                {

                    _comprasRepository.Add(factura);
                }
                else
                {
                    _comprasRepository.Update(factura);
                }

                
                responseDto.Estado = _comprasRepository.Save() > 0;
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
                var factura = _comprasRepository.GetFacturaNoIgualEstado(Id, EstadoFactura.Facturado);
                if (factura == null)
                {
                    responseDto.CodigoError = DomainConstants.ERROR_FACTURA_ANONIMA;
                    responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
                    return responseDto;
                }

                factura.Estado = EstadoFactura.Eliminado;
                factura.FechaModificacion = Utilities.Utilidades.GetHoraActual();

                _comprasRepository.Update(factura);

                responseDto.Estado = _comprasRepository.Save() > 0;
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

                    if (string.IsNullOrEmpty(model.NumeroDocumento))
                    {
                        responseDto.CodigoError = DomainConstants.ERROR_FACTURA_NUMERO_DOCUMENTO;
                        responseDto.Mensaje = DomainConstants.ObtenerDescripcionError(responseDto.CodigoError);
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
                var adjuntos = _comprasRepository.GetAdjuntosFactura(FacturaId);
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
                    string subdirectorioAdjuntosRecibidos = @"adjuntos_compras\";
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
