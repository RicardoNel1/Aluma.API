using DataService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace DataService.Context
{
    public class AlumaDBContext : DbContext
    {
        public AlumaDBContext()
        {

        }

        private readonly SqlConnection sqlConnectionToUse;

        public AlumaDBContext(SqlConnection existingConnection)
        {
            sqlConnectionToUse = existingConnection;
        }
        public AlumaDBContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        private static string connectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (sqlConnectionToUse != null)
            {
                optionsBuilder.UseSqlServer(sqlConnectionToUse);
                return;
            }
            if (optionsBuilder.IsConfigured)
                return;

            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        private string GetConnectionString()
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var config = new ConfigurationBuilder();
                config.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true)
                    .AddJsonFile($"appsettings.Development.json", true);
                var configuration = config.Build();
                connectionString = configuration.GetValue("ConnectionStrings:DefaultConnection", string.Empty);
            }
            return connectionString;
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Advisor
            mb.ApplyConfiguration(new AdvisorModelBuilder());

            //Application
            mb.ApplyConfiguration(new ApplicationModelBuilder());
            mb.ApplyConfiguration(new IRSW8ModelBuilder());
            mb.ApplyConfiguration(new RecordOfAdviceModelBuilder());

            //Product
            mb.ApplyConfiguration(new ProductModelBuilder());

            //Client
            mb.ApplyConfiguration(new ClientModelBuilder());
            mb.ApplyConfiguration(new FSPModelBuilder());
            mb.ApplyConfiguration(new TaxResidencyModelBuilder());
            mb.ApplyConfiguration(new ConsumerProtectionModelBuilder());

            //User
            mb.ApplyConfiguration(new UserModelBuilder());
            mb.ApplyConfiguration(new AddressModelBuilder());


            //FNA   
            
            mb.ApplyConfiguration(new FNAModelBuilder());
            mb.ApplyConfiguration(new PrimaryResidenceModelBuilder());
            mb.ApplyConfiguration(new AssetsAttractingCGTModelBuilder());
            mb.ApplyConfiguration(new AssetsExemptFromCGTModelBuilder());
            mb.ApplyConfiguration(new InsuranceModelBuilder());
            mb.ApplyConfiguration(new LiquidAssetsModelBuilder());
            mb.ApplyConfiguration(new LiabilitiesModelBuilder());
            mb.ApplyConfiguration(new EstateExpensesModelBuilder());
            mb.ApplyConfiguration(new RetirementPensionFundsModelBuilder());
            mb.ApplyConfiguration(new RetirementPreservationFundsModelBuilder());
            mb.ApplyConfiguration(new AccrualModelBuilder());
            mb.ApplyConfiguration(new AdministrationCostsModelBuilder());
            mb.ApplyConfiguration(new EstateDutyModelBuilder());
            mb.ApplyConfiguration(new CapitalGainsTaxModelBuilder());

            foreach (var property in mb.Model.GetEntityTypes().SelectMany(t => t.GetProperties()).Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                {
                    if (property.Name == "Description")
                        property.SetColumnType("varchar(MAX)");
                    else
                        property.SetColumnType("varchar(100)");
                }
            }
        }

        //Advisor
        public DbSet<AdvisorModel> Advisors { get; set; }

        //Application
        public DbSet<ApplicationDocumentModel> ApplicationDocuments { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }

        //public DbSet<DividendTaxModel> Dividends { get; set; }

        public DbSet<PurposeAndFundingModel> PurposeAndFunding { get; set; }
        public DbSet<FSPMandateModel> FspMandates { get; set; }
        //public DbSet<IRSW8Model> IRSW8 { get; set; }
        //public DbSet<IRSW9Model> IRSW9 { get; set; }
        public DbSet<RecordOfAdviceModel> RecordOfAdvice { get; set; }
        public DbSet<RecordOfAdviceItemsModel> RecordOfAdviceItems { get; set; }
        //public DbSet<USPersonsModel> USPersons { get; set; }

        //Client
        public DbSet<BankDetailsModel> BankDetails { get; set; }
        public DbSet<ClientModel> Clients { get; set; }
        public DbSet<KYCDataModel> KycData { get; set; }
        public DbSet<RiskProfileModel> RiskProfiles { get; set; }
        public DbSet<PassportModel> Passports { get; set; }
        public DbSet<TaxResidencyModel> TaxResidency { get; set; }
        public DbSet<ForeignTaxResidencyModel> TaxResidencyItems { get; set; }
        public DbSet<ConsumerProtectionModel> ConsumerProtection { get; set; }
        public DbSet<FNAModel> FNA { get; set; }
        public DbSet<ClientProductModel> ClientProducts { get; set; }


        //Product
        public DbSet<ProductModel> Products { get; set; }

        //FNA
        public DbSet<PrimaryResidenceModel> PrimaryResidence { get; set; }
        public DbSet<AssetsAttractingCGTModel> AssetsAttractingCGT { get; set; }
        public DbSet<AssetsExemptFromCGTModel> AssetsExemptFromCGT { get; set; }
        public DbSet<LiquidAssetsModel> LiquidAssets { get; set; }
        public DbSet<InsuranceModel> Insurance { get; set; }
        public DbSet<LiabilitiesModel> Liabilities { get; set; }
        public DbSet<EstateExpensesModel> EstateExpenses { get; set; }
        public DbSet<RetirementPensionFundsModel> RetirementPensionFunds { get; set; }
        public DbSet<RetirementPreservationFundsModel> RetirementPreservationFunds { get; set; }
        public DbSet<AccrualModel> Accrual { get; set; }
        public DbSet<RetirementPlanningModel> RetirementPlanning { get; set; }
        public DbSet<AssumptionsModel> Assumptions { get; set; }
        public DbSet<AdministrationCostsModel> AdministrationCosts { get; set; }
        public DbSet<EstateDutyModel> EstateDuty { get; set; }
        public DbSet<CapitalGainsTaxModel> CapitalGainsTax { get; set; }

        //Shared
        public DbSet<DisclosureModel> Disclosures { get; set; }

        //User
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<OtpModel> Otp { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserDocumentModel> UserDocuments { get; set; }


    }
}