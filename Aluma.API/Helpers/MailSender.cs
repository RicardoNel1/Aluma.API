
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Aluma.API.Helpers
{
    public class MailSender
    {
        private readonly AlumaDBContext _context;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IWebHostEnvironment _host;
        DocumentHelper _dh;

        public MailSender(AlumaDBContext context, IConfiguration config, IFileStorageRepo fileStorage, IWebHostEnvironment host)
        {
            _context = context;
            _config = config;
            _fileStorageRepo = fileStorage;
            _host = host;
            _dh = new DocumentHelper(_context, _config, _fileStorageRepo, _host);
        }

        public async void SendNewApplicationEmail(ClientModel client)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = "New Aluma Application: " + client.User.FirstName + " " + client.User.LastName,
                    IsBodyHtml = true
                };

                //message.To.Add(new MailAddress("sales@aluma.co.za"));
                message.To.Add(new MailAddress("system@aluma.co.za"));


                message.Body = "A new application has been submitted on the client portal by " + client.User.FirstName + " " + client.User.LastName + ". Contact number: " + client.User.MobileNumber + ".  Email: " + client.User.Email;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


        public async void SendApplicationDocumentsToBroker(ApplicationModel app, AdvisorModel advisor, ClientModel client)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            UserMail um = new UserMail()
            {
                Email = advisor.User.Email,
                Name = client.User.FirstName + " " + client.User.LastName,
                Subject = "Aluma Capital: Application Complete " + client.User.FirstName + " " + client.User.LastName,
                Template = "ApplicationComplete"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(client.User.Email));
                message.To.Add(new MailAddress(advisor.User.Email));
                //message.Bcc.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == client.UserId && !d.IsSigned).ToList();

                foreach (var doc in userDocs)
                {
                    byte[] data = await _dh.GetDocumentDataAsync(doc.URL, doc.Name);
                    var stream = new MemoryStream(data);

                    var attachment = new Attachment(stream, doc.Name);

                    message.Attachments.Add(attachment);
                };

                foreach (var doc in app.Documents)
                {
                    byte[] data = await _dh.GetDocumentDataAsync(doc.URL, doc.Name);
                    var stream = new MemoryStream(data);

                    var attachment = new Attachment(stream, doc.Name);

                    message.Attachments.Add(attachment);
                };

                message.Body = "Application Completed: " + um.Name;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


        public async void SendWelcomeEmail(AdvisorModel advisor)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            UserMail um = new UserMail()
            {
                Email = advisor.User.Email,
                Name = advisor.User.FirstName + " " + advisor.User.LastName,
                Subject = "Aluma Capital: Welcome letter for " + advisor.User.FirstName + " " + advisor.User.LastName,
                Template = "AdvisorWelcome"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(advisor.User.Email));
                //message.Bcc.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                message.Body = "Application Completed: " + um.Name;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}