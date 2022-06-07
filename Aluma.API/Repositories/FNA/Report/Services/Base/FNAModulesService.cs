using Aluma.API.RepoWrapper;
using DataService.Dto;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aluma.API.Repositories.FNA.Report.Services.Base
{
    public interface IFNAModulesService
    {
        Task<string> GetCoverPage(int fnaId);
    }

    public class FNAModulesService : IFNAModulesService
    {
        private readonly IWrapper _repo;

        public FNAModulesService(IWrapper repo)
        {
            _repo = repo;
        }

        public async Task<string> GetCoverPage(int fnaId)
        {
            int clientId = (await _repo.FNA.GetClientFNAbyFNAId(fnaId)).ClientId;
            ClientDto client = _repo.Client.GetClient(new() { Id = clientId });
            UserDto user = _repo.User.GetUserWithAddress(new UserDto() { Id = client.UserId });

            return ReplaceCoverPageHtmlPlaceholders(client, user);
        }

        private string ReplaceCoverPageHtmlPlaceholders(ClientDto client, UserDto user)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[date]", DateTime.Now.ToString("yyyy-MM-dd"));
            result = result.Replace("[FirstName]", user.FirstName);
            result = result.Replace("[LastName]", user.LastName);
            result = result.Replace("[LastName]", user.LastName);
            result = result.Replace("</body>", null);
            result = result.Replace("</html>", null);
            return result;
        }
    }
}
