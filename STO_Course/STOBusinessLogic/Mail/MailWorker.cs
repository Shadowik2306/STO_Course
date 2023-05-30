using STOContracts.BusinessLogicsContracts;
using STOContracts.BindingModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Pop3;
using MailKit.Security;
using DocumentFormat.OpenXml.EMMA;


namespace STOBusinessLogic.Mail
{
    public class MailWorker
    {
        
        private string _mailLogin = string.Empty;

        private string _mailPassword = string.Empty;

        private string _smtpClientHost = string.Empty;

        private int _smtpClientPort;

        private readonly ILogger logger;

        public MailWorker(ILogger<MailWorker> logger)
        {
            this.logger = logger;
        }

        public void MailConfig(MailConfigBindingModel config)
        {
            _mailLogin = config.MailLogin;
            _mailPassword = config.MailPassword;
            _smtpClientHost = config.SmtpClientHost;
            _smtpClientPort = config.SmtpClientPort;
        }

        public async void SendMailAsync(MailSendInfoBindingModel info)
        {
            using var objMailMessage = new MailMessage();
            using var objSmtpClient = new SmtpClient(_smtpClientHost, _smtpClientPort);

            try
            {
                objMailMessage.From = new MailAddress(_mailLogin);
                objMailMessage.To.Add(new MailAddress(info.MailAddress));
                objMailMessage.Subject = info.Subject;
                objMailMessage.Body = info.Text;
                objMailMessage.SubjectEncoding = Encoding.UTF8;
                objMailMessage.BodyEncoding = Encoding.UTF8;

                MemoryStream ms = new(info.File);

                objMailMessage.Attachments.Add(new Attachment(ms, "Отчёт_по_ТО.pdf", "application/pdf"));

                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(_mailLogin, _mailPassword);

                await Task.Run(() => objSmtpClient.Send(objMailMessage));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
