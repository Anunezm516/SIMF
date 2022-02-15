using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal List<IdQueryDto> ConsultarRolVentanas(short IdRol)
        {
            return IdQueryDto.FromSqlRaw("QRY_VentanasXIdRol @p0", IdRol).ToList();
        }
    }
}
