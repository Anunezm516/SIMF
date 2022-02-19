using FRIO.MAR.APPLICATION.CORE.DTOs;
using FRIO.MAR.APPLICATION.CORE.DTOs.AppServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.APPLICATION.CORE.Interfaces.DomainServices
{
    public interface IMailDomainService
    {
        MethodResponseDto EnviarMail(MailDto mailDto);
        MethodResponseDto EnviarMailRecuperarPassword(string Destinatario, string UsuarioNombreCompleto, string Password);
        MethodResponseDto EnviarMailBienvenida(string Destinatario, string UsuarioNombreCompleto, string Usuario, string Password);
        bool GuardarMail(MailDto mail);
    }
}
