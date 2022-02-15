namespace FRIO.MAR.APPLICATION.CORE.DTOs.QueryServices
{
    public sealed class LoginInternoQueryDto
    {
        //public long IdCompania { get; set; }
        public long IdUsuario { get; set; }
        public string Usuario { get; set; }
        //public string ClaveEncriptada { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        //public string NitCiaNube { get; set; }
        public bool ForzarCambioClave { get; set; }
        public bool Bloqueado { get; set; }
        public bool Autorizado { get; set; }
    }
}
