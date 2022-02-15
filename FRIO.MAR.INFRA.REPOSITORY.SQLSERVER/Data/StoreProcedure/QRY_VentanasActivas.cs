using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal List<PermisosQueryDto> ConsultarVentanasActivas()
        {
            return PermisosQueryDto.FromSqlRaw("QRY_VentanasActivas").ToList();
        }
    }
}
