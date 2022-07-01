namespace DataService.Dto
{

    public class InvestmentsDto: ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool AttractingCGT { get; set; }
        public string AllocateTo { get; set; }
        public double Contribution { get; set; }
        public double Escalating { get; set; }
        public string InvestmentType { get; set; }
        public double Bonds { get; set; }
        public double Equity { get; set; }
        public double Property { get; set; }
        public double OffshoreBonds { get; set; }
        public double OffshoreEquity { get; set; }
        public double OffshoreProperty { get; set; }
        public double PrivateEquity { get; set; }
        public double Cash { get; set; }
    }

}