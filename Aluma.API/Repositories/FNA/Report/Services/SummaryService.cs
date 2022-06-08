using Aluma.API.Extensions;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface ISummaryService
    {
        Task<string> SetSummaryDetail(int fnaId);
    }

    public class SummaryService : ISummaryService
    {
        private readonly IWrapper _repo;

        public SummaryService(IWrapper repo)
        {
            _repo = repo;
        }

        private string ReplaceHtmlPlaceholders(SummaryReportDto retirement)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-summary.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[TotalAssets]", retirement.TotalAssets);
            result = result.Replace("[TotalLiquidAssets]", retirement.TotalLiquidAssets);
            result = result.Replace("[TotalLiabilities]", retirement.TotalLiabilities);
            result = result.Replace("[LiquidityLabel]", retirement.LiquidityLabel);
            result = result.Replace("[TotalLiquidity]", retirement.TotalLiquidity);
            result = result.Replace("[TotalRetirementLabel]", retirement.TotalRetirementLabel);
            result = result.Replace("[TotalRetirement]", retirement.TotalRetirement);
            result = result.Replace("[SavingsRequired]", retirement.SavingsRequired);
            result = result.Replace("[EscPercentage]", retirement.EscPercentage);

            result = result.Replace("[DeathNeedsLabel]", retirement.DeathNeedsLabel);
            result = result.Replace("[TotalDeathNeeds]", retirement.TotalDeathNeeds);
            result = result.Replace("[DisabilityNeedsLabel]", retirement.DisabilityNeedsLabel);
            result = result.Replace("[TotalDisabilityNeeds]", retirement.TotalDisabilityNeeds);
            result = result.Replace("[DreadDiseaseLabel]", retirement.DreadDiseaseLabel);
            result = result.Replace("[TotalDreadDisease]", retirement.TotalDreadDisease);

            return result;

        }

        private SummaryReportDto SetReportFields(RetirementPlanningDto retirementPlanning,AssetSummaryDto assetSummary, InsuranceSummaryDto insuranceSummary,
            RetirementSummaryDto retirementSummaryDto, ProvidingDeathSummaryDto providingDeathSummary, ProvidingDisabilitySummaryDto providingDisabilitySummary,
            ProvidingOnDreadDiseaseDto providingOnDreadDisease)
        {
            double estateTotalAssets = assetSummary.TotalAssetsToEstate;
            double estateTotalLiquidAssets = assetSummary.TotalLiquidAssets;
            double estateTotalLiabilities = assetSummary.TotalLiabilities;
            double totalLiquidity = estateTotalLiabilities - estateTotalLiquidAssets;
            double totalRetirement = retirementSummaryDto.TotalAvailable - retirementSummaryDto.TotalNeeds;
            double totalDeath = providingDeathSummary.TotalAvailable - providingDeathSummary.TotalNeeds;
            double totalDisability = providingDisabilitySummary.TotalAvailable - providingDisabilitySummary.TotalNeeds;
            double totalDread = providingOnDreadDisease.Available_DreadDiseaseAmount - (providingOnDreadDisease.Needs_CapitalNeeds + providingOnDreadDisease.Needs_GrossAnnualSalaryTotal);

            return new()
            {
                TotalAssets = estateTotalAssets.ToString(),
                TotalLiquidAssets = estateTotalLiquidAssets.ToString(),
                TotalLiabilities = estateTotalLiabilities.ToString(),
                LiquidityLabel = totalLiquidity < 0 ? "Shortfall" : "Surplus",
                TotalLiquidity = totalLiquidity < 0 ? $"({totalLiquidity * -1})" : totalLiquidity.ToString(),
                TotalRetirementLabel = totalRetirement < 0 ? "Shortfall" : "Surplus",
                TotalRetirement = totalRetirement < 0 ? $"({totalRetirement * -1})" : totalLiquidity.ToString(),
                SavingsRequired = retirementSummaryDto.SavingsRequiredPremium.ToString(),
                EscPercentage = retirementPlanning.SavingsEscalation.ToString(),
                DeathNeedsLabel = totalDeath < 0 ? "Shortfall" : "Surplus",
                TotalDeathNeeds = totalDeath < 0 ? $"({totalDeath * -1})" : totalDeath.ToString(),
                DisabilityNeedsLabel = totalDisability < 0 ? "Shortfall" : "Surplus",
                TotalDisabilityNeeds = totalDisability < 0 ? $"({totalDisability * -1})" : totalDisability.ToString(),
                DreadDiseaseLabel = totalDread < 0 ? "Shortfall" : "Surplus",
                TotalDreadDisease = totalDread < 0 ? $"({totalDread * -1})" : totalDread.ToString(),
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });
                RetirementPlanningDto retirementPlanning = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
                AssetSummaryDto assetSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
                InsuranceSummaryDto insuranceSummary = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);
                RetirementSummaryDto retirementSummaryDto = _repo.RetirementSummary.GetRetirementSummary(fnaId);
                ProvidingDeathSummaryDto providingDeathSummary = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                ProvidingDisabilitySummaryDto providingDisabilitySummary= _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(clientId);

                retirementPlanning= retirementPlanning == null? new RetirementPlanningDto() : retirementPlanning;
                assetSummary = assetSummary == null ? new AssetSummaryDto() : assetSummary;
                insuranceSummary = insuranceSummary == null ? new InsuranceSummaryDto() : insuranceSummary;
                retirementSummaryDto = retirementSummaryDto == null ? new RetirementSummaryDto() : retirementSummaryDto;
                providingDeathSummary = providingDeathSummary == null ? new ProvidingDeathSummaryDto() : providingDeathSummary;
                providingDisabilitySummary = providingDisabilitySummary == null ? new ProvidingDisabilitySummaryDto() : providingDisabilitySummary;
                providingOnDreadDisease = providingOnDreadDisease == null ? new ProvidingOnDreadDiseaseDto() : providingOnDreadDisease;

                return ReplaceHtmlPlaceholders(SetReportFields(retirementPlanning, assetSummary, insuranceSummary, retirementSummaryDto, providingDeathSummary, providingDisabilitySummary, providingOnDreadDisease));
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> SetSummaryDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
