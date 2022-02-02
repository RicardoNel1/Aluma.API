using System.Collections.Generic;

namespace DataService.Dto
{
    public class KycSettingsDto
    {
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
        public string businessId { get; set; }
    }

    public class KycInitiationDto
    {
        public string BusinessId { get; set; }
        public ICollection<ConsumerDto> Consumers { get; set; }
        public string EnquiryID { get; set; }
        public string EnquiryResultID { get; set; }
        public AgentDto CapturingAgent { get; set; }
        public string DirectorsKYC { get; set; }
        public ConfigDto Config { get; set; }
    }

    public class ConfigDto
    {
        public string Authentication { get; set; }
        public string PresharedPassword { get; set; }
        public ICollection<ConsumerDto> ConsumersToCapture { get; set; }
    }

    public class AgentDto
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string CountryOfBirth { get; set; }
        public string Status { get; set; }
        public bool PreventAgentFromSigning { get; set; }
    }

    public class ConsumerDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string IdNumber { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool SendEmail { get; set; }
        public bool IsCurrent { get; set; }

        public int Section { get; set; }
        public int Sign { get; set; }
    }

    public class PortfolioDto
    {
        public string ReferenceNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Type { get; set; }
        public string PortfolioCount { get; set; }
        public string SendEmail { get; set; }
        public bool PackageEmailName { get; set; }
        public bool SendOnComplete { get; set; }
        public int EmailInformationURL { get; set; }
    }

    public class HeaderDto
    {
        public string TransactionID { get; set; }
        public string Organisation { get; set; }
        public string UserID { get; set; }
        public string SystemID { get; set; }
        public string Timestamp { get; set; }

        public PortfolioDto PortfolioSettings { get; set; }
    }

    public class KycInitiationResponseDto
    {
        public int FactoryId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int NextStep { get; set; }
    }

    public class KycInitiationBusinessResponseDto
    {
        public HeaderDto Header { get; set; }
        public int FactoryId { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string FactoryLink { get; set; }
        public string CompanyName { get; set; }
        public string RegNumber { get; set; }
        public AddressDto RegisteredAddress { get; set; }
        public AddressDto TradingAddress { get; set; }
        public string RegistrationDate { get; set; }
        public string BusinessStartDate { get; set; }
        public string FinancialYearEnd { get; set; }
        public string CommercialStatus { get; set; }
        public string CommercialType { get; set; }
        public string SIC { get; set; }
        public string TaxNo { get; set; }
        public string AgeofBusiness { get; set; }
        public ICollection<DirectorDto> Directors { get; set; }
        public ICollection<string> StreetViewLinks { get; set; }
        public IdentitySummaryDto IdentitySummary { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }

    public class IdentitySummaryDto
    {
        public string CIPCResult { get; set; }
        public AddressDto GoldenAddress { get; set; }
        public string GeoLocation { get; set; }
        public string WebsiteContact { get; set; }
        public string WebsiteCertificate { get; set; }
        public string WhoisCheck { get; set; }
        public string GleifCheck { get; set; }
        public string SanctionsResult { get; set; }
    }

    public class DirectorDto
    {
        public string FirstNames { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string DirectorType { get; set; }
        public string DateOfBirth { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
    }

    public class KycAddressDto
    {
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
    }

    public class KycEventDto
    {
        public string EventDate { get; set; }
        public int FactoryId { get; set; }
        public string Identity { get; set; }
        public string EventType { get; set; }
        public string IdentityType { get; set; }
        public string EventResult { get; set; }
        public string CurrentStep { get; set; }
        public string NextStep { get; set; }
    }

    public class FactoryDetailsDto
    {
        public int factoryId { get; set; }
        public string idNumber { get; set; }
    }

    public class MetaDataDto
    {
        public IdVerifyDto IdVerify { get; set; }
    }

    public class IdVerifyDto
    {
        public KycResultsDto RealTimeResults { get; set; }
    }

    public class KycResultsDto
    {
        public string IdNumber { get; set; }
        public string FirstNames { get; set; }
        public string SurName { get; set; }
        public string Dob { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Citizenship { get; set; }
        public string DeceasedStatus { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
    }

    public class ComplianceReportDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Document { get; set; }
    }
}