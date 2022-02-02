using System;

namespace DataService.Dto
{
    public class RecordOfAdviceItemsDto
    {
        public int RecordOfAdviceId { get; set; }
        public string ProductName { get; set; }
        public double RecommendedLumpSum { get; set; }
        public double AcceptedLumpSum { get; set; }
        public double RecommendedRecurringPremium { get; set; }
        public double AcceptedRecurringPremium { get; set; }
        public string DeviationReason { get; set; }
    }
}