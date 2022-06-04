using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Dto.FNA.Report
{
    public class ProvidingOnDread
    {
        public string CapitalNeeds { get; set; }
        public string CapitalAndOr { get; set; }
        public string MultipleGrossAnnualSalary { get; set; }
        public string TotalNeeds { get; set; }
        public string DreadCoverAvailable { get; set; }
        public string TotalAvailableCapital { get; set; }
        public string SurplusOnDread { get; set; }
        public string DreadCoverAllowed { get; set; }
        public string Age { get; set; }
        public string CurrentNetIncome { get; set; }
        public List<GraphDto> Graphs { get; set; }
    }
}
