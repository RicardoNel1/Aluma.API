using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("providing_on_death")]
    public class ProvidingOnDeathModel : BaseModel
    {
        public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }        
        public double RetirementFunds { get; set; }
        public double ApprovedGroupLife { get; set; }
        public double PreTaxIncome { get; set; }
        public int IncomeTerm_Years { get; set; }
        public double IncomeEscalation { get; set; }
        public double IncomeNeeds { get; set; }
        public int NeedsTerm_Years { get; set; }
        public double IncomeNeedsEscalation { get; set; }
        public double CapitalNeeds { get; set; }

    }

    public class ProvidingOnDeathModelBuilder : IEntityTypeConfiguration<ProvidingOnDeathModel>
    {
        public void Configure(EntityTypeBuilder<ProvidingOnDeathModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

}