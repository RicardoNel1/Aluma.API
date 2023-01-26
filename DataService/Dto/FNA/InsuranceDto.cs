using System;

namespace DataService.Dto
{
    public class InsuranceDto: ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public string Beneficiary { get; set; }
        public double LifeCover { get; set; }
        public double Disability { get; set; }
        public double DreadDisease { get; set; }
        public double AbsoluteIpPm { get; set; }
        public double ExtendedIpPm { get; set; }
        public string AllocateTo { get; set; }
        public string DataSource { get; set; }
        public DateTime Modified { get; set; }
    }
}
