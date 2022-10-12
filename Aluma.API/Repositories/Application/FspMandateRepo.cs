using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IFspMandateRepo : IRepoBase<FSPMandateModel>
    {
        bool DoesApplicationHaveMandate(FSPMandateDto dto);

        FSPMandateDto UpdateFSPMandate(FSPMandateDto dto);

        FSPMandateDto CreateFSPMandate(FSPMandateDto dto);

        FSPMandateDto GetFSPMandate(int clientId);

        bool DeleteFSPMandate(FSPMandateDto dto);

        Task GenerateMandate(ClientModel client, AdvisorModel advisor, FSPMandateModel fsp);
    }

    public class FspMandateRepo : RepoBase<FSPMandateModel>, IFspMandateRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        public FspMandateRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
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

        public async Task GenerateMandate(ClientModel client, AdvisorModel ad, FSPMandateModel fsp)
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


            if (client.User.Address.Count > 0)
            {
                foreach (var item in client.User.Address)
                {
                    string street = string.Empty;
                    string unitComplex = string.Empty;
                    if (item.Type == DataService.Enum.AddressTypesEnum.Residential)
                    {
                        signCity = item.City;
                        street = $"{item.StreetNumber} {item.StreetName}";
                        unitComplex = $"{item.UnitNumber} {item.ComplexName}";

                        d[$"address1"] = unitComplex != " " ? $"{street}, {unitComplex}": street ;

                        d[$"address2"] = $"{item.Suburb} " + $"{item.City} ";

                        d["postalCode"] = item.PostalCode;

                        d["yearsAtAddress"] = item.YearsAtAddress.ToString();
                    }

                    if (item.Type == DataService.Enum.AddressTypesEnum.Postal)
                    {
                        street = $"{item.StreetNumber} {item.StreetName}";
                        unitComplex = $"{item.UnitNumber} {item.ComplexName}";

                        d["p_address"] = unitComplex != " " ? $"{street}, {unitComplex}, {item.Suburb}, {item.City}, {item.Country}" : $"{street}, {item.Suburb}, {item.City}, {item.Country}";
                        d["p_postalCode"] = item.PostalCode;
                    }

                }

            }



            d["businessTel"] = client.EmploymentDetails.WorkNumber ?? String.Empty;
            d["mobile"] = "0" + client.User.MobileNumber ?? string.Empty;
            d["email"] = client.User.Email;



            d[$"{client.MaritalDetails.MaritalStatus}"] = "x";



            if (client.MaritalDetails.MaritalStatus != "single")
            {
                d["dateOfMarriage"] = client.MaritalDetails.DateOfMarriage;
                //d["dateOfMarriage_month"] = clientDetails.DateOfMarriage.ToString().Substring(5, 2);
                //d["dateOfMarriage_year"] = (clientDetails.DateOfMarriage).ToString().Substring(0, 4);

                if (client.MaritalDetails.ForeignMarriage == true)
                    d["foreignMarriage_Y"] = "x";
                else
                    d["foreignMarriage_N"] = "x";

                d["countryOfMarriage"] = client.MaritalDetails.CountryOfMarriage;
                d["spouseName"] = client.MaritalDetails.FirstName;
                d["surname"] = client.MaritalDetails.Surname;
                d["idNumber"] = client.MaritalDetails.IdNumber;
                d["maidenName"] = client.MaritalDetails.MaidenName;
                //d["spouseDateOfBirth"] = clientDetails.SpouseDateOfBirth;
                d["spouseDateOfBirth"] = client.MaritalDetails.SpouseDateOfBirth;
                //d["spouseDateOfBirth_month"] = clientDetails.SpouseDateOfBirth.ToString().Substring(5, 2);
                //d["spouseDateOfBirth_year"] = (clientDetails.SpouseDateOfBirth).ToString().Substring(0, 4);

                if (client.MaritalDetails.PowerOfAttorney == true)
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
                    else bv = item;
                }
            }
            else
            {
                bv = client.BankDetails.First();
            }

            //UtilityHelper uh = new();
            d["accountHolder"] = client.User.FirstName + " " + client.User.LastName; //$"{uh.Initials(client.User.FirstName)}"; //Fixed Note Mandate issue
            d["bank"] = bv.BankName ?? string.Empty;
            d["branchNo"] = bv.BranchCode ?? string.Empty;
            d["branchName"] = string.Empty;
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

                if (fsp.InvestmentObjective == "incomeGeneration")
                    d["maximumDividend"] = "x";
                else if (fsp.InvestmentObjective == "capitalGrowth")
                    d["maximumCapital"] = "x";

                d["date_full"] = DateTime.UtcNow.ToString();

            }

            else if (fsp.DiscretionType == "limited_DE")
            {
                d["dealAndExecution"] = "x";


                if (fsp.LimitedInstruction != "instructionBoth")
                {
                    d[$"{fsp.LimitedInstruction}_DE"] = "x";
                }
                else
                {
                    d[$"instructionPersonal_DE"] = "x";
                    d[$"instructionAdvisor_DE"] = "x";
                }


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




                if (fsp.LimitedInstruction != "instructionBoth")
                {
                    d[$"{fsp.LimitedInstruction}_RM"] = "x";
                }
                else
                {
                    d[$"instructionPersonal_RM"] = "x";
                    d[$"instructionAdvisor_RM"] = "x";
                }

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


            DocumentHelper dh = new(_context, _config, _fileStorage, _host);

            await dh.PopulateAndSaveDocument(DocumentTypesEnum.FSPMandate, d, client.User);
        }
    }
}