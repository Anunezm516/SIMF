using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.DomainService;
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
                //EstadoFactura[] Estados = new EstadoFactura[] { EstadoFactura.Facturado };

                //List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
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
                //EstadoFactura[] Estados = new EstadoFactura[] { EstadoFactura.Facturado };

                //List<VentasDomainServiceResultDto> facturas = new List<VentasDomainServiceResultDto>();
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
    }
}
