using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IMailDomainService
    {
        MethodResponseDto EnviarMail(MailDto mailDto);
    }
}
