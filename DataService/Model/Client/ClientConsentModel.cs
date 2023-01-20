using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Model.Client
{
    [Table("client_consent")]
    public class ClientConsentModel : BaseModel
    {
        public ClientModel Client { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public bool OtpVerified { get; set; }

        public List<ClientConsentProvidersModel> ConsentedProviders { get; set; }

    }

    public class ClientConsentModelBuilder : IEntityTypeConfiguration<ClientConsentModel>
    {
        public void Configure(EntityTypeBuilder<ClientConsentModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.ClientId).IsUnique();

            mb.HasMany(c => c.ConsentedProviders)
              .WithOne(c => c.ClientConsent)
              .HasForeignKey(c => c.ClientConsentId)
              .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
