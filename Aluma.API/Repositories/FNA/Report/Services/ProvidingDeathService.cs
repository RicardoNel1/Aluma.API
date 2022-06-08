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

            result = result.Replace("[AvailableCapital]", deathReport.TotalAvailable);
            result = result.Replace("[descSurplusProviding]", Convert.ToInt32(deathReport.TotalAvailable) > 0 ? "Surplus" : "Shortfall");
            result = result.Replace("[descShortfallSettling]", Convert.ToInt32(deathReport.TotalAvailable) > 0 ? "Surplus" : "Shortfall");

            result = result.Replace("[ShortfallSetEstate]", deathReport.ShortfallSettEstate);
            result = result.Replace("[TotalShortfallDeath]", deathReport.TotalShortfallDeath);

            ///var graph = _graph.SetGraphHtml(deathReport.Graph);

            //result = result.Replace("[graph]", graph.Html);

            ReportServiceResult returnResult = new()
            {
                Html = result,
                //Script = graph.Script
            };

            return returnResult;

        }

        private ProvidingOnDeathReportDto SetReportFields(ClientDto client, UserDto user,
                                                                AssumptionsDto assumptions,
                                                                ProvidingOnDeathDto deathDto,
                                                                ProvidingDeathSummaryDto summaryDeath,
                                                                AssetSummaryDto assetSummary,
                                                                EconomyVariablesDto economy_variables)
        {

            return new ProvidingOnDeathReportDto()
            {
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : Convert.ToDateTime(user.DateOfBirth).CalculateAge().ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                YearsTillLifeExpectancy = assumptions.YearsTillLifeExpectancy.ToString(),
                InvestmentReturns = economy_variables.InvestmentReturnRate.ToString(),
                RiskInflation = economy_variables.InflationRate.ToString(),
                TotalAvailable = summaryDeath.TotalAvailable.ToString(),
                ShortfallSettEstate = assetSummary.TotalLiabilities.ToString(),
                TotalShortfallDeath = (Convert.ToInt32(assetSummary.TotalLiquidAssets) - Convert.ToInt32(assetSummary.TotalLiabilities)).ToString(),

                //Graph = new()
                //{
                //    Type = GraphType.Pie,
                //    Name = "Capital Solution",
                //    XaxisHeader = "Capital",
                //    YaxisHeader = "Amount",
                //    Data = new() {
                //        {"Capitalized Income Shortfall", summaryDeath.TotalNeeds.ToString()},
                //        {"Lump sum Needs", deathDto.IncomeNeeds.ToString()},
                //        {"Available Lump sum", summaryDeath.TotalAvailable.ToString()},
                //        {"Total Lump sum Shortfall", (summaryDeath.TotalAvailable - summaryDeath.TotalNeeds).ToString()},
                //    }
                //}
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

