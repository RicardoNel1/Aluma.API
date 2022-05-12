using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class EstateDutyDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double Abatement { get; set; }
        public double Section4pValue { get; set; }
        public double LimitedRights { get; set; }
        public bool ResidueToSpouse { get; set; }
    }
}
