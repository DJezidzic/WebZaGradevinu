using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebZaGradevinu.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace WebZaGradevinu.Services
{
    public class HomeService
    {
        private readonly WebAppDbContext _dbcontext;
        private readonly IOptions<Security> _config;  

        public HomeService(WebAppDbContext _db, IOptions<Security> config)
        {
            _dbcontext = _db;
            _config = config;
        }
        public async Task SaveContactForm(ContactForm contact)
        {
            if (contact != null)
            {
                try
                {
                    await _dbcontext.ContactForms.AddAsync(contact);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task OnEmailSend(ContactForm contact)
        {
            if(contact != null)
            {
                var security = _config.Value;
                var ime = contact.Ime;
                var email = contact.Email;
                var poruka = contact.Poruka;
                var tiporuke = contact.TipUpita;
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress(security.Email);
                message.Subject = tiporuke;
                message.Body = string.Format(body, ime, email, poruka);
                message.IsBodyHtml = true;
                var smtp = new SmtpClient();
                NetworkCredential credential = new NetworkCredential
                {
                    UserName = security.Email,
                    Password = security.Password
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp-mail.outlook.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
        }

    }
}
