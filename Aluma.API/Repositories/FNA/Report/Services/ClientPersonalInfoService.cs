using Aluma.API.Extensions;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.FNA.Report;
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

        private string ReplaceHtmlPlaceholders(PersonalDetailDto client)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-personal-details.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[FirstName]", client.FirstName);
            result = result.Replace("[LastName]", client.Lastname);
            result = result.Replace("[SpouseFirstName]", client.SpouseFirstName);
            result = result.Replace("[SpouseLastName]", client.SpouseLastName);
            result = result.Replace("[RSAIdNumber]", client.RSAIdNumber);
            result = result.Replace("[SpouseRSAIdNumber]", client.SpouseRSAIdNumber);
            result = result.Replace("[DateOfBirth]", client.DateOfBirth);
            result = result.Replace("[SpouseClientAge]", client.SpouseClientAge);
            result = result.Replace("[SpouseGender]", client.SpouseGender);
            result = result.Replace("[LifeExpectancy]", client.LifeExpectancy);
            result = result.Replace("[MaritalStatus]", client.MaritalStatus);
            result = result.Replace("[SpouseMaritalStatus]", client.SpouseMaritalStatus);
            result = result.Replace("[DateOfMarriage]", client.DateOfMarriage);
            result = result.Replace("[SpouseDateOfMarriage]", client.SpouseDateOfMarriage);
            result = result.Replace("[Email]", client.Email);
            result = result.Replace("[SpouseEmail]", client.SpouseEmail);
            result = result.Replace("[WorkNumber]", client.WorkNumber);
            result = result.Replace("[SpouseWorkNumber]", client.SpouseWorkNumber);
            result = result.Replace("[ClientAddress]", client.ClientAddress);
            result = result.Replace("[ClientPostal]", client.ClientPostal);

            return result;

        }

        private PersonalDetailDto SetReportFields(ClientDto client, UserDto user, AssumptionsDto assumptions)
        {
            return new PersonalDetailDto()
            {
                FirstName = user.FirstName,
                Lastname = user.LastName,
                SpouseFirstName = client.MaritalDetails?.FirstName,
                SpouseLastName = client.MaritalDetails?.Surname,
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
                ClientAddress = user.Address?.Where(x => x.Type == AddressTypesEnum.Residential.ToString()).ToString(),
                ClientPostal = user.Address?.Where(x => x.Type == AddressTypesEnum.Postal.ToString()).ToString()
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
