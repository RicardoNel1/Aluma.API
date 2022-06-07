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
        public string LiabilitiesLabel { get; set; }
        public string TotalLiabilities { get; set; }
        public string TotalRetirementLabel { get; set; }
        public string TotalRetirement { get; set; }
        public string LiquidAssets { get; set; }
        public string Liabilities { get; set; }
        public string TotalDeathNeeds { get; set; }
        public string TotalDisability { get; set; }
        public string TotalDreadDisease { get; set; }
    }
}
