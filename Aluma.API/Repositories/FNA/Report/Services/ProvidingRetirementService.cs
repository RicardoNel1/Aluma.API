using Aluma.API.Extensions;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingRetirementService
    {
        Task<string> SetRetirementDetail(int fnaId);
    }

    public class ProvidingRetirementService : IProvidingRetirementService
    {
        private readonly IWrapper _repo;

        public ProvidingRetirementService(IWrapper repo)
        {
            _repo = repo;
        }

        private string ReplaceHtmlPlaceholders(RetirementPlanningReportDto retirement)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-retirement-planning.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[date]", DateTime.Now.ToString("yyyy/MM/dd"));
            result = result.Replace("[Age]", retirement.Age);
            result = result.Replace("[RetirementAge]", retirement.RetirementAge);
            result = result.Replace("[EscalationPercent]", retirement.EscalationPercent);
            result = result.Replace("[YearsBeforeRetirement]", retirement.YearsBeforeRetirement);
            result = result.Replace("[YearsAfterRetirement]", retirement.YearsAfterRetirement);
            result = result.Replace("[LifeExpectancy]", retirement.LifeExpectancy);
            result = result.Replace("[RiskRating]", retirement.RiskRating);
            result = result.Replace("[InvestmentReturns]", retirement.InvestmentReturnRate);
            result = result.Replace("[InflationRate]", retirement.InflationRate);

            return result;

        }

        private RetirementPlanningReportDto SetReportFields(ClientDto client, UserDto user,
                                                        AssumptionsDto assumptions,
                                                        RetirementPlanningDto retirement,
                                                        RetirementSummaryDto summary_retirement,
                                                        EconomyVariablesDto economy_variables)
        {
            string riskRating = "";
            if (Enum.IsDefined(typeof(RiskRatingsEnum), assumptions.RetirementInvestmentRisk))
                riskRating = ((RiskRatingsEnum)Convert.ToInt32(assumptions.RetirementInvestmentRisk)).ToString();
            else
                riskRating = "";

            return new RetirementPlanningReportDto()
            {
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                RetirementAge = assumptions.RetirementAge.ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                YearsBeforeRetirement = assumptions.YearsTillRetirement.ToString(),
                YearsAfterRetirement = assumptions.YearsAfterRetirement.ToString(),
                RiskRating = riskRating,
                InvestmentReturnRate = economy_variables.InvestmentReturnRate.ToString(),
                InflationRate = economy_variables.InflationRate.ToString(),
                IncomeNeed = retirement.IncomeNeeds.ToString(),
                NeedsRetirementTerm_Years = retirement.NeedsTerm_Years.ToString(),
                EscalationPercent = economy_variables.InflationRate.ToString(),

            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
                RetirementPlanningDto retirement = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
                RetirementSummaryDto summary_retirement = _repo.RetirementSummary.GetRetirementSummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, retirement, summary_retirement, economy_variables));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
