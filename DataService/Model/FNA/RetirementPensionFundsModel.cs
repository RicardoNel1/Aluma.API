﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("retirement_pension_funds")]
    public class RetirementPensionFundsModel : BaseModel
    {
        public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double MonthlyContributions { get; set; }
        public double EscPercent { get; set; }

    }

    public class RetirementPensionFundsModelBuilder : IEntityTypeConfiguration<RetirementPensionFundsModel>
    {
        public void Configure(EntityTypeBuilder<RetirementPensionFundsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}