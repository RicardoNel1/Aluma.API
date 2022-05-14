using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class CapitalGainsTaxDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double PreviousCapitalLosses { get; set; }
        public double TotalCGTPayable { get; set; }
    }
}
