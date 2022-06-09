using Aluma.API.Extensions;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
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
    public interface IProvidingDreadService
    {
        Task<string> SetDreadDetail(int fnaId);
    }

    public class ProvidingDreadService : IProvidingDreadService
    {
        private readonly IWrapper _repo;

        public ProvidingDreadService(IWrapper repo)
        {
            _repo = repo;
        }

        private string ReplaceHtmlPlaceholders(ProvidingOnDreadReportDto dreadDisease)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-dread-disease.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[CapitalNeeds]", dreadDisease.CapitalNeeds);
            result = result.Replace("[MultipleGrossSalary]", dreadDisease.MultipleGrossAnnualSalary);
            result = result.Replace("[TotalNeeds]", dreadDisease.TotalNeeds);
            result = result.Replace("[descDreadCoverAvailable]", dreadDisease.DescDreadCoverAvailable);
            result = result.Replace("[DreadCoverAvailable]", dreadDisease.DreadCoverAvailable);
            result = result.Replace("[TotalAvailableCapital]", dreadDisease.TotalAvailableCapital);
            result = result.Replace("[DreadDiseaseSurplus]", Convert.ToInt32(dreadDisease.TotalDreadDisease) > 0 ? "Surplus" : "Shortfall");
            result = result.Replace("[TotalDreadDisease]", dreadDisease.TotalDreadDisease);
            result = result.Replace("[Age]", dreadDisease.Age);
            result = result.Replace("[CurrentNetIncome]", dreadDisease.CurrentNetIncome);
            result = result.Replace("[GrossMonthlyIncome]", dreadDisease.GrossMonthlyIncome);

            return result;

        }

        private ProvidingOnDreadReportDto SetReportFields(ClientDto client, UserDto user,
                                                        AssumptionsDto assumptions,
                                                        ProvidingOnDreadDiseaseDto dreadDisease,
                                                        EconomyVariablesDto economy_variables)
        {
            return new ProvidingOnDreadReportDto()
            {
                CapitalNeeds = dreadDisease.Needs_CapitalNeeds.ToString(),
                MultipleGrossAnnualSalary = dreadDisease.Needs_GrossAnnualSalaryTotal.ToString(),
                TotalNeeds = (Convert.ToInt32(dreadDisease.Needs_CapitalNeeds) + Convert.ToInt32(dreadDisease.Needs_GrossAnnualSalaryTotal)).ToString(),
                DreadCoverAvailable = dreadDisease.Available_DreadDiseaseAmount.ToString(),
                DescDreadCoverAvailable = string.IsNullOrEmpty(dreadDisease.Available_DreadDiseaseDescription)? string.Empty : dreadDisease.Available_DreadDiseaseDescription.ToString(),
                TotalAvailableCapital = dreadDisease.Available_DreadDiseaseAmount.ToString(),
                TotalDreadDisease = (Convert.ToInt32(dreadDisease.Available_DreadDiseaseAmount) - Convert.ToInt32(dreadDisease.Needs_CapitalNeeds)).ToString(),
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                CurrentNetIncome = assumptions.CurrentNetIncome.ToString(),
                GrossMonthlyIncome = assumptions.CurrentGrossIncome.ToString(),

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
                ProvidingOnDreadDiseaseDto dreadDisease = _repo.ProvidingOnDreadDisease.GetProvidingOnDreadDisease(fnaId);
                EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, dreadDisease, economy_variables));
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public async Task<string> SetDreadDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
