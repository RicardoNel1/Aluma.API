
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using FileStorageService;
using JwtService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aluma.API.Helpers
{
    public class MailSender
    {
        private readonly AlumaDBContext _context;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IWebHostEnvironment _host;
        DocumentHelper _dh;
        MailServerSettingsDto mailSettings;
        SystemSettingsDto systemSettings;

        public MailSender(AlumaDBContext context, IConfiguration config, IFileStorageRepo fileStorage, IWebHostEnvironment host)
        {
            _context = context;
            _config = config;
            _fileStorageRepo = fileStorage;
            _host = host;
            _dh = new DocumentHelper(_context, _config, _fileStorageRepo, _host);
            mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();
            systemSettings = _config.GetSection("SystemSettings").Get<SystemSettingsDto>();
        }

        public async void SendNewApplicationEmail(ClientModel client, string productName)
        {

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

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}NewApplication.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";
                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, client.User.FirstName + " " + client.User.LastName, productName);

                message.Body = bb.HtmlBody;

                //message.Body = "A new application has been submitted on the client portal by " + client.User.FirstName + " " + client.User.LastName + ". Contact number: " + client.User.MobileNumber + ".  Email: " + client.User.Email;

                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
                //};

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
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

                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
                //};

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
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



        public async Task SendClientWelcomeEmail(ClientModel client)
        {
            UserMail um = new UserMail()
            {
                Email = client.User.Email,
                Name = client.User.FirstName + " " + client.User.LastName,
                Subject = "Aluma Capital: Client welcome letter for " + client.User.FirstName + " " + client.User.LastName,
                Template = "ClientWelcome"
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
                //message.To.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";

                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, client.User.FirstName);

                message.Body = bb.HtmlBody;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };

                //var smtpClient = new SmtpClient

                //{
                //    Host = "smtp.office365.com",
                //    Port = 587,
                //    UseDefaultCredentials = false,
                //    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password, "aluma.co.za"),
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    EnableSsl = true,
                //    TargetName = "STARTTLS/smtp.office365.com"

                //};



                smtpClient.Send(message);

                message.Dispose();
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

        public async Task SendAdvisorWelcomeEmail(AdvisorModel advisor)
        {
            UserMail um = new UserMail()
            {
                Email = advisor.User.Email,
                Name = advisor.User.FirstName + " " + advisor.User.LastName,
                Subject = "Aluma Capital: Advisor welcome letter for " + advisor.User.FirstName + " " + advisor.User.LastName,
                Template = "AdvisorWelcome"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true,

                };

                message.To.Add(new MailAddress(advisor.User.Email));
                //message.To.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-private-equity.jpg";
                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, advisor.User.FirstName, advisor.User.FirstName, advisor.User.LastName, advisor.User.Email, advisor.User.MobileNumber, advisor.User.Email, $"Aluma{advisor.User.FirstName.Trim()}");

                message.Body = bb.HtmlBody;


                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };

                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.aluma.co.za",
                //    Port = 587,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                //    Timeout = 1000000
                //};

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

        public async Task SendForgotPasswordMail(UserModel user)
        {
            JwtRepo jwt = new JwtRepo();
            string key = _config.GetSection("SystemSettings").Get<SystemSettingsDto>().EncryptionKey;
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
            UserMail um = new UserMail()
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                Subject = "Aluma Capital: Forgot Password",
                Template = "ForgotPassword"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(user.Email));
                //message.To.Add(new MailAddress("johan@fintegratetech.co.za"));
                //message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }


                string token = jwt.CreateJwtToken(user.Id, user.Role, jwtSettings.LifeSpan);

                string encryptedUserId = Base64UrlEncoder.Encode(UtilityHelper.EncryptString(key, user.Id.ToString()));
                string url = string.Empty;
                if (user.Role == RoleEnum.Client || user.Role == RoleEnum.Guest)
                {
                    if (systemSettings.ApiUrl != "https://adminapi.aluma.co.za/")
                    {
                        url = $"{systemSettings.FrontendUrl}client/auth/reset-password?id={encryptedUserId}&token={token}";
                    }
                    else
                    {
                        url = $"https://clientportal.aluma.co.za/auth/reset-password?id={encryptedUserId}&token={token}";
                    }
                }
                else
                {
                    if (systemSettings.ApiUrl != "https://adminapi.aluma.co.za/")
                    {
                        url = $"{systemSettings.FrontendUrl}advisor/auth/reset-password?id={encryptedUserId}&token={token}";
                    }
                    else
                    {
                        url = $"https://adminportal.aluma.co.za/auth/reset-password?id={encryptedUserId}&token={token}";
                    }
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-investments.jpg";
                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, user.FirstName, url);

                message.Body = bb.HtmlBody;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };

                //var smtpClient = new SmtpClient
                //{
                //    Host = "smtp.office365.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                //    Timeout = 1000000
                //};

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