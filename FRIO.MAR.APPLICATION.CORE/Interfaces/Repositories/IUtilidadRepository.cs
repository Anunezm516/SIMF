using FRIO.MAR.APPLICATION.CORE.Entities;
using System.Collections.Generic;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories
{
    public interface IUtilidadRepository
    {
        void InicializarDb();
        Parametros GetParametro(string Codigo);
        List<Rol> GetRolesPrincipales();
        bool AddRol(int Id, string Nombre);
        void AgregarUsuarioAdministrador();
        List<Impuesto> GetImpuestos(int TipoImpuesto);
        List<TipoIdentificacion> GetTipoIdentificaciones();
        List<UnidadMedida> GetUnidadesMedida();
        string GenerarCodigoSeguimientoProducto(long ProductoClienteId);
    }
}
    