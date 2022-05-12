using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_assets_attracting_cgt")]
    public class AssetsAttractingCGTModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }

        public double RecurringPremium { get; set; }
        public double EscPercent { get; set; }
        public double Growth { get; set; }
        public PropertyTypeEnum PropertyType { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
        public double BaseCost { get; set; }
        public bool DisposedAtRetirement { get; set; }
        public bool DisposedOnDisability { get; set; }
    }

    public class AssetsAttractingCGTModelBuilder : IEntityTypeConfiguration<AssetsAttractingCGTModel>
    {
        public void Configure(EntityTypeBuilder<AssetsAttractingCGTModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }

}