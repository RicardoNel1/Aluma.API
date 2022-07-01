namespace DataService.Dto
{
    public class RecordOfAdviceItemsDto
    {
        public int Id { get; set; } 
        public int RecordOfAdviceId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double RecommendedLumpSum { get; set; }
        public double AcceptedLumpSum { get; set; }
        public double RecommendedRecurringPremium { get; set; }
        public double AcceptedRecurringPremium { get; set; }
        public string DeviationReason { get; set; }
        public int CapitalProtection { get; set; }
    }
}