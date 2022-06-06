using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_summary_retirement")]
    public class RetirementSummaryModel : BaseModel
    {
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double TotalPensionFund { get; set; }
        public double TotalPreservation { get; set; }
        public double TotalNeeds{ get; set; }
        public double TotalAvailable { get; set; }
        public double SavingsRequiredPremium { get; set; }
    }
}