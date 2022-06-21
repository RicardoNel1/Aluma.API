using Aluma.API.Helpers;
using Aluma.API.Repositories;
using AutoMapper;
using Azure.Storage.Files.Shares;
using BankValidationService;
using DataService.Context;
using FileStorageService;
using JwtService;
using KycService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using SignatureService;
using SmsService;
using StringHasher;

namespace Aluma.API.RepoWrapper
{
    public class Wrapper : IWrapper
    {
        private IAdvisorRepo _advisor;

        private IApplicationDocumentsRepo _applicationDocuments;
        private IApplicationRepo _application;

        //private IDividendTaxRepo _dividendTax;
        private IFspMandateRepo _fspMandate;

        private IIRSW8Repo _irsw8;
        private IIRSW9Repo _irsw9;
        private IPurposeAndFundingRepo _purposeAndFunding;
        private IRecordOfAdviceRepo _recordOfAdvice;


        private ILeadRepo _lead;
        private IBankDetailsRepo _bankDetails;
        private ITaxResidencyRepo _taxResidency;
        private IConsumerProtectionRepo _consumerProtection;
        private IClientRepo _client;
        private IKYCDataRepo _kycData;
        private IRiskProfileRepo _riskProfile;

        private IDisclosureRepo _disclosures;

        private IProductRepo _product;

        private IFNARepo _fna;
        private IPrimaryResidenceRepo _primaryResidence;
        private IAssetsAttractingCGTRepo _assetsAttractingCGT;
        private IAssetsExemptFromCGTRepo _assetsExemptFromCGT;
        private ILiquidAssetsRepo _liquidAssets;
        private IInsuranceRepo _insurance;
        private ILiabilitiesRepo _liabilities;
        private IEstateExpensesRepo _estateExpenses;
        private IEstateDutiesRepo _estateDuties;
        private ICapitalGainsTaxRepo _capitalGainsTax;
        private IRetirementPensionFundsRepo _retirementPensionFunds;
        private IRetirementPreservationFundsRepo _retirementPreservationFunds;
        private IAccrualRepo _accrual;
        private IRetirementPlanningRepo _retirementPlanning;
        private IProvidingOnDeathRepo _providingOnDeath;
        private IProvidingOnDreadDiseaseRepo _providingOnDreadDisease;
        private IProvidingOnDisabilityRepo _providingOnDisability;
        private IAssumptionsRepo _assumptions;
        private IAdministrationCostsRepo _administrationCosts;
        private ITaxLumpsumRepo _taxLumpsum;

        private IAssetSummaryRepo _assetSummary;
        private IInsuranceSummaryRepo _insuranceSummary;
        private IProvidingDeathSummaryRepo _providingDeathSummary;
        private IProvidingDisabilitySummaryRepo _providingDisabilitySummary;
        private IRetirementSummaryRepo _retirementSummary;
        private IEconomyVariablesSummaryRepo _economyVariablesSummary;


        private IFIRepo _fi;
        private IPEFRepo _pef;
        private IDocumentHelper _documentHelper;
        private IDocumentSignHelper _signHelper;

        private IOtpRepo _otp;
        private IUserRepo _user;
        private IUserDocumentsRepo _userDocuments;

        private ISmsRepo _sms;
        private IJwtRepo _jwt;
        private IKycFactoryRepo _kyc;
        private IBankValidationServiceRepo _bankValidation;
        private readonly ISignatureRepo _signature;
        private IFileStorageRepo _fileStorage;

        // Short-term Insurance
        private IShortTermInsuranceRepo _shortTermInsurance;

        private IStringHasher _hasher;

        private AlumaDBContext _dbContext;
        private IConfiguration _config;
        private readonly IWebHostEnvironment _host;
        private IMapper _mapper;
        private readonly ShareServiceClient _shareServiceClient;

