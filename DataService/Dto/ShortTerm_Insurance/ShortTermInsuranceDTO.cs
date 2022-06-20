using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ShortTermInsuranceDTO : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Provider { get; set; }
        public string Type { get; set; }
        public string PolicyNumber { get; set; }
        public double MonthlyPremium { get; set; }
    }
}