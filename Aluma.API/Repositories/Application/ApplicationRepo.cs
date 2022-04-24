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
using SignatureService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IApplicationRepo : IRepoBase<ApplicationModel>
    {
        public ApplicationDto GetApplication(ApplicationDto dto);

        public Task<List<ApplicationDocumentDto>> GetApplicationDocuments(int applicationId);

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

        Task GenerateApplicationDocuments(int applicationId);
        void ConsentToSign(int applicationId);
        bool CheckSignConsent(int applicationId);
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
        private readonly IFileStorageRepo _fileStorage;
        DocumentHelper _dh;
        MailSender _ms;

        public ApplicationRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _dh = new DocumentHelper(_context, _config, _fileStorage, _host);
            _ms = new MailSender(_context, _config, _fileStorage, _host);
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
        public bool CheckSignConsent(int applicationId)
        {
            try
            {
                ApplicationModel app = _context.Applications.SingleOrDefault(a => a.Id == applicationId  );
                if (app.SignatureConsent)
                {
                    if (app.SignatureConsentDate < DateTime.UtcNow.AddDays(-1))
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
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
            RecordOfAdviceRepo roaRepo = new RecordOfAdviceRepo(_context, _host, _config, _mapper, _fileStorage);
            foreach (var app in result)
            {
                app.ProductName = _context.Products.First(p => p.Id == app.ProductId).Name;
                app.showRecordOfAdvice = !roaRepo.DoesApplicationHaveRecordOfAdice(app.Id);
                app.showRiskMismatch = _context.RiskProfiles.Where(r => r.ClientId == app.ClientId && r.AgreeWithOutcome == false && r.AdvisorNotes == null).Any();

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

            ApplicationDto response = _mapper.Map<ApplicationDto>(application);

            RecordOfAdviceRepo roaRepo = new RecordOfAdviceRepo(_context, _host, _config, _mapper, _fileStorage);

            response.showRecordOfAdvice = !roaRepo.DoesApplicationHaveRecordOfAdice(response.Id);
            response.showRiskMismatch = _context.RiskProfiles.Where(r => r.ClientId == response.ClientId && r.AgreeWithOutcome == false && r.AdvisorNotes == null).Any();

            return response;
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

            applicationInProgress = _context.Applications.Where(a => a.ClientId == dto.ClientId && a.ApplicationStatus == ApplicationStatusEnum.InProgress && a.ProductId == productId).Any();


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

            ApplicationModel application = _mapper.Map<ApplicationModel>(dto);
            ClientModel client = _context.Clients.SingleOrDefault(c => c.Id == dto.ClientId);

            Enum.TryParse(dto.ApplicationStatus, true, out DataService.Enum.ApplicationStatusEnum appStatus);
            application.ApplicationStatus = appStatus;
            ProductModel product = _context.Products.Where(a => a.Name == dto.ProductName).First();
            application.ProductId = product.Id;
            _context.Applications.Add(application);
            _context.SaveChanges();
            dto = _mapper.Map<ApplicationDto>(application);
            dto.ProductName = product.Name;

            _ms.SendNewApplicationEmail(client, product.Name);

            return dto;

        }

        //public async Task<List<ApplicationDocumentDto>> GetApplicationDocuments(int applicationId)
        //{
        //    ApplicationModel a = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
        //    ClientModel c = _context.Clients.Include(c => c.User).SingleOrDefault(c => c.Id == a.ClientId);

        //    //List<UserDocumentDto> response = await _dh.GetAllUserDocuments(c.User);

        //    return response;

        //}


        public async Task GenerateApplicationDocuments(int applicationId)
        {
            ApplicationModel application = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
            RecordOfAdviceModel roa = _context.RecordOfAdvice.Include(r => r.SelectedProducts).SingleOrDefault(a => a.ApplicationId == applicationId);
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(u => u.Address).Include(c => c.TaxResidency).Include(c => c.BankDetails).SingleOrDefault(c => c.Id == application.ClientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).ThenInclude(u => u.Address).SingleOrDefault(ad => ad.Id == client.AdvisorId);
            RiskProfileModel risk = _context.RiskProfiles.SingleOrDefault(r => r.ClientId == client.Id);
            FSPMandateModel fsp = _context.FspMandates.SingleOrDefault(r => r.ClientId == client.Id);
            ConsumerProtectionModel cp = _context.ConsumerProtection.SingleOrDefault(r => r.ClientId == client.Id);
            DisclosureModel disc = _context.Disclosures.First(r => r.ClientId == client.Id);


            //ROA only application document thus far
            RecordOfAdviceRepo roaRepo = new RecordOfAdviceRepo(_context, _host, _config, _mapper, _fileStorage);
            RiskProfileRepo riskRepo = new RiskProfileRepo(_context, _host, _config, _mapper, _fileStorage);
            FspMandateRepo fspMandateRepo = new FspMandateRepo(_context, _host, _config, _mapper, _fileStorage);
            DisclosureRepo disclosure = new DisclosureRepo(_context, _host, _config, _mapper, _fileStorage, null);
            PEFRepo pefRepo = new PEFRepo(_context, _host, _config, _mapper, _fileStorage);
            FIRepo fiRepo = new FIRepo(_context, _host, _config, _mapper, _fileStorage);

            roaRepo.GenerateRecordOfAdvice(client, advisor, roa, risk);
            riskRepo.GenerateRiskProfile(client, advisor, risk);
            fspMandateRepo.GenerateMandate(client, advisor, fsp);
            disclosure.GenerateClientConsent(client, advisor);
            disclosure.GenerateDisclosure(client, advisor, cp, disc);

            foreach (var product in roa.SelectedProducts)
            {
                if (product.ProductId == 5 || product.ProductId == 6)
                {
                    pefRepo.GenerateDOA(client, advisor, product);
                    pefRepo.GenerateQuote(client, advisor, product);
                }
                else if (product.ProductId == 7)
                {
                    fiRepo.GenerateDOA (client, advisor, product);
                    fiRepo.GenerateQuote (client, advisor, product);
                }
            }

            application.DocumentsCreated = true;
            _context.Applications.Update(application);
            _context.SaveChanges();

        }

        public async Task<List<ApplicationDocumentDto>> GetApplicationDocuments(int applicationId)
        {
            ApplicationModel a = _context.Applications.First(c => c.Id == applicationId);

            List<ApplicationDocumentDto> response = await _dh.GetAllApplicationDocuments(a);

            return response;
        }

        
        public void ConsentToSign(int applicationId)
        {
            ApplicationModel application = _context.Applications.SingleOrDefault(a => a.Id == applicationId);

            application.SignatureConsent = true;
            application.SignatureConsentDate = DateTime.UtcNow;
            _context.Applications.Update(application);
            _context.SaveChanges();
        }

    }

}