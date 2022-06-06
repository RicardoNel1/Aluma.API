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
        public string OverviewModule(int fnaId);
        //JavaScript

        public string CapitalSolutionGraphJavascript(int fnaId);

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
            return result;
        }





















        public string OverviewModule(int fnaId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-summary.html");

            string result = File.ReadAllText(path);

            //result = result.Replace("[clientSurname]", "Tiago");
            return result;
        }

        private string CapitalSolutionGraph()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/capital-solution-graph.html");

            string result = File.ReadAllText(path);

            return result;
        }

        public string CapitalSolutionGraphJavascript(int fnaId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/js/capital-solution-graph.js");

            string result = File.ReadAllText(path);

            result = result.Replace("[Capitalized-Income-shortfall]", "2749934")
                .Replace("[Lump-sum-Needs]", "0")
                .Replace("[Available-Lump-sums]", "1332225")
                .Replace("[Total-Lump-sum-Shortfall]", "1417709");

            return result;
        }
    }
}
