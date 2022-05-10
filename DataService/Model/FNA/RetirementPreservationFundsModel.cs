using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("retirement_preservation_funds")]
    public class RetirementPreservationFundsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Growth { get; set; }

    }

    public class RetirementPreservationFundsModelBuilder : IEntityTypeConfiguration<RetirementPreservationFundsModel>
    {
        public void Configure(EntityTypeBuilder<RetirementPreservationFundsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}