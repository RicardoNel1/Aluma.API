using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class EconomyVariablesDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double InflationRate { get; set; }
        public double InvestmentReturnRate { get; set; }
    }
}


