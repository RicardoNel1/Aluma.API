using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class QuickReviewReport
    {
        public string TotalAssets { get; set; }
        public string TotalLiquidAssets { get; set; }
        public string TotalLiabilities { get; set; }
        public string TotalRetirementLabel { get; set; }
        public string TotalRetirement { get; set; }
        public string SavingsRequiredPremium { get; set; }
    }
}
