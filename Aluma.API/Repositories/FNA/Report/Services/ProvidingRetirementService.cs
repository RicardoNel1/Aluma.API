using Aluma.API.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingRetirementService
    {
        Task<ReportServiceResult> SetRetirementDetail(int fnaId);
    }

    public class ProvidingRetirementService : IProvidingRetirementService
    {
        private readonly IWrapper _repo;
        private readonly IGraphService _graph;

        public ProvidingRetirementService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(RetirementPlanningReportDto retirement)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-retirement-planning.html");
            string result = File.ReadAllText(path);

            string descTotalCapital = Convert.ToInt32(retirement.TotalCapital) > 0 ? "Surplus" : "Shortfall";
            result = result.Replace("[Age]", retirement.Age);
            result = result.Replace("[RetirementAge]", retirement.RetirementAge);
            result = result.Replace("[EscalationPercent]", retirement.EscalationPercent);
            result = result.Replace("[YearsBeforeRetirement]", retirement.YearsBeforeRetirement);
            result = result.Replace("[YearsAfterRetirement]", retirement.YearsAfterRetirement);
            result = result.Replace("[LifeExpectancy]", retirement.LifeExpectancy);
            result = result.Replace("[RiskRating]", retirement.RiskRating);
            result = result.Replace("[InvestmentReturns]", retirement.InvestmentReturnRate);
            result = result.Replace("[InflationRate]", retirement.InflationRate);

            result = result.Replace("[CapitalNeeds]", retirement.CapitalNeeds);
            result = result.Replace("[TotalNeeds]", retirement.TotalNeeds);
            result = result.Replace("[OutstandingLiabilities]", retirement.OutstandingLiabilities);
            result = result.Replace("[AvailableCapital]", retirement.AvailableCapital);
            result = result.Replace("[TotalAvailable]", retirement.TotalAvailable);
            result = result.Replace("[descTotalCapital]", descTotalCapital);
            result = result.Replace("[lineEstimatedNetIncome]", "");
            result = result.Replace("[lineEstimatedIncome]", "");
            result = result.Replace("[lineMonthlyNetIncome]", "");
            result = result.Replace("[IncomeNeedsTotal]", retirement.IncomeNeedsTotal);
            result = result.Replace("[IncomeAvailableTotal]", retirement.IncomeAvailableTotal);
            result = result.Replace("[MonthlySavingsRequired]", retirement.MonthlySavingsRequired);
            result = result.Replace("[MonthlySavingsEscalating]", retirement.MonthlySavingsEscalating);
            result = result.Replace("[IncomeNeed]", retirement.IncomeNeed); //Important to be replaced last

            string script = string.Empty;
            if (retirement.Graphs != null && retirement.Graphs.Count > 0)
            {
                foreach (var graphData in retirement.Graphs)
                {
                    var graph = _graph.SetGraphHtml(graphData);
                    script += graph.Script;

                    if (graphData.Name.ToLower().Contains("capital"))
                        result = result.Replace("[CapitalGraph]", graph.Html);
                    else if (graphData.Name.ToLower().Contains("annual"))
                        result = result.Replace("[AnnualGraph]", graph.Html);
                }
            }
            else
            {
                result = result.Replace("[CapitalGraph]", string.Empty);
                result = result.Replace("[AnnualGraph]", string.Empty);
            }

            ReportServiceResult returnResult = new()
            {
                Html = result,
                Script = script
            };

            return returnResult;

        }

        private RetirementPlanningReportDto SetReportFields(ClientDto client, UserDto user,
                                                        AssumptionsDto assumptions,
                                                        RetirementPlanningDto retirement,
                                                        RetirementSummaryDto summaryRetirement,
                                                        EconomyVariablesDto economy_variables)
        {
            string riskRating = "";
            if (Enum.IsDefined(typeof(RiskRatingsEnum), assumptions.RetirementInvestmentRisk))
                riskRating = ((RiskRatingsEnum)Enum.Parse(typeof(RiskRatingsEnum), assumptions.RetirementInvestmentRisk)).ToString();
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
                TotalNeeds = summaryRetirement.TotalNeeds.ToString(),
                CapitalNeeds = retirement.CapitalNeeds.ToString(),
                OutstandingLiabilities = retirement.OutstandingLiabilities.ToString(),
                AvailableCapital = retirement.CapitalAvailable.ToString(),
                TotalAvailable = summaryRetirement.TotalAvailable.ToString(),
                IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString(),
                IncomeAvailableTotal = retirement.IncomeAvailableTotal.ToString(),
                MonthlySavingsRequired = summaryRetirement.SavingsRequiredPremium.ToString(),
                MonthlySavingsEscalating = retirement.SavingsEscalation.ToString(),
                TotalCapital = (summaryRetirement.TotalAvailable - summaryRetirement.TotalNeeds).ToString()

            };
        }

        private async Task<ReportServiceResult> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
                RetirementPlanningDto retirement = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
                RetirementSummaryDto summaryRetirement = _repo.RetirementSummary.GetRetirementSummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, retirement, summaryRetirement, economy_variables));
            }
            catch (Exception)
            {
                return new ReportServiceResult();
            }
        }

        public async Task<ReportServiceResult> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }
    }
}
