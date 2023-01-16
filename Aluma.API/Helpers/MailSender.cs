
using Aluma.API.Repositories;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using FileStorageService;
using JwtService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Utilities.Net;
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
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";
                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, client.User.FirstName + " " + client.User.LastName, productName);

                message.Body = bb.HtmlBody;

                //message.Body = "A new application has been submitted on the client portal by " + client.User.FirstName + " " + client.User.LastName + ". Contact number: " + client.User.MobileNumber + ".  Email: " + client.User.Email;

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };
                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
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
        public async void SendClientNewApplicationEmail(ClientModel client, string productName)
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
                message.To.Add(new MailAddress(client.User.Email));
                //message.To.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}InvestNowNewApplication.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";
                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, client.User.FirstName, productName);

                message.Body = bb.HtmlBody;

                //message.Body = "A new application has been submitted on the client portal by " + client.User.FirstName + " " + client.User.LastName + ". Contact number: " + client.User.MobileNumber + ".  Email: " + client.User.Email;

                var smtpClient = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };
                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
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
        public async void SendApplicationDocumentsToBroker(ApplicationModel app, AdvisorModel advisor, ClientModel client)
        {
            UserMail um = new()
            {
                Email = advisor.User.Email,
                Name = client.User.FirstName + " " + client.User.LastName,
                Subject = "Aluma Capital - Application Complete: " + client.User.FirstName + " " + client.User.LastName,
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
                message.Bcc.Add(new MailAddress("carolien@aluma.co.za"));

                List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == client.UserId && d.IsSigned).ToList();

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
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };

                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
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
        public async Task SendClientWelcomeEmail(UserModel user)
        {
            UserMail um = new()
            {
                Email = user.Email,
                Name = user.FirstName + " " + user.LastName,
                Subject = "Aluma Capital: Client welcome letter for " + user.FirstName + " " + user.LastName,
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

                message.To.Add(new MailAddress(user.Email));
                //message.To.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";

                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, user.FirstName);

                message.Body = bb.HtmlBody;

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
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };



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
        public async Task SendInvestNowClientWelcomeEmail(ClientModel client)
        {
            UserMail um = new()
            {
                Email = client.User.Email,
                Name = client.User.FirstName + " " + client.User.LastName,
                Subject = "Aluma Capital: Client welcome letter for " + client.User.FirstName + " " + client.User.LastName,
                Template = "InvestNowClientWelcome"
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
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";

                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, client.User.FirstName);

                message.Body = bb.HtmlBody;

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
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };



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
            UserMail um = new()
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
                MimeKit.BodyBuilder bb = new();

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
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };

                //var smtpClient = new SmtpClient
                //{
                //    Host = "mail.administr8it.co.za",
                //    Port = 25,
                //    EnableSsl = false,
                //    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                //    Timeout = 1000000
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

        public async Task SendWeeklyFNAReport(List<CompletedFNADto> dataList)
        {
            UserMail um = new()
            {
                Email = "justin@fintegratetech.co.za",
                Name = "Test",
                Subject = "Test",
                Template = "FNAWeeklyReport"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true,

                };

                message.To.Add(new MailAddress("justin@fintegratetech.co.za"));
                //message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-private-equity.jpg";

                List<string> clientList = dataList.Select(x => x.Client).ToList();

                var listItems = "";

                for (var i = 0; i < clientList.Count; i++)
                {
                    listItems += "<li>" + clientList[i] + "</li>";
                }



                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, "TestName", listItems);


                message.Body = bb.HtmlBody;


                var smtpClient = new SmtpClient

                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };

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
                NLog.LogManager.Shutdown();
            }
        }
        public async Task SendForgotPasswordMail(UserModel user)
        {
            JwtRepo jwt = new();
            string key = _config.GetSection("SystemSettings").Get<SystemSettingsDto>().EncryptionKey;
            var jwtSettings = _config.GetSection("JwtSettings").Get<JwtSettingsDto>();
            UserMail um = new()
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
                MimeKit.BodyBuilder bb = new();

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
                    if (systemSettings.ApiUrl != "https://adminapi.aluma.co.za")
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
                    if (systemSettings.ApiUrl != "https://adminapi.aluma.co.za")
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
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };



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
        public async Task SendOTPEmail(UserDto user, string otp)
        {

            OtpMail um = new()
            {
                Email = user.Email,
                Name = user.FirstName,
                Otp = otp,
                Subject = "Aluma Capital: OTP",
                Template = "OTP"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(um.Email));

                char slash = Path.DirectorySeparatorChar;
                string templatePath = $"{_host.WebRootPath}{slash}html{slash}{um.Template}.html";

                // Create Body Builder
                MimeKit.BodyBuilder bb = new();

                // Create streamreader to read content of the the given template
                using (StreamReader sr = File.OpenText(templatePath))
                {
                    bb.HtmlBody = sr.ReadToEnd();
                }

                var imgSrc = $"{systemSettings.ApiUrl}{slash}img{slash}email-banner-fixed-income.jpg";

                bb.HtmlBody = string.Format(bb.HtmlBody, imgSrc, um.Name, um.Otp);

                message.Body = bb.HtmlBody;

                var smtpClient = new SmtpClient

                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,

                };



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

    }
}