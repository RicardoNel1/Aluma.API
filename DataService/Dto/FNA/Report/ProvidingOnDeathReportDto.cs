using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ProvidingOnDeathReportDto
    {
        public string TotalCapital { get; set; }
        public string AvailableCapital { get; set; }
        public string descSurplusProviding { get; set; } //rename
        public string SurplusProviding { get; set; } //rename
        public string descSettlingEstate { get; set; }
        public string SettlingEstate { get; set; }
        public string descTotalOnDeath { get; set; }
        public string TotalOnDeath { get; set; }
        public string Age { get; set; }
        public string InvestmentReturns { get; set; }
        public string LifeExpectancy { get; set; }
        public string InflationRate { get; set; }
        public string YrsTillLifeExpectancy { get; set; }
        public GraphReportDto IncomeGraph { get; set; }
        public GraphReportDto LumpsumGraph { get; set; }
        public GraphReportDto CapitalizedGraph { get; set; }
    }
}
