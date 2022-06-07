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
;
            result = result.Replace("[TotalAssets]", retirement.TotalAssets);
            result = result.Replace("[TotalLiquidAssets]", retirement.TotalLiquidAssets);
            result = result.Replace("[LiabilitiesLabel]", retirement.LiabilitiesLabel);
            result = result.Replace("[TotalLiabilities]", retirement.TotalLiabilities);
            result = result.Replace("[TotalRetirementLabel]", retirement.TotalRetirementLabel);
            result = result.Replace("[TotalRetirement]", retirement.TotalRetirement);
            result = result.Replace("[LiquidAssets]", retirement.LiquidAssets);
            result = result.Replace("[Liabilities]", retirement.Liabilities);
            result = result.Replace("[TotalDeathNeeds]", retirement.TotalDeathNeeds);
            result = result.Replace("[TotalDisability]", retirement.TotalDisability);
            result = result.Replace("[TotalDreadDisease]", retirement.TotalDreadDisease);

            return result;

        }

        private SummaryReportDto SetReportFields(PrimaryResidenceDto primaryResidence, AssetSummaryDto assetSummary, InsuranceSummaryDto insuranceSummary, RetirementSummaryDto retirementSummaryDto, string totalLiquidAssets, ProvidingDeathSummaryDto providingDeathSummary)
        {
            return new()
            {
                TotalAssets = (primaryResidence.Value + assetSummary.TotalAssetsAttractingCGT + assetSummary.TotalAssetsExcemptCGT).ToString(),
                TotalLiquidAssets = assetSummary.TotalLiquidAssets.ToString(),
                LiabilitiesLabel = assetSummary.TotalLiabilities >= 0 ? "Surplus" : "Shortfall",
                TotalLiabilities = assetSummary.TotalLiabilities >= 0 ? assetSummary.TotalLiabilities.ToString() : $"({assetSummary.TotalLiabilities * -1})",
                TotalRetirementLabel = string.Empty,
                TotalRetirement = string.Empty,
                LiquidAssets = totalLiquidAssets,
                Liabilities = string.Empty,
                TotalDeathNeeds = string.Empty,
                TotalDisability = string.Empty,
                TotalDreadDisease = string.Empty,
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

                PrimaryResidenceDto primaryResidence = _repo.PrimaryResidence.GetPrimaryResidence(fnaId);
                AssetSummaryDto assetSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
                InsuranceSummaryDto insuranceSummary = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);
                List<LiquidAssetsDto> liquidAssets = _repo.LiquidAssets.GetLiquidAssets(fnaId);
                EstateExpensesDto estateExpenses = _repo.EstateExpenses.GetEstateExpenses(fnaId);
                ProvidingDeathSummaryDto providingDeathSummary = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                ProvidingDisabilitySummaryDto providingDisabilitySummary = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(fnaId);
                RetirementSummaryDto retirementSummaryDto = _repo.RetirementSummary.GetRetirementSummary(fnaId);

                double TotalLiquidAssets = 0;
                if (liquidAssets != null && liquidAssets.Count > 0)
                {
                    
                    foreach (var liquidAsset in liquidAssets)
                    {
                        TotalLiquidAssets += liquidAsset.Value;
                    }
                }
                return ReplaceHtmlPlaceholders(SetReportFields(primaryResidence, assetSummary, insuranceSummary, retirementSummaryDto, TotalLiquidAssets.ToString(), providingDeathSummary));
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
