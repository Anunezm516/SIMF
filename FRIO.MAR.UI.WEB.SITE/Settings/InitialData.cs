
using FRIO.MAR.APPLICATION.CORE.Constants;
using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Repositories;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRIO.MAR.UI.WEB.SITE.Settings
{
    public class InitialData
    {
        private readonly IUtilidadRepository _utilidadRepository;
        private string mensaje;

        public InitialData(IUtilidadRepository utilidadRepository)
        {
            this._utilidadRepository = utilidadRepository;
        }

        public void Start()
        {
            var settings = GSUtilities.LeerAppSettings<WebSiteSettings>(typeof(Program), ref mensaje);

            //GlobalSettings.EnvioMailClave = GSCrypto.DescifrarClave(_utilidadRepository.GetParametro("MAIL_CLAVE")?.Valor ?? "", DomainConstants.ENCRIPTA_KEY);
            //GlobalSettings.EnvioMailCorreoMostrar = _utilidadRepository.GetParametro("MAIL_CORREO_MOSTRAR")?.Valor ?? "";
            //GlobalSettings.EnvioMailNombreMostrar = _utilidadRepository.GetParametro("MAIL_NOMBRE_MOSTRAR")?.Valor ?? "";
            //GlobalSettings.TelegramToken = _utilidadRepository.GetParametro("TELEGRAM_TOKEN")?.Valor ?? "";

            GlobalSettings.LoginAppNumeroIntentoBloqueo = int.Parse(_utilidadRepository.GetParametro("NUMERO_INTENTOS_BLOQUEO")?.Valor ?? "5");
            GlobalSettings.LoginAppDiasForzarCambioPassword = int.Parse(_utilidadRepository.GetParametro("NUMERO_DIAS_FORZAR_CAMBIO_PASSWORD")?.Valor ?? "90");
            GlobalSettings.NumeroDiasReaperturaTicket = int.Parse(_utilidadRepository.GetParametro("NUMERO_DIAS_REAPERTURA_TICKET")?.Valor ?? "30");
            GlobalSettings.NumeroIntentosEnvioBloqueo = int.Parse(_utilidadRepository.GetParametro("NUMERO_INTENTOS_ENVIO_BLOQUEO")?.Valor ?? "5");


            #region Verificar Existencia de roles
            var roles = _utilidadRepository.GetRolesPrincipales();
            if (roles == null || !roles.Any())
            {
                _utilidadRepository.AddRol(((int)Roles.SuperAdministrador), Roles.SuperAdministrador.ToString());
                _utilidadRepository.AddRol(((int)Roles.Agente), Roles.Agente.ToString());
                _utilidadRepository.AddRol(((int)Roles.Administrador), Roles.Administrador.ToString());
                _utilidadRepository.AddRol(((int)Roles.Seguimiento), Roles.Seguimiento.ToString());
            }
            else
            {
                if (roles.FirstOrDefault(x => x.Nombre == Roles.SuperAdministrador.ToString()) == null)
                {
                    _utilidadRepository.AddRol(((int)Roles.SuperAdministrador), Roles.SuperAdministrador.ToString());
                }

                if (roles.FirstOrDefault(x => x.Nombre == Roles.Administrador.ToString()) == null)
                {
                    _utilidadRepository.AddRol(((int)Roles.Administrador), Roles.Administrador.ToString());
                }

                if (roles.FirstOrDefault(x => x.Nombre == Roles.Agente.ToString()) == null)
                {
                    _utilidadRepository.AddRol(((int)Roles.Agente), Roles.Agente.ToString());
                }

                if (roles.FirstOrDefault(x => x.Nombre == Roles.Seguimiento.ToString()) == null)
                {
                    _utilidadRepository.AddRol(((int)Roles.Seguimiento), Roles.Seguimiento.ToString());
                }
            }
            #endregion

            #region Crear usuario SuperAdministrador
            _utilidadRepository.AgregarUsuarioSuperAdministrador();
            #endregion

            #region Asignar Permisos SuperAdministrador

            #endregion
        }
    }
}
