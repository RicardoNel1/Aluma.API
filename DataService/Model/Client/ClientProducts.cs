using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_products")]
    public class ClientProductModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public ClientModel Client { get; set; }
        public ProductModel Product { get; set; }
    }

    public class ClientProductModelBuilder : IEntityTypeConfiguration<ClientProductModel>
    {
        public void Configure(EntityTypeBuilder<ClientProductModel> mb)
        {            

            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();
           
        }
    }
}