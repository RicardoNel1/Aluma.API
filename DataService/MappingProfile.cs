using AutoMapper;
using DataService.Dto;
using DataService.Model;

namespace DataService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Advisor
            CreateMap<AdvisorModel, AdvisorDto>()
              .ReverseMap();

            // Applications

            CreateMap<ApplicationDocumentModel, ApplicationDocumentDto>()
             .ReverseMap();

            CreateMap<ApplicationDocumentModel, UserDocumentModel>()
             .ReverseMap();

            CreateMap<ApplicationModel, ApplicationDto>()
                .ReverseMap();

            //CreateMap<DividendTaxModel, DividendTaxDto>()
            //    .ReverseMap();

            CreateMap<FSPMandateModel, FSPMandateDto>()
                .ReverseMap();

            CreateMap<IRSW8Model, IRSW8Dto>()
               .ReverseMap();

            CreateMap<IRSW9Model, IRSW9Dto>()
               .ReverseMap();

            CreateMap<PurposeAndFundingModel, PurposeAndFundingDto>()
                .ReverseMap();

            CreateMap<RecordOfAdviceModel, RecordOfAdviceDto>()
               .ReverseMap();

            CreateMap<RecordOfAdviceItemsModel, RecordOfAdviceItemsDto>()
                .ReverseMap();

            CreateMap<USPersonsModel, USPersonsDto>()
                .ReverseMap();

            //Client

            CreateMap<BankDetailsModel, BankDetailsDto>()
                .ReverseMap();

            CreateMap<LeadModel, LeadDto>().ReverseMap(); 

            CreateMap<ClientModel, ClientDto>()
               .ReverseMap();

            //CreateMap<ClientModel, RegistrationDto>()
            //    .ReverseMap();

            CreateMap<KYCDataModel, KycResultsDto>()
                .ReverseMap();

            CreateMap<PassportModel, PassportDto>()
                .ReverseMap();

            CreateMap<RiskProfileModel, RiskProfileDto>()
                .ReverseMap();

            CreateMap<TaxResidencyModel, TaxResidencyDto>()
                .ReverseMap();

            CreateMap<ForeignTaxResidencyModel, ForeignTaxResidencyDto>()
                .ReverseMap();

            CreateMap<ConsumerProtectionModel, ConsumerProtectionDto>()
                .ReverseMap();


            //Product
            CreateMap<ProductModel, ProductDto>()
                .ReverseMap();

            //FNA
            CreateMap<ClientFNADto, ClientFNAModel>()
                .ReverseMap();

            CreateMap<PrimaryResidenceModel, PrimaryResidenceDto>()
                .ReverseMap();

            CreateMap<EstateExpensesModel, EstateExpensesDto>()
                .ReverseMap();

            CreateMap<AccrualModel, AccrualDto>()
                .ReverseMap();

            CreateMap<AssumptionsModel, AssumptionsDto>()
                .ReverseMap();


            CreateMap<RetirementPlanningModel, RetirementPlanningDto>()
                .ReverseMap();

            CreateMap<ProvidingOnDeathModel, ProvidingOnDeathDto>()
                .ReverseMap();

            CreateMap<EstateDutyModel, EstateDutyDto>()
                .ReverseMap();

            CreateMap<AdministrationCostsModel, AdministrationCostsDto>()
                .ReverseMap();

            CreateMap<CapitalGainsTaxModel, CapitalGainsTaxDto>()
                .ReverseMap();

            CreateMap<TaxLumpsumModel, TaxLumpsumDto>()
                .ReverseMap();

            //Shared
            CreateMap<DisclosureModel, DisclosureDto>()
                .ReverseMap();

            //Users

            CreateMap<AddressModel, AddressDto>()
                .ReverseMap();

            //CreateMap<OtpModel, OtpDto>()
            //    .ReverseMap();

            CreateMap<UserModel, UserDto>()
                .ReverseMap();

            CreateMap<UserModel, RegistrationDto>()
                .ReverseMap();

            CreateMap<UserDocumentModel, UserDocumentDto>()
                .ReverseMap();
        }
    }

    public class StringToByte : IValueConverter<string, byte[]>
    {
        public byte[] Convert(string sourceMember, ResolutionContext context) =>
            System.Convert.FromBase64String(sourceMember);
    }

    public class ByteToString : IValueConverter<byte[], string>
    {
        public string Convert(byte[] sourceMember, ResolutionContext context) =>
            System.Convert.ToBase64String(sourceMember);
    }
}