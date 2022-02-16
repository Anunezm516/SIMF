using FRIO.MAR.APPLICATION.CORE.Contants;
using FRIO.MAR.APPLICATION.CORE.Interfaces.Services;
using FRIO.MAR.APPLICATION.CORE.Parameters;
using GS.TOOLS;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FRIO.MAR.INFRA.SERVICE.MAIL.Services
{
    public class EnvioMail : IEnvioMail
    {
        private readonly SmtpClient cliente;
        private MailMessage email;

        public EnvioMail()
        {
            cliente = new SmtpClient(GlobalSettings.ConfiguracionMailHost, GlobalSettings.ConfiguracionMailPort)
            {
                EnableSsl = GlobalSettings.ConfiguracionMailSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(GlobalSettings.ConfiguracionMailUser, GlobalSettings.ConfiguracionMailPassword)
            };
        }

        public void EnviarCorreo(string destinatario, string asunto, string mensaje, bool esHtlm = false)
        {
            email = new MailMessage(GlobalSettings.ConfiguracionMailUser, destinatario, asunto, mensaje);
            email.IsBodyHtml = esHtlm;
            //email.Bcc = new MailAddressCollection();


            cliente.Send(email);
        }

        public void EnviarCorreo(MailMessage message)
        {
            cliente.Send(message);
        }

        public async Task EnviarCorreoAsync(MailMessage message)
        {
            await cliente.SendMailAsync(message);
        }
    }
}
