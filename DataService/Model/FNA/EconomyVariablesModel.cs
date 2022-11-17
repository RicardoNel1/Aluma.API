using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna_economy")]
    public class EconomyVariablesModel : BaseModel
    {    
        public int Id { get; set; }
        public double InflationRate { get; set; }
    }


    public class EconomyVariablesBuilder : IEntityTypeConfiguration<EconomyVariablesModel>
    {
        public void Configure(EntityTypeBuilder<EconomyVariablesModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasData(new EconomyVariablesModel()
            {
                Id = 1,
                InflationRate = 6
            });
        }
    }
}