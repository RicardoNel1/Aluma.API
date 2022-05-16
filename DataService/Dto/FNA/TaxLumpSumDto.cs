using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class TaxLumpSumDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public float PreviouslyDisallowed { get; set; }
        public float RetirementReceived { get; set; }
        public float WithdrawalReceived { get; set; }
        public float SeverenceReceived { get; set; }
        public float TaxPayable { get; set; }
    }
}
