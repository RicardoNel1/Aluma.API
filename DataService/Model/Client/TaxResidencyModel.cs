using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("tax_residency")]
    public class TaxResidencyModel : BaseModel
    {
        public ClientModel Client { get; set; }
        
        public ICollection<ForeignTaxResidencyModel> TaxResidencyItems { get; set; }
        //public ForeignTaxResidencyModel TaxResidencyItems { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string TaxNumber { get; set; }
        public bool TaxObligations { get; set; }
        public bool UsCitizen { get; set; }
        public bool UsRelinquished { get; set; }
        public bool UsOther { get; set; }
    }

    public class TaxResidencyModelBuilder : IEntityTypeConfiguration<TaxResidencyModel>
    {
        public void Configure(EntityTypeBuilder<TaxResidencyModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();
           
        }
    }

    [Table("foreign_tax_residency")]
    public class ForeignTaxResidencyModel : BaseModel
    {
        public TaxResidencyModel TaxResidency { get; set; }
        public int Id { get; set; }
        public int TaxResidencyId { get; set; }     
        public string Country { get; set; }
        public string TinNumber { get; set; }
        public string TinUnavailableReason { get; set; }
    }

    //public class ForeignTaxResidencyModelBuilder : IEntityTypeConfiguration<ForeignTaxResidencyModel>
    //{
    //    public void Configure(EntityTypeBuilder<ForeignTaxResidencyModel> mb)
    //    {
    //        mb.HasKey(x => x.Id);
    //        mb.Property(x => x.Id).ValueGeneratedOnAdd();


    //    }
    //}
}