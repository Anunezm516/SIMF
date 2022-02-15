using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.DomainServices
{
    public class MailDomainService : IMailDomainService
    {
        public MethodResponseDto EnviarMail(MailDto mailDto)
        {
            MethodResponseDto responseDto = new MethodResponseDto();

            try
            {

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
