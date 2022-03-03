using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using FRIO.MAR.APPLICATION.CORE.Models;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class VentasDomainService : IVentasDomainService
    {
        private readonly IVentasRepository _ventasRepository;

        public VentasDomainService(IVentasRepository ventasRepository)
        {
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
                factura = _ventasRepository.GetFactura(Id, model.EstadoFactura);
                if (factura is null)
                {
                    esNuevo = true;
                    factura = new Factura();
                }
                factura.FechaEmision = model.FechaEmision;
                factura.SucursalId = model.SucursalId;

                factura.ClienteId = model.Cliente.ClienteId;
                factura.Identificacion = model.Cliente.Identificacion;
                factura.CorreoElectronico = model.Cliente.CorreoCliente;
                factura.RazonSocial = model.Cliente.RazonSocial;
                factura.NombreComercial = model.Cliente.NombreComercial;
                factura.Telefono = model.Cliente.Telefono;

                factura.FechaModificacion = Utilities.Utilidades.GetHoraActual();
                factura.NumeroDocumento = "";
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
                    SucursalId = 0,
                    BodegaId = 0,
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

                if (esNuevo)
                {
                    factura.FechaCreacion = Utilities.Utilidades.GetHoraActual();
                }

                factura.Estado = model.EstadoFactura;

                if (esNuevo)
                {
                    _ventasRepository.Add(factura);
                }
                else
                {
                    _ventasRepository.Update(factura);
                }

                
                responseDto.Estado = _ventasRepository.Save() > 0;
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
