namespace DataService.Dto
{
    public class MedicalAidDTO : ApiResponseDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Provider { get; set; }
        public string Type { get; set; }
        public string MedicalAidNumber { get; set; }
        public bool MainMember { get; set; } = true;
        public bool NetworkPlan { get; set; } = true;
        public bool SavingsPlan { get; set; } = true;
        public bool GapCover { get; set; } = true;
        public int NumberOfDependants { get; set; }
        public double MonthlyPremium { get; set; }
        public double MaxAnnualSavings { get; set; }
    }
}