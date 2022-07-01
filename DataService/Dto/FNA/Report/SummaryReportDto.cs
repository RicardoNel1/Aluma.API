using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class SummaryReportDto
    {
        public string TotalAssets { get; set; }
        public string TotalLiquidAssets { get; set; }
        public string TotalLiabilities { get; set; }
        public string LiquidityLabel { get; set; }
        public string TotalLiquidity { get; set; }

        public bool IsSurplus { get; set; }
        public string TotalRetirementLabel { get; set; }
        public string TotalRetirement { get; set; }
        public string SavingsRequired { get; set; }
        public string EscPercentage { get; set; }
        public string ExistingRetirementFund { get; set; }
        public string YearsToRetirement { get; set; }

        public string DeathNeedsLabel { get; set; }
        public string TotalDeathNeeds { get; set; }
        public string DisabilityNeedsLabel { get; set; }
        public string TotalDisabilityNeeds { get; set; }
        public string DreadDiseaseLabel { get; set; }
        public string TotalDreadDisease { get; set; }
    }
}
