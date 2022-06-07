using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class RetirementPlanningReportDto
    {
        public string IncomeNeed { get; set; }
        public string Age { get; set; }
        public string RetirementAge { get; set; }
        public string EscalationPercent { get; set; }
        public string LifeExpectancy { get; set; }
        public string YearsBeforeRetirement { get; set; }
        public string YearsAfterRetirement { get; set; }
        public string RiskRating { get; set; }
        public string InvestmentReturnRate { get; set; }
        public string TotalAvailable { get; set; }
        public string TotalNeeds { get; set; }
        public string CapitalNeeds { get; set; }
        public string InflationRate { get; set; }
        public string NeedsRetirementTerm_Years { get; set; }
        public string OutstandingLiabilities { get; set; }
        public string AvailableCapital { get; set; }
        public string TotalCapital { get; set; }
        public string IncomeNeedsTotal { get; set; }
        public string IncomeAvailableTotal{ get; set; }
        public string MonthlySavingsRequired { get; set; }
        public string MonthlySavingsEscalating { get; set; }
        public GraphReportDto Graph { get; set; }


    }
}
