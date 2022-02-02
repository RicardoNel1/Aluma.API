using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fsp_mandate")]
    public class FSPMandateModel : BaseModel
    {
        public ApplicationModel Application { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string ClientDetails { get; set; }
        public string Products { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string AccNo { get; set; }
        public string StartDate { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string Address_3 { get; set; }
        public string Email { get; set; }
        public string FspSignatory { get; set; }
        public string AtFsp { get; set; }
        public string DateFsp { get; set; }
        public string AtClient { get; set; }
        public string DateClient { get; set; }
        public string Objective { get; set; }
        public string InstructionPersonal { get; set; }
        public string InstructionAdvisor { get; set; }
        public string InstructionFsp { get; set; }
        public string Advisor { get; set; }
        public string PayoutOption { get; set; }
        public string Discretion { get; set; }
        public string Vote { get; set; }
        public string DividendInstruction { get; set; }
        public string MonthlyFee { get; set; }
        public string InitialFee { get; set; }
        public string AnnualFee { get; set; }
        public string AdditionalFee { get; set; }
        public string AdminFee { get; set; }
    }
}