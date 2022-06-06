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

        private string ReplaceHtmlPlaceholders(PersonalDetailReportDto client)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-dread-disease.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[LastName]", client.LastName);

            return result;

        }

        private PersonalDetailReportDto SetReportFields(ClientDto client, UserDto user)
        {
            return new PersonalDetailReportDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
        }

        private async Task<string> GetReportData(int fnaId)
        {
            int clientId =  (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUser(new UserDto() { Id = client.UserId });

            return ReplaceHtmlPlaceholders(SetReportFields(client, user));
        }

        public async Task<string> SetDreadDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
