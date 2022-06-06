namespace DataService.Dto
{
    public class ProvidingOnDisabilityDto : ApiResponseDto
    {
        public int Id { get; set; }
        public int FNAId { get; set; }
        public double ShortTermProtection { get; set; }
        public int IncomeProtectionTerm_Months { get; set; }
        public double LongTermProtection { get; set; }
        public double IncomeNeeds { get; set; }
        public int NeedsTerm_Years { get; set; }
        public double LiabilitiesToClear { get; set; }
        public double CapitalNeeds { get; set; }
        public string Satus { get; set; }
        public string Message { get; set; }
    }
}



