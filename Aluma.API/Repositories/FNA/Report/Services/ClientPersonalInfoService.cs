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

        private string ReplaceHtmlPlaceholders(PersonalDetailReportDto client)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-personal-details.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[LastName]", client.Lastname);

            return result;

        }

        private PersonalDetailReportDto SetReportFields(ClientDto client, UserDto user, AssumptionsDto assumptions)
        {
            return new PersonalDetailReportDto()
            {
                FirstName = user.FirstName,
                Lastname = user.LastName,
                SpouseFirstName = client.MaritalDetails?.FirstName,
                SpouseLastName = user.LastName,
                RSAIdNumber = user.RSAIdNumber,
                SpouseRSAIdNumber = client.MaritalDetails?.IdNumber,
                DateOfBirth = user.DateOfBirth,
                ClientAge = (Convert.ToDateTime(user.DateOfBirth)).CalculateAge().ToString(),
                //SpouseClientAge
                //SpouseGender
                LifeExpectancy = assumptions.LifeExpectancy.ToString(),
                MaritalStatus = client.MaritalDetails?.MaritalStatus,
                SpouseMaritalStatus = client.MaritalDetails?.MaritalStatus,
                DateOfMarriage = client.MaritalDetails?.DateOfMarriage,
                SpouseDateOfMarriage = client.MaritalDetails?.DateOfMarriage,
                Email = user.Email,
                //SpouseEmail
                WorkNumber = user.MobileNumber,
                //SpouseWorkNumber
               // ClientAddress = user.Address?.Where(x => x.Type == "1").
               // ClientPostal
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            int clientId =  (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });
            AssumptionsDto assumptions = _repo.Assumptions.GetAssumptions(fnaId);

            return ReplaceHtmlPlaceholders(SetReportFields(client, user, assumptions));
        }

        public async Task<string> SetPersonalDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
