using Aluma.API.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Services
{
    public interface IProvidingDeathService
    {
        Task<ReportServiceResult> SetDeathDetail(int fnaId);
    }

    public class ProvidingDeathService : IProvidingDeathService
    {
        private readonly IWrapper _repo;
        private readonly IGraphService _graph;

        public ProvidingDeathService(IWrapper repo)
        {
            _repo = repo;
            _graph = new GraphService();
        }

        public async Task<ReportServiceResult> SetDeathDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }

        private async Task<ReportServiceResult> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
                ProvidingOnDeathDto deathDto = _repo.ProvidingOnDeath.GetProvidingOnDeath(fnaId);
                ProvidingDeathSummaryDto summaryDeath = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                AssetSummaryDto assetsSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, deathDto, summaryDeath, assetsSummary, economy_variables));
            }
            catch (Exception)
            {
                return new ReportServiceResult();
            }
        }

        private ReportServiceResult ReplaceHtmlPlaceholders(ProvidingOnDeathReportDto deathReport)
        {
            string script = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-providing-on-death.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[AvailableCapital]", deathReport.AvailableCapital);
            result = result.Replace("[descSurplusProviding]", deathReport.descSurplusProviding);
            result = result.Replace("[SurplusProviding]", deathReport.SurplusProviding);
            result = result.Replace("[descSettlingEstate]", deathReport.descSettlingEstate);
            result = result.Replace("[SettlingEstate]", deathReport.SettlingEstate);
            result = result.Replace("[descTotalOnDeath]", deathReport.descTotalOnDeath);
            result = result.Replace("[TotalOnDeath]", deathReport.TotalOnDeath);
            result = result.Replace("[Age]", deathReport.Age);
            result = result.Replace("[InvestmentReturns]", deathReport.InvestmentReturns);
            result = result.Replace("[LifeExpectancy]", deathReport.LifeExpectancy);
            result = result.Replace("[InflationRate]", deathReport.InflationRate);
            result = result.Replace("[YrsTillLifeExpectancy]", deathReport.YrsTillLifeExpectancy);

            if (deathReport.IncomeGraph != null)
            {
                var graph = _graph.SetGraphHtml(deathReport.IncomeGraph);
                script += graph.Script;
                result = result.Replace("[IncomeGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[IncomeGraph]", string.Empty);
            }

            if (deathReport.LumpsumGraph != null)
            {
                var graph = _graph.SetGraphHtml(deathReport.LumpsumGraph);
                script += graph.Script;
                result = result.Replace("[LumpsumGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[LumpsumGraph]", string.Empty);
            }

            if (deathReport.CapitalizedGraph != null)
            {
                var graph = _graph.SetGraphHtml(deathReport.CapitalizedGraph);
                script += graph.Script;
                result = result.Replace("[CapitalizedGraph]", graph.Html);
            }
            else
            {
                result = result.Replace("[CapitalizedGraph]", string.Empty);
            }

            return new()
            {
                Html = result,
                Script = script
            };

        }

        private ProvidingOnDeathReportDto SetReportFields(
            ClientDto client, UserDto user, AssumptionsDto assumptions, ProvidingOnDeathDto deathDto,
            ProvidingDeathSummaryDto summaryDeath, AssetSummaryDto assetSummary, EconomyVariablesDto economy_variables
        )
        {
            double available = summaryDeath.TotalAvailable;
            double settling = assetSummary.TotalLiabilities;
            double totalOnDeath = assetSummary.TotalLiquidAssets - assetSummary.TotalLiabilities;
            double capitalSustainableIncome = Math.Round(summaryDeath.TotalAvailable + (summaryDeath.TotalAvailable * economy_variables.InvestmentReturnRate / 100));

            return new ProvidingOnDeathReportDto()
            {
                AvailableCapital = available < 0 ? $"({available * -1})" : available.ToString(),
                descSurplusProviding = available < 0 ? "Shortfall" : "Surplus",
                SurplusProviding = available < 0 ? $"({available * -1})" : available.ToString(),
                descSettlingEstate = settling < 0 ? "Shortfall" : "Surplus",
                SettlingEstate = settling < 0 ? $"({settling * -1})" : settling.ToString(),
                descTotalOnDeath = totalOnDeath < 0 ? "Shortfall" : "Surplus",
                TotalOnDeath = totalOnDeath < 0 ? $"({totalOnDeath * -1})" : totalOnDeath.ToString(),
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : Convert.ToDateTime(user.DateOfBirth).CalculateAge().ToString(),
                InvestmentReturns = EnumConvertions.RiskExpectations(assumptions.RetirementInvestmentRisk).ToString() ?? string.Empty,
                LifeExpectancy = assumptions.LifeExpectancy.ToString() ?? string.Empty,
                InflationRate = economy_variables.InflationRate.ToString() ?? string.Empty,
                YrsTillLifeExpectancy = assumptions.YearsTillLifeExpectancy.ToString() ?? string.Empty,

                IncomeGraph = new()
                {
                    Type = GraphType.Pie,
                    Name = "Income",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 350,
                    Width = 350,
                    Data = SetIncomeGraphData(deathDto)
                },
                LumpsumGraph = new()
                {
                    Type = GraphType.Pie,
                    Name = "Lumpsum",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 350,
                    Width = 350,
                    Data = SetLumpsumGraphhData(deathDto)
                },
                CapitalizedGraph = new()
                {
                    Type = GraphType.Pie,
                    Name = "Capitalized",
                    XaxisHeader = "Capital",
                    YaxisHeader = "Amount",
                    Height = 350,
                    Data = SetCapitalizedGraphData(available, settling)
                }
            };

        }

        private static List<string> SetIncomeGraphData(ProvidingOnDeathDto deathDto)
        {
            string retirementDescription = deathDto.RetirementFunds < 0 ? "Shortfall" : "Surplus";
            double retirementFunds = deathDto.RetirementFunds < 0 ? deathDto.RetirementFunds * -1 : deathDto.RetirementFunds;

            return new()
            {
                $"Insurance: {deathDto.Available_InsuranceDescription},{deathDto.Available_Insurance_Amount}",
                $"Retirement {retirementDescription},{retirementFunds}",
                $"Available Pre-Tax Income,{deathDto.Available_PreTaxIncome_Amount}"
            };
        }

        private static List<string> SetLumpsumGraphhData(ProvidingOnDeathDto deathDto)
        {
            double incomeNeeds = deathDto.IncomeNeeds < 0 ? deathDto.IncomeNeeds * -1 : deathDto.IncomeNeeds;
            double capitalNeeds = deathDto.CapitalNeeds < 0 ? deathDto.CapitalNeeds * -1 : deathDto.CapitalNeeds;

            return new()
            {
                $"Income Needs,{incomeNeeds}",
                $"Capital Needs,{capitalNeeds}"
            };
        }

        private static List<string> SetCapitalizedGraphData(double available, double lumpsum)
        {
            string availableDescription = available < 0 ? "Shortfall" : "Surplus";
            string lumpsumDescription = lumpsum < 0 ? "Shortfall" : "Surplus";

            available = available < 0 ? available * -1 : available;
            lumpsum = lumpsum < 0 ? lumpsum * -1 : lumpsum;

            return new()
            {
                $"Total Income {availableDescription}, {available}",
                $"Total lumpsum {lumpsumDescription}, {lumpsum}",
            };
        }
    }
}

