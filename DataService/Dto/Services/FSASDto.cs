using System;
using System.Collections.Generic;

namespace DataService.Dto
{

    public class FSASConfigDto
    {
        public string BaseUrl { get; set; }
        public string Memberkey { get; set; }
        public string Password { get; set; }
    }

    public class SubmitCCPResponseDto
    {
        public string BaseUrl { get; set; }
        public string Authorization { get; set; }
        public string Memberkey { get; set; }
        public string Password { get; set; }
    }

    public class ClientCCPResponseDto
    {
        public List<PackageDto> Packages { get; set; }
        public List<InvestmentDto> Investments { get; set; }

    }

    public class InvestmentDto
    {
        public string AccountNumber { get; set; }
        public double AccountBalance { get; set; }
        public string ProviderName { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }

    }

    public class PackageDto
    {
        public string PackageName { get; set; }
        public string ProviderName { get; set; }
        public PolicyDto PolicyDetails { get; set; }
    }

    public class PolicyDto
    {
        public string PolicyNumber { get; set; }
        public double TotalPremium { get; set; }
        public DateTime PolicyStartDate { get; set; }
        public DateTime PolicyEndDate { get; set; }
        public string PolicyOwner { get; set; }
        public string Beneficiary { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<CoverDto> Covers { get; set; }
    }

    public class CoverDto
    {
        public string CoverType { get; set; }
        public double TotalCover { get; set; }
        public string CoverProductName { get; set; }
        public double DeathValue { get; set; }
        public double NetSurrenderValue { get; set; }
        public double FundBalance { get; set; }
        public double NetLoanValue { get; set; }
        public double OutstandingLoanAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


    public class SubmitCCPRequestDto
    {
        public RequestClientDetails Client { get; set; }
        public string OurReference { get; set; }

    }

    public class RequestClientDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber { get; set; }
        public string IdType { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string[] ConsentedProviders { get; set; }

    }

}