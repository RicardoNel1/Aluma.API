using System;

namespace DataService.Dto
{
    public class FSPMandateDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }        
        public string StartDate { get; set; }        
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