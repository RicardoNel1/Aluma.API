using Aluma.API.Helpers.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface ISummaryService
    {
        Task<string> SetSummaryDetail(int fnaId);
    }

    public class SummaryService : BaseReportData, ISummaryService
    {
        public SummaryService(IWrapper repo)
        {
            _repo = repo;
        }

        private static string ReplaceHtmlPlaceholders(SummaryReportDto summary)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-summary.html");
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
                assetSummary.TotalLiquidAssets + assetSummary.TotalInvestmentsExemptCGT + tottalInsurance;      //not adding TotalInvestmentsAttractingCGT yet


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
                TotalAssets = estateTotalAssets < 0 ? $"({(estateTotalAssets * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : estateTotalAssets.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                TotalLiquidAssets = estateTotalLiquidAssets < 0 ? $"({(estateTotalLiquidAssets * -1).ToString("C", CultureInfo.CreateSpecificCulture("en - za"))})" : estateTotalLiquidAssets.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                TotalLiabilities = estateTotalLiabilities < 0 ? $"({(estateTotalLiabilities * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : $"({(estateTotalLiabilities).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})",
                LiquidityLabel = totalLiquidity < 0 ? "Shortfall" : "Surplus",
                TotalLiquidity = totalLiquidity < 0 ? $"({(totalLiquidity * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalLiquidity.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),

                TotalRetirementLabel = totalRetirement < 0 ? "Shortfall" : "Surplus",
                TotalRetirement = totalRetirement < 0 ? $"({(totalRetirement * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalRetirement.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                SavingsRequired = retirementSummaryDto.SavingsRequiredPremium < 0 ? $"({(retirementSummaryDto.SavingsRequiredPremium * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : retirementSummaryDto.SavingsRequiredPremium.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                EscPercentage = retirementPlanning.SavingsEscalation.ToString() ?? string.Empty,

                ExistingRetirementFund = totalRetirementFunds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                YearsToRetirement = assumptions.YearsTillRetirement.ToString() ?? string.Empty,

                DeathNeedsLabel = totalDeath < 0 ? "Shortfall" : "Surplus",
                TotalDeathNeeds = totalDeath < 0 ? $"({(totalDeath * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalDeath.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                DisabilityNeedsLabel = totalDisability < 0 ? "Shortfall" : "Surplus",
                TotalDisabilityNeeds = totalDisability < 0 ? $"({(totalDisability * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalDisability.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
                DreadDiseaseLabel = totalDread < 0 ? "Shortfall" : "Surplus",
                TotalDreadDisease = totalDread < 0 ? $"({(totalDread * -1).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))})" : totalDread.ToString("C", CultureInfo.CreateSpecificCulture("en-za")),
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
                ClientDto client = await GetClient(fnaId);
                UserDto user = await GetUser(client.UserId);

                AssumptionsDto assumptions = GetAssumptions(fnaId);
                AssetSummaryDto assetSummary = GetAssetSummary(fnaId);
                InsuranceSummaryDto insuranceSummary = GetInsuranceSummary(fnaId);
                RetirementSummaryDto retirementSummaryDto = GetRetirementSummary(fnaId);

                List<InsuranceDto> insurances = GetInsurance(fnaId);
                PrimaryResidenceDto primaryResidence = GetPrimaryResidence(fnaId);
                EstateExpensesDto estateExpenses = GetEstateExpenses(fnaId);
                RetirementPlanningDto retirementPlanning = GetRetirementPlanning(fnaId);
                ProvidingDeathSummaryDto providingDeathSummary = GetProvidingDeathSummary(fnaId);
                ProvidingDisabilitySummaryDto providingDisabilitySummary = GetProvidingDisabilitySummary(fnaId);
                ProvidingOnDreadDiseaseDto providingOnDreadDisease = GetProvidingOnDreadDisease(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(retirementPlanning, assetSummary, estateExpenses, insuranceSummary, insurances, assumptions, retirementSummaryDto, providingDeathSummary,
                    providingDisabilitySummary, providingOnDreadDisease, primaryResidence));
        }

        public async Task<string> SetSummaryDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
