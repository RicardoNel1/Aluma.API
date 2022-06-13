using Aluma.API.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingDisabilityService
    {
        Task<ReportServiceResult> SetDisabilityDetail(int fnaId);
    }

    public class ProvidingDisabilityService : IProvidingDisabilityService
    {
        private readonly IWrapper _repo;
        private readonly IGraphService _graph;

        public ProvidingDisabilityService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(ProvidingOnDisabilityReportDto disability)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-providing-on-disability.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[TotalIncomeNeed]", disability.IncomeNeed);
            result = result.Replace("[Age]", disability.Age);
            result = result.Replace("[NeedsDisabilityTerm_Years]", disability.NeedsDisabilityTerm_Years);
            result = result.Replace("[RetirementAge]", disability.RetirementAge);
            result = result.Replace("[EscDisabilityPercent]", disability.EscDisabilityPercent);
            result = result.Replace("[EscPercentage]", disability.EscDisabilityPercent);
            result = result.Replace("[LifeExpectancy]", disability.LifeExpectancy);
            result = result.Replace("[CapitalNeeds]", disability.CapitalDisabilityNeeds);
            result = result.Replace("[YearsTillRetirement]", disability.YearsTillRetirement);
            result = result.Replace("[InvestmentReturnRate]", disability.InvestmentReturnRate);
            result = result.Replace("[ShortTermProtection]", disability.ShortTermProtectionIncome);
            result = result.Replace("[InflationRate]", disability.InflationRate);
            result = result.Replace("[LongTermProtectionIncome]", disability.LongTermProtectionIncome);
            result = result.Replace("[AvailableCapital]", disability.TotalAvailable);
            result = result.Replace("[CurrentNetIncome]", disability.CurrentNetIncome);
            result = result.Replace("[TotalNeeds]", disability.TotalNeeds);
            result = result.Replace("[CapitalizedIncomeShortfall]", disability.CapitalizedIncomeShortfall);
            result = result.Replace("[TotalCapShortfall]", disability.TotalCapShortfall);

            var graph = _graph.SetGraphHtml(disability.Graph);

            result = result.Replace("[CapitalSolutionGraph]", graph.Html);

            ReportServiceResult returnResult = new()
            {
                Html = result,
                Script = graph.Script
            };

            return returnResult;

        }

        private static ProvidingOnDisabilityReportDto SetReportFields(ClientDto client, UserDto user, 
                                                                AssumptionsDto assumptions,
                                                                ProvidingOnDisabilityDto disability,
                                                                ProvidingDisabilitySummaryDto summaryDisability,
                                                                EconomyVariablesDto economy_variables)
        {

            double totalLumpSum = summaryDisability.TotalAvailable - summaryDisability.TotalNeeds;
            string totalLumpSumLabel = totalLumpSum < 0 ? "Shortfall" : "Surplus";

            totalLumpSum = totalLumpSum < 0 ? totalLumpSum * -1 : totalLumpSum;


            return new ProvidingOnDisabilityReportDto()
            {
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                RetirementAge = assumptions.RetirementAge.ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                YearsTillRetirement = assumptions.YearsTillRetirement.ToString(),
                CurrentNetIncome = assumptions.CurrentNetIncome.ToString(),
                InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString(),
                InflationRate = economy_variables.InflationRate.ToString(),
                IncomeNeed = disability.IncomeNeeds.ToString(),
                NeedsDisabilityTerm_Years = disability.NeedsTerm_Years.ToString(),
                EscDisabilityPercent = economy_variables.InflationRate.ToString(),
                CapitalDisabilityNeeds = disability.CapitalNeeds.ToString(),
                ShortTermProtectionIncome = summaryDisability.TotalExistingShortTermIncome.ToString(),
                LongTermProtectionIncome = summaryDisability.TotalExistingLongTermIncome.ToString(),
                TotalAvailable = summaryDisability.TotalAvailable.ToString(),
                TotalNeeds = summaryDisability.TotalNeeds.ToString(),
                CapitalNeeds = disability.CapitalNeeds.ToString(),
                CapitalizedIncomeShortfall = summaryDisability.TotalIncomeNeed.ToString(),
                TotalCapShortfall = (summaryDisability.TotalAvailable - summaryDisability.TotalNeeds).ToString(),
                Graph = new()
                {
                    Type = GraphType.Pie,
                    Name = "Capital Solution",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Data = new List<string>() {
                        $"Capitalized Income Shortfall, {summaryDisability.TotalIncomeNeed}",
                        $"Lump sum Needs, {disability.IncomeNeeds}",
                        $"Available Lump sum, {summaryDisability.TotalAvailable}",
                        $"Total Lump sum {totalLumpSumLabel}, {totalLumpSum}", 
                    }
                }
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
                ProvidingOnDisabilityDto disability = _repo.ProvidingOnDisability.GetProvidingOnDisability(fnaId);
                ProvidingDisabilitySummaryDto summaryDisability = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, disability, summaryDisability, economy_variables));
            }
            catch (Exception ex)
            {
                return new ReportServiceResult();
            }
        }

        public async Task<ReportServiceResult> SetDisabilityDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
