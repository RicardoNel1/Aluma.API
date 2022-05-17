using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DataService.Enum;

namespace DataService.Model
{
    [Table("fna_liabilities")]
    public class LiabilitiesModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public ClientFNAModel FNA { get; set; }
        public int FNAId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
    }

    public class LiabilitiesModelBuilder : IEntityTypeConfiguration<LiabilitiesModel>
    {
        public void Configure(EntityTypeBuilder<LiabilitiesModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }

}