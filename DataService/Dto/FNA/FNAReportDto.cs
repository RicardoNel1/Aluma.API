namespace DataService.Dto
{
    public class FNAReportDto : ApiResponseDto
    {
        public int FNAId { get; set; }
        public bool ClientModule { get; set; }
        public bool ProvidingOnDisability { get; set; }
        public bool ProvidingOnDreadDisease { get; set; }
        public bool ProvidingOnDeath { get; set; }
        public bool RetirementPlanning { get; set; }
        public bool InvestmentPlanning { get; set; }
    }
}
