using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class TaxLumpsumDto
    {
        public int Id { get; set; }
        public int FnaId { get; set; }
        public double PreviouslyDisallowed { get; set; }
        public double RetirementReceived { get; set; }
        public double WithdrawalReceived { get; set; }
        public double SeverenceReceived { get; set; }
        public double TaxPayable { get; set; }
    }
}
