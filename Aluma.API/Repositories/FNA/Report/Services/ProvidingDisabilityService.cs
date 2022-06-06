﻿using Aluma.API.Extensions;
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
    public interface IProvidingDisabilityService
    {
        Task<string> SetDisabilityDetail(int fnaId);
    }

    public class ProvidingDisabilityService : IProvidingDisabilityService
    {
        private readonly IWrapper _repo;

        public ProvidingDisabilityService(IWrapper repo)
        {
            _repo = repo;
        }

        private string ReplaceHtmlPlaceholders(ProvidingOnDisabilityReportDto disability)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-disability.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[IncomeNeed]", disability.IncomeNeed);
            result = result.Replace("[Age]", disability.Age);
            result = result.Replace("[TermYears]", disability.NeedsDisabilityTerm_Years);
            result = result.Replace("[RetirementAge]", disability.RetirementAge);
            result = result.Replace("[EscPercent]", disability.EscDisabilityPercent);
            result = result.Replace("[LifeExpectancy]", disability.LifeExpectancy);
            result = result.Replace("[CapitalNeeds]", disability.CapitalDisabilityNeeds);
            result = result.Replace("[YearsTillRetirement]", disability.YearsTillRetirement);
            result = result.Replace("[InvestmentReturnRate]", disability.InvestmentReturnRate);
            result = result.Replace("[ShortTermProtection]", disability.ShortTermProtectionIncome);
            result = result.Replace("[InflationRate]", disability.InflationRate);
            result = result.Replace("[LongTermProtectionIncome]", disability.LongTermProtectionIncome);
            result = result.Replace("[TotalAvailable]", disability.TotalAvailable);
            result = result.Replace("[CurrentNetIncome]", disability.CurrentNetIncome);
            result = result.Replace("[TotalNeeds]", disability.TotalNeeds);
            result = result.Replace("[CapitalizedIncomeShortfall]", disability.CapitalizedIncomeShortfall);
            result = result.Replace("[TotalCapitalShortfall]", disability.TotalCapShortfall);
            result = result.Replace("[date]", DateTime.Now.ToString("yyyy/MM/dd"));

            return result;

        }

        private ProvidingOnDisabilityReportDto SetReportFields(ClientDto client, UserDto user, 
                                                                AssumptionsDto assumptions,
                                                                ProvidingOnDisabilityDto disability,
                                                                ProvidingDisabilitySummaryDto summary_disability,
                                                                EconomyVariablesDto economy_variables)
        {

            return new ProvidingOnDisabilityReportDto()
            {
                Age = string.IsNullOrEmpty(user.DateOfBirth) ? string.Empty : (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                RetirementAge = assumptions.RetirementAge.ToString(),
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                YearsTillRetirement = assumptions.YearsTillRetirement.ToString(),
                CurrentNetIncome = assumptions.CurrentNetIncome.ToString(),
                InvestmentReturnRate = economy_variables.InvestmentReturnRate.ToString(),
                InflationRate = economy_variables.InflationRate.ToString(),
                IncomeNeed = disability.IncomeNeeds.ToString(),
                NeedsDisabilityTerm_Years = disability.NeedsTerm_Years.ToString(),
                EscDisabilityPercent = economy_variables.InflationRate.ToString(),
                CapitalDisabilityNeeds = disability.CapitalNeeds.ToString(),
                ShortTermProtectionIncome = summary_disability.TotalExistingShortTermIncome.ToString(),
                LongTermProtectionIncome = summary_disability.TotalExistingLongTermIncome.ToString(),
                TotalAvailable = summary_disability.TotalAvailable.ToString(),
                TotalNeeds = summary_disability.TotalNeeds.ToString(),
                CapitalNeeds = disability.CapitalNeeds.ToString(),
                CapitalizedIncomeShortfall = summary_disability.TotalIncomeNeed.ToString(),
                TotalCapShortfall = (summary_disability.TotalAvailable - summary_disability.TotalNeeds).ToString()

            };

        }

        private async Task<string> GetReportData(int fnaId)
        {
            int clientId =  (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

            AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);
            ProvidingOnDisabilityDto disability = _repo.ProvidingOnDisability.GetProvidingOnDisability(fnaId);
            ProvidingDisabilitySummaryDto summary_disability = _repo.ProvidingDisabilitySummary.GetProvidingDisabilitySummary(fnaId);
            EconomyVariablesDto economy_variables = _repo.EconomyVariablesSummary.GetEconomyVariablesSummary(fnaId);

            return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions, disability, summary_disability, economy_variables));
        }

        public async Task<string> SetDisabilityDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
