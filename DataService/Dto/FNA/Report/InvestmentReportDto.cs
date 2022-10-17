using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class InvestmentReportDto
    {
        public string EscalationPercent { get; set; }
        public string OutstandingLiabilities { get; set; }
        public string AvailableCapital { get; set; }
        public string IncomeAvailableTotal{ get; set; }
        public string InvestmentReturnRate { get; set; }
        public string RiskRating { get; set; }
        public string InflationRate { get; set; }
        public string TotalAvailable { get; set; }
        public string ExhaustionPeriod { get; set; }
        public string IncomeNeedsTotal { get; set; }
        public string DescTotalCapital { get; set; }
        public string TotalCapital { get; set; }
        public string MonthlySavingsRequired { get; set; }
        public string MonthlySavingsEscalating { get; set; }
        public GraphReportDto CapitalGraph { get; set; }
        public GraphReportDto AnnualGraph { get; set; }
    }
}
