using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("record_of_advice_items")]
    public class RecordOfAdviceItemsModel : BaseModel
    {
        public RecordOfAdviceModel RecordOfAdvice { get; set; }
        public int Id { get; set; }
        public int RecordOfAdviceId { get; set; }
        public int ProductId { get; set; }
        public double RecommendedLumpSum { get; set; }
        public double AcceptedLumpSum { get; set; }
        public double RecommendedRecurringPremium { get; set; }
        public double AcceptedRecurringPremium { get; set; }
        public string DeviationReason { get; set; }
    }
}