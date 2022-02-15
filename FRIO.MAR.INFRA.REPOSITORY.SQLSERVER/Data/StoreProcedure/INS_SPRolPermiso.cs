using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FRIO.MAR.INFRA.REPOSITORY.SQLSERVER.Data
{
    public partial class SIFMContext
    {
        internal short AsignarVentanas(long IdRol, string IdPermisos, long UsuarioAuditoria)
        {
            var param = new List<SqlParameter>();
            param.Add(new SqlParameter("@p" + param.Count, IdRol));
            param.Add(new SqlParameter("@p" + param.Count, IdPermisos));
            param.Add(new SqlParameter("@p" + param.Count, UsuarioAuditoria));
            param.Add(new SqlParameter() { ParameterName = "@p" + param.Count, SqlDbType = SqlDbType.SmallInt, Direction = ParameterDirection.Output });
            string commandText = "INS_RolPermiso ";
            for (var i = 0; i < param.Count - 1; i++) commandText += $"@p{i},";
            commandText += $"@p{param.Count - 1} OUTPUT";
            Database.ExecuteSqlRaw(commandText, param);
            return Convert.ToInt16(param.ElementAt(param.Count - 1).Value);
        }
    }
}
