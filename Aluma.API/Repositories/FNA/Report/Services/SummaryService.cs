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

        private static string ReplaceHtmlPlaceholders(SummaryReportDto summary)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-summary.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[TotalAssets]", summary.TotalAssets);
            result = result.Replace("[TotalLiquidAssets]", summary.TotalLiquidAssets);
            result = result.Replace("[TotalLiabilities]", summary.TotalLiabilities);
            result = result.Replace("[LiquidityLabel]", summary.LiquidityLabel);
            result = result.Replace("[TotalLiquidity]", summary.TotalLiquidity);

            result = result.Replace("[TotalRetirementLabel]", summary.TotalRetirementLabel);
            result = result.Replace("[TotalRetirement]", summary.TotalRetirement);
            result = result.Replace("[SavingsRequired]", summary.SavingsRequired);
            result = result.Replace("[EscPercentage]", summary.EscPercentage);
            result = result.Replace("[ExistingRetirementFund]", summary.ExistingRetirementFund);
            result = result.Replace("[YearsToRetirement]", summary.YearsToRetirement);

            result = result.Replace("[DeathNeedsLabel]", summary.DeathNeedsLabel);
            result = result.Replace("[TotalDeathNeeds]", summary.TotalDeathNeeds);
            result = result.Replace("[DisabilityNeedsLabel]", summary.DisabilityNeedsLabel);
            result = result.Replace("[TotalDisabilityNeeds]", summary.TotalDisabilityNeeds);
            result = result.Replace("[DreadDiseaseLabel]", summary.DreadDiseaseLabel);
            result = result.Replace("[TotalDreadDisease]", summary.TotalDreadDisease);

            return result;
        }

        private static SummaryReportDto SetReportFields(
            RetirementPlanningDto retirementPlanning, AssetSummaryDto assetSummary, EstateExpensesDto estateExpenses, InsuranceSummaryDto insuranceSummary, List<InsuranceDto> insurances, AssumptionsDto assumptions,
            RetirementSummaryDto retirementSummaryDto, ProvidingDeathSummaryDto providingDeathSummary, ProvidingDisabilitySummaryDto providingDisabilitySummary,
            ProvidingOnDreadDiseaseDto providingOnDreadDisease, PrimaryResidenceDto primaryResidence)
        {
            double tottalInsurance = 0;
            if (insuranceSummary != null)
            {
                tottalInsurance = insuranceSummary.TotalToSpouse + insuranceSummary.TotalToThirdParty + insuranceSummary.TotalToLiquidity;
            }
            else if (insurances != null && insurances.Count > 0)
            {
                tottalInsurance = 0;
                foreach (InsuranceDto insurance in insurances)
                {
                    tottalInsurance += insurance.LifeCover;
                }
            }
            
            double estateTotalAssets = primaryResidence.Value + assetSummary.TotalAssetsAttractingCGT + assetSummary.TotalAssetsExcemptCGT + 
                assetSummary.TotalLiquidAssets + tottalInsurance;


            double estateTotalLiquidAssets = assetSummary.TotalAssetsToEstate;
            double estateTotalLiabilities = assetSummary.TotalLiabilities + estateExpenses.TotalEstateExpenses;
            double totalLiquidity = estateTotalLiquidAssets - (assetSummary.TotalLiabilities + estateExpenses.TotalEstateExpenses);

            double totalRetirement = retirementSummaryDto.TotalAvailable - retirementSummaryDto.TotalNeeds;
            double totalRetirementFunds = retirementSummaryDto.TotalPensionFund + retirementSummaryDto.TotalPreservation;

            double totalDeath = providingDeathSummary.TotalAvailable - providingDeathSummary.TotalNeeds;
            double totalDisability = providingDisabilitySummary.TotalAvailable - providingDisabilitySummary.TotalNeeds;
            double totalDread = providingOnDreadDisease.TotalDreadDisease;

            return new()
            {
                TotalAssets = estateTotalAssets < 0 ? $"({estateTotalAssets * -1})" : estateTotalAssets.ToString(),
                TotalLiquidAssets = estateTotalLiquidAssets < 0 ? $"({estateTotalLiquidAssets * -1})" : estateTotalLiquidAssets.ToString(),
                TotalLiabilities = estateTotalLiabilities < 0 ? $"({estateTotalLiabilities * -1})" : $"({estateTotalLiabilities})",
                LiquidityLabel = totalLiquidity < 0 ? "Shortfall" : "Surplus",
                TotalLiquidity = totalLiquidity < 0 ? $"({totalLiquidity * -1})" : totalLiquidity.ToString(),

                TotalRetirementLabel = totalRetirement < 0 ? "Shortfall" : "Surplus",
                TotalRetirement = totalRetirement < 0 ? $"({totalRetirement * -1})" : totalRetirement.ToString(),
                SavingsRequired = retirementSummaryDto.SavingsRequiredPremium.ToString(),
                EscPercentage = retirementPlanning.SavingsEscalation.ToString(),

                ExistingRetirementFund = totalRetirementFunds.ToString(),
                YearsToRetirement = assumptions.YearsTillRetirement.ToString(),

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

                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
                AssetSummaryDto assetSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
                InsuranceSummaryDto insuranceSummary = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);
                RetirementSummaryDto retirementSummaryDto = _repo.RetirementSummary.GetRetirementSummary(fnaId);

                List<InsuranceDto> insurances = _repo.Insurance.GetInsurance(fnaId);
                PrimaryResidenceDto primaryResidence = _repo.PrimaryResidence.GetPrimaryResidence(fnaId);
                EstateExpensesDto estateExpenses = _repo.EstateExpenses.GetEstateExpenses(fnaId);
                RetirementPlanningDto retirementPlanning = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
                ProvidingDeathSummaryDto providingDeathSummary = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
                ProvidingDisabilitySummaryDto providingDisabilitySummary = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(clientId);

                assetSummary = assetSummary == null ? new AssetSummaryDto() : assetSummary;
                retirementSummaryDto = retirementSummaryDto == null ? new RetirementSummaryDto() : retirementSummaryDto;
                insurances = insurances == null ? new List<InsuranceDto>() : insurances;
                retirementPlanning = retirementPlanning == null ? new RetirementPlanningDto() : retirementPlanning;
                providingDeathSummary = providingDeathSummary == null ? new ProvidingDeathSummaryDto() : providingDeathSummary;
                providingDisabilitySummary = providingDisabilitySummary == null ? new ProvidingDisabilitySummaryDto() : providingDisabilitySummary;
                providingOnDreadDisease = providingOnDreadDisease == null ? new ProvidingOnDreadDiseaseDto() : providingOnDreadDisease;

                return ReplaceHtmlPlaceholders(SetReportFields(retirementPlanning, assetSummary, estateExpenses, insuranceSummary, insurances, assumptions, retirementSummaryDto, providingDeathSummary,
                    providingDisabilitySummary, providingOnDreadDisease, primaryResidence));
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
