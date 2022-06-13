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
        string GetCSS();
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

        private static string ReplaceCoverPageHtmlPlaceholders(ClientDto client, UserDto user)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\html\aluma-fna-report.html");
            string result = File.ReadAllText(path);

            result = result.Replace("[name]", $"{user.LastName ?? string.Empty} {user.LastName ?? string.Empty}");
            result = result.Replace("</body>", null);
            result = result.Replace("</html>", null);
            return result;
        }

        public string GetCSS()
        {
            //"wwwroot/css/print.css"
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"wwwroot\css\print.css");
            string result = $"<style rel=\"stylesheet\" type=\"text/css\">{File.ReadAllText(path)}</style>";

            result = result.Replace("../images/", Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Replace("\\","/"), @"wwwroot/img/"));
            return result;
        }
    }
}
