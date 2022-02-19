using FRIO.MAR.APPLICATION.CORE.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.AppServices
{
    public interface INotificacionAppService
    {
        MethodResponseDto GetNotificaciones(long IdUsuario, bool Leidas = false);
        MethodResponseDto MarcarLeido(long Id);
    }
}
