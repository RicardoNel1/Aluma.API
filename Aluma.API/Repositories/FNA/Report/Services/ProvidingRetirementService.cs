using Aluma.API.RepoWrapper;
using DataService.Dto;
using DataService.Dto.FNA.Report;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Service
{
    public interface IProvidingRetirementService
    {
        Task<string> SetRetirementDetail(int fnaId);
    }

    public class ProvidingRetirementService : IProvidingRetirementService
    {
        private readonly IWrapper _repo;

        public ProvidingRetirementService(IWrapper repo)
        {
            _repo = repo;
        }

        private string ReplaceHtmlPlaceholders(PersonalDetailDto client)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-retirement-planning.html");
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

        public async Task<string> SetRetirementDetail(int fnaId)
        {
            return await GetReportData(fnaId);
        }


    }
}
