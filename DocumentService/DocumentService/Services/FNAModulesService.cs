using System;
using System.IO;

namespace DocumentService.Services
{
    public interface IFNAModulesService
    {
        public string ClientModule(int FNAId);
        public string OverviewModule(int FNAId);
        public string ProvidingOnDisabilityCapitalSolution(int FNAId);
        //JavaScript

        public string CapitalSolutionGraphJavascript(int FNAId);

    }

    public class FNAModulesService : IFNAModulesService
    {
        public string ClientModule(int FNAId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-personal-details.html");

            string result = File.ReadAllText(path);

            result = result.Replace("[clientSurname]", "Tiago");
            return result;
        }

        public string OverviewModule(int FNAId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-quick-review.html");

            string result = File.ReadAllText(path);

            //result = result.Replace("[clientSurname]", "Tiago");
            return result;
        }

        public string ProvidingOnDisabilityCapitalSolution(int FNAId)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/aluma-fna-report-providing-on-disability.html");

            string result = File.ReadAllText(path);

            var graph = CapitalSolutionGraph();

            result = result.Replace("[CapitalSolutionGraph]", graph);

            return result;
        }

        private string CapitalSolutionGraph()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot/html/capital-solution-graph.html");

            string result = File.ReadAllText(path);

            return result;
        }

        public string CapitalSolutionGraphJavascript(int FNAId)
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
