using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("clients")]
    public class ClientModel : BaseModel
    {
        public UserModel User { get; set; }

        public AdvisorModel Advisor { get; set; }
        public KYCDataModel KycData { get; set; }
        public TaxResidencyModel TaxResidency { get; set; }
        public RiskProfileModel RiskProfile { get; set; }
        public FSPMandateModel FspMandate { get; set; }

        public ICollection<ApplicationModel> Applications { get; set; }
        public ICollection<BankDetailsModel> BankDetails { get; set; }
        public ICollection<PassportModel> Passports { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public System.Nullable<int> AdvisorId { get; set; }

        public string ClientType { get; set; }
        public string Title { get; set; }
        public string CountryOfResidence { get; set; }       //Added
        public string CountryOfBirth { get; set; }
        public string CityOfBirth { get; set; }
        public string Nationality { get; set; }
        public string EmploymentStatus { get; set; }
        public string Employer { get; set; }
        public string Industry { get; set; }
        public string Occupation { get; set; }
        public string WorkNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string DateOfMarriage { get; set; }
        public bool ForeignMarriage { get; set; }
        public string CountryOfMarriage { get; set; }
        public string SpouseName { get; set; }
        public string MaidenName { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public bool PowerOfAttorney { get; set; }
        public bool NonResidentialAccount { get; set; }
        public bool isSmoker { get; set; }
        public string LeadType { get; set; }
        public string Education { get; set; }
        public bool isDeleted { get; set; }
    }

    public class ClientModelBuilder : IEntityTypeConfiguration<ClientModel>
    {
        public void Configure(EntityTypeBuilder<ClientModel> mb)
        {            

            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.UserId).IsUnique();

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
        }
    }
}