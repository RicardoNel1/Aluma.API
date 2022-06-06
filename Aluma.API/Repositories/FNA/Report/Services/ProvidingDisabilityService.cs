using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.FNA.Report;
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

        private string ReplaceHtmlPlaceholders(PersonalDetailDto client)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-disability.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[LastName]", client.Lastname);

            return result;

        }

        private PersonalDetailDto SetReportFields(ClientDto client, UserDto user)
        {
            return new PersonalDetailDto()
            {
                FirstName = user.FirstName,
                Lastname = user.LastName,
                SpouseFirstName = client.MaritalDetails?.FirstName,
                SpouseLastName = user.LastName
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            int clientId =  (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

            return ReplaceHtmlPlaceholders(SetReportFields(client, user));
        }

        public async Task<string> SetDisabilityDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
