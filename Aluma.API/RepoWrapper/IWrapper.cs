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
        IAdvisorRepo Advisor { get; }

        IApplicationDocumentsRepo ApplicationDocuments { get; }
        IApplicationRepo Applications { get; }

        //IDividendTaxRepo DividendTax { get; }
        IFspMandateRepo FSPMandate { get; }
        IFNARepo FNA { get; }

        IFIRepo FI { get; }
        IPEFRepo PEF { get; }
        IDocumentHelper DocumentHelper { get; }
        IDocumentSignHelper SignHelper { get; }

        IIRSW8Repo IRSW8 { get; }
        IIRSW9Repo IRSW9 { get; }
        IPurposeAndFundingRepo PurposeAndFunding { get; }
        IRecordOfAdviceRepo RecordOfAdvice { get; }

        //Client
        IClientRepo Client { get; }
        IBankDetailsRepo BankDetails { get; }
        IKYCDataRepo KycData { get; }
        IRiskProfileRepo RiskProfile { get; }
        ITaxResidencyRepo TaxResidency { get; }
        IConsumerProtectionRepo ConsumerProtection { get; }

        //Product
        IProductRepo ProductRepo { get; }

        //FNA
        IPrimaryResidenceRepo PrimaryResidence { get; }
        IAssetsAttractingCGTRepo AssetsAttractingCGT { get; }
        IAssetsExemptFromCGTRepo AssetsExemptFromCGT { get; }
        ILiquidAssetsRepo LiquidAssets { get; }
        IInsuranceRepo Insurance { get; }
        ILiabilitiesRepo Liabilities { get; }
        IEstateExpensesRepo EstateExpenses { get; }
        IRetirementPensionFundsRepo RetirementPensionFunds { get; }
        IRetirementPreservationFundsRepo RetirementPreservationFunds { get; }
        IAccrualRepo Accrual { get; }
        IRetirementPlanningRepo RetirementPlanning { get; }

        IAdministrationCostsRepo AdministrationCosts { get; }

        //Shared
        IDisclosureRepo Disclosures { get; }

        //User
        IOtpRepo Otp { get; }

        IUserRepo User { get; }
        IUserDocumentsRepo UserDocuments { get; }

        // Third Party Services
        ISmsRepo SmsRepo { get; }

        IJwtRepo JwtRepo { get; }
        IKycFactoryRepo KycRepo { get; }
        IBankValidationServiceRepo BankValidationRepo { get; }
        ISignatureRepo SignatureRepo { get; }
        IFileStorageRepo FileStorageRepo { get; }
        IStringHasher StrHasher { get; }
    }
}