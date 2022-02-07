using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fsp_mandate")]
    public class FSPMandateModel : BaseModel
    {
        public ClientModel Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }

        //Discretion
        public string Discretion { get; set; }
        
        //Full
        public string InvestmentObjective { get; set; }
        
        //Limited
        public bool MyProperty { get; set; }

        public string InstructionPersonal { get; set; }
        public string InstructionAdvisor { get; set; }
        public string InstructionFsp { get; set; }
        public string Advisor { get; set; }
        public string PayoutOption { get; set; }
        public string Vote { get; set; }
        public string DividendInstruction { get; set; }
        public string MonthlyFee { get; set; }
        public string InitialFee { get; set; }
        public string AnnualFee { get; set; }
        public string AdditionalFee { get; set; }
        public string AdminFee { get; set; }

        public string FspSignatory { get; set; }
        public string AtFsp { get; set; }
        public string DateFsp { get; set; }
        public string AtClient { get; set; }
        public string DateClient { get; set; }
        public string Objective { get; set; }
    }

    public class FSPModelBuilder : IEntityTypeConfiguration<FSPMandateModel>
    {
        public void Configure(EntityTypeBuilder<FSPMandateModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
        }
    }
}