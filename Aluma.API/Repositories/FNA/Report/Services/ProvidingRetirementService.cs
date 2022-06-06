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

            string descTotalCapital = Convert.ToInt32(retirement.TotalCapital) > 0 ? "Surplus" : "Shortfall";
            //int EstimatedNetIncome = 0;

            //bool showEstimatedNetIncome = true;

            //if (Convert.ToInt32(retirement.TotalAvailable) > Convert.ToInt32(retirement.TotalNeeds))
            //{
            //    showEstimatedNetIncome = false;
            //}
            //else
            //{
            //    EstimatedNetIncome = Convert.ToInt32(retirement.CapitalNeeds);
            //}

            //string lineEstimatedNetIncome = "This is estimated to provide a net income (PV) of R [EstimatedNetIncome]";
            //string lineEstimatedIncome = "(Income [descEstimatedIncome] (PV) R [EstimatedIncome] pm)";
            //string lineMonthlyNetIncome = "A monthly net income withdrawal (PV) of R [IncomeNeed], your Capital will be exhausted after 6 years";

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
            
            result = result.Replace("[CapitalNeeds]", retirement.CapitalNeeds);
            result = result.Replace("[TotalNeeds]", retirement.TotalNeeds);
            result = result.Replace("[OutstandingLiabilities]", retirement.OutstandingLiabilities);
            result = result.Replace("[AvailableCapital]", retirement.AvailableCapital);
            result = result.Replace("[TotalAvailable]", retirement.TotalAvailable);
            result = result.Replace("[descTotalCapital]", descTotalCapital);
            //result = result.Replace("[lineEstimatedNetIncome]", showEstimatedNetIncome == true ? lineEstimatedNetIncome : "");
            //result = result.Replace("[lineEstimatedIncome]", showEstimatedNetIncome == true ? lineEstimatedIncome : "");
            result = result.Replace("[lineEstimatedNetIncome]", "");
            result = result.Replace("[lineEstimatedIncome]", "");
            //result = result.Replace("[EstimatedNetIncome]", EstimatedNetIncome.ToString());
            //result = result.Replace("[EstimatedIncome]", EstimatedNetIncome.ToString());
            //result = result.Replace("[lineMonthlyNetIncome]", showEstimatedNetIncome == true ? lineMonthlyNetIncome : "");
            result = result.Replace("[lineMonthlyNetIncome]", "");
            result = result.Replace("[IncomeNeedsTotal]", retirement.IncomeNeedsTotal);
            result = result.Replace("[IncomeAvailableTotal]", retirement.IncomeAvailableTotal);
            result = result.Replace("[MonthlySavingsRequired]", retirement.MonthlySavingsRequired);
            result = result.Replace("[MonthlySavingsEscalating]", retirement.MonthlySavingsEscalating);
            result = result.Replace("[IncomeNeed]", retirement.IncomeNeed); //Important to be replaced last

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
                TotalNeeds = summary_retirement.TotalNeeds.ToString(),
                CapitalNeeds = retirement.CapitalNeeds.ToString(),
                OutstandingLiabilities = retirement.OutstandingLiabilities.ToString(),
                AvailableCapital = retirement.CapitalAvailable.ToString(),
                TotalAvailable = summary_retirement.TotalAvailable.ToString(),
                IncomeNeedsTotal = retirement.IncomeNeedsTotal.ToString(),
                IncomeAvailableTotal = retirement.IncomeAvailableTotal.ToString(),
                MonthlySavingsRequired = summary_retirement.SavingsRequiredPremium.ToString(),
                MonthlySavingsEscalating = retirement.SavingsEscalation.ToString(),
                TotalCapital = (summary_retirement.TotalAvailable - summary_retirement.TotalNeeds).ToString()

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
            catch (Exception e)
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
