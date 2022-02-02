using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IDisclosureRepo : IRepoBase<DisclosureModel>
    {
        public DisclosureDto GetDisclosure(DisclosureDto dto);

        public DisclosureDto GetDisclosureDocument(DisclosureDto dto);

        public List<DisclosureDto> GetDisclosureListByAdvisor(AdvisorDto dto);

        public List<DisclosureDto> GetDisclosureListByClient(ClientDto dto);

        public bool DoesClientHaveDisclosure(ClientDto dto);

        DisclosureDto GetDisclosureByClient(ClientDto dto);

        public DisclosureDto CreateDisclosure(DisclosureDto dto);

        void UpdateDisclosure(ClientDto dto);

        bool DeleteDisclosure(DisclosureDto dto);
    }

    public class DisclosureRepo : RepoBase<DisclosureModel>, IDisclosureRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUserDocumentsRepo _userDocuments;

        public DisclosureRepo(AlumaDBContext databaseContext, IMapper mapper, IUserDocumentsRepo userDocuments) : base(databaseContext)
        {
            _context = databaseContext;
            _mapper = mapper;
            _userDocuments = userDocuments;
        }

        public DisclosureDto CreateDisclosure(DisclosureDto dto)
        {
            try
            {
                DisclosureModel disclosure = _mapper.Map<DisclosureModel>(dto);
                //Create Document

                UserDocumentModel DisclosureDocument = _userDocuments.CreateClientDisclosure(disclosure);
                //Create Disclosure
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

        public DisclosureDto GetDisclosureDocument(DisclosureDto dto)
        {
            DisclosureModel disclosure = _context.Disclosures.Where(c => c.Id == dto.Id).Include(c => c.Document).First();
            return _mapper.Map<DisclosureDto>(disclosure);
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

        //private void CreateDisclosure(DisclosureModel disclosure)
        //{
        //    ApplicationRepo appRepo = new ApplicationRepo(_context, _host, _config);

        //    var adviser = _context.Advisors
        //        .Where(a => a.Id == disclosure.AdvisorID)
        //        .Include(a => a.BrokerDetails)
        //        .Include(a => a.User)
        //        .First();

        //    Dictionary<string, string> d = new Dictionary<string, string>();

        //    //Advisor Details
        //    d["advisor"] = adviser.User.FirstName + " " + adviser.User.LastName;
        //    d["dofa"] = adviser.AppointmentDate.ToString("yyyy/MM/dd") ?? string.Empty;

        //    //Physical Address
        //    d["resAddressLine1"] = adviser.BrokerDetails.ResidentialAddressLine1 ?? string.Empty;
        //    d["resAddressLine2"] = adviser.BrokerDetails.ResidentialAddressLine2 ?? string.Empty;
        //    d["resAddressLine3"] = adviser.BrokerDetails.ResidentialAddressLine3 ?? string.Empty;
        //    d["resAddressLine4"] = adviser.BrokerDetails.ResidentialAddressLine4 ?? string.Empty;
        //    d["resAddressPostalCode"] = adviser.BrokerDetails.ResidentialAddressPostalCode ?? string.Empty;

        //    //Postal Address
        //    d["postAddressLine1"] = adviser.BrokerDetails.PostalAddressLine1 ?? string.Empty;
        //    d["postAddressLine2"] = adviser.BrokerDetails.PostalAddressLine2 ?? string.Empty;
        //    d["postAddressLine3"] = adviser.BrokerDetails.PostalAddressLine3 ?? string.Empty;
        //    d["postAddressLine4"] = adviser.BrokerDetails.PostalAddressLine4 ?? string.Empty;
        //    d["postAddressPostalCode"] = adviser.BrokerDetails.PostalAddressPostalCode ?? string.Empty;

        //    //Contact Details
        //    d["adviserBusTel"] = adviser.BrokerDetails.BusinessTel ?? string.Empty;
        //    d["adviserHomeTel"] = adviser.BrokerDetails.HomeTel ?? string.Empty;
        //    d["adviserCell"] = adviser.User.MobileNumber ?? string.Empty;
        //    d["adviserFax"] = adviser.BrokerDetails.Fax ?? string.Empty;
        //    d["adviserEmail"] = adviser.User.Email ?? string.Empty;

        //    //Financial Services

        //    d["LongTermSubA_A"] = adviser.AdviceLTSubCatA ? "X" : null;

        //    d["LongTermSubA_S"] = adviser.SupervisedLTSubCatA ? "X" : null;

        //    d["ShortTermPersonal_A"] = adviser.AdviceSTPersonal ? "X" : null;

        //    d["ShortTermPersonal_S"] = adviser.SupervisedSTPersonal ? "X" : null;

        //    d["LongTermSubB1_A"] = adviser.AdviceLTSubCatB1 ? "X" : null; ;

        //    d["LongTermSubB1_S"] = adviser.SupervisedLTSubCatB1 ? "X" : null;

        //    d["LongTermSubC_A"] = adviser.AdviceLTSubCatC ? "X" : null;

        //    d["LongTermSubC_S"] = adviser.SupervisedLTSubCatC ? "X" : null;

        //    d["RetailPension_A"] = adviser.AdviceRetailPension ? "X" : null;

        //    d["RetailPension_S"] = adviser.SupervisedRetailPension ? "X" : null;

        //    d["ShortTermCommercial_A"] = adviser.AdviceSTCommercial ? "X" : null;

        //    d["ShortTermCommercial_S"] = adviser.SupervisedSTCommercial ? "X" : null;

        //    d["PensionFundsBenefits_A"] = adviser.AdvicePensionFunds ? "X" : null;

        //    d["PensionFundsBenefits_S"] = adviser.SupervisedPensionFunds ? "X" : null;

        //    d["Shares_A"] = adviser.AdviceShares ? "X" : null;

        //    d["Shares_S"] = adviser.SupervisedShares ? "X" : null;

        //    d["MoneyMarket_A"] = adviser.AdviceMoneyMarket ? "X" : null;

        //    d["MoneyMarket_S"] = adviser.SupervisedMoneyMarket ? "X" : null;

        //    d["Debentures_A"] = adviser.AdviceDebentures ? "X" : null;

        //    d["Debentures_S"] = adviser.SupervisedDebentures ? "X" : null;

        //    d["Warrants_A"] = adviser.AdviceWarrants ? "X" : null;

        //    d["Warrants_S"] = adviser.SupervisedWarrants ? "X" : null;

        //    d["Bonds_A"] = adviser.AdviceBonds ? "X" : null;

        //    d["Bonds_S"] = adviser.SupervisedBonds ? "X" : null;

        //    d["DerivativeInstruments_A"] = adviser.AdviceDerivatives ? "X" : null;

        //    d["DerivativeInstruments_S"] = adviser.SupervisedDerivatives ? "X" : null;

        //    d["CollectiveInvestmentScheme_A"] = adviser.AdviceParticipatoryInterestCollective ? "X" : null;

        //    d["CollectiveInvestmentScheme_S"] = adviser.SupervisedParticipatoryInterestCollective ? "X" : null;

        //    //d["MedicalAid_A"] = adviser.AdviceMedicalAid ? "X" : null;

        //    //d["MedicalAid_S"] = adviser.SupervisedMedicalAid ? "X" : null;

        //    d["LongTermDeposits_A"] = adviser.AdviceLTDeposits ? "X" : null;

        //    d["LongTermDeposits_S"] = adviser.SupervisedLTDeposits ? "X" : null;

        //    d["ShortTermDeposits_A"] = adviser.AdviceSTDeposits ? "X" : null;

        //    d["ShortTermDeposits_S"] = adviser.SupervisedSTDeposits ? "X" : null;

        //    d["LongTermSubB2_A"] = adviser.AdviceLTSubCatB2 ? "X" : null;

        //    d["LongTermSubB2_S"] = adviser.SupervisedLTSubCatB2 ? "X" : null;

        //    d["LongTermSubB2A_A"] = adviser.AdviceLTSubCatB2A ? "X" : null;

        //    d["LongTermSubB2A_S"] = adviser.SupervisedLTSubCatB2A ? "X" : null;

        //    d["LongTermSubB1A_A"] = adviser.AdviceLTSubCatB1A ? "X" : null;

        //    d["LongTermSubB1A_S"] = adviser.SupervisedLTSubCatB1A ? "X" : null;

        //    d["ShortTermPersonalA1_A"] = adviser.AdviceSTPersonalA1 ? "X" : null;

        //    d["ShortTermPersonalA1_S"] = adviser.SupervisedSTPersonalA1 ? "X" : null;

        //    d["StructuredDeposits_A"] = adviser.AdviceStructuredDeposits ? "X" : null;

        //    d["StructuredDeposits_S"] = adviser.SupervisedStructuredDeposits ? "X" : null;

        //    d["Securities_A"] = adviser.AdviceSecurities ? "X" : null;

        //    d["Securities_S"] = adviser.SupervisedSecurities ? "X" : null;

        //    d["HedgeFunds_A"] = adviser.AdviceParticipatoryInterestHedge ? "X" : null;

        //    d["HedgeFunds_S"] = adviser.SupervisedParticipatoryInterestHedge ? "X" : null;

        //    //CPA Options
        //    if (disclosure.AlumaOffers)
        //        d["cpaDis1Y"] = "X";
        //    else
        //        d["cpaDis1N"] = "X";

        //    if (disclosure.OtherOffers)
        //        d["cpaDis2Y"] = "X";
        //    else
        //        d["cpaDis2N"] = "X";

        //    if (disclosure.ReputableOrg)
        //        d["cpaDis3Y"] = "X";
        //    else
        //        d["cpaDis3N"] = "X";

        //    if (disclosure.MethodOfCommunication != null)
        //        d["comm" + disclosure.MethodOfCommunication] = "X";

        //    if (disclosure.OtherMethodOfCommunication)
        //        d["cpaDis4Y"] = "X";
        //    else
        //        d["cpaDis4N"] = "X";

        //    //Service Level Agreement
        //    d["clientName"] = disclosure.ClientName + " " + disclosure.ClientSurname;
        //    d["clientID"] = disclosure.ClientIDNumber;
        //    d["clientCapacity"] = disclosure.ClientCapacity;

        //    d["authUsers"] = disclosure.AuthorisedUsers ?? string.Empty;

        //    d["date"] = DateTime.Now.ToString("yyyy/MM/dd");

        //    //Broker Appointment
        //    if (disclosure.AdvisorAuthority != null)
        //    {
        //        if (!disclosure.AdvisorAuthority)
        //            d["authorityAll"] = "X";
        //        else
        //        {
        //            d["authoritySome"] = "X";
        //            d["authorityProducts"] = disclosure.AdvisorAuthorityProducts;
        //        }

        //        d["date2"] = DateTime.Now.ToString("yyyy/MM/dd");
        //    }

        //    disclosure.DocumentData = appRepo.PopulateDocument("DisclosureLetter.pdf", d);
        //    disclosure.DocumentType = DocumentTypesEnum.DisclosureLetter;
        //    disclosure.B64Prefix = "data:application/pdf;base64";
        //    disclosure.Name = "Disclosure Letter: " + disclosure.ClientName + " " + disclosure.ClientSurname;
        //    disclosure.DocCreated = true;

        //    _context.Disclosures.Update(disclosure);
        //    _context.SaveChanges();
        //}
        //private void SignDisclosure(DisclosureModel disclosures)
        //{
        //    SignatureRepo _signiflow = new SignatureRepo();
        //    var advisor = _context.Advisors
        //        .Where(c => c.Id == disclosures.AdvisorID)
        //        .Include(c => c.BrokerDetails)
        //        .Include(c => c.User)
        //        .First();

        //    var pageList = new List<int> { 1, 2, 3 };

        //    var signerList = new List<SignerListItemDto>();

        //    pageList.ForEach(p => signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //    {
        //        Signature = Convert.ToBase64String(disclosures.ClientSignature),
        //        FirstName = disclosures.ClientName,
        //        LastName = disclosures.ClientSurname,
        //        Email = disclosures.ClientEmail,
        //        IdNo = disclosures.ClientIDNumber,
        //        Mobile = disclosures.ClientMobile,
        //        XField = 450,
        //        YField = 800,
        //        HField = 20,
        //        WField = 60,
        //        Page = p
        //    })));

        //    signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //    {
        //        Signature = Convert.ToBase64String(disclosures.ClientSignature),
        //        FirstName = disclosures.ClientName,
        //        LastName = disclosures.ClientSurname,
        //        Email = disclosures.ClientEmail,
        //        IdNo = disclosures.ClientIDNumber,
        //        Mobile = disclosures.ClientMobile,
        //        XField = 40,
        //        YField = 400,
        //        HField = 30,
        //        WField = 120,
        //        Page = 4,
        //    }));

        //    if (disclosures.BrokerAppointment)
        //    {
        //        //adviser
        //        signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //        {
        //            Signature = Convert.ToBase64String(advisor.User.Signature),
        //            FirstName = advisor.User.FirstName,
        //            LastName = advisor.User.LastName,
        //            Email = advisor.User.Email,
        //            IdNo = advisor.User.IdNumber,
        //            Mobile = advisor.User.MobileNumber,
        //            XField = 40,
        //            YField = 750,
        //            HField = 30,
        //            WField = 120,
        //            Page = 4,
        //        }));
        //        //client
        //        signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //        {
        //            Signature = Convert.ToBase64String(disclosures.ClientSignature),
        //            FirstName = disclosures.ClientName,
        //            LastName = disclosures.ClientSurname,
        //            Email = disclosures.ClientEmail,
        //            IdNo = disclosures.ClientIDNumber,
        //            Mobile = disclosures.ClientMobile,
        //            XField = 270,
        //            YField = 750,
        //            HField = 30,
        //            WField = 120,
        //            Page = 4,
        //        }));
        //    }

        //    var ceremony = _signiflow.CreateMultipleSignersCeremony(disclosures.DocumentData,
        //        disclosures.Name, signerList);

        //    disclosures.DocumentData = Convert.FromBase64String(
        //        _signiflow.RunMultiSignerCeremony(ceremony));

        //    _context.Disclosures.Update(disclosures);
        //    _context.SaveChanges();
        //    //SendDisclosureLetter(clientUser, advisor, application);
        //}
    }
}