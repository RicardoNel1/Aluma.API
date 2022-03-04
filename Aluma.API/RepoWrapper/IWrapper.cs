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

        IIRSW8Repo IRSW8 { get; }
        IIRSW9Repo IRSW9 { get; }
        IPurposeAndFundingRepo PurposeAndFunding { get; }
        IRecordOfAdviceRepo RecordOfAdvice { get; }

        //Client
        IClientRepo Client { get; }
        IBankDetailsRepo BankDetails { get; }
        IKycDataRepo KycData { get; }
        IRiskProfileRepo RiskProfile { get; }
        ITaxResidencyRepo TaxResidency { get; }
        IConsumerProtectionRepo ConsumerProtection { get; }

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