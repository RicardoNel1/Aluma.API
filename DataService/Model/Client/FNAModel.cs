using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("fna")]
    public class FNAModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public int ClientId { get; set; }

    }

    public class FNAModelBuilder : IEntityTypeConfiguration<FNAModel>
    {
        public void Configure(EntityTypeBuilder<FNAModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasOne(c => c.Client)
               .WithOne(c => c.FNA)
               .HasForeignKey<ClientModel>(c => c.Id)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}