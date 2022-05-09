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
        #region Private Fields

        private readonly IWebHostEnvironment _host;

        private readonly ShareServiceClient _shareServiceClient;

        private readonly ISignatureRepo _signature;

        private IAccrualRepo _accrual;

        private IAdvisorRepo _advisor;

        private IApplicationRepo _application;

        private IApplicationDocumentsRepo _applicationDocuments;
        private IAssetsAttractingCGTRepo _assetsAttractingCGT;

        private IAssetsExemptFromCGTRepo _assetsExemptFromCGT;

        private IAssumptionsRepo _assumptions;

        private IBankDetailsRepo _bankDetails;

        private IBankValidationServiceRepo _bankValidation;

        private IClientRepo _client;

        private IConfiguration _config;

        private IConsumerProtectionRepo _consumerProtection;

        private AlumaDBContext _dbContext;

        private IDisclosureRepo _disclosures;

        private IDocumentHelper _documentHelper;

        private IEstateExpensesRepo _estateExpenses;

        private IFIRepo _fi;

        private IFileStorageRepo _fileStorage;

        private IFNARepo _fna;

        //private IDividendTaxRepo _dividendTax;
        private IFspMandateRepo _fspMandate;
        private IStringHasher _hasher;

        private IInsuranceRepo _insurance;

        private IIRSW8Repo _irsw8;
        private IIRSW9Repo _irsw9;
        private IJwtRepo _jwt;

        private IKycFactoryRepo _kyc;

        private IKYCDataRepo _kycData;

        private ILiabilitiesRepo _liabilities;

        private ILiquidAssetsRepo _liquidAssets;

        private IMapper _mapper;

        private IOtpRepo _otp;

        private IPEFRepo _pef;

        private IPrimaryResidenceRepo _primaryResidence;

        private IProductRepo _product;

        private IPurposeAndFundingRepo _purposeAndFunding;
        private IRecordOfAdviceRepo _recordOfAdvice;
        private IRetirementPensionFundsRepo _retirementPensionFunds;

        private IRetirementPlanningRepo _retirementPlanning;

        private IRetirementPreservationFundsRepo _retirementPreservationFunds;

        private IRiskProfileRepo _riskProfile;

        private IDocumentSignHelper _signHelper;

        private ISmsRepo _sms;

        private ITaxResidencyRepo _taxResidency;
        private IUserRepo _user;
        private IUserDocumentsRepo _userDocuments;

        #endregion Private Fields

        #region Public Constructors

        public Wrapper(AlumaDBContext dbContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage)
        {
            _dbContext = dbContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        #endregion Public Constructors

        #region Public Properties

        public IAccrualRepo Accrual
        {
            get { return _accrual == null ? new AccrualRepo(_dbContext, _host, _config, _mapper) : _accrual; }
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

        public IAssetsAttractingCGTRepo AssetsAttractingCGT
        {
            get { return _assetsAttractingCGT == null ? new AssetsAttractingCGTRepo(_dbContext, _host, _config, _mapper) : _assetsAttractingCGT; }
        }

        public IAssetsExemptFromCGTRepo AssetsExemptFromCGT
        {
            get { return _assetsExemptFromCGT == null ? new AssetsExemptFromCGTRepo(_dbContext, _host, _config, _mapper) : _assetsExemptFromCGT; }
        }

        public IAssumptionsRepo Assumptions
        {
            get { return _assumptions == null ? new AssumptionsRepo(_dbContext, _host, _config, _mapper) : _assumptions; }
        }

        public IBankDetailsRepo BankDetails
        {
            get { return _bankDetails == null ? new BankDetailsRepo(_dbContext, _host, _config, _mapper) : _bankDetails; }
        }

        public IBankValidationServiceRepo BankValidationRepo
        {
            get { return _bankValidation == null ? new BankValidationServiceRepo() : _bankValidation; }
        }

        public IClientRepo Client
        {
            get { return _client == null ? new ClientRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _client; }
        }

        public IConsumerProtectionRepo ConsumerProtection
        {
            get { return _consumerProtection == null ? new ConsumerProtectionRepo(_dbContext, _host, _config, _mapper) : _consumerProtection; }
        }

        public IDisclosureRepo Disclosures
        {
            get { return _disclosures == null ? new DisclosureRepo(_dbContext, _host, _config, _mapper, _fileStorage, _userDocuments) : _disclosures; }
        }

        public IDocumentHelper DocumentHelper
        {
            get { return _documentHelper == null ? new DocumentHelper(_dbContext, _config, _fileStorage, _host) : _documentHelper; }
        }

        public IEstateExpensesRepo EstateExpenses
        {
            get { return _estateExpenses == null ? new EstateExpensesRepo(_dbContext, _host, _config, _mapper) : _estateExpenses; }
        }

        //Products and Documents
        public IFIRepo FI
        {
            get { return _fi == null ? new FIRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fi; }
        }

        public IFileStorageRepo FileStorageRepo
        {
            get { return _fileStorage == null ? new FileStorageRepo(_shareServiceClient) : _fileStorage; }
        }

        public IFNARepo FNA
        {
            get { return _fna == null ? new FNARepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fna; }
        }

        //public IDividendTaxRepo DividendTax
        //{
        //    get { return _dividendTax == null ? new DividendTaxRepo(_dbContext) : _dividendTax; }
        //}
        public IFspMandateRepo FSPMandate
        {
            get { return _fspMandate == null ? new FspMandateRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _fspMandate; }
        }
        public IInsuranceRepo Insurance
        {
            get { return _insurance == null ? new InsuranceRepo(_dbContext, _host, _config, _mapper) : _insurance; }
        }

        public IIRSW8Repo IRSW8
        {
            get { return _irsw8 == null ? new IRSW8Repo(_dbContext, _host, _config, _mapper) : _irsw8; }
        }

        public IIRSW9Repo IRSW9
        {
            get { return _irsw9 == null ? new IRSW9Repo(_dbContext, _host, _config, _mapper) : _irsw9; }
        }

        public IJwtRepo JwtRepo
        {
            get { return _jwt == null ? new JwtRepo() : _jwt; }
        }

        public IKYCDataRepo KycData
        {
            get { return _kycData == null ? new KYCDataRepo(_dbContext, _host, _config, _mapper) : _kycData; }
        }

        public IKycFactoryRepo KycRepo
        {
            get { return _kyc == null ? new KycFactoryRepo() : _kyc; }
        }

        public ILiabilitiesRepo Liabilities
        {
            get { return _liabilities == null ? new LiabilitiesRepo(_dbContext, _host, _config, _mapper) : _liabilities; }
        }

        public ILiquidAssetsRepo LiquidAssets
        {
            get { return _liquidAssets == null ? new LiquidAssetsRepo(_dbContext, _host, _config, _mapper) : _liquidAssets; }
        }

        //User
        public IOtpRepo Otp
        {
            get { return _otp == null ? new OtpRepo(_dbContext, _host, _config, _mapper) : _otp; }
        }

        public IPEFRepo PEF
        {
            get { return _pef == null ? new PEFRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _pef; }
        }

        //FNA
        public IPrimaryResidenceRepo PrimaryResidence
        {
            get { return _primaryResidence == null ? new PrimaryResidenceRepo(_dbContext, _host, _config, _mapper) : _primaryResidence; }
        }

        //Product
        public IProductRepo ProductRepo
        {
            get { return _product == null ? new ProductRepo(_dbContext, _host, _config, _mapper) : _product; }
        }

        public IPurposeAndFundingRepo PurposeAndFunding
        {
            get { return _purposeAndFunding == null ? new PurposeAndFundingRepo(_dbContext, _host, _config, _mapper) : _purposeAndFunding; }
        }

        public IRecordOfAdviceRepo RecordOfAdvice
        {
            get { return _recordOfAdvice == null ? new RecordOfAdviceRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _recordOfAdvice; }
        }
        public IRetirementPensionFundsRepo RetirementPensionFunds
        {
            get { return _retirementPensionFunds == null ? new RetirementPensionFundsRepo(_dbContext, _host, _config, _mapper) : _retirementPensionFunds; }
        }

        public IRetirementPlanningRepo RetirementPlanning
        {
            get { return _retirementPlanning == null ? new RetirementPlanningRepo(_dbContext, _host, _config, _mapper) : _retirementPlanning; }
        }

        public IRetirementPreservationFundsRepo RetirementPreservationFunds
        {
            get { return _retirementPreservationFunds == null ? new RetirementPreservationFundsRepo(_dbContext, _host, _config, _mapper) : _retirementPreservationFunds; }
        }

        public IRiskProfileRepo RiskProfile
        {
            get { return _riskProfile == null ? new RiskProfileRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _riskProfile; }
        }

        public ISignatureRepo SignatureRepo
        {
            get { return _signature == null ? new SignatureRepo() : _signature; }
        }

        public IDocumentSignHelper SignHelper
        {
            get { return _signHelper == null ? new DocumentSignHelper(_dbContext, _config, _fileStorage, _host) : _signHelper; }
        }

        // Third Party Services
        public ISmsRepo SmsRepo
        {
            get { return _sms == null ? new SmsRepo() : _sms; }
        }

        public IStringHasher StrHasher
        {
            get { return _hasher == null ? new StringHasherRepo() : _hasher; }
        }

        public ITaxResidencyRepo TaxResidency
        {
            get { return _taxResidency == null ? new TaxResidencyRepo(_dbContext, _host, _config, _mapper) : _taxResidency; }
        }
        public IUserRepo User
        {
            get { return _user == null ? new UserRepo(_dbContext, _host, _config, _fileStorage, _mapper) : _user; }
        }

        public IUserDocumentsRepo UserDocuments
        {
            get { return _userDocuments == null ? new UserDocumentsRepo(_dbContext, _host, _config, _mapper, _fileStorage) : _userDocuments; }
        }

        #endregion Public Properties
    }
}