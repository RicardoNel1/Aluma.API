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
        string GetCSS(string baseUrl);
    }

    public class FNAModulesService : BaseReportData, IFNAModulesService
    {

        public FNAModulesService(IWrapper repo)
        {
            _repo = repo;
        }

        public async Task<string> GetCoverPage(int fnaId)
        {
            ClientDto client = await GetClient(fnaId);
            UserDto user = await GetUser(client.UserId);

            return ReplaceCoverPageHtmlPlaceholders(client, user);
        }


        private static string ReplaceCoverPageHtmlPlaceholders(ClientDto client, UserDto user)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[name]", $"{user.FirstName ?? string.Empty} {user.LastName ?? string.Empty}");
            result = result.Replace("</body>", null);
            result = result.Replace("</html>", null);
            return result;
        }

        public string GetCSS(string baseUrl)
        {
            //"wwwroot/css/print.css"
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\css\print.css");
            string result = $"<style>{File.ReadAllText(path)}</style>";

            // result = result.Replace("../images/", Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\", "/"), @"wwwroot/img/"));
            result = result.Replace("../images/",  $@"{baseUrl}img/");
            return result;
        }
    }
}
