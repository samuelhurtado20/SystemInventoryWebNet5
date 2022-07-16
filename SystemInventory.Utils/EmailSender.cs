using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SistemaInventario.Utilidades
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();

            //return Execute(subject, htmlMessage, email);
        }

        public Task Execute(string subject, string mensaje, string email)
        {
            MailMessage mm = new();
            mm.To.Add(email);
            mm.Subject = subject;
            mm.Body = mensaje;
            mm.From = new MailAddress("tu_email@email.com");
            mm.IsBodyHtml = true;

            SmtpClient smtp = new("smtp.sendgrid.net")
            {
                Port = 587,
                UseDefaultCredentials = true,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("apikey", "TU_CLAVE_API_DE_SENDGRID.COM")
            };

            return smtp.SendMailAsync(mm);
        }
    }
}
