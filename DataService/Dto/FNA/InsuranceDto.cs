namespace DataService.Dto
{
    public class InsuranceDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public double LifeCover { get; set; }
        public double Disability { get; set; }
        public double DreadDisease { get; set; }
        public double AbsoluteIpPm { get; set; }
        public double ExtendedIpPm { get; set; }
        public string AllocateTo { get; set; }
    }
}
