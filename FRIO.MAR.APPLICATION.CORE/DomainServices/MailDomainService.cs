using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class MailDomainService : IMailDomainService
    {
        private readonly IEnvioMail _envioMail;

        public MailDomainService(IEnvioMail envioMail)
        {
            _envioMail = envioMail;
        }

        public MethodResponseDto EnviarMail(MailDto mailDto)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {
                switch (mailDto.Tipo)
                {
                    case Contants.TipoMail.Bienvenida:
                        mailDto.Asunto = "Bienvenido";
                        mailDto.Mensaje = "Bienvenido estimado usuario. <br> usuario: Username <br> password: ";

                        break;
                    case Contants.TipoMail.RecuperarContraseña:
                        mailDto.Asunto = "Recuperacion de contraseña";
                        mailDto.Mensaje = "";
                        break;
                    default:
                        mailDto.Asunto = "";
                        mailDto.Mensaje = "";
                        break;
                }
                
                _envioMail.EnviarCorreo(mailDto.Correos, mailDto.Asunto, mailDto.Mensaje, true);
            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }
    }
}
