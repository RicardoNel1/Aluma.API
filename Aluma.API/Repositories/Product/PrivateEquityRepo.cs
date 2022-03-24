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
    public interface IPrivateEquityRepo : IRepoBase<ProductModel>
    {
        void GenerateDOA(ClientModel user, AdvisorModel advisor, RecordOfAdviceItemsModel roa);
    }

    public class PrivateEquityRepo : RepoBase<ProductModel>, IPrivateEquityRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        DocumentHelper dh = new DocumentHelper();
        public PrivateEquityRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
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

            byte[] doc = dh.PopulateDocument(docName, d, _host);

            UserDocumentModel udm = new UserDocumentModel()
            {
                DocumentType = DocumentTypesEnum.FSPMandate,
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