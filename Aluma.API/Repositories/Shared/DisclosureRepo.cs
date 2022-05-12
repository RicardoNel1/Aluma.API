using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IDisclosureRepo : IRepoBase<DisclosureModel>
    {
        public DisclosureDto GetDisclosure(DisclosureDto dto);

        public List<DisclosureDto> GetDisclosureListByAdvisor(AdvisorDto dto);

        public List<DisclosureDto> GetDisclosureListByClient(ClientDto dto);

        public bool DoesClientHaveDisclosure(ClientDto dto);

        DisclosureDto GetDisclosureByClient(ClientDto dto);

        public DisclosureDto CreateDisclosure(DisclosureDto dto);

        void UpdateDisclosure(ClientDto dto);

        bool DeleteDisclosure(DisclosureDto dto);
        Task GenerateClientConsent(ClientModel client, AdvisorModel advisor);

    }

    public class DisclosureRepo : RepoBase<DisclosureModel>, IDisclosureRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUserDocumentsRepo _userDocuments;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _fileStorage;

        public DisclosureRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage, IUserDocumentsRepo userDocuments) : base(databaseContext)
        {
            _context = databaseContext;
            _config = config;
            _host = host;
            _mapper = mapper;
            _userDocuments = userDocuments;
            _fileStorage = fileStorage;
        }

        public DisclosureDto CreateDisclosure(DisclosureDto dto)
        {
            try
            {
                DisclosureModel disclosure = _mapper.Map<DisclosureModel>(dto);

                _context.Disclosures.Add(disclosure);
                _context.SaveChanges();

                return _mapper.Map<DisclosureDto>(disclosure);
            }
            catch (Exception ex)
            {
                //log error
                return null;
            }
        }

        public bool DoesClientHaveDisclosure(ClientDto dto)
        {
            bool exists = _context.Disclosures.Where(c => c.ClientId == dto.Id).Any();

            return exists;
        }

        public DisclosureDto GetDisclosure(DisclosureDto dto)
        {
            DisclosureModel disclosure = _context.Disclosures.Where(c => c.Id == dto.Id).Include(c => c.Client).Include(c => c.Advisor).First();
            return _mapper.Map<DisclosureDto>(disclosure);
        }

        public List<DisclosureDto> GetDisclosureListByAdvisor(AdvisorDto dto)
        {
            List<DisclosureModel> applications = _context.Disclosures.Where(c => c.AdvisorId == dto.Id).Include(c => c.Client).ToList();
            return _mapper.Map<List<DisclosureDto>>(applications);
        }

        public List<DisclosureDto> GetDisclosureListByClient(ClientDto dto)
        {
            List<DisclosureModel> applications = _context.Disclosures.Where(c => c.ClientId == dto.Id).Include(c => c.Advisor).ToList();
            return _mapper.Map<List<DisclosureDto>>(applications);
        }

        public DisclosureDto GetDisclosureByClient(ClientDto dto)
        {
            throw new NotImplementedException();
        }

        public void UpdateDisclosure(ClientDto dto)
        {
            throw new NotImplementedException();
        }

        public bool DeleteDisclosure(DisclosureDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task GenerateDisclosure(ClientModel client, AdvisorModel advisor, ConsumerProtectionModel cpa, DisclosureModel disclosure)
        {
            var d = new Dictionary<string, string>();
            //Advisor Details
            d["advisor"] = advisor.User.FirstName + " " + advisor.User.LastName;
            d["dofa"] = advisor.AppointmentDate.ToString("yyyy/MM/dd") ?? string.Empty;

            if (advisor.User.Address.Count > 1)
            {
                var resAddress = advisor.User.Address.First(a => a.Type == AddressTypesEnum.Residential);
                var postalAddress = advisor.User.Address.First(a => a.Type == AddressTypesEnum.Postal);

                //Physical Address
                d["resAddressLine1"] = resAddress.ComplexName + " " + resAddress.UnitNumber;
                d["resAddressLine2"] = resAddress.StreetNumber + " " + resAddress.StreetName;
                d["resAddressLine3"] = resAddress.Suburb + ", " + resAddress.City;
                d["resAddressLine4"] = resAddress.Country;
                d["resAddressPostalCode"] = resAddress.PostalCode;

                //Postal Address
                d["postAddressLine1"] = postalAddress.UnitNumber + " " + postalAddress.ComplexName;
                d["postAddressLine2"] = postalAddress.StreetNumber + " " + postalAddress.StreetName;
                d["postAddressLine3"] = postalAddress.Suburb + ", " + postalAddress.City;
                d["postAddressLine4"] = postalAddress.Country;
                d["postAddressPostalCode"] = postalAddress.PostalCode;

            }

            //Contact Details
            d["adviserBusTel"] = advisor.BusinessTel ?? string.Empty;
            d["adviserHomeTel"] = advisor.HomeTel ?? string.Empty;
            d["adviserCell"] = "0" + advisor.User.MobileNumber ?? string.Empty;
            d["adviserFax"] = advisor.Fax ?? string.Empty;
            d["adviserEmail"] = advisor.User.Email ?? string.Empty;

            //Financial Services

            d["LongTermSubA_A"] = advisor.AdviceLTSubCatA ? "X" : null;

            d["LongTermSubA_S"] = advisor.SupervisedLTSubCatA ? "X" : null;


            d["ShortTermPersonal_A"] = advisor.AdviceSTPersonal ? "X" : null;

            d["ShortTermPersonal_S"] = advisor.SupervisedSTPersonal ? "X" : null;


            d["LongTermSubB1_A"] = advisor.AdviceLTSubCatB1 ? "X" : null;

            d["LongTermSubB1_S"] = advisor.SupervisedLTSubCatB1 ? "X" : null;


            d["LongTermSubC_A"] = advisor.AdviceLTSubCatC ? "X" : null;

            d["LongTermSubC_S"] = advisor.SupervisedLTSubCatC ? "X" : null;


            d["RetailPension_A"] = advisor.AdviceRetailPension ? "X" : null;

            d["RetailPension_S"] = advisor.SupervisedRetailPension ? "X" : null;


            d["ShortTermCommercial_A"] = advisor.AdviceSTCommercial ? "X" : null;

            d["ShortTermCommercial_S"] = advisor.SupervisedSTCommercial ? "X" : null;


            d["PensionFundsBenefits_A"] = advisor.AdvicePensionFunds ? "X" : null;

            d["PensionFundsBenefits_S"] = advisor.SupervisedPensionFunds ? "X" : null;


            d["Shares_A"] = advisor.AdviceShares ? "X" : null;

            d["Shares_S"] = advisor.SupervisedShares ? "X" : null;


            d["MoneyMarket_A"] = advisor.AdviceMoneyMarket ? "X" : null;

            d["MoneyMarket_S"] = advisor.SupervisedMoneyMarket ? "X" : null;


            d["Debentures_A"] = advisor.AdviceDebentures ? "X" : null;

            d["Debentures_S"] = advisor.SupervisedDebentures ? "X" : null;


            d["Warrants_A"] = advisor.AdviceWarrants ? "X" : null;

            d["Warrants_S"] = advisor.SupervisedWarrants ? "X" : null;


            d["Bonds_A"] = advisor.AdviceBonds ? "X" : null;

            d["Bonds_S"] = advisor.SupervisedBonds ? "X" : null;


            d["DerivativeInstruments_A"] = advisor.AdviceDerivatives ? "X" : null;

            d["DerivativeInstruments_S"] = advisor.SupervisedDerivatives ? "X" : null;


            d["CollectiveInvestmentScheme_A"] = advisor.AdviceParticipatoryInterestCollective ? "X" : null;

            d["CollectiveInvestmentScheme_S"] = advisor.SupervisedParticipatoryInterestCollective ? "X" : null;


            //d["MedicalAid_A"] = adviser.AdviceMedicalAid ? "X" : null;

            //d["MedicalAid_S"] = adviser.SupervisedMedicalAid ? "X" : null;


            d["LongTermDeposits_A"] = advisor.AdviceLTDeposits ? "X" : null;

            d["LongTermDeposits_S"] = advisor.SupervisedLTDeposits ? "X" : null;


            d["ShortTermDeposits_A"] = advisor.AdviceSTDeposits ? "X" : null;

            d["ShortTermDeposits_S"] = advisor.SupervisedSTDeposits ? "X" : null;


            d["LongTermSubB2_A"] = advisor.AdviceLTSubCatB2 ? "X" : null;

            d["LongTermSubB2_S"] = advisor.SupervisedLTSubCatB2 ? "X" : null;


            d["LongTermSubB2A_A"] = advisor.AdviceLTSubCatB2A ? "X" : null;

            d["LongTermSubB2A_S"] = advisor.SupervisedLTSubCatB2A ? "X" : null;


            d["LongTermSubB1A_A"] = advisor.AdviceLTSubCatB1A ? "X" : null;

            d["LongTermSubB1A_S"] = advisor.SupervisedLTSubCatB1A ? "X" : null;


            d["ShortTermPersonalA1_A"] = advisor.AdviceSTPersonalA1 ? "X" : null;

            d["ShortTermPersonalA1_S"] = advisor.SupervisedSTPersonalA1 ? "X" : null;


            d["StructuredDeposits_A"] = advisor.AdviceStructuredDeposits ? "X" : null;

            d["StructuredDeposits_S"] = advisor.SupervisedStructuredDeposits ? "X" : null;


            d["Securities_A"] = advisor.AdviceSecurities ? "X" : null;

            d["Securities_S"] = advisor.SupervisedSecurities ? "X" : null;


            d["HedgeFunds_A"] = advisor.AdviceParticipatoryInterestHedge ? "X" : null;

            d["HedgeFunds_S"] = advisor.SupervisedParticipatoryInterestHedge ? "X" : null;


            //CPA Options
            if (cpa.InformProducts)
                d["cpaDis1Y"] = "X";
            else
                d["cpaDis1N"] = "X";

            if (cpa.InformOffers)
                d["cpaDis2Y"] = "X";
            else
                d["cpaDis2N"] = "X";

            if (cpa.RequestResearch)
                d["cpaDis3Y"] = "X";
            else
                d["cpaDis3N"] = "X";

            if (cpa.PreferredComm != null)
                d["comm" + cpa.PreferredComm] = "X";

            if (cpa.OtherCommMethods)
                d["cpaDis4Y"] = "X";
            else
                d["cpaDis4N"] = "X";


            //Service Level Agreement
            d["clientName"] = client.User.FirstName + " " + client.User.LastName;
            d["clientID"] = client.User.RSAIdNumber;
            d["clientCapacity"] = "Self";

            //d["authUsers"] = disclosure.AuthorisedUsers ?? string.Empty;

            d["date"] = DateTime.Now.ToString("yyyy/MM/dd");
            //d["date2"] = DateTime.Now.ToString("yyyy/MM/dd");

            //Broker Appointment
            //if (disclosure != null)
            //{
            //    if (disclosure.AdvisorAuthority)
            //    {
            //        d["authorityAll"] = "X";
            //    }
            //    else
            //    {
            //        d["authoritySome"] = "X";
            //        d["authorityProducts"] = disclosure.AdvisorAuthorityProducts;
            //    }

            //    d["date2"] = DateTime.Now.ToString("yyyy/MM/dd");
            //}


            DocumentHelper dh = new DocumentHelper(_context, _config, _fileStorage, _host);

            await dh.PopulateAndSaveDocument(DocumentTypesEnum.DisclosureLetter, d, client.User);
        }

        public async Task GenerateClientConsent(ClientModel client, AdvisorModel advisor)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d["fullName"] = $"{client.User.FirstName} {client.User.LastName}";
            d["idNumber"] = client.User.RSAIdNumber;
            d["onBehalfOf"] = "Self";

            d["advisorName"] = $"{advisor.User.FirstName} {advisor.User.LastName}";

            d["listedAbove"] = "x";


            d["signAt"] = client.User.Address.First().City;

            d["signedOnDay"] = DateTime.UtcNow.Day.ToString();
            d["signedOnMonth"] = DateTime.UtcNow.Month.ToString();
            d["signedOnYear"] = DateTime.UtcNow.Year.ToString().Substring(2, 2);

            d["clientContact"] = "Self";

            if (client.isSmoker)
                d["smokerTrue"] = "x";
            else
                d["smokerFalse"] = "x";

            if (client.Lead.LeadType == LeadTypesEnum.Direct)
                d["leadType"] = "x";

            if (client.Education != null)
            {
                d[$"education_{client.Education}"] = "x";
            }

            DocumentHelper dh = new DocumentHelper(_context, _config, _fileStorage, _host);

            await dh.PopulateAndSaveDocument(DocumentTypesEnum.ClientConsent, d, client.User);
        }
    }
}