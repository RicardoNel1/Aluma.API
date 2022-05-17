using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("advisors")]
    public class AdvisorModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        
        public DateTime AppointmentDate { get; set; }

        public string Title { get; set; }
        public string BusinessTel { get; set; }
        public string HomeTel { get; set; }
        public string Fax { get; set; }

        //INSURANCE
        
        public bool AdviceLTSubCatA { get; set; }

        public bool SupervisedLTSubCatA { get; set; }

        
        public bool AdviceLTSubCatB1 { get; set; }

        public bool SupervisedLTSubCatB1 { get; set; }

        
        public bool AdviceLTSubCatB1A { get; set; }

        public bool SupervisedLTSubCatB1A { get; set; }

       
        public bool AdviceLTSubCatB2 { get; set; }

        public bool SupervisedLTSubCatB2 { get; set; }

        
        public bool AdviceLTSubCatB2A { get; set; }

        public bool SupervisedLTSubCatB2A { get; set; }

        
        public bool AdviceLTSubCatC { get; set; }

        public bool SupervisedLTSubCatC { get; set; }

       
        public bool AdviceSTPersonal { get; set; }

        public bool SupervisedSTPersonal { get; set; }

       
        public bool AdviceSTPersonalA1 { get; set; }

        public bool SupervisedSTPersonalA1 { get; set; }

        
        public bool AdviceSTCommercial { get; set; }

        public bool SupervisedSTCommercial { get; set; }

        //DEPOSITS
        
        public bool AdviceLTDeposits { get; set; }

        public bool SupervisedLTDeposits { get; set; }

        
        public bool AdviceSTDeposits { get; set; }

        public bool SupervisedSTDeposits { get; set; }

        
        public bool AdviceStructuredDeposits { get; set; }

        public bool SupervisedStructuredDeposits { get; set; }

        //PENSION
        
        public bool AdviceRetailPension { get; set; }

        public bool SupervisedRetailPension { get; set; }

        
        public bool AdvicePensionFunds { get; set; }

        public bool SupervisedPensionFunds { get; set; }

       
        public bool AdviceShares { get; set; }

        public bool SupervisedShares { get; set; }

        
        public bool AdviceMoneyMarket { get; set; }

        public bool SupervisedMoneyMarket { get; set; }

      
        public bool AdviceDebentures { get; set; }

        public bool SupervisedDebentures { get; set; }

       
        public bool AdviceWarrants { get; set; }

        public bool SupervisedWarrants { get; set; }

        
        public bool AdviceBonds { get; set; }

        public bool SupervisedBonds { get; set; }

        
        public bool AdviceDerivatives { get; set; }

        public bool SupervisedDerivatives { get; set; }

       
        public bool AdviceParticipatoryInterestCollective { get; set; }

        public bool SupervisedParticipatoryInterestCollective { get; set; }

      
        public bool AdviceSecurities { get; set; }

        public bool SupervisedSecurities { get; set; }

        
        public bool AdviceParticipatoryInterestHedge { get; set; }

        public bool SupervisedParticipatoryInterestHedge { get; set; }

        public bool isExternalBroker { get; set; }
        public bool isSupervised { get; set; }
        public bool isActive { get; set; }

        public UserModel User { get; set; }
        public ICollection<ApplicationModel> Applications { get; set; }
        public ICollection<ClientModel> Clients { get; set; }
        public ICollection<ClientFNAModel> FNAs { get; set; }


    }

    public class AdvisorModelBuilder : IEntityTypeConfiguration<AdvisorModel>
    {
        public void Configure(EntityTypeBuilder<AdvisorModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasIndex(c => c.UserId).IsUnique();

            mb.HasMany(c => c.Clients)
                .WithOne(c => c.Advisor)
                .HasForeignKey(c => c.AdvisorId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.HasMany(c => c.FNAs)
              .WithOne(c => c.Advisor)
              .HasForeignKey(c => c.AdvisorId)
              .OnDelete(DeleteBehavior.NoAction);

            mb.HasMany(c => c.Applications)
                .WithOne(c => c.Advisor)
                .HasForeignKey(c => c.AdvisorId)
                .OnDelete(DeleteBehavior.NoAction);

            mb.HasData(new AdvisorModel()
            {
                Id = 1,
                UserId = 1,
                AppointmentDate = DateTime.Now.AddYears(-1),
                AdviceLTSubCatA = true,
                AdviceLTSubCatB1 = true,
                AdviceLTSubCatB1A = true,
                AdviceLTSubCatB2 = true,
                AdviceLTSubCatB2A = true,
                AdviceLTSubCatC = true,
                AdviceSTPersonal = true,
                AdviceSTCommercial = true,
                AdviceSTDeposits = true,
                AdviceSTPersonalA1 = true,
                AdviceStructuredDeposits = true,
                AdviceDebentures = true,
                AdviceShares = true,
                AdviceBonds = true,
                AdviceWarrants = true,
                AdviceDerivatives = true,
                AdviceLTDeposits = true,
                AdviceMoneyMarket = true,
                AdviceParticipatoryInterestCollective = true,
                AdviceParticipatoryInterestHedge = true,
                AdviceRetailPension = true,
                AdvicePensionFunds = true,
                AdviceSecurities = true,
                SupervisedLTSubCatA = true,
                SupervisedLTSubCatB1 = true,
                SupervisedLTSubCatB1A = true,
                SupervisedLTSubCatB2 = true,
                SupervisedLTSubCatB2A = true,
                SupervisedLTSubCatC = true,
                SupervisedSTPersonal = true,
                SupervisedSTCommercial = true,
                SupervisedSTDeposits = true,
                SupervisedSTPersonalA1 = true,
                SupervisedStructuredDeposits = true,
                SupervisedDebentures = true,
                SupervisedShares = true,
                SupervisedBonds = true,
                SupervisedWarrants = true,
                SupervisedDerivatives = true,
                SupervisedLTDeposits = true,
                SupervisedMoneyMarket = true,
                SupervisedParticipatoryInterestCollective = true,
                SupervisedParticipatoryInterestHedge = true,
                SupervisedRetailPension = true,
                SupervisedPensionFunds = true,
                SupervisedSecurities = true,
                isActive = true
            });
        }
    }
}