using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ProvidingOnDeathReportDto
    {
        public string TotalDeathAvailable { get; set; }
        public string TotalAvailable { get; set; }
        public string CapitalDreadNeeds { get; set; }
        public string SurplusProvDependants { get; set; }
        public string ShortfallSettEstate { get; set; }
        public string TotalShortfallDeath { get; set; }
        public string Age { get; set; }
        public string InvestmentReturns { get; set; }
        public string LifeExpectancy { get; set; }
        public string RiskInflation { get; set; }
        public string YearsTillLifeExpectancy { get; set; }
        public List<GraphReportDto> Graphs { get; set; }
    }
}
