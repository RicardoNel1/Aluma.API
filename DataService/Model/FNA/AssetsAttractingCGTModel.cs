using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("assets_attracting_cgt")]
    public class AssetsAttractingCGTModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
        public double BaseCost { get; set; }
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