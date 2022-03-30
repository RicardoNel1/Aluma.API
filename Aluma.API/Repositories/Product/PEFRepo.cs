using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IPEFRepo : IRepoBase<ProductModel>
    {
        void GenerateDOA(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);
        //void GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa);
        //void PEQuoteCalc(RecordOfAdviceItemsModel product);

        void GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product);


    }


    public class PEFRepo : RepoBase<ProductModel>, IPEFRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        DocumentHelper dh = new DocumentHelper();
        public PEFRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public void GenerateDOA(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;
            DocumentTypesEnum docType;
            string docName = string.Empty;
            string fileName = string.Empty;

            //change when incorporating entities
            d["individual"] = "x";


            Enum.TryParse(product.ProductId.ToString(), out ProductsEnum parsedProduct);
            //check for pe fund product
            if (parsedProduct == ProductsEnum.PE1 || parsedProduct == ProductsEnum.PE2)
            {
                docName = "DOA.pdf";
                fileName = $"Aluma Capital - Private Equity Fund Growth - Deed of Adherence - {client.User.FirstName + " " + client.User.LastName}.pdf";
                d[$"committedCapital"] = product.AcceptedLumpSum.ToString();
            }

            if (parsedProduct == ProductsEnum.PE2)
            {
                docName = "DOA2.pdf";
                fileName = $"Aluma Capital - Private Equity Fund Income - Deed of Adherence - {client.User.FirstName + " " + client.User.LastName}.pdf";
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

            docType = docName == "DOA.pdf" ? DocumentTypesEnum.PEFDOA : DocumentTypesEnum.PEF2Quote;

            byte[] doc = dh.PopulateDocument(docName, d, _host);

            UserDocumentModel udm = new UserDocumentModel()
            {
                DocumentType = docType,
                FileType = FileTypesEnum.Pdf,
                Name = fileName,
                URL = "data:application/pdf;base64," + Convert.ToBase64String(doc, 0, doc.Length),
                UserId = client.User.Id
            };

            _context.UserDocuments.Add(udm);
            _context.SaveChanges();
        }
                

        public void GenerateQuote(ClientModel client, AdvisorModel advisor, RecordOfAdviceItemsModel product)
        {
            var d = new Dictionary<string, string>();
            string signCity = string.Empty;
            DocumentTypesEnum docType;
            string docName = string.Empty;
            string fileName = string.Empty;

            //d[$"committedCapital"] = product.AcceptedLumpSum.ToString();

            Enum.TryParse(product.ProductId.ToString(), out ProductsEnum parsedProduct);
            
            if (parsedProduct == ProductsEnum.PE1)
            {               
                docName = "PEFQuote.pdf";
                fileName = $"Aluma Capital - Private Equity Fund Growth - Quote - {client.User.FirstName + " " + client.User.LastName}.pdf";
                
            }

            if (parsedProduct == ProductsEnum.PE2)
            {                
                docName = "PEF2Quote.pdf";
                fileName = $"Aluma Capital - Private Equity Fund Income - Quote - {client.User.FirstName + " " + client.User.LastName}.pdf";                
            }

            d["nameSurname"] = $"{client.User.FirstName} {client.User.LastName}";
            d["identityNumber"] = client.User.RSAIdNumber;
            d["contactNumber"] = "0" + client.User.MobileNumber;
            d["emailAddress"] = client.User.Email;



            //Calculations
            double i = product.AcceptedLumpSum;
            double r1 = (12.9 / 100);
            double r2 = (20 / 100);
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


            d[$"initialInvestment"] = i.ToString();
            d[$"1dividendYear1Gross"] = dividendYear1Gross1.ToString();
            d[$"1dividendYear2Gross"] = dividendYear2Gross1.ToString();
            d[$"1dividendYear3Gross"] = dividendYear3Gross1.ToString();
            d[$"1dividendYear4Gross"] = dividendYear4Gross1.ToString();
            d[$"1dividendYear5Gross"] = dividendYear5Gross1.ToString();
            d[$"1totalCumulativeDividendGross"] = totalCumulativeDividendGross1.ToString();
            d[$"1totalPayoutMaturityGross"] = totalPayoutMaturityGross1.ToString();

            d[$"2dividendYear1Gross"] = dividendYear1Gross2.ToString();
            d[$"2dividendYear2Gross"] = dividendYear2Gross2.ToString();
            d[$"2dividendYear3Gross"] = dividendYear3Gross2.ToString();
            d[$"2dividendYear4Gross"] = dividendYear4Gross2.ToString();
            d[$"2dividendYear5Gross"] = dividendYear5Gross2.ToString();
            d[$"2totalCumulativeDividendGross"] = totalCumulativeDividendGross2.ToString();
            d[$"2totalPayoutMaturityGross"] = totalPayoutMaturityGross2.ToString();


            docType = docName == "PEFQuote.pdf" ? DocumentTypesEnum.PEFQuote : DocumentTypesEnum.PEF2Quote;


            byte[] doc = dh.PopulateDocument(docName, d, _host);

            

            UserDocumentModel udm = new UserDocumentModel()
            {
                DocumentType = docType,
                FileType = FileTypesEnum.Pdf,
                Name = fileName,
                URL = "data:application/pdf;base64," + Convert.ToBase64String(doc, 0, doc.Length),
                UserId = client.User.Id
            };

            _context.UserDocuments.Add(udm);
            _context.SaveChanges();
        }

    }
}