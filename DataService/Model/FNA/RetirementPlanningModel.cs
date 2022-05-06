using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("retirement_planning")]
    public class RetirementPlanningModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }        
        public double MonthlyIncome { get; set; }
        public int TermPostRetirement_Years { get; set; }
        public double IncomeEscalation { get; set; }
        public double IncomeNeeds { get; set; }
        public int NeedsTerm_Years { get; set; }
        public double IncomeNeedsEscalation { get; set; }
        public double CapitalNeeds { get; set; }
        public double OutstandingLiabilities { get; set; }
        public double SavingsEscalation { get; set; }

    }

    public class RetirementPlanningModelBuilder : IEntityTypeConfiguration<RetirementPlanningModel>
    {
        public void Configure(EntityTypeBuilder<RetirementPlanningModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

}