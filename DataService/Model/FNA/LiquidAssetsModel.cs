using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("liquid_assets")]
    public class LiquidAssetsModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public EstateAllocationEnum AllocateTo { get; set; }
    }

    public class LiquidAssetsModelBuilder : IEntityTypeConfiguration<LiquidAssetsModel>
    {
        public void Configure(EntityTypeBuilder<LiquidAssetsModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

        }
    }

}