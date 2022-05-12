using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataService.Model
{
    [Table("client_fna")]
    public class ClientFNAModel : BaseModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public int ClientId { get; set; }
        public AdvisorModel Advisor { get; set; }
        public int AdvisorId { get; set; }

        public AccrualModel Accrual { get; set; }
        public AdministrationCostsModel AdministrationCost { get; set; }
        public ICollection<AssetsAttractingCGTModel> AssetsCGT { get; set; }
        public ICollection<AssetsExemptFromCGTModel> AssetsNonCGT { get; set; }
        public AssumptionsModel Assumptions { get; set; }
        public EstateDutyModel EstateDuty { get; set; }
        public EstateExpensesModel EstateExpenses { get; set; }
        public ICollection<InsuranceModel> Insurances { get; set; }
        public ICollection<LiabilitiesModel> Liabilities { get; set; }
        public ICollection<LiquidAssetsModel> LiquidAssets { get; set; }
        public PrimaryResidenceModel PrimaryResidence { get; set; }
        public ProvidingOnDeathModel ProvidingDeathModel { get; set; }
        public ProvidingOnDreadDiseaseModel ProvidingDreadDiseaseModel { get; set; }
        public ProvidingOnDisabilityModel ProvidingDisabilityModel { get; set; }
        public ICollection<RetirementPensionFundsModel> RetirementFunds { get; set; }
        public RetirementPlanningModel RetirementPlanning { get; set; }
        public ICollection<RetirementPreservationFundsModel> RetirementPreservationFundsModel { get; set; }

    }
    public class ClientFNAModelBuilder : IEntityTypeConfiguration<ClientFNAModel>
    {
        public void Configure(EntityTypeBuilder<ClientFNAModel> mb)
        {
            mb.HasKey(x => x.Id);
            mb.Property(x => x.Id).ValueGeneratedOnAdd();

            mb.HasOne(c => c.Accrual)
              .WithOne(c => c.FNA)
              .HasForeignKey<AccrualModel>(c => c.FNAId)
              .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.AdministrationCost)
             .WithOne(c => c.FNA)
             .HasForeignKey<AdministrationCostsModel>(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.AssetsCGT)
              .WithOne(c => c.FNA)
              .HasForeignKey(c => c.FNAId)
              .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.AssetsNonCGT)
             .WithOne(c => c.FNA)
             .HasForeignKey(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.Assumptions)
             .WithOne(c => c.FNA)
             .HasForeignKey<AssumptionsModel>(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.EstateDuty)
             .WithOne(c => c.FNA)
             .HasForeignKey<EstateDutyModel>(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.EstateExpenses)
             .WithOne(c => c.FNA)
             .HasForeignKey<EstateExpensesModel>(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Insurances)
             .WithOne(c => c.FNA)
             .HasForeignKey(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.Liabilities)
             .WithOne(c => c.FNA)
             .HasForeignKey(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.LiquidAssets)
             .WithOne(c => c.FNA)
             .HasForeignKey(c => c.FNAId)
             .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.PrimaryResidence)
            .WithOne(c => c.FNA)
            .HasForeignKey<PrimaryResidenceModel>(c => c.FNAId)
            .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.ProvidingDeathModel)
           .WithOne(c => c.FNA)
           .HasForeignKey<ProvidingOnDeathModel>(c => c.FNAId)
           .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.ProvidingDreadDiseaseModel)
           .WithOne(c => c.FNA)
           .HasForeignKey<ProvidingOnDreadDiseaseModel>(c => c.FNAId)
           .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.ProvidingDisabilityModel)
           .WithOne(c => c.FNA)
           .HasForeignKey<ProvidingOnDisabilityModel>(c => c.FNAId)
           .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.RetirementFunds)
          .WithOne(c => c.FNA)
          .HasForeignKey(c => c.FNAId)
          .OnDelete(DeleteBehavior.Cascade);

            mb.HasOne(c => c.RetirementPlanning)
            .WithOne(c => c.FNA)
            .HasForeignKey<RetirementPlanningModel>(c => c.FNAId)
            .OnDelete(DeleteBehavior.Cascade);

            mb.HasMany(c => c.RetirementPreservationFundsModel)
                        .WithOne(c => c.FNA)
                        .HasForeignKey(c => c.FNAId)
                        .OnDelete(DeleteBehavior.Cascade);

        }
    }


}