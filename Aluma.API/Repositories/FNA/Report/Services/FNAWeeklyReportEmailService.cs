using Aluma.API.Helpers;
using Aluma.API.Repositories.FNA.Report.Service;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using DataService.Dto;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Services
{
    public class FNAWeeklyReportEmailService : BaseReportData
    {
        MailSender _ms;

        private async Task<ReportServiceResult> GetReportData(int fnaId)
        {
            ClientDto client = await GetClient(fnaId);
            //UserDto users = await GetUser(client.UserId);
            //AdvisorDto advisors 

            //AssumptionsDto assumptions = GetAssumptions(fnaId);
            //RetirementPlanningDto retirement = GetRetirementPlanning(fnaId);
            //RetirementSummaryDto summaryRetirement = GetRetirementSummary(fnaId);
            //EconomyVariablesDto economy_variables = GetEconomyVariablesSummary(fnaId);

            return null; //ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, retirement, summaryRetirement, economy_variables));
        }

        public async Task<ReportServiceResult> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }

        //public void SendWeeklyReport()
        //{
        //    BackgroundJob.Schedule(() => _ms.SendWeeklyFNAReport(), TimeSpan.FromMinutes(1));
        //    RecurringJob.AddOrUpdate(() => _ms.SendWeeklyFNAReport(), Cron.Minutely);

        //}



    }
}
