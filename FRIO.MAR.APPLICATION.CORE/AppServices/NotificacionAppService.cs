using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Entities;
using FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.AppServices
{
    public class NotificacionAppService : INotificacionAppService
    {
        private readonly INotificacionRepository notificacionRepository;

        public NotificacionAppService(INotificacionRepository notificacionRepository)
        {
            this.notificacionRepository = notificacionRepository;
        }

        public MethodResponseDto GetNotificaciones(long IdUsuario, bool Todo = false, bool Leidas = false)
        {
            MethodResponseDto responseDto = new MethodResponseDto();
            try
            {
                List<NotificacionAppResultDto> notificacionDtos = new List<NotificacionAppResultDto>();
                List<Notificacion> result;

                if (Todo)
                {
                    result = notificacionRepository.Find(x => x.Estado == 1).ToList();
                }
                else
                {
                    result = notificacionRepository.Find(x => x.IdUsuario == IdUsuario && x.Estado == 1 && x.EsNotificacionLeida == Leidas ).ToList();
                }

                responseDto.Data = result.Select(c => new NotificacionAppResultDto 
                {
                    Id = c.IdNotificacion,
                    Titulo = c.Titulo ,
                    Cuerpo = c.Mensaje,
                    TipoNotificacion = (TipoNotificacion)c.TipoNotificacion,
                    Fecha = c.FechaCreacion,
                    NotificacionLeida = c.EsNotificacionLeida
                }).ToList();

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
