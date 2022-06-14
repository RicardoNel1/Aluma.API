using Aluma.API.Helpers.Extensions;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IClientPersonalInfoService
    {
        Task<string> SetPersonalDetail(int fnaId);
    }

    public class ClientPersonalInfoService : IClientPersonalInfoService
    {
        private readonly IWrapper _repo;

        public ClientPersonalInfoService(IWrapper repo)
        {
            _repo = repo;
        }

        private static string ReplaceHtmlPlaceholders(PersonalDetailReportDto client, PersonalDetailReportDto spouse)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-personal-details.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[FirstName]", client.FirstName);
            result = result.Replace("[LastName]", client.LastName);
            result = result.Replace("[RSAIdNumber]", client.RSAIdNumber);
            result = result.Replace("[DateOfBirth]", client.DateOfBirth);
            result = result.Replace("[ClientAge]", client.Age);
            result = result.Replace("[Gender]", client.Gender);
            result = result.Replace("[LifeExpectancy]", client.LifeExpectancy);
            result = result.Replace("[MaritalStatus]", client.MaritalStatus);
            result = result.Replace("[DateOfMarriage]", client.DateOfMarriage);
            result = result.Replace("[Email]", client.Email);
            result = result.Replace("[WorkNumber]", client.WorkNumber);
            result = result.Replace("[ClientAddress]", client.Address);
            result = result.Replace("[ClientPostal]", client.Postal);

            result = result.Replace("[SpouseFirstName]", spouse.FirstName);
            result = result.Replace("[SpouseLastName]", spouse.LastName);
            result = result.Replace("[SpouseRSAIdNumber]", spouse.RSAIdNumber);
            result = result.Replace("[SpouseDateOfBirth]", spouse.DateOfBirth);
            result = result.Replace("[SpouseAge]", spouse.Age);
            result = result.Replace("[SpouseGender]", spouse.Gender);
            result = result.Replace("[SpouseLifeExpectancy]", spouse.LifeExpectancy);
            result = result.Replace("[SpouseMaritalStatus]", spouse.MaritalStatus);
            result = result.Replace("[SpouseDateOfMarriage]", spouse.DateOfMarriage);
            result = result.Replace("[SpouseEmail]", spouse.Email);
            result = result.Replace("[SpouseWorkNumber]", spouse.WorkNumber);
            result = result.Replace("[SpouseClientAddress]", spouse.Address);
            result = result.Replace("[SpouseClientPostal]", spouse.Postal);

            return result;

        }

        private static PersonalDetailReportDto SetReportFieldsClient(ClientDto client, UserDto user, AssumptionsDto assumptions)
        {
            AddressDto residentialAddress = user.Address?.Where(x => x.Type == AddressTypesEnum.Residential.ToString()).FirstOrDefault();
            AddressDto postalAddress = user.Address?.Where(x => x.Type == AddressTypesEnum.Postal.ToString()).FirstOrDefault();

            return new PersonalDetailReportDto()
            {
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                RSAIdNumber = user.RSAIdNumber ?? string.Empty,
                DateOfBirth = user.DateOfBirth ?? string.Empty,
                Age = !string.IsNullOrEmpty(user.DateOfBirth) ? (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString() : string.Empty,
                Gender = user.RSAIdNumber.GetGenderFromRsaIdNumber() ?? string.Empty,
                LifeExpectancy = assumptions.LifeExpectancy.ToString() ?? string.Empty,
                MaritalStatus = client.MaritalDetails?.MaritalStatus ?? string.Empty,
                DateOfMarriage = !string.IsNullOrEmpty(client.MaritalDetails?.DateOfMarriage) ? Convert.ToDateTime(client.MaritalDetails?.DateOfMarriage).ToString("yyyy-MM-dd") : string.Empty,
                Email = user.Email ?? string.Empty,
                WorkNumber = user.MobileNumber ?? string.Empty,
                Address = residentialAddress == null ? string.Empty : residentialAddress.ToString(),
                Postal = postalAddress == null ? string.Empty : postalAddress.ToString()
            };
        }

        private static PersonalDetailReportDto SetReportFieldsSpouse(ClientDto client)
        {
            if (client.MaritalDetails == null || client.MaritalDetails.MaritalStatus.ToLower() == "single")
                return new();

            try
            {
                return new()
                {
                    FirstName = client.MaritalDetails.FirstName ?? string.Empty,
                    LastName = client.MaritalDetails.Surname ?? string.Empty,
                    RSAIdNumber = !string.IsNullOrEmpty(client.MaritalDetails?.IdNumber) ? client.MaritalDetails?.IdNumber : string.Empty,
                    DateOfBirth = !string.IsNullOrEmpty(client.MaritalDetails?.IdNumber) ? client.MaritalDetails?.IdNumber.GetDateOfBirthFromRsaIdNumber() : string.Empty,
                    Age = !string.IsNullOrEmpty(client.MaritalDetails?.IdNumber) ? (Convert.ToDateTime(client.MaritalDetails?.IdNumber.GetDateOfBirthFromRsaIdNumber())).CalculateAge().ToString() : string.Empty,
                    Gender = !string.IsNullOrEmpty(client.MaritalDetails?.IdNumber) ? client.MaritalDetails?.IdNumber.GetGenderFromRsaIdNumber() : string.Empty,
                    LifeExpectancy = string.Empty,
                    MaritalStatus = client.MaritalDetails?.MaritalStatus ?? string.Empty,
                    DateOfMarriage = !string.IsNullOrEmpty(client.MaritalDetails?.DateOfMarriage) ? Convert.ToDateTime(client.MaritalDetails?.DateOfMarriage).ToString("yyyy-MM-dd") : string.Empty,
                    Email = string.Empty,
                    WorkNumber = string.Empty,
                };
            }
            catch (Exception)
            {
                return new()
                {
                    FirstName = client.MaritalDetails.FirstName ?? string.Empty,
                    LastName = client.MaritalDetails.Surname ?? string.Empty,
                    RSAIdNumber = string.Empty,
                    DateOfBirth = string.Empty,
                    Age = string.Empty,
                    Gender = string.Empty,
                    LifeExpectancy = string.Empty,
                    MaritalStatus = client.MaritalDetails?.MaritalStatus ?? string.Empty,
                    DateOfMarriage = client.MaritalDetails?.DateOfMarriage != null ? Convert.ToDateTime(client.MaritalDetails?.DateOfMarriage).ToString("yyyy-MM-dd") : string.Empty,
                    Email = string.Empty,
                    WorkNumber = string.Empty,
                };
            }
        }

        private async Task<string> GetReportData(int fnaId)
        {
            try
            {
                int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
                ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
                UserDto user = _repo.User.GetUserWithAddress(new() { Id = client.UserId });
                AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);

                //Find a way to get the dto
                //SummaryReportDto summary = BuildSummaryReportDto(fnaId, clientId);

                PersonalDetailReportDto clientInfo = SetReportFieldsClient(client, user, assumptions);
                PersonalDetailReportDto spouseInfo = SetReportFieldsSpouse(client);
                return ReplaceHtmlPlaceholders(clientInfo, spouseInfo);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public async Task<string> SetPersonalDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }

        //NASTY AS ALL HELL
        public SummaryReportDto BuildSummaryReportDto(int fnaId, int clientId)
        {
            //AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
            //AssetSummaryDto assetSummary = _repo.AssetSummary.GetAssetSummary(fnaId);
            //InsuranceSummaryDto insuranceSummary = _repo.InsuranceSummary.GetInsuranceSummary(fnaId);
            //RetirementSummaryDto retirementSummaryDto = _repo.RetirementSummary.GetRetirementSummary(fnaId);

            //List<InsuranceDto> insurances = _repo.Insurance.GetInsurance(fnaId);
            //PrimaryResidenceDto primaryResidence = _repo.PrimaryResidence.GetPrimaryResidence(fnaId);
            //EstateExpensesDto estateExpenses = _repo.EstateExpenses.GetEstateExpenses(fnaId);
            //RetirementPlanningDto retirementPlanning = _repo.RetirementPlanning.GetRetirementPlanning(fnaId);
            //ProvidingDeathSummaryDto providingDeathSummary = _repo.ProvidingDeathSummary.GetProvidingDeathSummary(fnaId);
            //ProvidingDisabilitySummaryDto providingDisabilitySummary = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
            //ProvidingOnDreadDiseaseDto providingOnDreadDisease = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(clientId);
            //double tottalInsurance = 0;

            //if (insuranceSummary != null)
            //{
            //    tottalInsurance = insuranceSummary.TotalToSpouse + insuranceSummary.TotalToThirdParty + insuranceSummary.TotalToLiquidity;
            //}
            //else if (insurances != null && insurances.Count > 0)
            //{
            //    tottalInsurance = 0;
            //    foreach (InsuranceDto insurance in insurances)
            //    {
            //        tottalInsurance += insurance.LifeCover;
            //    }
            //}

            //double estateTotalAssets = primaryResidence.Value + assetSummary.TotalAssetsAttractingCGT + assetSummary.TotalAssetsExcemptCGT +
            //    assetSummary.TotalLiquidAssets + tottalInsurance;


            //double estateTotalLiquidAssets = assetSummary.TotalAssetsToEstate;
            //double estateTotalLiabilities = assetSummary.TotalLiabilities + estateExpenses.TotalEstateExpenses;
            //double totalLiquidity = estateTotalLiquidAssets - (assetSummary.TotalLiabilities + estateExpenses.TotalEstateExpenses);

            //double totalRetirement = retirementSummaryDto.TotalAvailable - retirementSummaryDto.TotalNeeds;
            //double totalRetirementFunds = retirementSummaryDto.TotalPensionFund + retirementSummaryDto.TotalPreservation;

            //double totalDeath = providingDeathSummary.TotalAvailable - providingDeathSummary.TotalNeeds;
            //double totalDisability = providingDisabilitySummary.TotalAvailable - providingDisabilitySummary.TotalNeeds;
            //double totalDread = providingOnDreadDisease.TotalDreadDisease;

            return new()
            {
                //TotalAssets = estateTotalAssets < 0 ? $"({estateTotalAssets * -1})" : estateTotalAssets.ToString(),
                //TotalLiquidAssets = estateTotalLiquidAssets < 0 ? $"({estateTotalLiquidAssets * -1})" : estateTotalLiquidAssets.ToString(),
                //TotalLiabilities = estateTotalLiabilities < 0 ? $"({estateTotalLiabilities * -1})" : $"({estateTotalLiabilities})",
                //LiquidityLabel = totalLiquidity < 0 ? "Shortfall" : "Surplus",
                //TotalLiquidity = totalLiquidity < 0 ? $"({totalLiquidity * -1})" : totalLiquidity.ToString(),

                //TotalRetirementLabel = totalRetirement < 0 ? "Shortfall" : "Surplus",
                //TotalRetirement = totalRetirement < 0 ? $"({totalRetirement * -1})" : totalRetirement.ToString(),
                //SavingsRequired = retirementSummaryDto.SavingsRequiredPremium.ToString(),
                //EscPercentage = retirementPlanning.SavingsEscalation.ToString(),

                //ExistingRetirementFund = totalRetirementFunds.ToString(),
                //YearsToRetirement = assumptions.YearsTillRetirement.ToString(),

                //DeathNeedsLabel = totalDeath < 0 ? "Shortfall" : "Surplus",
                //TotalDeathNeeds = totalDeath < 0 ? $"({totalDeath * -1})" : totalDeath.ToString(),
                //DisabilityNeedsLabel = totalDisability < 0 ? "Shortfall" : "Surplus",
                //TotalDisabilityNeeds = totalDisability < 0 ? $"({totalDisability * -1})" : totalDisability.ToString(),
                //DreadDiseaseLabel = totalDread < 0 ? "Shortfall" : "Surplus",
                //TotalDreadDisease = totalDread < 0 ? $"({totalDread * -1})" : totalDread.ToString(),
            };
        }


    }
}
