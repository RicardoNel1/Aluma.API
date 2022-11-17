using System.Collections.Generic;

namespace DataService.Dto
{
    public class ProvidingOnDreadReportDto
    {
        public string CapitalNeeds { get; set; }
        public string CapitalAndOr { get; set; }
        public string MultipleGrossAnnualSalary { get; set; }
        public string TotalNeeds { get; set; }
        public string DreadCoverAvailable { get; set; }
        public string DescDreadCoverAvailable { get; set; }
        public string AvailableCapital { get; set; }
        public string TotalAvailableCapital { get; set; }
        public double TotalDreadDisease { get; set; }
        public string DreadCoverAllowed { get; set; }
        public string Age { get; set; }
        public string CurrentNetIncome { get; set; }
        public string GrossMonthlyIncome { get; set; }
        public List<GraphReportDto> Graphs { get; set; }
    }
}
