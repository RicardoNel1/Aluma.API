using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("providing_on_dread_disease")]
    public class ProvidingOnDreadDiseaseModel : BaseModel
    {
        //public ClientModel Client { get; set; }        
        public int Id { get; set; }
        public int ClientId { get; set; }

    }

    public class ProvidingOnDreadDiseaseModelBuilder : IEntityTypeConfiguration<ProvidingOnDreadDiseaseModel>
    {
        public void Configure(EntityTypeBuilder<ProvidingOnDreadDiseaseModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();

        }
    }

}