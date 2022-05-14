﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model
{
    [Table("capital_gains_tax")]
    public class CapitalGainsTaxModel : BaseModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public double PreviousCapitalLosses { get; set; }
        public double TotalCGTPayable { get; set; }
    }

    public class CapitalGainsTaxModelBuilder : IEntityTypeConfiguration<CapitalGainsTaxModel>
    {
        public void Configure(EntityTypeBuilder<CapitalGainsTaxModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }
}