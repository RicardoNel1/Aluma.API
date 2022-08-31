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
    public interface IPEFRepo : IRepoBase<ProductModel>
    {
        Task GenerateDOA(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);
        //void GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa);
        //void PEQuoteCalc(RecordOfAdviceItemsModel product);

        Task GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);


    }


    public class PEFRepo : RepoBase<ProductModel>, IPEFRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        DocumentHelper _dh;
        public PEFRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
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


            if (product.ProductId == 6)
            {
                d[$"zarCapital"] = product.AcceptedLumpSum.ToString();
            }


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


            DocumentTypesEnum type = product.ProductId == 5 ? DocumentTypesEnum.PEFDOA : DocumentTypesEnum.PEF2DOA;
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

            d["quotationDate"] = DateTime.UtcNow.ToString("dd MMMM yyyy");
            d["signedDate"] = DateTime.UtcNow.ToString("ddMMyyyy");
            d["commencementDate"] = DateTime.UtcNow.ToString("dd MMMM yyyy");
            d["expiryDate"] = DateTime.UtcNow.AddYears(5).AddDays(-1).ToString("dd MMMM yyyy");


            d["consultant"] = $"{advisor.User.FirstName} {advisor.User.LastName}"; 

            if (product.ProductId == 5)
            {
                //Calculations
                double i = product.AcceptedLumpSum;
                double r1 = .132;
                double r2 = .191;
                double dt = .2;

                double dividendYear1Gross1 = i * r1;
                double dividendYear1Nett1 = dividendYear1Gross1 - (dividendYear1Gross1*dt);

                double dividendYear2Gross1 = (i + dividendYear1Gross1) * r1;
                double dividendYear2Nett1 = dividendYear2Gross1 - (dividendYear2Gross1 * dt);

                double dividendYear3Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1) * r1;
                double dividendYear3Nett1 = dividendYear3Gross1 - (dividendYear3Gross1 * dt);

                double dividendYear4Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1) * r1;
                double dividendYear4Nett1 = dividendYear4Gross1 - (dividendYear4Gross1 * dt);

                double dividendYear5Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1 + dividendYear4Gross1) * r1;
                double dividendYear5Nett1 = dividendYear5Gross1 - (dividendYear5Gross1 * dt);

                double totalCumulativeDividendGross1 = dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1 + dividendYear4Gross1 + dividendYear5Gross1;
                double totalCumulativeDividendNett1 = dividendYear1Nett1 + dividendYear2Nett1 + dividendYear3Nett1 + dividendYear4Nett1 + dividendYear5Nett1;

                double totalPayoutMaturityGross1 = totalCumulativeDividendGross1 + i;
                double totalPayoutMaturityNett1 = totalCumulativeDividendNett1 + i;

                double dividendYear1Gross2 = i * r2;
                double dividendYear1Nett2 = dividendYear1Gross2 - (dividendYear1Gross2 * dt);

                double dividendYear2Gross2 = (i + dividendYear1Gross2) * r2;
                double dividendYear2Nett2 = dividendYear2Gross2 - (dividendYear2Gross2 * dt);

                double dividendYear3Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2) * r2;
                double dividendYear3Nett2 = dividendYear3Gross2 - (dividendYear3Gross2 * dt);

                double dividendYear4Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2) * r2;
                double dividendYear4Nett2 = dividendYear4Gross2 - (dividendYear4Gross2 * dt);

                double dividendYear5Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2 + dividendYear4Gross2) * r2;
                double dividendYear5Nett2 = dividendYear5Gross2 - (dividendYear5Gross2 * dt);

                double totalCumulativeDividendGross2 = dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2 + dividendYear4Gross2 + dividendYear5Gross2;
                double totalCumulativeDividendNett2 = dividendYear1Nett2 + dividendYear2Nett2 + dividendYear3Nett2 + dividendYear4Nett2 + dividendYear5Nett2;

                double totalPayoutMaturityGross2 = totalCumulativeDividendGross2 + i;
                double totalPayoutMaturityNett2 = totalCumulativeDividendNett2 + i;


                d[$"initialInvestment"] = "R " + i.ToString("F");
                d[$"1dividendYear1Gross"] = "R " + dividendYear1Gross1.ToString("N");
                d[$"1dividendYear2Gross"] = "R " + dividendYear2Gross1.ToString("N");
                d[$"1dividendYear3Gross"] = "R " + dividendYear3Gross1.ToString("N");
                d[$"1dividendYear4Gross"] = "R " + dividendYear4Gross1.ToString("N");
                d[$"1dividendYear5Gross"] = "R " + dividendYear5Gross1.ToString("N");
                d[$"1totalCumulativeDividendGross"] = "R " + totalCumulativeDividendGross1.ToString("N");
                d[$"1totalPayoutMaturityGross"] = "R " + totalPayoutMaturityGross1.ToString("N");

                d[$"2dividendYear1Gross"] = "R " + dividendYear1Gross2.ToString("N");
                d[$"2dividendYear2Gross"] = "R " + dividendYear2Gross2.ToString("N");
                d[$"2dividendYear3Gross"] = "R " + dividendYear3Gross2.ToString("N");
                d[$"2dividendYear4Gross"] = "R " + dividendYear4Gross2.ToString("N");
                d[$"2dividendYear5Gross"] = "R " + dividendYear5Gross2.ToString("N");
                d[$"2totalCumulativeDividendGross"] = "R " + totalCumulativeDividendGross2.ToString("N");
                d[$"2totalPayoutMaturityGross"] = "R " + totalPayoutMaturityGross2.ToString("N");



                d[$"1dividendYear1Nett"] = "R " + dividendYear1Nett1.ToString("N");
                d[$"1dividendYear2Nett"] = "R " + dividendYear2Nett1.ToString("N");
                d[$"1dividendYear3Nett"] = "R " + dividendYear3Nett1.ToString("N");
                d[$"1dividendYear4Nett"] = "R " + dividendYear4Nett1.ToString("N");
                d[$"1dividendYear5Nett"] = "R " + dividendYear5Nett1.ToString("N");
                d[$"1totalCumulativeDividendNett"] = "R " + totalCumulativeDividendNett1.ToString("N");
                d[$"1totalPayoutMaturityNett"] = "R " + totalPayoutMaturityNett1.ToString("N");

                d[$"2dividendYear1Nett"] = "R " + dividendYear1Nett2.ToString("N");
                d[$"2dividendYear2Nett"] = "R " + dividendYear2Nett2.ToString("N");
                d[$"2dividendYear3Nett"] = "R " + dividendYear3Nett2.ToString("N");
                d[$"2dividendYear4Nett"] = "R " + dividendYear4Nett2.ToString("N");
                d[$"2dividendYear5Nett"] = "R " + dividendYear5Nett2.ToString("N");
                d[$"2totalCumulativeDividendNett"] = "R " + totalCumulativeDividendNett2.ToString("N");
                d[$"2totalPayoutMaturityNett"] = "R " + totalPayoutMaturityNett2.ToString("N");



            }

            if (product.ProductId == 6)
            {
                //Calculations                
                double i = product.AcceptedLumpSum;
                double r = .132;
                double ab = .025;
                double dt = .2;
                double t = 5;

                double monthlyDividendGross = i * (r / 12);
                double monthlyDividendNett = monthlyDividendGross - (monthlyDividendGross * dt);
                double monthlyDividendAnnualGross = i * r;
                double monthlyDividendAnnualNett = monthlyDividendAnnualGross - (monthlyDividendAnnualGross * dt);
                double annualBonusGross = i * ab;
                double annualBonusNet = annualBonusGross - (annualBonusGross * dt);
                double dividendPayoutGross = (monthlyDividendAnnualGross * t) + (annualBonusGross * t);
                double dividendPayoutNett = dividendPayoutGross - (dividendPayoutGross * dt);


                d[$"initialInvestment"] = "R " + i.ToString("N");

                d[$"monthlyDividendGross"] = "R " + monthlyDividendGross.ToString("N");
                d[$"monthlyDividendNett"] = "R " + monthlyDividendNett.ToString("N");
                d[$"monthlyDividendAnnualGross"] = "R " + monthlyDividendAnnualGross.ToString("N");
                d[$"monthlyDividendAnnualNett"] = "R " + monthlyDividendAnnualNett.ToString("N");
                d[$"annualBonusGross"] = "R " + annualBonusGross.ToString("N");
                d[$"annualBonusNet"] = "R " + annualBonusNet.ToString("N");
                d[$"dividendPayoutGross"] = "R " + dividendPayoutGross.ToString("N");
                d[$"dividendPayoutNett"] = "R " + dividendPayoutNett.ToString("N");

            }
            //quoteNumber
            //quotationVersion

            int expiryDate = DateTime.UtcNow.Day + 1827;
            d["expiryDate"] = expiryDate.ToString("dd MMMM yyyy");

            RecordOfAdviceModel roa = _context.RecordOfAdvice.SingleOrDefault(r => r.Id == product.RecordOfAdviceId);
            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);

            DocumentTypesEnum type = product.ProductId == 5 ? DocumentTypesEnum.PEFQuote : DocumentTypesEnum.PEF2Quote;
            await _dh.PopulateAndSaveDocument(type, d, client.User, app);
        }

    }
}