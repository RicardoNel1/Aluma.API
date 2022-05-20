using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_administration_costs")]
    public class AdministrationCostsModel : BaseModel
    {      
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public double OtherConveyanceCosts { get; set; }
        public double AdvertisingCosts { get; set; }
        public double RatesAndTaxes { get; set; }
        public string OtherAdminDescription { get; set; }
        public double OtherAdminCosts { get; set; }
        public double TotalEstimatedCosts { get; set; }
    }

    public class AdministrationCostsModelBuilder : IEntityTypeConfiguration<AdministrationCostsModel>
    {
        public void Configure(EntityTypeBuilder<AdministrationCostsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }

}