using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto
{
    public class ProvidingOnDreadDiseaseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double Needs_CapitalNeeds { get; set; }
        public double Needs_GrossAnnualSalary { get; set; }
        public string Available_DreadDiseaseDescription { get; set; }
        public double Available_DreadDiseaseAmount { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}

