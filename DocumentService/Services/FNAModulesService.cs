using System;
using System.IO;

namespace DocumentService.Services
{
    public interface IFNAModulesService
    {
        public string ClientModule(int ClientId);
        public string OverviewModule(int ClientId);
    }

    public class FNAModulesService : IFNAModulesService
    {
        public string ClientModule(int ClientId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-personal-details.html");

            string result = File.ReadAllText(path);

            result = result.Replace("[clientSurname]", "Tiago");
            return result;
        }

        public string OverviewModule(int ClientId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-quick-review.html");

            string result = File.ReadAllText(path);

            //result = result.Replace("[clientSurname]", "Tiago");
            return result;
        }
    }
}
