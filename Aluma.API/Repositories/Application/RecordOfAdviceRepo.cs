﻿using Aluma.API.Helpers;
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

namespace Aluma.API.Repositories
{
    public interface IRecordOfAdviceRepo : IRepoBase<RecordOfAdviceModel>
    {
        RecordOfAdviceDto GetRecordOfAdvice(int applicationId);

        bool DoesApplicationHaveRecordOfAdice(int applicationId);

        RecordOfAdviceDto CreateRecordOfAdvice(RecordOfAdviceDto dto);

        RecordOfAdviceDto UpdateRecordOfAdvice(RecordOfAdviceDto dto);

        bool DeleteRecordOfAdvice(RecordOfAdviceDto dto);

        void GenerateRecordOfAdvice(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa, RiskProfileModel risk);
    }

    public class RecordOfAdviceRepo : RepoBase<RecordOfAdviceModel>, IRecordOfAdviceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;


        public RecordOfAdviceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _host = host;
            _config = config;
            _mapper = mapper;
            _context = databaseContext;
            _fileStorage = fileStorage;
        }

        public bool DoesApplicationHaveRecordOfAdice(int applicationId)
        {
            var roa = _context.RecordOfAdvice.Where(r => r.ApplicationId == applicationId);
            if (roa.Any())
            {
                return true;
            }
            return false;
        }

        public RecordOfAdviceDto GetRecordOfAdvice(int applicationId)
        {
            var roa = _context.RecordOfAdvice.Where(r => r.ApplicationId == applicationId);

            if (roa.Any())
            {
                RecordOfAdviceDto result = _mapper.Map<RecordOfAdviceDto>(roa.Include(r => r.SelectedProducts).First());

                foreach (var product in result.SelectedProducts)
                {                    
                    product.ProductName = _context.Products.First(p => p.Id == (int)product.ProductId).Name;
                }

                return result;
            }
            return null;
        }

        public RecordOfAdviceDto CreateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            RecordOfAdviceModel newRoa = _mapper.Map<RecordOfAdviceModel>(dto);

            _context.RecordOfAdvice.Add(newRoa);
            _context.SaveChanges();


            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == dto.ApplicationId);
            app.AdvisorId = newRoa.AdvisorId;
            _context.Applications.Update(app);
            _context.SaveChanges();

            ClientModel client = _context.Clients.SingleOrDefault(a => a.Id == app.ClientId);
            client.AdvisorId = newRoa.AdvisorId;
            _context.Clients.Update(client);
            _context.SaveChanges();

            DisclosureRepo discRepo = new DisclosureRepo(_context, _host, _config, _mapper, _fileStorage, null);

            var discDto = new DisclosureDto()
            {
                ClientId = client.Id,
                AdvisorId = newRoa.AdvisorId
            };

            discRepo.CreateDisclosure(discDto);



            dto = _mapper.Map<RecordOfAdviceDto>(newRoa);

            return dto;
        }

        public RecordOfAdviceDto UpdateRecordOfAdvice(RecordOfAdviceDto dto)
        {
            RecordOfAdviceModel newRoa = _mapper.Map<RecordOfAdviceModel>(dto);

            _context.RecordOfAdvice.Update(newRoa);
            _context.SaveChanges();

            ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == dto.ApplicationId);
            app.ApplicationStatus = ApplicationStatusEnum.Completed;
            _context.Applications.Update(app);
            _context.SaveChanges();


            dto = _mapper.Map<RecordOfAdviceDto>(newRoa);
            foreach (var product in dto.SelectedProducts)
            {
                product.ProductName = _context.Products.First(p => p.Id == (int)product.ProductId).Name;
            }

            return dto;
        }

        public bool DeleteRecordOfAdvice(RecordOfAdviceDto dto)
        {
            throw new System.NotImplementedException();
        }

        public void GenerateRecordOfAdvice(ClientModel client, AdvisorModel advisor, RecordOfAdviceModel roa, RiskProfileModel risk)
        {
            var data = new Dictionary<string, string>();
            var date = DateTime.Today.ToString("yyyy/MM/dd");
            var nameSurname = $"{client.User.FirstName} {client.User.LastName}";

            data["bdaNumber"] = string.Empty;
            data["date"] = date;
            data["nameSurname"] = nameSurname;
            data["idNo"] = client.User.RSAIdNumber;
            data["advisorName"] = $"{advisor.User.FirstName} {advisor.User.LastName}";
            data["distributorName"] = "Dwayne de Waal";
            data["advisorEmail"] = advisor.User.Email;
            data["advisorMobile"] = "0" + advisor.User.MobileNumber;

            if (advisor.User.Address.Count > 1)
            {
                foreach (var item in advisor.User.Address)
                {
                    if (item.Type == DataService.Enum.AddressTypesEnum.Residential)
                    {
                        data["advisorAddress"] = $"{item.UnitNumber}  {item.ComplexName}, " +
                        $"{item.StreetNumber} {item.StreetName}, " +
                        $"{item.Suburb} {item.City}";
                    }
                }
            }

            data["introduction"] = roa.Introduction;
            data["materialInformation"] = roa.MaterialInformation;
            data["replacementReason"] = roa.ReplacementReason ?? string.Empty;
            data["derivedProfile"] = risk.DerivedProfile;

            if (roa.Replacement_A)
                data["replacement_A"] = "x";

            if (roa.Replacement_B)
                data["replacement_B"] = "x";

            if (roa.Replacement_C)
                data["replacement_C"] = "x";

            if (roa.Replacement_D)
                data["replacement_D"] = "x";

            foreach (var item in roa.SelectedProducts)
            {
                ProductModel product = _context.Products.Where(c => c.Id == item.ProductId).FirstOrDefault();

                data[$"{product.Name.Trim().Replace(" ","")}_productName"] = product.Name; //not used?

                data[$"{product.Name.Trim().Replace(" ", "")}_recommendedLumpSum"] = item.RecommendedLumpSum > 0 ?
                    item.RecommendedLumpSum.ToString() : string.Empty;

                data[$"{product.Name.Trim().Replace(" ", "")}_acceptedLumpSum"] = item.AcceptedLumpSum > 0 ?
                    item.AcceptedLumpSum.ToString() : string.Empty;

                if (item.RecommendedRecurringPremium > 0)
                {
                    data[$"{product.Name.Trim().Replace(" ", "")}_recommendedRecurringPremium"] = item.RecommendedRecurringPremium > 0 ?            //removed new doc
                      item.RecommendedRecurringPremium.ToString() : string.Empty; ;

                    data[$"{product.Name.Trim().Replace(" ", "")}_acceptedRecurringPremium"] = item.AcceptedRecurringPremium > 0 ?                  //removed new doc
                        item.AcceptedRecurringPremium.ToString() : string.Empty; ;
                }

                data[$"{product.Name}_deviationReason"] = item.DeviationReason ?? string.Empty; ;
            }

            DocumentHelper dh = new DocumentHelper(_context, _config, _fileStorage, _host);

            dh.PopulateAndSaveDocument(DocumentTypesEnum.RecordOfAdvice, data, client.User);
        }
    }
}