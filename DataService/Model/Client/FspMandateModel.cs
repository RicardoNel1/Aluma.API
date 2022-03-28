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
        public string DiscretionType { get; set; }              
        public string InvestmentObjective { get; set; }
        public string LimitedInstruction { get; set; }

        //Voting
        public string VoteInstruction { get; set; }

        //Managed Account Fees
        public string PortfolioManagementFee { get; set; }
        public string InitialFee { get; set; }
        public string AdditionalAdvisorFee { get; set; }
        public string ForeignInvestmentInitialFee { get; set; }
        public string ForeignInvestmentAnnualFee { get; set; }

        //Commisions
        public string AdminFee { get; set; }

        //Dividend
        public string DividendInstruction { get; set; }       

    }

    public class FSPModelBuilder : IEntityTypeConfiguration<FSPMandateModel>
    {
        public void Configure(EntityTypeBuilder<FSPMandateModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            //mb.HasIndex(c => c.ClientId).IsUnique();
        }
    }
}