        public Wrapper(AlumaDBContext dbContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage)
        {
            _dbContext = dbContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public IAdvisorRepo Advisor
        {
            get { return _advisor == null ? new AdvisorRepo(_dbContext, _host, _config, _mapper) : _advisor; }
        }

        public IApplicationDocumentsRepo ApplicationDocuments
        {
            get { return _applicationDocuments == null ? new ApplicationDocumentsRepo(_dbContext, _host, _config, _mapper) : _applicationDocuments; }
        }

        public IApplicationRepo Applications
        {
            get { return _application == null ? new ApplicationRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _application; }
        }

        //public IDividendTaxRepo DividendTax
        //{
        //    get { return _dividendTax == null ? new DividendTaxRepo(_dbContext) : _dividendTax; }
        //}
        public IFspMandateRepo FSPMandate
        {
            get { return _fspMandate == null ? new FspMandateRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fspMandate; }
        }


        public ILeadRepo Leads
        {
            get { return _lead == null ? new LeadRepo(_dbContext, _host, _config, _mapper) : _lead; }
        }
        public IIRSW8Repo IRSW8
        {
            get { return _irsw8 == null ? new IRSW8Repo(_dbContext, _host, _config, _mapper) : _irsw8; }
        }

        public IIRSW9Repo IRSW9
        {
            get { return _irsw9 == null ? new IRSW9Repo(_dbContext, _host, _config, _mapper) : _irsw9; }
        }

        public IPurposeAndFundingRepo PurposeAndFunding
        {
            get { return _purposeAndFunding == null ? new PurposeAndFundingRepo(_dbContext, _host, _config, _mapper) : _purposeAndFunding; }
        }

        public IRecordOfAdviceRepo RecordOfAdvice
        {
            get { return _recordOfAdvice == null ? new RecordOfAdviceRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _recordOfAdvice; }
        }

        public IClientRepo Client
        {
            get { return _client == null ? new ClientRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _client; }
        }

        public IBankDetailsRepo BankDetails
        {
            get { return _bankDetails == null ? new BankDetailsRepo(_dbContext, _host, _config, _mapper) : _bankDetails; }
        }

        public ITaxResidencyRepo TaxResidency
        {
            get { return _taxResidency == null ? new TaxResidencyRepo(_dbContext, _host, _config, _mapper) : _taxResidency; }
        }

        public IConsumerProtectionRepo ConsumerProtection
        {
            get { return _consumerProtection == null ? new ConsumerProtectionRepo(_dbContext, _host, _config, _mapper) : _consumerProtection; }
        }

        public IKYCDataRepo KycData
        {
            get { return _kycData == null ? new KYCDataRepo(_dbContext, _host, _config, _mapper) : _kycData; }
        }

        public IRiskProfileRepo RiskProfile
        {
            get { return _riskProfile == null ? new RiskProfileRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _riskProfile; }
        }

        public IDisclosureRepo Disclosures
        {
            get { return _disclosures == null ? new DisclosureRepo(_dbContext, _host, _config, _mapper, _fileStorage, _userDocuments) : _disclosures; }
        }

        //Product
        public IProductRepo ProductRepo
        {
            get { return _product == null ? new ProductRepo(_dbContext, _host, _config, _mapper) : _product; }
        }

        //FNA
        public IFNARepo FNA
        {
            get { return _fna == null ? new FNARepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fna; }
        }
        public IPrimaryResidenceRepo PrimaryResidence
        {
            get { return _primaryResidence == null ? new PrimaryResidenceRepo(_dbContext, _host, _config, _mapper) : _primaryResidence; }
        }
        public IAssetsAttractingCGTRepo AssetsAttractingCGT
        {
            get { return _assetsAttractingCGT == null ? new AssetsAttractingCGTRepo(_dbContext, _host, _config, _mapper) : _assetsAttractingCGT; }
        }
        public IAssetsExemptFromCGTRepo AssetsExemptFromCGT
        {
            get { return _assetsExemptFromCGT == null ? new AssetsExemptFromCGTRepo(_dbContext, _host, _config, _mapper) : _assetsExemptFromCGT; }
        }
        public ILiquidAssetsRepo LiquidAssets
        {
            get { return _liquidAssets == null ? new LiquidAssetsRepo(_dbContext, _host, _config, _mapper) : _liquidAssets; }
        }

        public ILiabilitiesRepo Liabilities
        {
            get { return _liabilities == null ? new LiabilitiesRepo(_dbContext, _host, _config, _mapper) : _liabilities; }
        }

        public IInsuranceRepo Insurance
        {
            get { return _insurance == null ? new InsuranceRepo(_dbContext, _host, _config, _mapper) : _insurance; }
        }

        public IEstateExpensesRepo EstateExpenses
        {
            get { return _estateExpenses == null ? new EstateExpensesRepo(_dbContext, _host, _config, _mapper) : _estateExpenses; }
        }

        public IEstateDutiesRepo EstateDuties
        {
            get { return _estateDuties == null ? new EstateDutyRepo(_dbContext, _host, _config, _mapper) : _estateDuties; }
        }

        public IAdministrationCostsRepo AdministrationCosts
        {
            get { return _administrationCosts == null ? new AdministrationCostsRepo(_dbContext, _host, _config, _mapper) : _administrationCosts; }
        }

        public IRetirementPensionFundsRepo RetirementPensionFunds
        {
            get { return _retirementPensionFunds == null ? new RetirementPensionFundsRepo(_dbContext, _host, _config, _mapper) : _retirementPensionFunds; }
        }

        public IRetirementPreservationFundsRepo RetirementPreservationFunds
        {
            get { return _retirementPreservationFunds == null ? new RetirementPreservationFundsRepo(_dbContext, _host, _config, _mapper) : _retirementPreservationFunds; }
        }

        public IRetirementPlanningRepo RetirementPlanning
        {
            get { return _retirementPlanning == null ? new RetirementPlanningRepo(_dbContext, _host, _config, _mapper) : _retirementPlanning; }
        }

        public IProvidingOnDeathRepo ProvidingOnDeath
        {
            get { return _providingOnDeath == null ? new ProvidingOnDeathRepo(_dbContext, _host, _config, _mapper) : _providingOnDeath; }
        }

        public IProvidingOnDreadDiseaseRepo ProvidingOnDreadDisease
        {
            get { return _providingOnDreadDisease == null ? new ProvidingOnDreadDiseaseRepo(_dbContext, _host, _config, _mapper) : _providingOnDreadDisease; }
        }

        public IProvidingOnDisabilityRepo ProvidingOnDisability
        {
            get { return _providingOnDisability == null ? new ProvidingOnDisabilityRepo(_dbContext, _host, _config, _mapper) : _providingOnDisability; }
        }

        public IAssumptionsRepo Assumptions
        {
            get { return _assumptions == null ? new AssumptionsRepo(_dbContext, _host, _config, _mapper) : _assumptions; }
        }

        public ICapitalGainsTaxRepo CapitalGainsTax
        {
            get { return _capitalGainsTax == null ? new CapitalGainsTaxRepo(_dbContext, _host, _config, _mapper) : _capitalGainsTax; }
        }

        public ITaxLumpsumRepo TaxLumpsum
        {
            get { return _taxLumpsum == null ? new TaxLumpsumRepo(_dbContext, _host, _config, _mapper) : _taxLumpsum; }
        }

        //FNA - SUMMARY
        public IAssetSummaryRepo AssetSummary
        {
            get { return _assetSummary == null ? new AssetSummaryRepo(_dbContext, _host, _config, _mapper) : _assetSummary; }
        }
        public IInsuranceSummaryRepo InsuranceSummary
        {
            get { return _insuranceSummary == null ? new InsuranceSummaryRepo(_dbContext, _host, _config, _mapper) : _insuranceSummary; }
        }
        public IProvidingDeathSummaryRepo ProvidingDeathSummary
        {
            get { return _providingDeathSummary == null ? new ProvidingDeathSummaryRepo(_dbContext, _host, _config, _mapper) : _providingDeathSummary; }
        }
        public IProvidingDisabilitySummaryRepo ProvidingDisabilitySummary
        {
            get { return _providingDisabilitySummary == null ? new ProvidingDisabilitySummaryRepo(_dbContext, _host, _config, _mapper) : _providingDisabilitySummary; }
        }
        public IRetirementSummaryRepo RetirementSummary
        {
            get { return _retirementSummary == null ? new RetirementSummaryRepo(_dbContext, _host, _config, _mapper) : _retirementSummary; }
        }
        public IEconomyVariablesSummaryRepo EconomyVariablesSummary
        {
            get { return _economyVariablesSummary == null ? new EconomyVariablesSummaryRepo(_dbContext, _host, _config, _mapper) : _economyVariablesSummary; }
        }

        // Short-term Insurance
        public IShortTermInsuranceRepo ShortTermInsurance
        {
            get { return _shortTermInsurance == null ? new ShortTermInsuranceRepo(_dbContext, _host, _config, _mapper) : _shortTermInsurance; }
        }


        //Products and Documents
        public IFIRepo FI
        {
            get { return _fi == null ? new FIRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fi; }
        }
        public IPEFRepo PEF
        {
            get { return _pef == null ? new PEFRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _pef; }
        }
        public IDocumentHelper DocumentHelper
        {
            get { return _documentHelper == null ? new DocumentHelper(_dbContext, _config, _fileStorage, _host) : _documentHelper; }
        }
        public IDocumentSignHelper SignHelper
        {
            get { return _signHelper == null ? new DocumentSignHelper(_dbContext, _config, _fileStorage, _host) : _signHelper; }
        }
        public IAccrualRepo Accrual
        {
            get { return _accrual == null ? new AccrualRepo(_dbContext, _host, _config, _mapper) : _accrual; }
        }

        //User
        public IOtpRepo Otp
        {
            get { return _otp == null ? new OtpRepo(_dbContext, _host, _config, _mapper) : _otp; }
        }

        public IUserRepo User
        {
            get { return _user == null ? new UserRepo(_dbContext, _host, _config, _fileStorage, _mapper) : _user; }
        }

        public IUserDocumentsRepo UserDocuments
        {
            get { return _userDocuments == null ? new UserDocumentsRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _userDocuments; }
        }

        // Third Party Services
        public ISmsRepo SmsRepo
        {
            get { return _sms == null ? new SmsRepo() : _sms; }
        }

        public IJwtRepo JwtRepo
        {
            get { return _jwt == null ? new JwtRepo() : _jwt; }
        }

        public IKycFactoryRepo KycRepo
        {
            get { return _kyc == null ? new KycFactoryRepo() : _kyc; }
        }

        public IBankValidationServiceRepo BankValidationRepo
        {
            get { return _bankValidation == null ? new BankValidationServiceRepo() : _bankValidation; }
        }

        public ISignatureRepo SignatureRepo
        {
            get { return _signature == null ? new SignatureRepo() : _signature; }
        }

        public IFileStorageRepo FileStorageRepo
        {
            get { return _fileStorage == null ? new FileStorageRepo(_shareServiceClient) : _fileStorage; }
        }

        public IStringHasher StrHasher
        {
            get { return _hasher == null ? new StringHasherRepo() : _hasher; }
        }


    }
}