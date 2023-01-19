using Aluma.API.Helpers;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Aluma.API.Repositories.FNA.Report.Services
{
    public class FNAWeeklyReportEmail
    {
        MailSender _ms;



        //public void SendWeeklyReport()
        //{
        //    BackgroundJob.Schedule(() => _ms.SendWeeklyFNAReport(), TimeSpan.FromMinutes(1));
        //    RecurringJob.AddOrUpdate(() => _ms.SendWeeklyFNAReport(), Cron.Minutely);

        //}
        


    }
}
