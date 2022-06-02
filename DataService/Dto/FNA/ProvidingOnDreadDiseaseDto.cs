using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ProvidingOnDreadDiseaseDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double Needs_CapitalNeeds { get; set; }
        public double Needs_GrossAnnualSalaryMultiple { get; set; }
        public double Needs_GrossAnnualSalaryTotal { get; set; }
        public string Available_DreadDiseaseDescription { get; set; }
        public double Available_DreadDiseaseAmount { get; set; }
        public double TotalDreadDisease { get; set; }
        
    }
}

