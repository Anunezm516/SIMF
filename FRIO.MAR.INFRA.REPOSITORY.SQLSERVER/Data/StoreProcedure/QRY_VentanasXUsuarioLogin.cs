using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal List<VentanaLoginQueryDto> ConsultarVentanasXUsuarioLogin(long IdUsuario)
        {
            return VentanaLoginInternoQueryDto.FromSqlRaw("QRY_VentanasXUsuarioLogin @p0", IdUsuario).ToList();
        }
    }
}
