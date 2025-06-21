using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace KProyecto.Services
{
    public class Utilitarios
    {
        public bool EnviarCorreo(string destinatario, string mensaje, string asunto)
        {
            try
            {
                var remitente = ConfigurationManager.AppSettings["CorreoRemitente"];
                var contrasena = ConfigurationManager.AppSettings["CorreoPassword"];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(remitente);
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = mensaje;
                mail.IsBodyHtml = true;

                // Para Office 365
                SmtpClient smtp = new SmtpClient("smtp.office365.com", 587);

                // Para Gmail (descomenta si usas Gmail en vez de Office365)
                //SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

                smtp.Credentials = new NetworkCredential(remitente, contrasena);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                return true;

                //El servidor SMTP requiere una conexión segura o el cliente no se autenticó. La respuesta del servidor fue: 5.7.0 Authentication Required. For more information, go to
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}