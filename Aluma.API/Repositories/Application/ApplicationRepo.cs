using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IApplicationRepo : IRepoBase<ApplicationModel>
    {
        public ApplicationDto GetApplication(ApplicationDto dto);

        public List<ApplicationDocumentDto> GetApplicationDocuments(int applicationId);

        public List<ApplicationDto> GetApplications();

        public List<ApplicationDto> GetApplicationsByClient(string clientId);

        public List<ApplicationDto> GetApplicationsByAdvisor(AdvisorDto dto);

        public ApplicationDto UpdateApplication(ApplicationDto dto);

        public bool DeleteApplication(ApplicationDto dto);

        public ApplicationDto SoftDeleteApplication(ApplicationDto dto);

        public ApplicationDto CreateNewApplication(ApplicationDto dto);

        bool DoesApplicationExist(ApplicationDto dto);

        bool DoesApplicationExist(int clientId);

        bool ApplicationInProgress(ApplicationDto dto);

        void GenerateApplicationDocuments(int applicationId);
        //ApplicationDocumentsModel PopulateTestDocument();

        //void CreateDocuments(Guid applicationId);

        //void SignDocuments(Guid applicationId, Guid userId);

        //void ProcessApplication(Guid applicationId, Guid userId);

        //byte[] PopulateDocument(string teplateName, Dictionary<string, string> formData);

        //Task<string> SendNewApplicationNotification();
        //Task<string> SendSwitchPortfoliolOTP(UserModel user, AdvisorModel adviser, string otp, ApplicationsModel app);
        //Task<string> SendSwitchPortfolioSignedDocs(UserModel user, AdvisorModel adviser, ApplicationsModel app);
    }

    public class ApplicationRepo : RepoBase<ApplicationModel>, IApplicationRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        

        public ApplicationRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public bool DeleteApplication(ApplicationDto dto)
        {
            try
            {
                var application = _mapper.Map<ApplicationModel>(dto);
                application.ApplicationStatus = 0;
                _context.Applications.Update(application);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //log error
                return false;
            }
        }

        public List<ApplicationDto> GetApplications()
        {
            List<ApplicationModel> applications = _context.Applications.Where(a => a.ApplicationStatus != 0).Include(a => a.Client).ToList();
            return _mapper.Map<List<ApplicationDto>>(applications);
        }

        public List<ApplicationDto> GetApplicationsByClient(string clientId)
        {
            List<ApplicationModel> applications = _context.Applications.Where(c => c.ClientId.ToString() == clientId && c.ApplicationStatus != 0).ToList();

            //remove when productID is implemented
            List<ApplicationDto> result = _mapper.Map<List<ApplicationDto>>(applications);
            foreach (var app in result)
            {
                app.ProductName = _context.Products.First(p => p.Id == app.ProductId).Name;
            }

            return result;
        }

        public List<ApplicationDto> GetApplicationsByAdvisor(AdvisorDto dto)
        {
            List<ApplicationModel> applications = _context.Applications.Where(c => c.AdvisorId == dto.Id).ToList();
            return _mapper.Map<List<ApplicationDto>>(applications);
        }

        public ApplicationDto GetApplication(ApplicationDto dto)
        {
            ApplicationModel application = _context.Applications.Where(a => a.Id == dto.Id).First();
            return _mapper.Map<ApplicationDto>(application);
        }

        public ApplicationDto UpdateApplication(ApplicationDto dto)
        {
            throw new NotImplementedException();
        }

        public bool DoesApplicationExist(ApplicationDto dto)
        {
            bool applicationExists = false;

            applicationExists = _context.Applications.Where(a => a.Id == dto.Id).Any();

            return applicationExists;
        }

        public bool DoesApplicationExist(int clientId)
        {
            bool applicationExists = false;

            applicationExists = _context.Applications.Where(a => a.ClientId == clientId && a.ApplicationStatus != 0).Any();

            return applicationExists;
        }

        public bool ApplicationInProgress(ApplicationDto dto)
        {
            bool applicationInProgress = false;

            //Enum.TryParse(dto.Product, true, out DataService.Enum.ProductsEnum parsedProduct);
            int productId = _context.Products.Where(a => a.Name == dto.ProductName).First().Id;

            //applicationInProgress = _context.Applications.Where(a => a.ClientId == dto.ClientId && Convert.ToString(a.Product) == dto.Product && a.ApplicationStatus == DataService.Enum.StatusEnum.InProgress).Any();

            applicationInProgress = _context.Applications.Where(a => a.ClientId == dto.ClientId && a.ApplicationStatus == DataService.Enum.ApplicationStatusEnum.InProgress && a.ProductId == productId).Any();


            return applicationInProgress;
        }

        public ApplicationDto SoftDeleteApplication(ApplicationDto dto)
        {
            ApplicationModel application = _context.Applications.Where(x => x.Id == dto.Id).FirstOrDefault();

            application.ApplicationStatus = 0;

            _context.Applications.Update(application);
            _context.SaveChanges();
            dto = _mapper.Map<ApplicationDto>(application);
            return dto;
        }

        public ApplicationDto CreateNewApplication(ApplicationDto dto)
        {
            //ClientModel client = _mapper.Map<ClientModel>(dto);
            //ApplicationModel application = _mapper.Map<ApplicationModel>(dto);



            ApplicationModel application = _mapper.Map<ApplicationModel>(dto);
            Enum.TryParse(dto.ApplicationStatus, true, out DataService.Enum.ApplicationStatusEnum appStatus);
            application.ApplicationStatus = appStatus;
            ProductModel product = _context.Products.Where(a => a.Name == dto.ProductName).First();
            application.ProductId = product.Id;
            _context.Applications.Add(application);
            _context.SaveChanges();
            dto = _mapper.Map<ApplicationDto>(application);
            dto.ProductName = product.Name;
            return dto;
            //throw new NotImplementedException();
        }

        public List<ApplicationDocumentDto> GetApplicationDocuments(int applicationId)
        {
            ApplicationModel a = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
            ClientModel c = _context.Clients.Include(c => c.User).SingleOrDefault(c => c.Id == a.ClientId);
            //List<ApplicationDocumentModel> result = _context.ApplicationDocuments.Where(d => d.ApplicationId == applicationId).ToList();
            List<UserDocumentModel> result = _context.UserDocuments.Where(d => d.UserId == c.User.Id).ToList();
            List<ApplicationDocumentDto> response = new List<ApplicationDocumentDto>();
            foreach (var doc in result)
            {
                ApplicationDocumentDto dto = new ApplicationDocumentDto()
                {
                    Id = doc.Id,
                    DocumentName = doc.Name,
                    b64 = doc.URL
                };

                response.Add(dto);
            }

            return response;

        }


        public void GenerateApplicationDocuments(int applicationId)
        {
            ApplicationModel application = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
            RecordOfAdviceModel roa = _context.RecordOfAdvice.Include( r => r.SelectedProducts).SingleOrDefault(a => a.ApplicationId == applicationId);
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(u => u.Address).SingleOrDefault(c => c.Id == application.ClientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).ThenInclude(u => u.Address).SingleOrDefault(ad => ad.Id == client.AdvisorId);
            RiskProfileModel risk = _context.RiskProfiles.SingleOrDefault(r => r.ClientId == client.Id);

            //ROA only application document thus far
            RecordOfAdviceRepo roaRepo = new RecordOfAdviceRepo(_context,_host,_config,_mapper);
            roaRepo.GenerateRecordOfAdvice(client, advisor, roa, risk);
        }
        
    }

}