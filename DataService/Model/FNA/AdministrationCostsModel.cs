using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("administration_costs")]
    public class AdministrationCostsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string OtherFixedProperty { get; set; }
        public double OtherFixedPropertyValue { get; set; }
        public double OtherConveyanceCosts { get; set; }
        public double AdvertisingCosts { get; set; }
        public double RatesAndTaxes { get; set; }
        public string OtherAdministrationCosts { get; set; }
        public double OtherAdministrationCostsValue { get; set; }
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