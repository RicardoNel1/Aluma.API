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

        private ReportServiceResult ReplaceHtmlPlaceholders(ProvidingOnDeathReportDto deathReport)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-death.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[AvailableCapital]", deathReport.AvailableCapital);
            result = result.Replace("[descSurplusProviding]", deathReport.descSurplusProviding);
            result = result.Replace("[descSettlingEstate]", deathReport.descSettlingEstate);
            result = result.Replace("[SettlingEstate]", deathReport.SettlingEstate);
            result = result.Replace("[descTotalOnDeath]", deathReport.descTotalOnDeath);
            result = result.Replace("[TotalOnDeath]", deathReport.TotalOnDeath);
            result = result.Replace("[Age]", deathReport.Age);
            result = result.Replace("[InvestmentReturns]", deathReport.InvestmentReturns);
            result = result.Replace("[LifeExpectancy]", deathReport.LifeExpectancy);
            result = result.Replace("[InflationRate]", deathReport.InflationRate);
            result = result.Replace("[YrsTillLifeExpectancy]", deathReport.YrsTillLifeExpectancy);

            string script = string.Empty;
            if (deathReport.Graphs != null && deathReport.Graphs.Count > 0)
            {
                foreach (var graphData in deathReport.Graphs)
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

        private ProvidingOnDeathReportDto SetReportFields(
            ClientDto client, UserDto user, AssumptionsDto assumptions, ProvidingOnDeathDto deathDto,
            ProvidingDeathSummaryDto summaryDeath, AssetSummaryDto assetSummary, EconomyVariablesDto economy_variables
        )
        {
            double available = summaryDeath.TotalAvailable;
            double settling = assetSummary.TotalLiabilities;
            double totalOnDeath = assetSummary.TotalLiquidAssets - assetSummary.TotalLiabilities;
            double capitalSustainableIncome = Math.Round(summaryDeath.TotalAvailable + (summaryDeath.TotalAvailable * economy_variables.InvestmentReturnRate / 100));

            string capitalGraphAvailable = available < 0 ? $"{available * -1}" : available.ToString();
            string capitalGraphSustainableIncome = capitalSustainableIncome < 0 ? $"{capitalSustainableIncome * -1}" : capitalSustainableIncome.ToString();
            string capitalGraphProposed = "0";

            string annualGraphAvailable = available < 0 ? $"{available * -1}" : available.ToString();
            string annualGraphSustainableIncome = "0";
            string annualGraphProposed = summaryDeath.TotalNeeds < 0 ? $"{summaryDeath.TotalNeeds * -1}" : summaryDeath.TotalNeeds.ToString();

            GraphReportDto capitalPositionGraph = new()
            {
                Type = GraphType.Pie,
                Name = "Capital Position over planning term",
                XaxisHeader = "Capital",
                YaxisHeader = "Amount",
                Height = 350,
                Data = new List<string>() {
                        $"Available Capital: Full Income, {capitalGraphAvailable}",
                        $"Available Capital: Sustainable Income, {capitalGraphSustainableIncome}",
                        $"Proposed Capital, {capitalGraphProposed}",
                }
            };

            GraphReportDto annualPositionGraph = new()
            {
                Type = GraphType.Pie,
                Name = "Annual Income Position over planning term",
                XaxisHeader = "Capital",
                YaxisHeader = "Amount",
                Height = 350,
                Data = new List<string>() {
                        $"Full Income from available capital, {annualGraphAvailable}",
                        $"Sustainable income from available capital, {annualGraphSustainableIncome}",
                        $"Income Need, {annualGraphProposed}",
                }
            };


            return new ProvidingOnDeathReportDto()
            {
                AvailableCapital = available < 0 ? $"({available * -1})" : available.ToString(),
                descSurplusProviding = available < 0 ? "Shortfall" : "Surplus",

                descSettlingEstate = settling < 0 ? "Shortfall" : "Surplus",
                SettlingEstate = settling < 0 ? $"({settling * -1})" : settling.ToString(),

                descTotalOnDeath = totalOnDeath < 0 ? "Shortfall" : "Surplus",
                TotalOnDeath = totalOnDeath < 0 ? $"({totalOnDeath * -1})" : totalOnDeath.ToString(),

                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : Convert.ToDateTime(user.DateOfBirth).CalculateAge().ToString(),
                InvestmentReturns = economy_variables.InvestmentReturnRate.ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                InflationRate = economy_variables.InflationRate.ToString(),
                YrsTillLifeExpectancy = assumptions.YearsTillLifeExpectancy.ToString(),

                Graphs = new List<GraphReportDto> {
                    capitalPositionGraph,
                    annualPositionGraph
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
                ProvidingOnDeathDto deathDto = _repo.ProvidingOnDeath.GetProvidingOnDeath(fnaId);
                ProvidingDeathSummaryDto summaryDeath = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                AssetSummaryDto assetsSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, deathDto, summaryDeath, assetsSummary, economy_variables));
            }
            catch (Exception ex)
            {
                return new ReportServiceResult();
            }
        }

        public async Task<ReportServiceResult> SetDeathDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}

