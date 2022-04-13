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

        void GenerateApplicationDocuments(int applicationId);
        void SignDocuments(int applicationId);
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



        public ApplicationRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _dh = new DocumentHelper(_context, _config, _fileStorage, _host);
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

            SendNewApplicationEmail(client);

            return dto;
            
        }

        //public async Task<List<ApplicationDocumentDto>> GetApplicationDocuments(int applicationId)
        //{
        //    ApplicationModel a = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
        //    ClientModel c = _context.Clients.Include(c => c.User).SingleOrDefault(c => c.Id == a.ClientId);

        //    //List<UserDocumentDto> response = await _dh.GetAllUserDocuments(c.User);

        //    return response;

        //}


        public void GenerateApplicationDocuments(int applicationId)
        {
            ApplicationModel application = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
            RecordOfAdviceModel roa = _context.RecordOfAdvice.Include(r => r.SelectedProducts).SingleOrDefault(a => a.ApplicationId == applicationId);
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(u => u.Address).Include(c => c.TaxResidency).Include(c => c.BankDetails).SingleOrDefault(c => c.Id == application.ClientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).ThenInclude(u => u.Address).SingleOrDefault(ad => ad.Id == client.AdvisorId);
            RiskProfileModel risk = _context.RiskProfiles.SingleOrDefault(r => r.ClientId == client.Id);
            FSPMandateModel fsp = _context.FspMandates.SingleOrDefault(r => r.ClientId == client.Id);
            ConsumerProtectionModel cp = _context.ConsumerProtection.SingleOrDefault(r => r.ClientId == client.Id);
            DisclosureModel disc = _context.Disclosures.SingleOrDefault(r => r.ClientId == client.Id);


            //ROA only application document thus far
            RecordOfAdviceRepo roaRepo = new RecordOfAdviceRepo(_context, _host, _config, _mapper, _fileStorage);
            roaRepo.GenerateRecordOfAdvice(client, advisor, roa, risk);

            RiskProfileRepo riskRepo = new RiskProfileRepo(_context, _host, _config, _mapper, _fileStorage);
            riskRepo.GenerateRiskProfile(client, advisor, risk);

            //FspMandateRepo fspMandateRepo = new FspMandateRepo(_context, _host, _config, _mapper, _fileStorage);
            //fspMandateRepo.GenerateMandate(client, advisor, fsp);

            DisclosureRepo disclosure = new DisclosureRepo(_context, _host, _config, _mapper, _fileStorage, null);
            disclosure.GenerateClientConsent(client, advisor);
            disclosure.GenerateDisclosure(client, advisor, cp, disc);

            PEFRepo pefRepo = new PEFRepo(_context, _host, _config, _mapper, _fileStorage);
            foreach (var product in roa.SelectedProducts)
            {
                if (product.ProductId == 5 || product.ProductId == 6)
                {
                    pefRepo.GenerateDOA(client, advisor, product);
                    pefRepo.GenerateQuote(client, advisor, product);
                }
            }

        }

        public async Task<List<ApplicationDocumentDto>> GetApplicationDocuments(int applicationId)
        {
            ApplicationModel a = _context.Applications.First(c => c.Id == applicationId);

            List<ApplicationDocumentDto> response = await _dh.GetAllApplicationDocuments(a);

            return response;
        }

        private List<SignerListItemDto> FspMandateSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();


            //var pageList = mandate.Objective[0].ToString() == "L" ?
            //    new List<int> { 2, 3, 4, 5, 6, 7, 9 } : // Limited Discretiuon
            //    new List<int> { 2, 3, 4, 5, 6, 7, 8 }; // Full Discretion

            var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
            {
                //  initial
                Signature = Convert.ToBase64String(client.User.Signature),
                FirstName = client.User.FirstName,
                LastName = client.User.LastName,
                Email = client.User.Email,
                IdNo = client.User.RSAIdNumber,
                Mobile = client.User.MobileNumber,
                IncludeSignedBy = false,
                XField = 20,//450,//495,//490,
                YField = 767,//772,//800,//795,//800,
                HField = 20,
                WField = 60,
                Page = p
            })));


            //signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()    //adviser signature no longer used
            //{   //adviser
            //    Signature = Convert.ToBase64String(advisor.User.Signature),
            //    FirstName = advisor.User.FirstName,
            //    LastName = advisor.User.LastName,
            //    Email = advisor.User.Email,
            //    IdNo = advisor.User.RSAIdNumber,
            //    Mobile = advisor.User.MobileNumber,
            //    XField = 120,
            //    YField = 512,
            //    HField = 30,
            //    WField = 120,
            //    Page = 7
            //}));

            signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
            {
                // client (all)
                Signature = Convert.ToBase64String(client.User.Signature),
                FirstName = client.User.FirstName,
                LastName = client.User.LastName,
                Email = client.User.Email,
                IdNo = client.User.RSAIdNumber,
                Mobile = client.User.MobileNumber,
                XField = 113,//120,
                YField = 205,//195,//266,//638,
                HField = 30,
                WField = 120,
                Page = 11//10//7
            }));

            signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
            {
                // advisor as witness
                Signature = Convert.ToBase64String(advisor.User.Signature),
                FirstName = advisor.User.FirstName,
                LastName = advisor.User.LastName,
                Email = advisor.User.Email,
                IdNo = advisor.User.RSAIdNumber,
                Mobile = advisor.User.MobileNumber,
                XField = 400,//400,//115,//120,
                YField = 205,//195,//296,//638,
                HField = 30,
                WField = 120,
                Page = 11//10//7
            }));


            if (client.FspMandate.DiscretionType == "full")
            {
                signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
                {
                    // client (full)
                    Signature = Convert.ToBase64String(client.User.Signature),
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    Email = client.User.Email,
                    IdNo = client.User.RSAIdNumber,
                    Mobile = client.User.MobileNumber,
                    XField = 117,//105,//96,
                    YField = 530,//510,//602,
                    HField = 30,
                    WField = 120,
                    Page = 11//10
                }));
            }
            else if (client.FspMandate.DiscretionType == "limited_DE")
            {
                signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
                {
                    // client (limited DE)
                    Signature = Convert.ToBase64String(client.User.Signature),
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    Email = client.User.Email,
                    IdNo = client.User.RSAIdNumber,
                    Mobile = client.User.MobileNumber,
                    XField = 370,
                    YField = 162,//74,
                    HField = 30,
                    WField = 120,
                    Page = 12//11
                }));
            }
            else if (client.FspMandate.DiscretionType == "limited_RM")
            {
                signerList.Add(_signRepo.CreateSignerListItem(new SignerDto()
                {
                    // client (limited RM)
                    Signature = Convert.ToBase64String(client.User.Signature),
                    FirstName = client.User.FirstName,
                    LastName = client.User.LastName,
                    Email = client.User.Email,
                    IdNo = client.User.RSAIdNumber,
                    Mobile = client.User.MobileNumber,
                    XField = 370,
                    YField = 296,//202,
                    HField = 30,
                    WField = 120,
                    Page = 12//11
                }));
            }


            return signerList;
        }


        private async void SendNewApplicationEmail(ClientModel client)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = "New Aluma Application: " + client.User.FirstName + " " + client.User.LastName,
                    IsBodyHtml = true
                };

                //message.To.Add(new MailAddress("sales@aluma.co.za"));
                message.To.Add(new MailAddress("system@aluma.co.za"));


                message.Body = "A new application has been submitted on the client portal by " + client.User.FirstName + " " + client.User.LastName + ". Contact number: " + client.User.MobileNumber + ".  Email: " + client.User.Email;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }


        private async void SendApplicationDocumentsToBroker(ApplicationModel app, AdvisorModel advisor, ClientModel client)
        {
            var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

            UserMail um = new UserMail()
            {
                Email = advisor.User.Email,
                Name = client.User.FirstName + " " + client.User.LastName,
                Subject = "Aluma Capital: Application Complete " + client.User.FirstName + " " + client.User.LastName,
                Template = "ApplicationComplete"
            };

            try
            {
                var message = new MailMessage
                {
                    From = new MailAddress(mailSettings.Username),
                    Subject = um.Subject,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(client.User.Email));
                message.To.Add(new MailAddress(advisor.User.Email));
                //message.Bcc.Add(new MailAddress("johan@fintegratetech.co.za"));
                message.Bcc.Add(new MailAddress("system@aluma.co.za"));

                List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == client.UserId && !d.IsSigned).ToList();

                foreach (var doc in userDocs)
                {
                    byte[] data = await _dh.GetDocumentData(doc.URL, doc.Name);
                    var stream = new MemoryStream(data);

                    var attachment = new Attachment(stream, doc.Name);

                    message.Attachments.Add(attachment);
                };

                foreach (var doc in app.Documents)
                {
                    byte[] data = await _dh.GetDocumentData(doc.URL, doc.Name);
                    var stream = new MemoryStream(data);

                    var attachment = new Attachment(stream, doc.Name);

                    message.Attachments.Add(attachment);
                };

                message.Body = "Application Completed: " + um.Name;

                var smtpClient = new SmtpClient
                {
                    Host = "mail.administr8it.co.za",
                    Port = 25,
                    EnableSsl = false,
                    Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
                    Timeout = 1000000
                };


                smtpClient.Send(message);

                return;

            }
            catch (System.Exception ex)
            {
                return;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public async void SignDocuments(int applicationId)
        {
            SignatureRepo _signRepo = new SignatureRepo();

            ApplicationModel application = _context.Applications.Include(a => a.Documents).SingleOrDefault(a => a.Id == applicationId);
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(u => u.Address).Include(c => c.TaxResidency).Include(c => c.BankDetails).SingleOrDefault(c => c.Id == application.ClientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).ThenInclude(u => u.Address).SingleOrDefault(ad => ad.Id == client.AdvisorId);

            List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == client.UserId && !d.IsSigned).ToList();

            List<DocumentTypesEnum> docTypeRequireSignature = new List<DocumentTypesEnum>();

            if (application.ApplicationType == ApplicationTypesEnum.Individual)
            {
                docTypeRequireSignature = new List<DocumentTypesEnum>()
                        {
                            DocumentTypesEnum.ClientConsent,
                            DocumentTypesEnum.DisclosureLetter,
                            DocumentTypesEnum.RiskProfile,
                            DocumentTypesEnum.RecordOfAdvice,
                            DocumentTypesEnum.PEFDOA,
                            DocumentTypesEnum.PEFQuote,
                            DocumentTypesEnum.PEF2DOA,
                            DocumentTypesEnum.PEF2Quote,
                            DocumentTypesEnum.FSPMandate,
                        };
            }

            if (application.Documents.Count > 0)
            {
                foreach (var item in application.Documents)
                {
                    if (docTypeRequireSignature.Contains(item.DocumentType))
                    {
                        List<SignerListItemDto> signers = item.DocumentType switch
                        {
                            DocumentTypesEnum.ClientConsent => new List<SignerListItemDto>()
                    {
                         _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 110,//134,//142,//40,
                                    YField = 543,//357,//735,//720,
                                    HField = 30,
                                    WField = 120,
                                    Page = 3//2
                                })
                    },
                            DocumentTypesEnum.RiskProfile => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 18,
                                    YField = 777,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 18,
                                    YField = 769,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),


                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 115,//38,//60,
                                    YField = 436,//360,//352,//345,
                                    HField = 30,
                                    WField = 120,
                                    Page = 2
                                })
                            },
                            DocumentTypesEnum.RecordOfAdvice => new List<SignerListItemDto>()
                            {
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 99,//121,//118,//109,//120,
                                    YField = 560,//417,//649,//635,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 385,//403,//417,//412,//410,
                                    YField = 560,//567,//417,//648,//635,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4
                                }),
                                //initials
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 1
                                //}),
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 2
                                //}),
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 3
                                //})
                            },
                            DocumentTypesEnum.DisclosureLetter => new List<SignerListItemDto>()
                            {
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 98,
                                    YField = 463,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4,
                                }),
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                // _signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = advisor.User.FirstName,
                                //    LastName = advisor.User.LastName,
                                //    Email = advisor.User.Email,
                                //    IdNo = advisor.User.RSAIdNumber,
                                //    Mobile = advisor.User.MobileNumber,
                                //    XField = 80,
                                //    YField = 718,
                                //    HField = 30,
                                //    WField = 120,
                                //    Page = 4,
                                //}),
                                //  _signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 280,
                                //    YField = 718,
                                //    HField = 30,
                                //    WField = 120,
                                //    Page = 4,
                                //}),
                            },
                            DocumentTypesEnum.FSPMandate => FspMandateSigningList(application, client, advisor),
                            DocumentTypesEnum.PEFDOA => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEF2DOA => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEFQuote => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEF2Quote => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                        };

                        byte[] docB64 = await _dh.GetDocumentData(item.URL, item.Name);

                        var ceremony = _signRepo.CreateMultipleSignersCeremony(docB64,
                                "testdocument.pdf", signers);

                        docB64 = Convert.FromBase64String(
                            _signRepo.RunMultiSignerCeremony(ceremony));

                        _dh.UploadSignedApplicationFile(docB64, item, client.User);

                    }
                }
            }
            if (userDocs.Count > 0)
            {
                foreach (var item in userDocs)
                {
                    if (docTypeRequireSignature.Contains(item.DocumentType))
                    {
                        List<SignerListItemDto> signers = item.DocumentType switch
                        {
                            DocumentTypesEnum.ClientConsent => new List<SignerListItemDto>()
                    {
                         _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 110,//134,//142,//40,
                                    YField = 543,//357,//735,//720,
                                    HField = 30,
                                    WField = 120,
                                    Page = 3//2
                                })
                    },
                            DocumentTypesEnum.RiskProfile => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 18,
                                    YField = 777,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 18,
                                    YField = 769,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),


                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 115,//38,//60,
                                    YField = 436,//360,//352,//345,
                                    HField = 30,
                                    WField = 120,
                                    Page = 2
                                })
                            },
                            DocumentTypesEnum.RecordOfAdvice => new List<SignerListItemDto>()
                            {
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 99,//121,//118,//109,//120,
                                    YField = 560,//417,//649,//635,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 385,//403,//417,//412,//410,
                                    YField = 560,//567,//417,//648,//635,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4
                                }),
                                //initials
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 1
                                //}),
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 2
                                //}),
                                //_signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 28,
                                //    YField = 766,
                                //    HField = 20,
                                //    WField = 60,
                                //    IncludeSignedBy = false,
                                //    Page = 3
                                //})
                            },
                            DocumentTypesEnum.DisclosureLetter => new List<SignerListItemDto>()
                            {
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 98,
                                    YField = 463,
                                    HField = 30,
                                    WField = 120,
                                    Page = 4,
                                }),
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 25,
                                    YField = 770,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                // _signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = advisor.User.FirstName,
                                //    LastName = advisor.User.LastName,
                                //    Email = advisor.User.Email,
                                //    IdNo = advisor.User.RSAIdNumber,
                                //    Mobile = advisor.User.MobileNumber,
                                //    XField = 80,
                                //    YField = 718,
                                //    HField = 30,
                                //    WField = 120,
                                //    Page = 4,
                                //}),
                                //  _signRepo.CreateSignerListItem(new SignerDto()
                                //{
                                //    Signature = Convert.ToBase64String(client.User.Signature),
                                //    FirstName = client.User.FirstName,
                                //    LastName = client.User.LastName,
                                //    Email = client.User.Email,
                                //    IdNo = client.User.RSAIdNumber,
                                //    Mobile = client.User.MobileNumber,
                                //    XField = 280,
                                //    YField = 718,
                                //    HField = 30,
                                //    WField = 120,
                                //    Page = 4,
                                //}),
                            },
                            DocumentTypesEnum.FSPMandate => FspMandateSigningList(application, client, advisor),
                            DocumentTypesEnum.PEFDOA => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEF2DOA => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEFQuote => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                            DocumentTypesEnum.PEF2Quote => new List<SignerListItemDto>()
                            {
                                //initials
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 1
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 2
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 3
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 4
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 5
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 6
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 7
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 8
                                }),
                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 520,
                                    YField = 790,
                                    HField = 20,
                                    WField = 60,
                                    IncludeSignedBy = false,
                                    Page = 9
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(client.User.Signature),
                                    FirstName = client.User.FirstName,
                                    LastName = client.User.LastName,
                                    Email = client.User.Email,
                                    IdNo = client.User.RSAIdNumber,
                                    Mobile = client.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 299,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),

                                _signRepo.CreateSignerListItem(new SignerDto()
                                {
                                    Signature = Convert.ToBase64String(advisor.User.Signature),
                                    FirstName = advisor.User.FirstName,
                                    LastName = advisor.User.LastName,
                                    Email = advisor.User.Email,
                                    IdNo = advisor.User.RSAIdNumber,
                                    Mobile = advisor.User.MobileNumber,
                                    XField = 170,//217,
                                    YField = 703,//215,
                                    HField = 30,
                                    WField = 120,
                                    Page = 10//11,
                                }),
                            },
                        };

                        byte[] docB64 = await _dh.GetDocumentData(item.URL, item.Name);

                        var ceremony = _signRepo.CreateMultipleSignersCeremony(docB64,
                                item.Name, signers);

                        docB64 = Convert.FromBase64String(
                            _signRepo.RunMultiSignerCeremony(ceremony));

                        _dh.UploadSignedUserFile(docB64, item);

                    }
                }
            }

            application.DocumentsSigned = true;
            _context.Applications.Update(application);
            _context.SaveChanges();


            SendApplicationDocumentsToBroker(application, advisor, client);
        }
    }

}