using Aluma.API.Helpers.Extensions;
using Aluma.API.Repositories.FNA.Report.Services.Base;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
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

    public class ProvidingDreadService : BaseReportData, IProvidingDreadService
    {

        public ProvidingDreadService(IWrapper repo)
        {
            _repo = repo;
        }

        private static string ReplaceHtmlPlaceholders(ProvidingOnDreadReportDto dreadDisease)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report-providing-on-dread-disease.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[CapitalNeeds]", dreadDisease.CapitalNeeds);
            result = result.Replace("[MultipleGrossAnnualSalary]", dreadDisease.MultipleGrossAnnualSalary);
            result = result.Replace("[TotalNeeds]", dreadDisease.TotalNeeds);
            result = result.Replace("[descDreadCoverAvailable]", dreadDisease.DescDreadCoverAvailable);
            result = result.Replace("[DreadCoverAvailable]", dreadDisease.DreadCoverAvailable);
            result = result.Replace("[TotalAvailableCapital]", dreadDisease.TotalAvailableCapital);
            result = result.Replace("[DreadDiseaseSurplus]", double.Parse(dreadDisease.TotalDreadDisease) > 0 ? "Surplus" : "Shortfall");
            result = result.Replace("[SurplusOnDread]", double.Parse(dreadDisease.TotalDreadDisease) > 0 ? dreadDisease.TotalDreadDisease : $"({double.Parse(dreadDisease.TotalDreadDisease) * -1})");
            result = result.Replace("[DreadCoverAllowed]", dreadDisease.DreadCoverAllowed);
            result = result.Replace("[TotalDreadDisease]", dreadDisease.TotalDreadDisease);
            result = result.Replace("[Age]", dreadDisease.Age);
            result = result.Replace("[CurrentNetIncome]", dreadDisease.CurrentNetIncome);
            result = result.Replace("[GrossMonthlyIncome]", dreadDisease.GrossMonthlyIncome);

            return result;

        }

        private static ProvidingOnDreadReportDto SetReportFields(ClientDto client, UserDto user,
                                                        AssumptionsDto assumptions,
                                                        ProvidingOnDreadDiseaseDto dreadDisease,
                                                        EconomyVariablesDto economy_variables)
        {
            return new ProvidingOnDreadReportDto()
            {
                CapitalNeeds = dreadDisease.Needs_CapitalNeeds.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                MultipleGrossAnnualSalary = dreadDisease.Needs_GrossAnnualSalaryTotal.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalNeeds = (dreadDisease.Needs_CapitalNeeds + dreadDisease.Needs_GrossAnnualSalaryTotal).ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                DreadCoverAvailable = dreadDisease.Available_DreadDiseaseAmount.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                DescDreadCoverAvailable = string.IsNullOrEmpty(dreadDisease.Available_DreadDiseaseDescription) ? string.Empty : dreadDisease.Available_DreadDiseaseDescription.ToString(),
                TotalAvailableCapital = dreadDisease.Available_DreadDiseaseAmount.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                TotalDreadDisease = (dreadDisease.Available_DreadDiseaseAmount - dreadDisease.Needs_CapitalNeeds).ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                CurrentNetIncome = assumptions.CurrentNetIncome.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,
                GrossMonthlyIncome = assumptions.CurrentGrossIncome.ToString("C", CultureInfo.CreateSpecificCulture("en-za")) ?? string.Empty,

                // Where to ge the data from ?????
                DreadCoverAllowed = (6000000).ToString("C", CultureInfo.CreateSpecificCulture("en-za"))

            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
                ClientDto client = await GetClient(fnaId);
                UserDto user = await GetUser(client.UserId);

                AssumptionsDto assumptions = GetAssumptions(fnaId);
                ProvidingOnDreadDiseaseDto dreadDisease = GetProvidingOnDreadDisease(client.UserId);
                EconomyVariablesDto economy_variables = GetEconomyVariablesSummary(fnaId);

                return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, dreadDisease, economy_variables));
        }

        public async Task<string> SetDreadDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
