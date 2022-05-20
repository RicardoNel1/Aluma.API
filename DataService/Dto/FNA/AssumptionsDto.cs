using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class AssumptionsDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double RetirementAge { get; set; }
        public double CurrentGrossIncome { get; set; }
        public string RetirementInvestmentRisk { get; set; }
        public string DeathInvestmentRisk { get; set; }
        public string DisabilityInvestmentRisk { get; set; }
    }
}
