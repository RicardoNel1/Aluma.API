using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IFspMandateRepo : IRepoBase<FSPMandateModel>
    {
        bool DoesApplicationHaveMandate(FSPMandateDto dto);

        FSPMandateDto UpdateFSPMandate(FSPMandateDto dto);

        FSPMandateDto CreateFSPMandate(FSPMandateDto dto);

        FSPMandateDto GetFSPMandate(int clientId);

        bool DeleteFSPMandate(FSPMandateDto dto);

        void GenerateMandate(ClientModel client, AdvisorModel advisor, FSPMandateModel fsp);
    }

    public class FspMandateRepo : RepoBase<FSPMandateModel>, IFspMandateRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        DocumentHelper dh = new DocumentHelper();

        public FspMandateRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public FSPMandateDto CreateFSPMandate(FSPMandateDto dto)
        {
            FSPMandateModel newFsp = _mapper.Map<FSPMandateModel>(dto);

            _context.FspMandates.Add(newFsp);
            _context.SaveChanges();

            dto = _mapper.Map<FSPMandateDto>(newFsp);

            return dto;
        }

        public bool DeleteFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveMandate(FSPMandateDto dto)
        {
            var fsp = _context.FspMandates.Where(r => r.ClientId == dto.ClientId);
            if (fsp.Any())
            {
                return true;
            }
            return false;
        }

        public FSPMandateDto GetFSPMandate(int clientId)
        {
            var fspModel = _context.FspMandates.Where(r => r.ClientId == clientId);

            if (fspModel.Any())
            {
                return _mapper.Map<FSPMandateDto>(fspModel.First());
            }
            return null;

        }

        public FSPMandateDto UpdateFSPMandate(FSPMandateDto dto)
        {
            FSPMandateModel newFsp = _mapper.Map<FSPMandateModel>(dto);

            _context.FspMandates.Update(newFsp);
            _context.SaveChanges();

            dto = _mapper.Map<FSPMandateDto>(newFsp);

            return dto;
        }

        public void GenerateMandate(ClientModel client, AdvisorModel ad, FSPMandateModel fsp)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;

            d["static"] = "x";


            //1. Individuals

            d["name"] = $"{client.User.FirstName} {client.User.LastName}";
            d["surname"] = client.User.LastName;
            d["firstName"] = client.User.FirstName;

            //if (names.Length > 1)
            //    d["middleName"] = names[1];


            d["title"] = client.Title;
            d["dateOfBirth"] = client.User.DateOfBirth;

            if (client.NonResidentialAccount == true)
                d["nonResidential_Y"] = "x";
            else
                d["nonResidential_N"] = "x";

            d["idNo"] = client.User.RSAIdNumber;
            d["nationality"] = client.Nationality;
            d["taxNo"] = client.TaxResidency.TaxNumber ?? " ";


            if (client.User.Address.Count > 1)
            {
                var addressList = client.User.Address.ToList();

                for (int i = 0; i < addressList.Count; i++)
                {
                    if (addressList[i].Type == DataService.Enum.AddressTypesEnum.Residential)
                    {
                        signCity = addressList[i].City;

                        d[$"address1"] = $"{addressList[i].StreetNumber} " + $"{addressList[i].StreetName}, " +
                    $"{addressList[i].UnitNumber} " + $"{addressList[i].ComplexName}";

                        d["postalCode"] = addressList[i].PostalCode;

                        d["yearsAtAddress"] = addressList[i].YearsAtAddress.ToString();
                    }

                    if (addressList[i].Type == DataService.Enum.AddressTypesEnum.Postal)
                    {
                        d["p_address"] = $"{addressList[i].StreetNumber} " + $"{addressList[i].StreetName}, " +
               $"{addressList[i].UnitNumber} " + $"{addressList[i].ComplexName}, " + $"{addressList[i].Suburb}, " + $"{addressList[i].City}, " +
               $"{addressList[i].Country}";
                        d["p_postalCode"] = addressList[i].PostalCode;
                    }
                }
            }


            d["businessTel"] = client.WorkNumber;
            d["mobile"] = "0" + client.User.MobileNumber ?? string.Empty;
            d["email"] = client.User.Email;


            if (client.MaritalStatus == "Single")
                d["single"] = "x";

            else if (client.MaritalStatus == "MarriedInCommunity")
                d["inCommunity"] = "x";

            else if (client.MaritalStatus == "MarriedOutCommunity")
                d["outCommunity"] = "x";

            if (client.MaritalStatus != "Single")
            {
                d["dateOfMarriage"] = client.DateOfMarriage;
                //d["dateOfMarriage_month"] = clientDetails.DateOfMarriage.ToString().Substring(5, 2);
                //d["dateOfMarriage_year"] = (clientDetails.DateOfMarriage).ToString().Substring(0, 4);

                if (client.ForeignMarriage == true)
                    d["foreignMarriage_Y"] = "x";
                else
                    d["foreignMarriage_N"] = "x";

                d["countryOfMarriage"] = client.CountryOfMarriage;
                d["spouseName"] = client.SpouseName;
                d["maidenName"] = client.MaidenName;
                //d["spouseDateOfBirth"] = clientDetails.SpouseDateOfBirth;
                d["spouseDateOfBirth"] = client.SpouseDateOfBirth;
                //d["spouseDateOfBirth_month"] = clientDetails.SpouseDateOfBirth.ToString().Substring(5, 2);
                //d["spouseDateOfBirth_year"] = (clientDetails.SpouseDateOfBirth).ToString().Substring(0, 4);

                if (client.PowerOfAttorney == true)
                    d["powerOfAttorney_Y"] = "x";
                else
                    d["powerOfAttorney_N"] = "x";
            }

            //2. Institutions (skipped for now)

            //3. Bank Details  
            BankDetailsModel bv = null;

            if (client.BankDetails.Count > 1)
            {
                foreach (var item in client.BankDetails)
                {
                    if (item.VerificationType == "Individual")
                    {
                        bv = item;
                    }
                }
            }
            else
            {
                bv = client.BankDetails.First();
            }

              
            d["accountHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
            d["bank"] = bv.BankName ?? string.Empty;
            d["branchNo"] = bv.BranchCode ?? string.Empty;
            d["branchName"] = bv.BranchCode ?? string.Empty;
            d["accountNo"] = bv.AccountNumber ?? string.Empty;
            d["accountType"] = bv.AccountType ?? string.Empty;


            //General Terms and Conditions

            if (fsp.DiscretionType == "full")
            {
                d["dividendInstruction_full"] = fsp.DividendInstruction ?? string.Empty;
                d["monthlyFee_full"] = fsp.PortfolioManagementFee ?? string.Empty;
                d["additionalFee"] = fsp.AdditionalAdvisorFee ?? string.Empty;
                d["initialFee_full"] = fsp.ForeignInvestmentInitialFee ?? string.Empty;
                d["annualFee_full"] = fsp.ForeignInvestmentAnnualFee ?? string.Empty;

                if (fsp.InvestmentObjective == "IncomeGeneration")
                    d["maximumDividend"] = "x";
                else if (fsp.InvestmentObjective == "CapitalGrowth")
                    d["maximumCapital"] = "x";

                d["date_full"] = DateTime.UtcNow.ToString();

            }

            else if (fsp.DiscretionType == "limited_DE")
            {
                d["dealAndExecution"] = "x";

                if (fsp.onClientInstruction)
                    d["instruction_personal_DE"] = "x";
                else if (fsp.onAdvisorInstruction)
                    d["instruction_advisor_DE"] = "x";

                d["dividendInstruction_limited"] = fsp.DividendInstruction ?? string.Empty;

                if (fsp.VoteInstruction == "advisorDiscretion")
                    d["voteFull"] = "x";
                else if (fsp.VoteInstruction == "clientDiscretion")
                    d["voteLimited"] = "x";

                d["monthlyFee_limited"] = fsp.PortfolioManagementFee ?? string.Empty;
                d["initialFee_limited"] = fsp.InitialFee ?? string.Empty;
                d["annualFee_limited"] = fsp.AdditionalAdvisorFee ?? string.Empty;

            }
            else if (fsp.DiscretionType == "limited_RM")
            {
                d["referralManaged"] = "x";

                if (fsp.onClientInstruction)
                    d["instruction_personal_RM"] = "x";
                else if (fsp.onAdvisorInstruction)
                    d["instruction_adviser_RM"] = "x";

                d["dividendInstruction_limited"] = fsp.DividendInstruction ?? string.Empty;

                if (fsp.VoteInstruction == "advisorDiscretion")
                    d["voteFull"] = "x";
                else if (fsp.VoteInstruction == "clientDiscretion")
                    d["voteLimited"] = "x";

                d["monthlyFee_limited"] = fsp.PortfolioManagementFee ?? string.Empty;
                d["initialFee_limited"] = fsp.InitialFee ?? string.Empty;
                d["annualFee_limited"] = fsp.AdditionalAdvisorFee ?? string.Empty;

            }

            d["adminFee"] = fsp.AdminFee ?? string.Empty;


            //signature details
            d["clientSignAt"] = signCity;
            d["witnessSignName"] = ad.User.FirstName + " " + ad.User.LastName;
           

            d["date"] = DateTime.UtcNow.ToString();
            d["signedOnDay"] = DateTime.UtcNow.Day.ToString();
            d["signedOnMonth"] = DateTime.UtcNow.Month.ToString();
            d["signedOnYear"] = DateTime.UtcNow.Year.ToString().Substring(2, 2);


            byte[] doc = dh.PopulateDocument("FspMandate.pdf", d, _host);

            UserDocumentModel udm = new UserDocumentModel()
            {
                DocumentType = DataService.Enum.DocumentTypesEnum.FSPMandate,
                FileType = DataService.Enum.FileTypesEnum.Pdf,
                Name = $"Aluma Capital Discretionary Mandate - {client.User.FirstName + " " + client.User.LastName}.pdf",
                URL = "data:application/pdf;base64," + Convert.ToBase64String(doc, 0, doc.Length),
                UserId = client.User.Id
            };

            _context.UserDocuments.Add(udm);
            _context.SaveChanges();
        }
    }
}