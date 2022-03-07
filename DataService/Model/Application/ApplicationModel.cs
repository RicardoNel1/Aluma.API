using DataService.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("application")]
    public class ApplicationModel : BaseModel
    {
        public ClientModel Client { get; set; }
        public AdvisorModel Advisor { get; set; }
        public ICollection<ApplicationDocumentModel> Documents { get; set; }
        public PurposeAndFundingModel PurposeAndFunding { get; set; }
        //public FSPMandateModel FSPMandate { get; set; }
        public RecordOfAdviceModel RecordOfAdvice { get; set; }
        public IRSW8Model IRSW8 { get; set; }
        public IRSW9Model IRSW9 { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public System.Nullable<int> AdvisorId { get; set; }
        public int ClientId { get; set; }        
        public ApplicationTypesEnum ApplicationType { get; set; }
        public ProductsEnum Product { get; set; }

        public ApplicationStatusEnum ApplicationStatus { get; set; }

        //public bool PersonalDetailsComplete { get; set; }
        //public bool BankingDetailsComplete { get; set; }
        //public bool TaxResidencyComplete { get; set; }
        //public bool PurposeAndFundingComplete { get; set; }
        //public bool RiskProfileComplete { get; set; }
        //public bool FSPMandateComplete { get; set; }
        //public bool ConsumerProtectionComplete { get; set; }
        public bool DocumentsCreated { get; set; }

        public bool SignatureConsent { get; set; }
        public DateTime SignatureConsentDate { get; set; }
        public bool DocumentsSigned { get; set; }
        public string BdaNumber { get; set; }
        public bool PaymentConfirmed { get; set; }
        
    }

    public class ApplicationModelBuilder : IEntityTypeConfiguration<ApplicationModel>
    {
        public void Configure(EntityTypeBuilder<ApplicationModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            //mb.HasIndex(c => c.ClientId).IsUnique();

            //mb.HasOne(c => c.PurposeAndFunding)
            //    .WithOne(c => c.Application)
            //    .HasForeignKey<PurposeAndFundingModel>(c => c.ApplicationId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //mb.HasOne(c => c.FSPMandate)
            //    .WithOne(c => c.Client)
            //    .HasForeignKey<FSPMandateModel>(c => c.client)
            //    .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.RecordOfAdvice)
                .WithOne(c => c.Application)
                .HasForeignKey<RecordOfAdviceModel>(c => c.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.IRSW8)
                .WithOne(c => c.Application)
                .HasForeignKey<IRSW8Model>(c => c.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.IRSW9)
                .WithOne(c => c.Application)
                .HasForeignKey<IRSW9Model>(c => c.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Documents)
                .WithOne(c => c.Application)
                .HasForeignKey(c => c.ApplicationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}