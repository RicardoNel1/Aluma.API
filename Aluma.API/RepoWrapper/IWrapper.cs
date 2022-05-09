using Aluma.API.Helpers;
using Aluma.API.Repositories;
using BankValidationService;
using FileStorageService;
using JwtService;
using KycService;
using SignatureService;
using SmsService;
using StringHasher;

namespace Aluma.API.RepoWrapper
{
    public interface IWrapper
    {
        #region Public Properties

        IAccrualRepo Accrual { get; }

        IAdvisorRepo Advisor { get; }

        IApplicationDocumentsRepo ApplicationDocuments { get; }
        IApplicationRepo Applications { get; }

        IAssetsAttractingCGTRepo AssetsAttractingCGT { get; }

        IAssetsExemptFromCGTRepo AssetsExemptFromCGT { get; }

        IBankDetailsRepo BankDetails { get; }

        IBankValidationServiceRepo BankValidationRepo { get; }

        //Client
        IClientRepo Client { get; }

        IConsumerProtectionRepo ConsumerProtection { get; }

        //Shared
        IDisclosureRepo Disclosures { get; }

        IDocumentHelper DocumentHelper { get; }

        IEstateExpensesRepo EstateExpenses { get; }

        IFIRepo FI { get; }

        IFileStorageRepo FileStorageRepo { get; }

        IFNARepo FNA { get; }

        //IDividendTaxRepo DividendTax { get; }
        IFspMandateRepo FSPMandate { get; }
        IInsuranceRepo Insurance { get; }

        IIRSW8Repo IRSW8 { get; }

        IIRSW9Repo IRSW9 { get; }

        IJwtRepo JwtRepo { get; }

        IKYCDataRepo KycData { get; }

        IKycFactoryRepo KycRepo { get; }

        ILiabilitiesRepo Liabilities { get; }

        ILiquidAssetsRepo LiquidAssets { get; }

        //User
        IOtpRepo Otp { get; }

        IPEFRepo PEF { get; }
        //FNA
        IPrimaryResidenceRepo PrimaryResidence { get; }

        //Product
        IProductRepo ProductRepo { get; }

        IPurposeAndFundingRepo PurposeAndFunding { get; }

        IRecordOfAdviceRepo RecordOfAdvice { get; }

        IRetirementPensionFundsRepo RetirementPensionFunds { get; }

        IRetirementPlanningRepo RetirementPlanning { get; }

        IRetirementPreservationFundsRepo RetirementPreservationFunds { get; }

        IRiskProfileRepo RiskProfile { get; }

        ISignatureRepo SignatureRepo { get; }

        IDocumentSignHelper SignHelper { get; }
        // Third Party Services
        ISmsRepo SmsRepo { get; }

        IStringHasher StrHasher { get; }

        ITaxResidencyRepo TaxResidency { get; }
        IUserRepo User { get; }
        IUserDocumentsRepo UserDocuments { get; }

        #endregion Public Properties
    }
}