using FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal IEnumerable<RolesQueryDto> ConsultaRolesXCompania(long idCompania)
        {
            return RolesQueryDto.FromSqlRaw("QRY_ConsultaSPRol @p0", idCompania).ToList();
        }
    }
}
