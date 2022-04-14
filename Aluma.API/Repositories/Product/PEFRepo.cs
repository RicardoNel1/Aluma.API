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


            Enum.TryParse(product.ProductId.ToString(), out ProductsEnum parsedProduct);
            //check for pe fund product
            if (parsedProduct == ProductsEnum.PE1 || parsedProduct == ProductsEnum.PE2)
            {
                d[$"committedCapital"] = product.AcceptedLumpSum.ToString();
            }

            if (parsedProduct == ProductsEnum.PE2)
            {
                d[$"zarCapital"] = product.AcceptedLumpSum.ToString();
            }


            d["taxpayer_True"] = "x";
            d["taxNo"] = client.TaxResidency.TaxNumber ?? " ";

            d["nameSurname"] = $"{client.User.FirstName} {client.User.LastName}";

            d["mobile"] = "0" + client.User.MobileNumber;
            d["email"] = client.User.Email;

            BankDetailsModel bv = client.BankDetails.First();
            d["bank"] = bv.BankName;
            d["accountHolder"] = $"{bv.Initials} {bv.Surname}";
            d["accountNo"] = bv.AccountNumber;


            d["idNo"] = client.User.RSAIdNumber;

            // signature
            d["onBehalfOf"] = "self";
            d["signDate_1"] = DateTime.Today.ToString("yyyyMMdd");
            d["signAt_1"] = signCity;
            d["nameSurname_2"] = "";
            d["signDate_2"] = "";
            d["signAt_2"] = "";

            d["nameSurname_3"] = $"{advisor.User.FirstName} {advisor.User.LastName}";  //Aluma signatory
            d["signDate_3"] = DateTime.Today.ToString("yyyyMMdd");
            d["signAt_3"] = advisor.User.Address.First().City;

            RecordOfAdviceModel roa = _context.RecordOfAdvice.SingleOrDefault(r => r.Id == product.RecordOfAdviceId);
            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);


            DocumentTypesEnum type = parsedProduct == ProductsEnum.PE1 ? DocumentTypesEnum.PEFDOA : DocumentTypesEnum.PEF2DOA;
            await _dh.PopulateAndSaveDocument(type, d, client.User, app);
        }


        public async Task GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;


            Enum.TryParse(product.ProductId.ToString(), out ProductsEnum parsedProduct);

            if (parsedProduct == ProductsEnum.PE1)
            {
                //Calculations
                double i = product.AcceptedLumpSum;
                double r1 = .129;
                double r2 = .1895;
                double dividendYear1Gross1 = i * r1;
                double dividendYear2Gross1 = (i + dividendYear1Gross1) * r1;
                double dividendYear3Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1) * r1;
                double dividendYear4Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1) * r1;
                double dividendYear5Gross1 = (i + dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1 + dividendYear4Gross1) * r1;
                double totalCumulativeDividendGross1 = dividendYear1Gross1 + dividendYear2Gross1 + dividendYear3Gross1 + dividendYear4Gross1 + dividendYear5Gross1;
                double totalPayoutMaturityGross1 = totalCumulativeDividendGross1 + i;

                double dividendYear1Gross2 = i * r2;
                double dividendYear2Gross2 = (i + dividendYear1Gross2) * r2;
                double dividendYear3Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2) * r2;
                double dividendYear4Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2) * r2;
                double dividendYear5Gross2 = (i + dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2 + dividendYear4Gross2) * r2;
                double totalCumulativeDividendGross2 = dividendYear1Gross2 + dividendYear2Gross2 + dividendYear3Gross2 + dividendYear4Gross2 + dividendYear5Gross2;
                double totalPayoutMaturityGross2 = totalCumulativeDividendGross2 + i;


                d[$"initialInvestment"] = "R " + i.ToString("F");
                d[$"1dividendYear1Gross"] = "R " + dividendYear1Gross1.ToString("F");
                d[$"1dividendYear2Gross"] = "R " + dividendYear2Gross1.ToString("F");
                d[$"1dividendYear3Gross"] = "R " + dividendYear3Gross1.ToString("F");
                d[$"1dividendYear4Gross"] = "R " + dividendYear4Gross1.ToString("F");
                d[$"1dividendYear5Gross"] = "R " + dividendYear5Gross1.ToString("F");
                d[$"1totalCumulativeDividendGross"] = "R " + totalCumulativeDividendGross1.ToString("F");
                d[$"1totalPayoutMaturityGross"] = "R " + totalPayoutMaturityGross1.ToString("F");

                d[$"2dividendYear1Gross"] = "R " + dividendYear1Gross2.ToString("F");
                d[$"2dividendYear2Gross"] = "R " + dividendYear2Gross2.ToString("F");
                d[$"2dividendYear3Gross"] = "R " + dividendYear3Gross2.ToString("F");
                d[$"2dividendYear4Gross"] = "R " + dividendYear4Gross2.ToString("F");
                d[$"2dividendYear5Gross"] = "R " + dividendYear5Gross2.ToString("F");
                d[$"2totalCumulativeDividendGross"] = "R " + totalCumulativeDividendGross2.ToString("F");
                d[$"2totalPayoutMaturityGross"] = "R " + totalPayoutMaturityGross2.ToString("F");
            }

            if (parsedProduct == ProductsEnum.PE2)
            {
                //Calculations                
                double i = product.AcceptedLumpSum;
                double r = .129;
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


                d[$"initialInvestment"] = "R " + i.ToString("F");

                d[$"monthlyDividendGross"] = "R " + monthlyDividendGross.ToString("F");
                d[$"monthlyDividendNett"] = "R " + monthlyDividendNett.ToString("F");
                d[$"monthlyDividendAnnualGross"] = "R " + monthlyDividendAnnualGross.ToString("F");
                d[$"monthlyDividendAnnualNett"] = "R " + monthlyDividendAnnualNett.ToString("F");
                d[$"annualBonusGross"] = "R " + annualBonusGross.ToString("F");
                d[$"annualBonusNet"] = "R " + annualBonusNet.ToString("F");
                d[$"dividendPayoutGross"] = "R " + dividendPayoutGross.ToString("F");
                d[$"dividendPayoutNett"] = "R " + dividendPayoutNett.ToString("F");

            }

            d["nameSurname"] = $"{client.User.FirstName} {client.User.LastName}";
            d["identityNumber"] = client.User.RSAIdNumber;
            d["contactNumber"] = "0" + client.User.MobileNumber;
            d["emailAddress"] = client.User.Email;

            d["quotationDate"] = DateTime.Now.ToString("dd MMMM yyyy");
            d["signedDate"] = DateTime.Now.ToString("ddMMyyyy");
            d["commencementDate"] = DateTime.Now.ToString("dd MMMM yyyy"); //TODO today's date?

            d["consultant"] = $"{advisor.User.FirstName} {advisor.User.LastName}"; //TODO breaks
            //quoteNumber
            //quotationVersion

            int expiryDate = DateTime.UtcNow.Day + 1827;
            d["expiryDate"] = expiryDate.ToString("dd MMMM yyyy");

            RecordOfAdviceModel roa = _context.RecordOfAdvice.SingleOrDefault(r => r.Id == product.RecordOfAdviceId);
            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == roa.ApplicationId);

            DocumentTypesEnum type = parsedProduct == ProductsEnum.PE1 ? DocumentTypesEnum.PEFQuote : DocumentTypesEnum.PEF2Quote;
            await _dh.PopulateAndSaveDocument(type, d, client.User, app);
        }

    }
}