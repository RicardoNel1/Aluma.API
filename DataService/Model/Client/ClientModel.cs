using DataService.Dto.Client;
using DataService.Model.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("clients")]
    public class ClientModel : BaseModel
    {
        public int Id { get; set; }
        public System.Nullable<int> UserId { get; set; }
        public System.Nullable<int> AdvisorId { get; set; }
        public System.Nullable<int> LeadId { get; set; }
        public string ClientType { get; set; }
        public string Title { get; set; }
        public string CountryOfResidence { get; set; }
        public string CountryOfBirth { get; set; }
        public string CityOfBirth { get; set; }
        public string Nationality { get; set; }
        public int EmploymentDetailsId { get; set; }
        public int MaritalDetailsId { get; set; }
        public bool NonResidentialAccount { get; set; }
        public bool isSmoker { get; set; }
        public string Education { get; set; }
        public bool isDeleted { get; set; }

        public int PrimaryFNA { get; set; }

        public UserModel User { get; set; }
        public AdvisorModel Advisor { get; set; }
        public KYCDataModel KycData { get; set; }
        public TaxResidencyModel TaxResidency { get; set; }
        public RiskProfileModel RiskProfile { get; set; }
        public FSPMandateModel FspMandate { get; set; }
        public EmploymentDetailsModel EmploymentDetails { get; set; }
        public LeadModel Lead { get; set; }
        public MaritalDetailsModel MaritalDetails { get; set; }

        public ICollection<ClientFNAModel> FNAs { get; set; }
        public ICollection<ApplicationModel> Applications { get; set; }
        public ICollection<BankDetailsModel> BankDetails { get; set; }
        public ICollection<ClientConsentModel> ClientConsents { get; set; }
        public ICollection<PassportModel> Passports { get; set; }
    }

    public class ClientModelBuilder : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> mb)
        {

            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.UserId).IsUnique();
            mb.HasIndex(c => c.LeadId).IsUnique();


            mb.HasOne(c => c.Lead)
               .WithOne(c => c.Client)
               .HasForeignKey<LeadModel>(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.MaritalDetails)
              .WithOne(c => c.Client)
              .HasForeignKey<MaritalDetailsModel>(c => c.ClientId)
              .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.EmploymentDetails)
            .WithOne(c => c.Client)
            .HasForeignKey<EmploymentDetailsModel>(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.TaxResidency)
                .WithOne(c => c.Client)
                .HasForeignKey<TaxResidencyModel>(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.TaxResidency)
                .WithOne(c => c.Client)
                .HasForeignKey<TaxResidencyModel>(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.RiskProfile)
                .WithOne(c => c.Client)
                .HasForeignKey<RiskProfileModel>(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.FspMandate)
                .WithOne(c => c.Client)
                .HasForeignKey<FSPMandateModel>(c => c.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.KycData)
               .WithOne(c => c.Client)
               .HasForeignKey<KYCDataModel>(c => c.ClientId)
               .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.FNAs)
              .WithOne(c => c.Client)
              .HasForeignKey(c => c.ClientId)
              .OnDelete(DeleteBehavior.NoAction);

            mb.HasMany(c => c.Passports)
               .WithOne(c => c.Client)
               .HasForeignKey(c => c.ClientId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Applications)
                  .WithOne(c => c.Client)
                  .HasForeignKey(c => c.ClientId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.BankDetails)
                  .WithOne(c => c.Client)
                  .HasForeignKey(c => c.ClientId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.ClientConsents)
                  .WithOne(c => c.Client)
                  .HasForeignKey(c => c.ClientId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}