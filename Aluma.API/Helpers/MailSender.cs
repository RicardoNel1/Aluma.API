namespace Aluma.API.Helpers
{
    public class MailSender
    {
        //public async Task<string> SendWelcomeEmail(UserModel user)
        //{
        //    var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();
        //    try
        //    {
        //        UserMail um = new UserMail()
        //        {
        //            Email = user.Email,
        //            Name = advisor.User.FirstName + " " + advisor.User.LastName,
        //            Template = "WelcomeToAluma-Advisor"
        //        };

        //        char slash = Path.DirectorySeparatorChar;
        //        string templatePath = $"{_host.WebRootPath}{slash}htmlTemplates{slash}{um.Template}.html";

        //        var systemSettings = _config.GetSection("SystemSettingsDto").Get<SystemSettingsDto>();

        //        // Create Body Builder
        //        MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

        //        // Create streamreader to read content of the the given template
        //        using (StreamReader sr = File.OpenText(templatePath))
        //        {
        //            bb.HtmlBody = sr.ReadToEnd();
        //        }

        //        bb.HtmlBody = string.Format(bb.HtmlBody, advisor.User.FirstName + " " + advisor.User.LastName);

        //        string msg = bb.HtmlBody;

        //        var message = new MailMessage
        //        {
        //            From = new MailAddress(mailSettings.Username),
        //            Subject = "Aluma Capital: Welcome to our team!",
        //            IsBodyHtml = true,
        //            Body = msg.Replace(Environment.NewLine, "<br/>"),
        //            BodyEncoding = System.Text.Encoding.ASCII
        //        };

        //        message.To.Add(new MailAddress(advisor.User.Email));
        //        //message.Bcc.Add(new MailAddress("nadine@aluma.co.za"));

        //        var smtpClient = new SmtpClient
        //        {
        //            Host = "mail.administr8it.co.za",
        //            Port = 587,
        //            EnableSsl = false,
        //            Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
        //            Timeout = 1000000
        //        };

        //        smtpClient.Send(message);

        //        return "Success";

        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        NLog.LogManager.Shutdown();
        //    }
        //}
    }
}