﻿using DataService.Model;
using Microsoft.EntityFrameworkCore;

namespace DataService.Context
{
    public class AlumaDBContext : DbContext
    {
        public AlumaDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Advisor
            mb.ApplyConfiguration(new AdvisorModelBuilder());

            //Application
            mb.ApplyConfiguration(new ApplicationModelBuilder());
            mb.ApplyConfiguration(new IRSW8ModelBuilder());
            mb.ApplyConfiguration(new RecordOfAdviceModelBuilder());

            //Client
            mb.ApplyConfiguration(new ClientModelBuilder());
            mb.ApplyConfiguration(new FSPModelBuilder());
            mb.ApplyConfiguration(new TaxResidencyModelBuilder());

            //User
            mb.ApplyConfiguration(new UserModelBuilder());
        }

        //Advisor
        public DbSet<AdvisorModel> Advisors { get; set; }

        //Application
        public DbSet<ApplicationDocumentModel> ApplicationDocuments { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }

        //public DbSet<DividendTaxModel> Dividends { get; set; }

        public DbSet<PurposeAndFundingModel> PurposeAndFunding { get; set; }
        public DbSet<FSPMandateModel> FspMandates { get; set; }
        public DbSet<IRSW8Model> IRSW8 { get; set; }
        public DbSet<IRSW9Model> IRSW9 { get; set; }
        public DbSet<RecordOfAdviceModel> RecordOfAdvice { get; set; }
        public DbSet<RecordOfAdviceItemsModel> RecordOfAdviceItems { get; set; }
        public DbSet<USPersonsModel> USPersons { get; set; }

        //Client
        public DbSet<BankDetailsModel> BankDetails { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<KycDataModel> KycData { get; set; }
        public DbSet<RiskProfileModel> RiskProfiles { get; set; }
        public DbSet<PassportModel> Passports { get; set; }
        public DbSet<TaxResidencyModel> TaxResidency { get; set; }

        public DbSet<ForeignTaxResidencyModel> TaxResidencyItems { get; set; }

        //Product

        public DbSet<ProductModel> Products { get; set; }

        //Shared
        public DbSet<DisclosureModel> Disclosures { get; set; }

        //User
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<OtpModel> Otp { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserDocumentModel> UserDocuments { get; set; }

        
    }
}