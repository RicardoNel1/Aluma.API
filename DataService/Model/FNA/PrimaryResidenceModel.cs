﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("primary_residence")]
    public class PrimaryResidenceModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }        
        public double BaseCost { get; set; }
        public double Growth { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
        public bool DisposedAtRetirement { get; set; }
        public bool DisposedOnDisability { get; set; }        

    }

    public class PrimaryResidenceModelBuilder : IEntityTypeConfiguration<PrimaryResidenceModel>
    {
        public void Configure(EntityTypeBuilder<PrimaryResidenceModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

}