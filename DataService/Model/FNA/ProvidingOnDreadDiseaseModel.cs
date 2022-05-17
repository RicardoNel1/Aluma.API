﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_providing_on_dread_disease")]
    public class ProvidingOnDreadDiseaseModel : BaseModel
    {
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double Needs_CapitalNeeds { get; set; }
        public double Needs_GrossAnnualSalary { get; set; }
        public string Available_DreadDiseaseDescription { get; set; }
        public double Available_DreadDiseaseAmount { get; set; }
    }

    public class ProvidingOnDreadDiseaseModelBuilder : IEntityTypeConfiguration<ProvidingOnDreadDiseaseModel>
    {
        public void Configure(EntityTypeBuilder<ProvidingOnDreadDiseaseModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.FNAId).IsUnique();
           
        }
    }

}