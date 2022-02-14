namespace FRIO.MAR.APPLICATION.CORE.DTOs.Repositories
{
    public sealed class VentanaLoginQueryDto
    {
        public long IdPermiso { get; set; }
        public long Codigo { get; set; }
        public long? IdPadre { get; set; }
        public string Url { get; set; }
        public string Icono { get; set; }
        public string NombreAbreviado { get; set; }
        public string Rol { get; set; }
        public long IdRol { get; set; }
    }
}
