using System;
using System.Collections.Generic;

namespace DataService.Dto
{
    public class IRSW8Dto
    {
        public int ApplicationId { get; set; }

        //part 1
        public string Name { get; set; }

        public string BusinessName { get; set; }
        public string DisregardedName { get; set; }
        public string EntitySelectedType { get; set; }
        public bool TreatyClaim { get; set; }
        public string FatcaSelectedStatus { get; set; }
        public string ResidentialAddress1 { get; set; }
        public string ResidentialAddress2 { get; set; }
        public string ResidentialAddressCountry { get; set; }
        public bool PostalSameAsResidential { get; set; }
        public string PostalAddress1 { get; set; }
        public string PostalAddress2 { get; set; }
        public string PostalAddressCountry { get; set; }
        public string TIN { get; set; }
        public string GIIN { get; set; }
        public string ForeignTIN { get; set; }
        public string ReferenceNumber { get; set; }

        //part 2
        public string DisregardedFatcaStatus { get; set; }

        public string DisregardedResidentialAddress1 { get; set; }
        public string DisregardedResidentialAddress2 { get; set; }
        public string DisregardedResidentialAddressCountry { get; set; }
        public string DisregardedGIIN { get; set; }

        //part 3
        public bool TaxTreatyCheck1 { get; set; }

        public string TaxTreatyAddress { get; set; }
        public bool TaxTreatyCheck2 { get; set; }
        public string TaxTreatySelectedBenefit { get; set; }
        public string TaxTreatySelectedBenefitOther { get; set; }
        public bool TaxTreatyCheck3 { get; set; }
        public string TaxTreatyClaimArticle { get; set; }
        public string TaxTreatyClaimPercent { get; set; }
        public string TaxTreatyClaimIncome { get; set; }
        public string WithholdingConditions { get; set; }

        //part 4
        public string SponsorEntityName { get; set; }

        public string SponsorEntityRadio { get; set; }

        //part 5
        public bool CertifiedNonRegCheck1 { get; set; }

        //part 6
        public bool CertifiedLowValueCheck1 { get; set; }

        //part 7
        public string CertifiedInvestVehicleSponsorName { get; set; }

        public bool CertifiedInvestVehicleCheck1 { get; set; }

        //part 8
        public bool CertifiedLimitedLifeCheck1 { get; set; }

        //part 9
        public bool InvestmentEntityNotAccountsCheck1 { get; set; }

        //part 10
        public bool OwnerDocumentedCheck1 { get; set; }

        public string OwnerDocumentedRadio { get; set; }
        public bool OwnerDocumentedCheck2 { get; set; }

        //part 11
        public bool RestrictedDistributorCheck1 { get; set; }

        public string RestrictedDistributorRadio { get; set; }

        //public bool RestrictedDistributorCheck2 { get; set; }
        //part12
        public bool NonReportingCheck1 { get; set; }

        public string NonReportingRadio1 { get; set; }
        public string NonReportingRadio2 { get; set; }
        public string NonReportingCountry { get; set; }
        public string NonReportingTreatedAs { get; set; }
        public string NonReportingTrusteeName { get; set; }

        //part13
        public bool ForeignGovCheck1 { get; set; }

        //part14
        public string InternationalOrgRadio { get; set; }

        //part15
        public string ExemptRetirementRadio { get; set; }

        //part16
        public bool ExemptBeneficialOwnerCheck1 { get; set; }

        //part17
        public bool TerritoryFinancialInstituteCheck1 { get; set; }

        //part18
        public bool ExceptedNonFinancialGroupCheck1 { get; set; }

        //part19
        public bool ExceptedNonFinancialStartUpCheck1 { get; set; }

        public string ExceptedNonFinancialStartUpDate { get; set; }

        //part20
        public bool ExceptedNonFinancialLiquidationCheck1 { get; set; }

        public string ExceptedNonFinancialLiquidationDate { get; set; }

        //part21
        public bool Organization501Check1 { get; set; }

        public string Organization501Date { get; set; }

        //part22
        public bool NonProfitCheck1 { get; set; }

        //part23
        public string PublicTradedNFFERadio { get; set; }

        public string SecuritiesMarket { get; set; }
        public string AffiliateEntityName { get; set; }
        public string AffiliateSecurityExchange { get; set; }

        //part24
        public bool ExceptedTerritoryNFFECheck1 { get; set; }

        //part25
        public bool ActiveNFFECheck1 { get; set; }

        //part26
        public bool PassiveNFFECheck1 { get; set; }

        public string PassiveNFFERadio { get; set; }

        //part27
        public bool ExceptedInterAffiliatedCheck1 { get; set; }

        //part28
        public string SponsoredDirectReportNFFEName { get; set; }

        public bool SponsoredDirectReportNFFECheck1 { get; set; }

        //part29
        public ICollection<USPersonsDto> USPersons { get; set; }
    }
}