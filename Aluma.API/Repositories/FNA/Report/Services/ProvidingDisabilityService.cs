using Aluma.API.Helpers.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingDisabilityService
    {
        Task<ReportServiceResult> SetDisabilityDetail(int fnaId);
    }

    public class ProvidingDisabilityService : BaseReportData, IProvidingDisabilityService
    {
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
            result = result.Replace("[LifeExpectancy]", disability.LifeExpectancy);
            result = result.Replace("[CapitalDisabilityNeeds]", disability.CapitalDisabilityNeeds);
            result = result.Replace("[YearsTillRetirement]", disability.YearsTillRetirement);
            result = result.Replace("[InvestmentReturnRate]", disability.InvestmentReturnRate);
            result = result.Replace("[ShortTermProtectionIncome]", disability.ShortTermProtectionIncome);
            result = result.Replace("[TotalAvailable]", disability.TotalAvailable);
            result = result.Replace("[InflationRate]", disability.InflationRate);
            result = result.Replace("[LongTermProtectionIncome]", disability.LongTermProtectionIncome);
            result = result.Replace("[Capital]", disability.Capital);
            result = result.Replace("[CurrentNetIncome]", disability.CurrentNetIncome);
            result = result.Replace("[TotalNeeds]", disability.TotalNeeds);
            result = result.Replace("[CapitalNeeds]", disability.CapitalNeeds);
            result = result.Replace("[CapitalizedIncomeShortfall]", disability.CapitalizedIncomeShortfall);
            result = result.Replace("[AvailableCapital]", disability.AvailableCapital);
            result = result.Replace("[TotalCapShortfallSurplusDesc]", disability.TotalCapShortfallSurplusDesc);
            result = result.Replace("[TotalCapShortfallSurplus]", disability.TotalCapShortfallSurplus);
            result = result.Replace("[MaxAdditionalCap]", disability.MaxAdditionalCap);

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
            double capitalShortfall = summaryDisability.TotalAvailable - summaryDisability.TotalNeeds;
            string capitalShortfallLabel = capitalShortfall < 0 ? "Shortfall" : "Surplus";

            double totalLumpSum = summaryDisability.TotalAvailable - summaryDisability.TotalNeeds;
            string totalLumpSumLabel = totalLumpSum < 0 ? "Shortfall" : "Surplus";

            totalLumpSum = totalLumpSum < 0 ? totalLumpSum * -1 : totalLumpSum;


            return new ProvidingOnDisabilityReportDto()
            {
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                RetirementAge = assumptions.RetirementAge.ToString() ?? string.Empty,
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                YearsTillRetirement = assumptions.YearsTillRetirement.ToString() ?? string.Empty,
                CurrentNetIncome = assumptions.CurrentNetIncome.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                InvestmentReturnRate = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString() ?? string.Empty,
                InflationRate = economy_variables.InflationRate.ToString() ?? string.Empty,
                IncomeNeed = disability.IncomeNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                NeedsDisabilityTerm_Years = disability.NeedsTerm_Years.ToString() ?? string.Empty,
                EscDisabilityPercent = economy_variables.InflationRate.ToString() ?? string.Empty,
                CapitalDisabilityNeeds = disability.CapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                ShortTermProtectionIncome = summaryDisability.TotalExistingShortTermIncome.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                LongTermProtectionIncome = summaryDisability.TotalExistingLongTermIncome.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalAvailable = summaryDisability.TotalAvailable.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalNeeds = summaryDisability.TotalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                CapitalNeeds = disability.CapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                CapitalizedIncomeShortfall = summaryDisability.TotalIncomeNeed.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalCapShortfallSurplusDesc = capitalShortfallLabel,
                TotalCapShortfallSurplus = capitalShortfall < 0 ? $"{(capitalShortfall * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))}" : capitalShortfall.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,

                // Where to get the value from ???
                MaxAdditionalCap = string.Empty,

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
                ClientDto client = await GetClient(fnaId);
                UserDto user = await GetUser(client.UserId);

                AssumptionsDto assumptions = GetAssumptions(fnaId);
                ProvidingOnDisabilityDto disability = GetProvidingOnDisability(fnaId);
                ProvidingDisabilitySummaryDto summaryDisability = GetProvidingDisabilitySummary(fnaId);
                EconomyVariablesDto economy_variables = GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, disability, summaryDisability, economy_variables));
        }

        public async Task<ReportServiceResult> SetDisabilityDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
