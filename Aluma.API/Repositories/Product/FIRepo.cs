using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
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
    public interface IFIRepo : IRepoBase<ProductModel>
    {
        Task GenerateDOA(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);

        Task GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);

        Task GenerateRecordOfAdvice(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa);
    }


    public class FIRepo : RepoBase<ProductModel>, IFIRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        DocumentHelper _dh;
        public FIRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _dh = new DocumentHelper(_context, _config, _fileStorage, _host);
        }

        public async Task GenerateDOA(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;

            //change when incorporating entities
            d["individual"] = "x";

            //check for pe fund product
            d[$"committedCapital"] = product.AcceptedLumpSum.ToString();

            if (client.User.Address.Count > 0)
            {
                AddressModel item = client.User.Address.First(a => a.Type == AddressTypesEnum.Residential);
                signCity = item.City;
                string street = $"{item.StreetNumber} {item.StreetName}";
                string unitComplex = $"{item.UnitNumber} {item.ComplexName}";
                d[$"address"] = unitComplex != " " ? $"{street}, {unitComplex}, {item.Suburb}, { item.City}" : $"{street}, {item.Suburb}, { item.City}";

            }
            d["country"] = client.CountryOfResidence;

            d["taxpayer_True"] = "x";
            d["taxNo"] = client.TaxResidency.TaxNumber ?? " ";

            d["nameSurname"] = $"{client.User.FirstName} {client.User.LastName}";

            d["mobile"] = "0" + client.User.MobileNumber;
            d["email"] = client.User.Email;

            BankDetailsModel bv = client.BankDetails.First();
            d["bank"] = bv.BankName;
            d["accountHolder"] = bv.Surname == " " ? $"{bv.Initials} {bv.Surname}" : $"{client.User.FirstName} {client.User.LastName}";
            d["accountNo"] = bv.AccountNumber;


            d["idNo"] = client.User.RSAIdNumber;

            // signature
            d["onBehalfOf"] = "Self";
            d["signAt_1"] = signCity;
            d["signDate_1"] = DateTime.Today.ToString("yyyyMMdd");

            d["nameSurname_2"] = "";
            d["signAt_2"] = "";
            d["signDate_2"] = "";



            d["nameSurname_3"] = $"{advisor.User.FirstName} {advisor.User.LastName}";  //Advisor 

            RecordOfAdviceModel roa = _context.RecordOfAdvice.SingleOrDefault(r => r.Id == product.RecordOfAdviceId);
            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);


            DocumentTypesEnum type = DocumentTypesEnum.FIDOA;
            await _dh.PopulateAndSaveDocument(type, d, client.User, app);
        }


        public async Task GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;

            d["nameSurname"] = $"{client.User.FirstName} {client.User.LastName}";
            d["identityNumber"] = client.User.RSAIdNumber;
            d["contactNumber"] = "0" + client.User.MobileNumber;
            d["emailAddress"] = client.User.Email;

            d["quoteDate"] = DateTime.UtcNow.ToString("dd MMMM yyyy");
            d["signedDate"] = DateTime.UtcNow.ToString("ddMMyyyy");
            d["date"] = DateTime.UtcNow.ToString("dd MMMM yyyy");
            d["dateExtended"] = DateTime.UtcNow.AddYears(5).AddDays(-1).ToString("dd MMMM yyyy");


            d["consultant"] = $"{advisor.User.FirstName} {advisor.User.LastName}";

            if (product.ProductId == 7)
            {
                //Calculations                
                double i = product.AcceptedLumpSum;
                //double r0 = .1133;
                double r50 = .11;//0.0977;
                double r75 = .105;//0.0898;
                double r100 = .1;// .082;

                double ab = .025;
                double dt = .2;
                double t = 5;

                //double monthlyDividendGross0 = i * (r0 / 12);
                double monthlyDividendGross50 = i * (r50 / 12);
                double monthlyDividendGross75 = i * (r75 / 12);
                double monthlyDividendGross100 = i * (r100 / 12);


                d[$"initialInvestment"] = "R " + i.ToString("N");

                //d[$"zerocp"] = "R " + monthlyDividendGross0.ToString("F");
                d[$"fiftycp"] = "R " + monthlyDividendGross50.ToString("N");
                d[$"seventyfivecp"] = "R " + monthlyDividendGross75.ToString("N");
                d[$"hundredcp"] = "R " + monthlyDividendGross100.ToString("N");

            }
            //quoteNumber
            //quotationVersion

            int expiryDate = DateTime.UtcNow.Day + 1827;
            d["expiryDate"] = expiryDate.ToString("dd MMMM yyyy");

            RecordOfAdviceModel roa = _context.RecordOfAdvice.SingleOrDefault(r => r.Id == product.RecordOfAdviceId);
            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);

            DocumentTypesEnum type = DocumentTypesEnum.FIQuote;
            await _dh.PopulateAndSaveDocument(type, d, client.User, app);
        }

        public async Task GenerateRecordOfAdvice(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa)
        {
            var data = new Dictionary<string, string>();
            var date = DateTime.Today.ToString("yyyy/MM/dd");
            var nameSurname = $"{client.User.FirstName} {client.User.LastName}";

            data["representativeCapacity"] = "not applicable";
            data["clientSignedAt"] = "Pretoria"; 
            data["date"] = date; 
            data["nameSurname"] = nameSurname; 
            data["idNo"] = client.User.RSAIdNumber; 
            data["advisorName"] = $"{advisor.User.FirstName} {advisor.User.LastName}"; 
            data["introduction"] = roa.Introduction; 
            data["materialInformation"] = roa.MaterialInformation; 

            DocumentHelper dh = new(_context, _config, _fileStorage, _host);

            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);

            DocumentTypesEnum type = DocumentTypesEnum.FIROA;
            await dh.PopulateAndSaveDocument(type, data, client.User, app);
        }


    }
}