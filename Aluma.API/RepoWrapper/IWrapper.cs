using Aluma.API.Helpers;
using Aluma.API.Repositories;
using BankValidationService;
using FileStorageService;
using FintegrateSharedAstuteService;
using IDVService;
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

        IFIRepo FI { get; }
        IPEFRepo PEF { get; }
        IDocumentHelper DocumentHelper { get; }
        IDocumentSignHelper SignHelper { get; }

        IIRSW8Repo IRSW8 { get; }
        IIRSW9Repo IRSW9 { get; }
        IPurposeAndFundingRepo PurposeAndFunding { get; }
        IRecordOfAdviceRepo RecordOfAdvice { get; }

        //Client
        ILeadRepo Leads { get; }
        IClientRepo Client { get; }
        IBankDetailsRepo BankDetails { get; }
        IKYCDataRepo KycData { get; }
        IRiskProfileRepo RiskProfile { get; }
        ITaxResidencyRepo TaxResidency { get; }
        IConsumerProtectionRepo ConsumerProtection { get; }
        IClientPortfolioRepo ClientPortfolio { get; }

        //Product
        IProductRepo ProductRepo { get; }

        //FNA
        IFNARepo FNA { get; }
        IPrimaryResidenceRepo PrimaryResidence { get; }
        IAssetsAttractingCGTRepo AssetsAttractingCGT { get; }
        IAssetsExemptFromCGTRepo AssetsExemptFromCGT { get; }
        IInvestmentsRepo Investments { get; }
        ILiquidAssetsRepo LiquidAssets { get; }
        IInsuranceRepo Insurance { get; }
        ILiabilitiesRepo Liabilities { get; }
        IEstateExpensesRepo EstateExpenses { get; }
        IRetirementPensionFundsRepo RetirementPensionFunds { get; }
        IRetirementPreservationFundsRepo RetirementPreservationFunds { get; }
        IAccrualRepo Accrual { get; }
        IRetirementPlanningRepo RetirementPlanning { get; }
        IProvidingOnDeathRepo ProvidingOnDeath { get; }
        IProvidingOnDreadDiseaseRepo ProvidingOnDreadDisease { get; }
        IProvidingOnDisabilityRepo ProvidingOnDisability { get; }

        IAdministrationCostsRepo AdministrationCosts { get; }
        IEstateDutiesRepo EstateDuties { get; }
        ICapitalGainsTaxRepo CapitalGainsTax { get; }
        IAssumptionsRepo Assumptions { get; }
        ITaxLumpsumRepo TaxLumpsum { get; }

        //FNA - SUMMARY
        IAssetSummaryRepo AssetSummary { get; }
        IInsuranceSummaryRepo InsuranceSummary { get; }
        IProvidingDeathSummaryRepo ProvidingDeathSummary { get; }
        IProvidingDisabilitySummaryRepo ProvidingDisabilitySummary { get; }
        IRetirementSummaryRepo RetirementSummary{ get; }
        IEconomyVariablesSummaryRepo EconomyVariablesSummary { get; }

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
        IFSASRepo FSASRepo { get; }
        IIDVServiceRepo IDVRepo { get; }
        ISignatureRepo SignatureRepo { get; }
        IFileStorageRepo FileStorageRepo { get; }
        IStringHasher StrHasher { get; }

        // Short-term Insurance
        IShortTermInsuranceRepo ShortTermInsurance { get; }

        // Medical Aid
        IMedicalAidRepo MedicalAid { get; }
    }
}