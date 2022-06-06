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

            //result = result.Replace("[LastName]", client.Lastname);

            return result;

        }

        private ProvidingOnDisabilityReportDto SetReportFields(ClientDto client, UserDto user, AssumptionsDto assumptions)
        {
            //return new PersonalDetailDto()
            //{
            //    FirstName = user.FirstName,
            //    Lastname = user.LastName,
            //    SpouseFirstName = client.MaritalDetails?.FirstName,
            //    SpouseLastName = user.LastName
            //};

            return new ProvidingOnDisabilityReportDto()
            {
                TotalIncomeNeed = "",
            };

        //public string TotalIncomeNeed { get; set; }
        //public string Age { get; set; }
        //public string NeedsDisabilityTerm_Years { get; set; }
        //public string RetirementAge { get; set; }
        //public string EscDisabilityPercent { get; set; }
        //public string LifeExpectancy { get; set; }
        //public string CapitalDisabilityNeeds { get; set; }
        //public string YearsTillRetirement { get; set; }
        //public string InvestmentReturnRate { get; set; }
        //public string ShortTermProtection { get; set; }
        //public string InflationRate { get; set; }
        //public string LongTermProtectionIncome { get; set; }
        //public string Capital { get; set; }
        //public string CurrentNetIncome { get; set; }
        //public string CapitalAndCapitalizedNeeds { get; set; }
        //public string CapitalNeeds { get; set; }
        //public string CapitalizedIncomeShortfall { get; set; }
        //public string AvailableCapital { get; set; }
        //public string TotalCapShortfall { get; set; }
        //public string MaxAdditionalCap { get; set; }
        //public GraphDto CapitalSolutionGraph { get; set; }

    }

        private async Task<string> GetReportData(int fnaId)
        {
            int clientId =  (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

            //Assumptions
            //string age = (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString();

            AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);

            string retirementAge = assumptions.RetirementAge.ToString();
            string lifeExpectancy = assumptions.LifeExpectancy.ToString();
            string yearsTillRetirement = assumptions.YearsTillRetirement.ToString();

            string curreneNetIncome = assumptions.CurrentNetIncome.ToString();

            //string investmentReturns
            //string inflationRate

            //Objective
            //string IncomeNeedsObjective
            //string termYearsNeedsObjective
            //string escalationNeedsObjective
            //string capitalNeedsObjective

            //Available
            //string shortTermIncomeAvailable
            //string longTermIncomeAvailable
            //string capitalAvailble

            //Capital Solution
            //string totalNeedsCapital
            //string CapitalNeedsCapital
            //string CapitalizedIncomeCapital
            //string AvailiableCapital
            //string TotalCapital

            //string maxAdditionalCapital


            return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions));
        }

        public async Task<string> SetDisabilityDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
