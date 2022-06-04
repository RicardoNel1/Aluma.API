using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.FNA.Report
{
    public class ProvidingOnDisabilityDto
    {
        public string TotalIncomeNeed { get; set; }
        public string Age { get; set; }
        public string NeedsDisabilityTerm_Years { get; set; }
        public string RetirementAge { get; set; }
        public string EscDisabilityPercent { get; set; }
        public string LifeExpectancy { get; set; }
        public string CapitalDisabilityNeeds { get; set; }
        public string YearsTillRetirement { get; set; }
        public string InvestmentReturnRate { get; set; }
        public string ShortTermProtection { get; set; }
        public string InflationRate { get; set; }
        public string LongTermProtectionIncome { get; set; }
        public string Capital { get; set; }
        public string CurrentNetIncome { get; set; }
        public string CapitalAndCapitalizedNeeds { get; set; }
        public string CapitalNeeds { get; set; }
        public string CapitalizedIncomeShortfall { get; set; }
        public string AvailableCapital { get; set; }
        public string TotalCapShortfall { get; set; }
        public string MaxAdditionalCap { get; set; }
        public GraphDto CapitalSolutionGraph { get; set; }
    }
}
