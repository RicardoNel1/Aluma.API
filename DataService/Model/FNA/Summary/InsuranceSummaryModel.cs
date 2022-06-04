using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_summary_insurance")]
    public class InsuranceSummaryModel : BaseModel
    {
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double TotalToSpouse { get; set; }
        public double TotalToThirdParty { get; set; }
        public double TotalToLiquidity{ get; set; }
    }
}