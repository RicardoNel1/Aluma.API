using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IApplicationRepo : IRepoBase<ApplicationModel>
    {
        public ApplicationDto GetApplication(ApplicationDto dto);

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

            applicationInProgress = _context.Applications.Where(a => a.ClientId == dto.ClientId && a.ApplicationStatus == DataService.Enum.ApplicationStatusEnum.InProgress &&  a.ProductId == productId).Any();


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

        //public void ProcessApplication(Guid applicationId, Guid userId)
        //{
        //    var application = _context.Applications.Find(applicationId);

        //    if (!application.PrimaryFormsComplete)
        //        throw new Exception("PrimaryFormsComplete_False");

        //    if (!application.BankValidationComplete)
        //        throw new Exception("BankValidationComplete_False");

        //    if (!application.DocumentsCreated)
        //        CreateDocuments(applicationId);

        //    if (!application.DocumentsSigned && !application.AllowSignature)
        //        throw new Exception("AllowSignature_False");

        //    if (!application.DocumentsSigned && application.AllowSignature || application.DocumentsSigned && application.AllowSignature && !application.SecondaryFormsComplete)
        //        SignDocuments(applicationId, userId);
        //}

        //public void SignDocuments(Guid applicationId, Guid userId)
        //{
        //    SigniflowRepo _signiflow = new SigniflowRepo();

        //    var application = _context.Applications
        //        .Where(c => c.Id == applicationId)
        //        .Include(c => c.Documents)
        //        .Include(c => c.Steps)
        //        .First();

        //    var advisorId = _context.AdvisorAdvise
        //        .Where(c => c.ApplicationId == applicationId)
        //        .First()
        //        .AdvisorId;

        //    var advisedProduct = _context.AdvisorAdvise
        //        .Where(c => c.ApplicationId == applicationId)
        //        .Include(c => c.AdvisedProducts)
        //        .First()
        //        .AdvisedProducts
        //        .First().Product;

        //    var advisor = _context.Advisors
        //        .Where(c => c.Id == advisorId)
        //        .Include(c => c.BrokerDetails)
        //        .Include(c => c.User)
        //        .First();

        //    var clientUser = _context.Users
        //        .First(c => c.Id == userId);

        //    application.Documents.ToList().ForEach(doc =>
        //    {
        //        List<DocumentTypesEnum> docTypeRequireSignature = new List<DocumentTypesEnum>();
        //        if (clientUser.Role == Roles.Guest)
        //        {
        //            docTypeRequireSignature = new List<DocumentTypesEnum>()
        //            {
        //                DocumentTypesEnum.SecondarySchedule,
        //                DocumentTypesEnum.Resolution
        //            };

        //            int xfieldSecondary = application.Description == "Trust" ? 250 :
        //                application.Description == "CC" ? 250 :
        //                application.Description == "PartnerShip" ? 250 :
        //                application.Description == "Company" ? 250 : 250;
        //            int yfieldSecondary = application.Description == "Trust" ? 560 :
        //                application.Description == "CC" ? 680 :
        //                application.Description == "Partnership" ? 480 :
        //                application.Description == "Company" ? 300 : 110;
        //            int pageSecondary = application.Description == "Trust" ? 4 :
        //                application.Description == "CC" ? 3 :
        //                application.Description == "PartnerShip" ? 4 :
        //                application.Description == "Company" ? 5 : 3;

        //            if (doc.Name == $"Associated Party, {application.Description} : {clientUser.FirstName} {clientUser.LastName}.pdf")
        //            {
        //                List<SignerListItemDto> signers = doc.DocumentType switch
        //                {
        //                    DocumentTypesEnum.SecondarySchedule => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = xfieldSecondary,
        //                            YField = yfieldSecondary,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = pageSecondary
        //                        })
        //                    }
        //                };

        //                var ceremony = _signiflow.CreateMultipleSignersCeremony(doc.DocumentData,
        //                  doc.Name, signers);

        //                doc.DocumentData = Convert.FromBase64String(
        //                    _signiflow.RunMultiSignerCeremony(ceremony));

        //                _context.ApplicationDocuments.Update(doc);
        //            }

        //        }
        //        else
        //        {
        //            if (application.Description == "SwitchPortfolio")
        //            {
        //                docTypeRequireSignature = new List<DocumentTypesEnum>()
        //                {
        //                    DocumentTypesEnum.StanlibSwitchForm,
        //                    DocumentTypesEnum.MentonovaFSP,
        //                    advisor.User.Role == Roles.External ? DocumentTypesEnum.LibertyRiskProfile : DocumentTypesEnum.RiskProfile
        //                };
        //            }
        //            else if (application.Description == "Individual" && advisedProduct == ProductsEnum.PrivateEquityFund)
        //            {
        //                docTypeRequireSignature = new List<DocumentTypesEnum>()
        //                {
        //                    DocumentTypesEnum.IntroLetter,
        //                    DocumentTypesEnum.FinancialNeedsAnalysis,
        //                    DocumentTypesEnum.RiskProfile,
        //                    DocumentTypesEnum.RecordOfAdvice,
        //                    DocumentTypesEnum.DOA,
        //                    DocumentTypesEnum.DisclosureLetter,
        //                };
        //            }
        //            else
        //            {
        //                docTypeRequireSignature = new List<DocumentTypesEnum>()
        //                {
        //                    DocumentTypesEnum.IntroLetter,
        //                    DocumentTypesEnum.FinancialNeedsAnalysis,
        //                    DocumentTypesEnum.PrimarySchedule,
        //                    DocumentTypesEnum.SecondarySchedule,
        //                    DocumentTypesEnum.RiskProfile,
        //                    DocumentTypesEnum.RecordOfAdvice,
        //                    DocumentTypesEnum.FSPMandate,
        //                    DocumentTypesEnum.Dividends,
        //                    DocumentTypesEnum.IRSW9E,
        //                    DocumentTypesEnum.FW8BENE,
        //                    DocumentTypesEnum.TermsAndConditions_Nedbank,
        //                    DocumentTypesEnum.SACompanyEnterpriseGeneralDeclaration,
        //                    DocumentTypesEnum.TrustEnterpriseGeneralDeclaration,
        //                    DocumentTypesEnum.PartnershipEnterpriseGeneralDeclaration,
        //                    DocumentTypesEnum.CCEnterpriseGeneralDeclaration,
        //                    DocumentTypesEnum.Resolution,
        //                    DocumentTypesEnum.SACompanySoleShareholderDeclaration,
        //                    DocumentTypesEnum.DOA,
        //                    DocumentTypesEnum.DisclosureLetter,
        //                };
        //            }

        //            // determine whether this document needs a signature
        //            if (docTypeRequireSignature.Contains(doc.DocumentType))
        //            {
        //                int xfield = application.Description == "Individual" ? 178 ://180 :
        //                    application.Description == "Trust" ? 177 ://180 :
        //                    application.Description == "CC" ? 172 ://180 :
        //                    application.Description == "Partnership" ? 171 ://180 :
        //                    application.Description == "Company" ? 171 : 180;//180 : 180;
        //                int yfield = application.Description == "Individual" ? 386 ://155 :
        //                    application.Description == "Trust" ? 99 ://660 :
        //                    application.Description == "CC" ? 162 ://680 :
        //                    application.Description == "Partnership" ? 749 ://650 :
        //                    application.Description == "Company" ? 569 : 110;//360 : 110;
        //                int page = application.Description == "Individual" ? 5 :
        //                   application.Description == "Trust" ? 6 ://5 :
        //                   application.Description == "CC" ? 6 ://5 :
        //                   application.Description == "Partnership" ? 5 :
        //                   application.Description == "Company" ? 6 : 5;

        //                int xfieldSecondary = application.Description == "Trust" ? 170 ://250 :
        //                    application.Description == "CC" ? 172 ://250 :
        //                    application.Description == "PartnerShip" ? 170 ://250 :
        //                    application.Description == "Company" ? 171 : 250;// 250 : 250;
        //                int yfieldSecondary = application.Description == "Trust" ? 138 ://560 :
        //                    application.Description == "CC" ? 753 ://680 :
        //                    application.Description == "Partnership" ? 622 ://480 :
        //                    application.Description == "Company" ? 421 : 110;//220 : 110;
        //                int pageSecondary = application.Description == "Trust" ? 5 ://4 :
        //                    application.Description == "CC" ? 4 ://3 :
        //                    application.Description == "PartnerShip" ? 4 :
        //                    application.Description == "Company" ? 5 : 3;

        //                // create signer list
        //                List<SignerListItemDto> signers = doc.DocumentType switch
        //                {
        //                    DocumentTypesEnum.IntroLetter => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 134,//142,//40,
        //                            YField = 357,//735,//720,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 3//2
        //                        })
        //                    },
        //                    DocumentTypesEnum.FinancialNeedsAnalysis => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 442,//350,
        //                            YField = 645,//652,//615,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1,
        //                        }),
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(advisor.User.Signature),
        //                            FirstName = advisor.User.FirstName,
        //                            LastName = advisor.User.LastName,
        //                            Email = advisor.User.Email,
        //                            IdNo = advisor.User.IdNumber,
        //                            Mobile = advisor.User.MobileNumber,
        //                            XField = 449,//350,
        //                            YField = 681,//693,//670,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1,
        //                        })
        //                    },
        //                    DocumentTypesEnum.PrimarySchedule => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = xfield,
        //                            YField = yfield,
        //                            HField = 30,
        //                            WField = 120,
        //                            //Page = 5
        //                            Page = application.Description == "Company" ? 6 : 5

        //                        })
        //                    },
        //                    DocumentTypesEnum.SecondarySchedule => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = xfieldSecondary,
        //                            YField = yfieldSecondary,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = pageSecondary
        //                        })
        //                    },
        //                    DocumentTypesEnum.TermsAndConditions_Nedbank => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 144,
        //                            YField = 583,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 2
        //                        })//,
        //                        //_signiflow.CreateSignerListItem(new SignerDto()
        //                        //{
        //                        //    Signature = Convert.ToBase64String(advisor.User.Signature),
        //                        //    FirstName = advisor.User.FirstName,
        //                        //    LastName = advisor.User.LastName,
        //                        //    Email = advisor.User.Email,
        //                        //    IdNo = advisor.User.IdNumber,
        //                        //    Mobile = advisor.User.MobileNumber,
        //                        //    XField = 144,
        //                        //    YField = 640,
        //                        //    HField = 30,
        //                        //    WField = 120,
        //                        //    Page = 2,
        //                        //})
        //                    },
        //                    DocumentTypesEnum.RiskProfile => new List<SignerListItemDto>()
        //                    {
        //                        //initials
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 18,
        //                            YField = 777,
        //                            HField = 20,
        //                            WField = 60,
        //                            IncludeSignedBy = false,
        //                            Page = 1
        //                        }),
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 18,
        //                            YField = 769,
        //                            HField = 20,
        //                            WField = 60,
        //                            IncludeSignedBy = false,
        //                            Page = 2
        //                        }),

        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 115,//38,//60,
        //                            YField = 436,//360,//352,//345,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 2
        //                        })
        //                    },
        //                    DocumentTypesEnum.LibertyRiskProfile => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 38,//60,
        //                            YField = 380,//345,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 2
        //                        })
        //                    },
        //                    DocumentTypesEnum.RecordOfAdvice => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 121,//118,//109,//120,
        //                            YField = 567,//417,//649,//635,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 4
        //                        }),
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(advisor.User.Signature),
        //                            FirstName = advisor.User.FirstName,
        //                            LastName = advisor.User.LastName,
        //                            Email = advisor.User.Email,
        //                            IdNo = advisor.User.IdNumber,
        //                            Mobile = advisor.User.MobileNumber,
        //                            XField = 403,//417,//412,//410,
        //                            YField = 567,//417,//648,//635,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 4
        //                        })//,
        //                        //_signiflow.CreateSignerListItem(new SignerDto()
        //                        //{
        //                        //    Signature = Convert.ToBase64String(advisor.User.Signature),
        //                        //    FirstName = advisor.User.FirstName,
        //                        //    LastName = advisor.User.LastName,
        //                        //    Email = advisor.User.Email,
        //                        //    IdNo = advisor.User.IdNumber,
        //                        //    Mobile = advisor.User.MobileNumber,
        //                        //    XField = 123,//120,
        //                        //    YField = 732,//720,
        //                        //    HField = 30,
        //                        //    WField = 120,
        //                        //    Page = 4
        //                        //})
        //                    },
        //                    DocumentTypesEnum.DOA => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 217,
        //                            YField = 215,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 11,
        //                        }),
        //                    },
        //                    DocumentTypesEnum.DisclosureLetter => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 98,
        //                            YField = 463,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 4,
        //                        }),
        //                        // _signiflow.CreateSignerListItem(new SignerDto()
        //                        //{
        //                        //    Signature = Convert.ToBase64String(clientUser.Signature),
        //                        //    FirstName = advisor.User.FirstName,
        //                        //    LastName = advisor.User.LastName,
        //                        //    Email = advisor.User.Email,
        //                        //    IdNo = advisor.User.IdNumber,
        //                        //    Mobile = advisor.User.MobileNumber,
        //                        //    XField = 80,
        //                        //    YField = 718,
        //                        //    HField = 30,
        //                        //    WField = 120,
        //                        //    Page = 4,
        //                        //}),
        //                        //  _signiflow.CreateSignerListItem(new SignerDto()
        //                        //{
        //                        //    Signature = Convert.ToBase64String(clientUser.Signature),
        //                        //    FirstName = clientUser.FirstName,
        //                        //    LastName = clientUser.LastName,
        //                        //    Email = clientUser.Email,
        //                        //    IdNo = clientUser.IdNumber,
        //                        //    Mobile = clientUser.MobileNumber,
        //                        //    XField = 280,
        //                        //    YField = 718,
        //                        //    HField = 30,
        //                        //    WField = 120,
        //                        //    Page = 4,
        //                        //}),
        //                    },

        //                    DocumentTypesEnum.FSPMandate => FspMandateSigningList(application, advisor),
        //                    DocumentTypesEnum.Dividends => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 135,//500,
        //                            YField = 591,//746,//745,
        //                            HField = 30,//20,
        //                            WField = 120,//60,
        //                            Page = 2//1
        //                        }),

        //                        //initials
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            IncludeSignedBy = false,
        //                            XField = 514,//135,//150,
        //                            YField = 753,//193,//190,
        //                            HField = 20,//30,
        //                            WField = 60,//120,
        //                            Page = 1
        //                        }),
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            IncludeSignedBy = false,
        //                            XField = 514,//135,//150,
        //                            YField = 769,//193,//190,
        //                            HField = 20,//30,
        //                            WField = 60,//120,
        //                            Page = 2
        //                        }),
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            IncludeSignedBy = false,
        //                            XField = 514,//135,//150,
        //                            YField = 769,//193,//190,
        //                            HField = 20,//30,
        //                            WField = 60,//120,
        //                            Page = 3
        //                        })
        //                    },
        //                    DocumentTypesEnum.IRSW9E => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 130,
        //                            YField = 541,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.FW8BENE => new List<SignerListItemDto>()
        //                    {
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 654,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 8
        //                        })
        //                    },
        //                    DocumentTypesEnum.MentonovaFSP => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 130,//160,
        //                            YField = 290,//285,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 9
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 154,//160,
        //                            YField = 706,//700,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 10
        //                        })
        //                    },
        //                    DocumentTypesEnum.TrustEnterpriseGeneralDeclaration => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 304,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 364,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.SACompanyEnterpriseGeneralDeclaration => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 304,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 364,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1

        //                        })
        //                    },
        //                    DocumentTypesEnum.PartnershipEnterpriseGeneralDeclaration => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 304,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 364,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.CCEnterpriseGeneralDeclaration => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 304,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 100,
        //                            YField = 364,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.Resolution => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 348,
        //                            YField = 478,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 348,
        //                            YField = 520,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        }),
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 348,
        //                            YField = 563,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.SACompanySoleShareholderDeclaration => new List<SignerListItemDto>() {
        //                         _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 138,
        //                            YField = 447,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 1
        //                        })
        //                    },
        //                    DocumentTypesEnum.StanlibSwitchForm => new List<SignerListItemDto>() {
        //                        //client
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 160,
        //                            YField = 200,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 5
        //                        }),
        //                        //adviser
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(advisor.User.Signature),
        //                            FirstName = advisor.User.FirstName,
        //                            LastName = advisor.User.LastName,
        //                            Email = advisor.User.Email,
        //                            IdNo = advisor.User.IdNumber,
        //                            Mobile = advisor.User.MobileNumber,
        //                            XField = 160,
        //                            YField = 260,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 5
        //                        }),
        //                        //client
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(clientUser.Signature),
        //                            FirstName = clientUser.FirstName,
        //                            LastName = clientUser.LastName,
        //                            Email = clientUser.Email,
        //                            IdNo = clientUser.IdNumber,
        //                            Mobile = clientUser.MobileNumber,
        //                            XField = 152,//160,
        //                            YField = 193,//190,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 7
        //                        }),
        //                        //adviser
        //                        _signiflow.CreateSignerListItem(new SignerDto()
        //                        {
        //                            Signature = Convert.ToBase64String(advisor.User.Signature),
        //                            FirstName = advisor.User.FirstName,
        //                            LastName = advisor.User.LastName,
        //                            Email = advisor.User.Email,
        //                            IdNo = advisor.User.IdNumber,
        //                            Mobile = advisor.User.MobileNumber,
        //                            XField = 152,//160,
        //                            YField = 265,//260,
        //                            HField = 30,
        //                            WField = 120,
        //                            Page = 7
        //                        })
        //                    }
        //                };

        //                var ceremony = _signiflow.CreateMultipleSignersCeremony(doc.DocumentData,
        //                    doc.Name, signers);

        //                doc.DocumentData = Convert.FromBase64String(
        //                    _signiflow.RunMultiSignerCeremony(ceremony));

        //                _context.ApplicationDocuments.Update(doc);
        //            }
        //        }

        //    });

        //    if (clientUser.Role == Roles.Client)
        //    {
        //        //  update application that documents have now been signed
        //        application.DocumentsSigned = true;
        //        _context.Applications.Update(application);

        //        //  update application step process application now completed
        //        var stepCheck = _context.ApplicationSteps.Where(
        //            c => c.ApplicationId == applicationId &&
        //            c.StepType == ApplicationStepTypesEnum.ProcessApplication);

        //        if (stepCheck.Any())
        //        {
        //            var step = stepCheck.First();
        //            step.ActiveStep = false;
        //            step.Complete = true;
        //            _context.ApplicationSteps.Update(step);
        //        }

        //    }

        //    _context.SaveChanges();

        //    if (application.Description == "SwitchPortfolio")
        //    {
        //        SendSwitchPortfolioSignedDocs(clientUser, advisor, application);

        //    }
        //    else
        //    {
        //        SendApplicationDocumentsToBroker(application, advisor);

        //    }

        //}

        //private List<SignerListItemDto> FspMandateSigningList(ApplicationsModel application, AdvisorModel advisor)
        //{
        //    SigniflowRepo _signiflow = new SigniflowRepo();
        //    var signerList = new List<SignerListItemDto>();

        //    var mandate = _context.FspMandatates
        //        .Where(c => c.ApplicationId == application.Id)
        //        .First();

        //    //var pageList = mandate.Objective[0].ToString() == "L" ?
        //    //    new List<int> { 2, 3, 4, 5, 6, 7, 9 } : // Limited Discretiuon
        //    //    new List<int> { 2, 3, 4, 5, 6, 7, 8 }; // Full Discretion

        //    var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

        //    pageList.ForEach(p => signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //    {
        //        //  initial
        //        Signature = Convert.ToBase64String(application.User.Signature),
        //        FirstName = application.User.FirstName,
        //        LastName = application.User.LastName,
        //        Email = application.User.Email,
        //        IdNo = application.User.IdNumber,
        //        Mobile = application.User.MobileNumber,
        //        IncludeSignedBy = false,
        //        XField = 450,//495,//490,
        //        YField = 800,//795,//800,
        //        HField = 20,
        //        WField = 60,
        //        Page = p
        //    })));

        //    //signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()    //adviser signature no longer used
        //    //{   //adviser
        //    //    Signature = Convert.ToBase64String(advisor.User.Signature),
        //    //    FirstName = advisor.User.FirstName,
        //    //    LastName = advisor.User.LastName,
        //    //    Email = advisor.User.Email,
        //    //    IdNo = advisor.User.IdNumber,
        //    //    Mobile = advisor.User.MobileNumber,
        //    //    XField = 120,
        //    //    YField = 512,
        //    //    HField = 30,
        //    //    WField = 120,
        //    //    Page = 7
        //    //}));

        //    signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //    {
        //        // client (all)
        //        Signature = Convert.ToBase64String(application.User.Signature),
        //        FirstName = application.User.FirstName,
        //        LastName = application.User.LastName,
        //        Email = application.User.Email,
        //        IdNo = application.User.IdNumber,
        //        Mobile = application.User.MobileNumber,
        //        XField = 115,//120,
        //        YField = 266,//638,
        //        HField = 30,
        //        WField = 120,
        //        Page = 10//7
        //    }));

        //    signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //    {
        //        // advisor as witness
        //        Signature = Convert.ToBase64String(advisor.User.Signature),
        //        FirstName = advisor.User.FirstName,
        //        LastName = advisor.User.LastName,
        //        Email = advisor.User.Email,
        //        IdNo = advisor.User.IdNumber,
        //        Mobile = advisor.User.MobileNumber,
        //        XField = 115,//120,
        //        YField = 296,//638,
        //        HField = 30,
        //        WField = 120,
        //        Page = 10//7
        //    }));

        //    if (mandate.Discretion == "full")
        //    {
        //        signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //        {
        //            // client (full)
        //            Signature = Convert.ToBase64String(application.User.Signature),
        //            FirstName = application.User.FirstName,
        //            LastName = application.User.LastName,
        //            Email = application.User.Email,
        //            IdNo = application.User.IdNumber,
        //            Mobile = application.User.MobileNumber,
        //            XField = 96,
        //            YField = 602,
        //            HField = 30,
        //            WField = 120,
        //            Page = 10
        //        }));
        //    }
        //    else if (mandate.Discretion == "limited_DE")
        //    {
        //        signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //        {
        //            // client (limited DE)
        //            Signature = Convert.ToBase64String(application.User.Signature),
        //            FirstName = application.User.FirstName,
        //            LastName = application.User.LastName,
        //            Email = application.User.Email,
        //            IdNo = application.User.IdNumber,
        //            Mobile = application.User.MobileNumber,
        //            XField = 370,
        //            YField = 74,
        //            HField = 30,
        //            WField = 120,
        //            Page = 11
        //        }));
        //    }
        //    else if (mandate.Discretion == "limited_RM")
        //    {
        //        signerList.Add(_signiflow.CreateSignerListItem(new SignerDto()
        //        {
        //            // client (limited RM)
        //            Signature = Convert.ToBase64String(application.User.Signature),
        //            FirstName = application.User.FirstName,
        //            LastName = application.User.LastName,
        //            Email = application.User.Email,
        //            IdNo = application.User.IdNumber,
        //            Mobile = application.User.MobileNumber,
        //            XField = 370,
        //            YField = 202,
        //            HField = 30,
        //            WField = 120,
        //            Page = 11
        //        }));
        //    }

        //    return signerList;
        //}

        //public void CreateDocuments(Guid applicationId)
        //{
        //    string documentName = string.Empty;
        //    var appl = _context.Applications
        //        .Include(c => c.Steps)
        //        .Include(c => c.Documents)
        //        .First(c => c.Id == applicationId);

        //    // get the adviser advise
        //    var advise = _context.AdvisorAdvise
        //        .Include(c => c.AdvisedProducts)
        //        .ThenInclude(c => c.SwitchAllocations)
        //        .First(c => c.ApplicationId == applicationId);

        //    // get the adviser
        //    var advisor = _context.Advisors
        //        .Include(c => c.BrokerDetails)
        //        .Include(c => c.User)
        //        .First(c => c.Id == advise.AdvisorId);

        //    // get the primary client
        //    var clientUser = _context.Users
        //        .Where(c => c.Applications.Any(a => a.Id == applicationId)).First();

        //    var marketingExist = _context.Marketing.Where(c => c.ApplicationId == applicationId);
        //    MarketingModel marketing = null;
        //    if (marketingExist.Any())
        //    {
        //        marketing = marketingExist.First();
        //    }

        //    // If we did a digital KYC, get it
        //    var kycExist = _context.KycMetaData.Any(c => c.ApplicationId == applicationId);
        //    KycMetaDataModel kyc = null;
        //    if (kycExist)
        //    {
        //        kyc = _context.KycMetaData.First(c => c.ApplicationId == applicationId);
        //    }

        //    // get record of advise
        //    var roaExist = _context.RecordOfAdvise
        //        .Where(c => c.ApplicationId == applicationId)
        //        .Include(c => c.SelectedProducts);

        //    RecordOfAdviseModel roa = null;
        //    if (roaExist.Any())
        //    {
        //        roa = roaExist.First();
        //    }

        //    // get the bank validation data

        //    var bankValidationExists = _context.BankVerifications
        //        .Where(c => c.ApplicationId == applicationId);
        //    BankVerificationsModel bankValidation = null;
        //    if (bankValidationExists.Any())
        //    {
        //        bankValidation = bankValidationExists.First();
        //    }

        //    // get schedule step
        //    var scheduleStepExist = _context.ApplicationSteps
        //        .Where(c => c.ApplicationId == applicationId &&
        //            c.StepType == ApplicationStepTypesEnum.PrimarySchedule);

        //    ApplicationStepModel scheduleStep = null;
        //    if (scheduleStepExist.Any())
        //    {
        //        scheduleStep = scheduleStepExist.First();
        //    }

        //    // get schedule details
        //    AddressDto address = null;
        //    AddressDto postal = null;
        //    ClientDto client = null;
        //    RequiredSecondarySchedulesModel associatedParty = null;
        //    SwitchPortfolioClientModel switchClient = null;
        //    if (appl.Description != "SwitchPortfolio")
        //    {
        //        if (scheduleStep.ScheduleType == "Individual")
        //        {
        //            var schedule = _context.PrimaryIndividuals
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.ContactDetails)
        //                .Include(c => c.ClientDetails)
        //                    .ThenInclude(c => c.IdentityDetails)
        //                .Include(c => c.TaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentName = schedule.ClientDetails.FirstNames + " " + schedule.ClientDetails.Surname;

        //            address = new AddressDto()
        //            {
        //                UnitNumber = schedule.ContactDetails.UnitNumber,
        //                Complex = schedule.ContactDetails.Complex,
        //                StreetNumber = schedule.ContactDetails.StreetNumber,
        //                StreetName = schedule.ContactDetails.StreetName,
        //                City = schedule.ContactDetails.City,
        //                Country = schedule.ContactDetails.Country,
        //                Suburb = schedule.ContactDetails.Suburb,
        //                PostalCode = schedule.ContactDetails.PostalCode,
        //            };

        //            postal = new AddressDto()
        //            {
        //                UnitNumber = schedule.ContactDetails.PA_UnitNumber,
        //                Complex = schedule.ContactDetails.PA_Complex,
        //                StreetNumber = schedule.ContactDetails.PA_StreetNumber,
        //                StreetName = schedule.ContactDetails.PA_StreetName,
        //                City = schedule.ContactDetails.PA_City,
        //                Country = schedule.ContactDetails.PA_Country,
        //                Suburb = schedule.ContactDetails.PA_Suburb,
        //                PostalCode = schedule.ContactDetails.PA_PostalCode,
        //            };

        //            client = new ClientDto()
        //            {
        //                DateOfBirth = schedule.ClientDetails.DateOfBirth,
        //                TaxNumber = schedule.TaxResidency.TaxNumber,
        //                WorkNumber = schedule.ContactDetails.WorkNumber,
        //                MobileNumber = schedule.ContactDetails.MobileNumber,
        //                Email = schedule.ContactDetails.Email
        //            };
        //        }
        //        else if (scheduleStep.ScheduleType == "Trust")
        //        {
        //            var schedule = _context.PrimaryTrusts
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.TrustDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.TrustTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentName = schedule.TrustDetails.Name;

        //            var postalAddress = schedule.TrustDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Postal).FirstOrDefault();

        //            address = new AddressDto()
        //            {
        //                UnitNumber = postalAddress.UnitNumber,
        //                Complex = postalAddress.ComplexName,
        //                StreetNumber = postalAddress.StreetNumber,
        //                StreetName = postalAddress.StreetName,
        //                City = postalAddress.City,
        //                Country = postalAddress.Country,
        //                Suburb = postalAddress.Suburb,
        //                PostalCode = postalAddress.PostalCode,
        //            };
        //            client = new ClientDto()
        //            {
        //                TaxNumber = schedule.TrustTaxResidency.TaxNumber,
        //                WorkNumber = schedule.TrustDetails.Mobile,
        //                Email = schedule.TrustDetails.Email
        //            };
        //        }
        //        else if (scheduleStep.ScheduleType == "Company")
        //        {
        //            var schedule = _context.PrimarySaCompany
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.SaCompanyDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.SaCompanyTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentName = schedule.SaCompanyDetails.RegisteredName;

        //            var regAddress = schedule.SaCompanyDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Registered).FirstOrDefault();

        //            address = new AddressDto()
        //            {
        //                UnitNumber = regAddress.UnitNumber,
        //                Complex = regAddress.ComplexName,
        //                StreetNumber = regAddress.StreetNumber,
        //                StreetName = regAddress.StreetName,
        //                City = regAddress.City,
        //                Country = regAddress.Country,
        //                Suburb = regAddress.Suburb,
        //                PostalCode = regAddress.PostalCode,
        //            };
        //            client = new ClientDto()
        //            {
        //                TaxNumber = schedule.SaCompanyTaxResidency.TaxNumber,
        //                WorkNumber = schedule.SaCompanyDetails.Mobile,
        //                Email = schedule.SaCompanyDetails.Email
        //            };
        //        }
        //        else if (scheduleStep.ScheduleType == "Partnership")
        //        {
        //            var schedule = _context.PrimaryPartnership
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.PartnershipDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.PartnershipTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentName = schedule.PartnershipDetails.Name;

        //            var tradeAddress = schedule.PartnershipDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Trading).FirstOrDefault();

        //            address = new AddressDto()
        //            {
        //                UnitNumber = tradeAddress.UnitNumber,
        //                Complex = tradeAddress.ComplexName,
        //                StreetNumber = tradeAddress.StreetNumber,
        //                StreetName = tradeAddress.StreetName,
        //                City = tradeAddress.City,
        //                Country = tradeAddress.Country,
        //                Suburb = tradeAddress.Suburb,
        //                PostalCode = tradeAddress.PostalCode,
        //            };
        //            client = new ClientDto()
        //            {
        //                TaxNumber = schedule.PartnershipTaxResidency.TaxNumber,
        //                WorkNumber = schedule.PartnershipDetails.Mobile,
        //                Email = schedule.PartnershipDetails.Email
        //            };
        //        }
        //        else if (scheduleStep.ScheduleType == "CC")
        //        {
        //            var schedule = _context.PrimaryCC
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.CCDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.CCTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentName = schedule.CCDetails.RegisteredName;

        //            var regAddress = schedule.CCDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Registered).FirstOrDefault();

        //            address = new AddressDto()
        //            {
        //                UnitNumber = regAddress.UnitNumber,
        //                Complex = regAddress.ComplexName,
        //                StreetNumber = regAddress.StreetNumber,
        //                StreetName = regAddress.StreetName,
        //                City = regAddress.City,
        //                Country = regAddress.Country,
        //                Suburb = regAddress.Suburb,
        //                PostalCode = regAddress.PostalCode,
        //            };
        //            client = new ClientDto()
        //            {
        //                TaxNumber = schedule.CCTaxResidency.TaxNumber,
        //                WorkNumber = schedule.CCDetails.Mobile,
        //                Email = schedule.CCDetails.Email
        //            };
        //        }

        //        var secondaryExist = _context.RequiredSecondarySchedules.Any(x => x.ApplicationId == applicationId);

        //        // Secondary schedule
        //        if (scheduleStep.ScheduleType != "Individual")
        //        {
        //            var associatedPartyCheck = _context.RequiredSecondarySchedules
        //                 .Where(a => a.ApplicationId == applicationId)
        //                 .Include(b => b.Contacts);

        //            if (associatedPartyCheck.Any())
        //            {
        //                associatedParty = _context.RequiredSecondarySchedules
        //                .Where(a => a.ApplicationId == applicationId)
        //                .Include(b => b.Contacts)
        //                .First();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        switchClient = _context.SwitchPortfolioClients
        //                .Where(c => c.ApplicationId == applicationId)
        //                .First();

        //        documentName = switchClient.FirstNames + " " + switchClient.Surname;
        //    }

        //    var irsw9Exist = _context.IRSW9.Any(c => c.ApplicationId == applicationId);
        //    IRSW9Model irsw9 = null;
        //    if (irsw9Exist)
        //    {
        //        irsw9 = _context.IRSW9.First(c => c.ApplicationId == applicationId);
        //    }

        //    var irsw8Exist = _context.IRSW8.Any(c => c.ApplicationId == applicationId);
        //    IRSW8Model irsw8 = null;
        //    if (irsw8Exist)
        //    {
        //        irsw8 = _context.IRSW8.Include(c => c.USPersons).First(c => c.ApplicationId == applicationId);
        //    }

        //    var risk = _context.RiskProfiles.First(c => c.ApplicationId == applicationId);

        //    var documentList = new List<ApplicationDocumentsModel>();

        //    documentList.Add(CreateDisclosureLetter(applicationId, advisor, clientUser, marketing));

        //    if (appl.Description != "SwitchPortfolio")
        //    {
        //        // Intro letter
        //        documentList.Add(kycExist == true ?
        //            CreateIntroLetter(advisor, kyc.FirstNames, kyc.SurName, kyc.IdNumber) :
        //            CreateIntroLetter(advisor, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber));

        //        var divTax = _context.Dividends.First(c => c.ApplicationId == applicationId);

        //        // FNA
        //        documentList.Add(kycExist == true ?
        //            CreateFinancialNeedsAnalysis(roa, address.City, kyc.FirstNames, kyc.SurName, kyc.IdNumber, advisor.User) :
        //            CreateFinancialNeedsAnalysis(roa, address.City, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber, advisor.User));

        //        // Bank Validation
        //        documentList.Add(kycExist == true ?
        //            CreateBankValidationReport(bankValidation, kyc.FirstNames, kyc.SurName) :
        //            CreateBankValidationReport(bankValidation, clientUser.FirstName, clientUser.LastName));

        //        // Enterprise BO Guideline
        //        if (scheduleStep.ScheduleType != "Individual")
        //            documentList.Add(CreateEnterpriseBOGuideline());

        //        // Primary Schedule
        //        if (scheduleStep.ScheduleType == "Individual")
        //        {
        //            var schedule = _context.PrimaryIndividuals
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.ContactDetails)
        //            .Include(c => c.ClientDetails)
        //                .ThenInclude(c => c.IdentityDetails)
        //            .Include(c => c.TaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .Include(c => c.PurposeAndFunding)
        //            .First();

        //            if (advise.AdvisedProducts.First().Product != ProductsEnum.PrivateEquityFund)
        //            {
        //                documentList.Add(CreatePrimaryIndividualSchedule(schedule, bankValidation, marketing));
        //            }

        //            // Record Of Advise
        //            documentList.Add(CreateRecordOfAdvice(roa, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber, address, advisor, risk, advise));

        //            if (advise.AdvisedProducts.First().Product == ProductsEnum.PrivateEquityFund)
        //            {
        //                documentList.Add(CreateDOA(appl.Description, schedule, clientUser.FirstName, clientUser.LastName, "", address, clientUser, advisor.User, advise, bankValidation));
        //            }

        //        }
        //        else if (scheduleStep.ScheduleType == "Trust")
        //        {
        //            var associate = associatedParty;

        //            var schedule = _context.PrimaryTrusts
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.TrustDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.TrustFunding)
        //                .Include(c => c.TrustTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentList.Add(CreatePrimaryTrustSchedule(schedule, bankValidation, clientUser, marketing));

        //            // Record Of Advise
        //            documentList.Add(CreateRecordOfAdvice(roa, schedule.TrustDetails.Name, "", schedule.TrustDetails.Number, address, advisor, risk, advise));

        //            if (associatedParty != null)
        //            {
        //                documentList.Add(CreateResolution(associate, address, bankValidation, clientUser));
        //                foreach (var contact in associatedParty.Contacts)
        //                {
        //                    UserModel associatedUser = _context.Users
        //                   .First(u => u.Id == contact.UserID);

        //                    //secondary schedule
        //                    documentList.Add(CreateSecondaryTrustSchedule(schedule, bankValidation, clientUser, associatedUser, contact));
        //                }
        //                documentList.Add(CreateTrustEnterpriseGeneralDeclaration(schedule, associate, bankValidation, clientUser));
        //            }
        //        }
        //        else if (scheduleStep.ScheduleType == "Company")
        //        {
        //            var associate = associatedParty;//advise.AdvisedProducts.First();

        //            var schedule = _context.PrimarySaCompany
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.SaCompanyDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.SaCompanyFunding)
        //                .Include(c => c.SaCompanyTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentList.Add(CreatePrimarySaCompanySchedule(schedule, bankValidation, clientUser, marketing));
        //            if (schedule.SaCompanyDetails.SoleShareholder == true)
        //            {
        //                documentList.Add(CreateSACompanySoleShareholderDeclaration(schedule, bankValidation, clientUser));
        //            }

        //            // Record Of Advise
        //            documentList.Add(CreateRecordOfAdvice(roa, schedule.SaCompanyDetails.RegisteredName, "", schedule.SaCompanyDetails.RegistrationNumber, address, advisor, risk, advise));

        //            if (associatedParty != null)
        //            {
        //                documentList.Add(CreateResolution(associate, address, bankValidation, clientUser));
        //                foreach (var contact in associatedParty.Contacts)
        //                {
        //                    UserModel associatedUser = _context.Users
        //                        .First(u => u.Id == contact.UserID);

        //                    //secondary schedule
        //                    documentList.Add(CreateSecondarySaCompanySchedule(schedule, bankValidation, clientUser, associatedUser, contact));
        //                }
        //                documentList.Add(CreateSACompanyEnterpriseGeneralDeclaration(schedule, associate, bankValidation, clientUser));

        //            }
        //        }

        //        else if (scheduleStep.ScheduleType == "Partnership")
        //        {
        //            var associate = associatedParty;

        //            var schedule = _context.PrimaryPartnership
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.PartnershipDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.PartnershipFunding)
        //                .Include(c => c.PartnershipTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentList.Add(CreatePrimaryPartnershipSchedule(schedule, bankValidation, clientUser, marketing));

        //            // Record Of Advise
        //            documentList.Add(CreateRecordOfAdvice(roa, schedule.PartnershipDetails.Name, "", "", address, advisor, risk, advise));

        //            if (associatedParty != null)
        //            {
        //                documentList.Add(CreateResolution(associate, address, bankValidation, clientUser));
        //                foreach (var contact in associatedParty.Contacts)
        //                {
        //                    UserModel associatedUser = _context.Users
        //                        .First(u => u.Id == contact.UserID);

        //                    //secondary schedule
        //                    documentList.Add(CreateSecondaryPartnershipSchedule(schedule, bankValidation, clientUser, associatedUser, contact));
        //                }
        //                documentList.Add(CreatePartnershipEnterpriseGeneralDeclaration(schedule, associate, bankValidation, clientUser));
        //            }
        //        }
        //        else if (scheduleStep.ScheduleType == "CC")
        //        {
        //            var associate = associatedParty;

        //            var schedule = _context.PrimaryCC
        //                .Where(c => c.ApplicationId == applicationId)
        //                .Include(c => c.CCDetails)
        //                    .ThenInclude(d => d.AddressItems)
        //                .Include(c => c.EntityDetails)
        //                .Include(c => c.CCFunding)
        //                .Include(c => c.CCTaxResidency)
        //                    .ThenInclude(c => c.TaxResidencyItems)
        //                .First();

        //            documentList.Add(CreatePrimaryCCSchedule(schedule, bankValidation, clientUser, marketing));

        //            // Record Of Advise
        //            documentList.Add(CreateRecordOfAdvice(roa, schedule.CCDetails.RegisteredName, "", schedule.CCDetails.RegistrationNumber, address, advisor, risk, advise));

        //            if (associatedParty != null)
        //            {
        //                documentList.Add(CreateResolution(associate, address, bankValidation, clientUser));
        //                foreach (var contact in associatedParty.Contacts)
        //                {
        //                    UserModel associatedUser = _context.Users
        //                        .First(u => u.Id == contact.UserID);

        //                    //secondary schedule
        //                    documentList.Add(CreateSecondaryCCSchedule(schedule, bankValidation, clientUser, associatedUser, contact));
        //                }
        //                documentList.Add(CreateCCEnterpriseGeneralDeclaration(schedule, associate, bankValidation, clientUser));
        //            }

        //        }

        //        // IRSW 8
        //        if (irsw8Exist)
        //        {
        //            documentList.Add(CreateIRSW8(irsw8, clientUser));
        //        }

        //        // IRSW 9
        //        if (irsw9Exist)
        //        {
        //            documentList.Add(CreateIRSW9(irsw9));
        //        }

        //        // Dividend

        //        if (advise.AdvisedProducts.First().Product != ProductsEnum.PrivateEquityFund)
        //        {         //remove Dividend for PE
        //            documentList.Add(kycExist == true ?
        //                CreateDividendTax(divTax, address, postal, client, kyc.FirstNames, kyc.SurName, kyc.IdNumber, applicationId) :
        //                CreateDividendTax(divTax, address, postal, client, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber, applicationId));
        //        }

        //    }
        //    else
        //    {
        //        // Switch Form
        //        documentList.Add(CreateSwitchForm(advisor, clientUser, switchClient, advise));

        //        // Switch FSP
        //        var fspMandate = _context.FspMandatates.First(c => c.ApplicationId == applicationId);

        //        documentList.Add(CreateSwitchFSP(advisor, clientUser, switchClient, advise, risk, bankValidation, fspMandate));
        //    }

        //    if (advisor.User.Role == Roles.External)
        //    {
        //        // Risk Profile
        //        documentList.Add(CreateLibertyRiskProfile(risk, advisor.User, advise, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber));
        //    }
        //    else
        //    {
        //        // Risk Profile
        //        documentList.Add(kycExist == true ?
        //            CreateRiskProfile(risk, advisor.User, advise, kyc.FirstNames, kyc.SurName, kyc.IdNumber) :
        //            CreateRiskProfile(risk, advisor.User, advise, clientUser.FirstName, clientUser.LastName, clientUser.IdNumber));
        //    }

        //    // FSP Mandate
        //    if (appl.Description != "SwitchPortfolio")
        //    {
        //        if (kycExist == true)
        //        {
        //            //documentList.Add(CreateFspMandate(kyc.FirstNames, kyc.SurName, risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));
        //            var fspMandate = _context.FspMandatates.First(c => c.ApplicationId == applicationId);
        //            documentList.Add(CreateFspMandate(appl.Description, null, kyc.FirstNames, kyc.SurName, "", risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));
        //        }
        //        else if (appl.Description != "Individual")
        //        {
        //            string entityName = string.Empty;
        //            string entityNumber = string.Empty;
        //            string vatNumber = string.Empty;

        //            if (appl.Description == "CC")
        //            {
        //                var schedule = _context.PrimaryCC
        //               .Where(c => c.ApplicationId == applicationId)
        //               .Include(c => c.CCDetails)
        //               .First();

        //                entityName = schedule.CCDetails.RegisteredName;
        //                entityNumber = schedule.CCDetails.RegistrationNumber;
        //                vatNumber = schedule.CCDetails.VATNumber;
        //            }
        //            else if (appl.Description == "Trust")
        //            {
        //                var schedule = _context.PrimaryTrusts
        //              .Where(c => c.ApplicationId == applicationId)
        //              .Include(c => c.TrustDetails)
        //              .First();

        //                entityName = schedule.TrustDetails.Name;
        //                entityNumber = schedule.TrustDetails.Number;
        //                vatNumber = "";
        //            }
        //            else if (appl.Description == "Partnership")
        //            {
        //                var schedule = _context.PrimaryPartnership
        //              .Where(c => c.ApplicationId == applicationId)
        //              .Include(c => c.PartnershipDetails)
        //              .First();

        //                entityName = schedule.PartnershipDetails.Name;
        //                entityNumber = "";
        //                vatNumber = "";
        //            }
        //            else if (appl.Description == "Company")
        //            {
        //                var schedule = _context.PrimarySaCompany
        //              .Where(c => c.ApplicationId == applicationId)
        //              .Include(c => c.SaCompanyDetails)
        //              .First();

        //                entityName = schedule.SaCompanyDetails.RegisteredName;
        //                entityNumber = schedule.SaCompanyDetails.RegistrationNumber;
        //                vatNumber = schedule.SaCompanyDetails.VATNumber;
        //            }

        //            //documentList.Add(CreateFspMandate(companyName, "", risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));
        //            var fspMandate = _context.FspMandatates.First(c => c.ApplicationId == applicationId);
        //            documentList.Add(CreateFspMandate(appl.Description, null, entityName, entityNumber, vatNumber, risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));

        //        }
        //        else if (advise.AdvisedProducts.First().Product != ProductsEnum.PrivateEquityFund)                  //remove FSP for PE
        //        {
        //            //documentList.Add(CreateFspMandate(clientUser.FirstName, clientUser.LastName, risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));

        //            var schedule = _context.PrimaryIndividuals
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.ContactDetails)
        //            .Include(c => c.ClientDetails)
        //                .ThenInclude(c => c.IdentityDetails)
        //            .Include(c => c.TaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .Include(c => c.PurposeAndFunding)
        //            .First();

        //            var fspMandate = _context.FspMandatates.First(c => c.ApplicationId == applicationId);
        //            documentList.Add(CreateFspMandate(appl.Description, schedule, clientUser.FirstName, clientUser.LastName, "", risk, fspMandate, address, clientUser, advisor.User, roa, bankValidation));
        //        }
        //    }

        //    // Nedbank T&C
        //    if (advise.AdvisedProducts.First().Product == ProductsEnum.StructuredNote)
        //    {
        //        documentList.Add(CreateTermsAndConditions(advisor, clientUser, address));
        //    }

        //    documentList.ForEach(doc =>
        //    {
        //        doc.ApplicationId = appl.Id;
        //        doc.B64Prefix = "data:application/pdf;base64";
        //        doc.Name =
        //            doc.DocumentType == DocumentTypesEnum.IntroLetter ?
        //                $"Intro Letter: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.DisclosureLetter ?
        //                $"Disclosure Letter: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.FinancialNeedsAnalysis ?
        //                $"Financial Needs Analysis: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.BankVerification ?
        //                $"Bank Validation Report: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.EnterpriseBOGuideline ?
        //                $"Enterprise BO Guideline: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.PrimarySchedule ?
        //                $"Primary Schedule, {appl.Description}: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.SecondarySchedule ?
        //                 doc.Name :
        //            doc.DocumentType == DocumentTypesEnum.RiskProfile ?
        //                $"Risk Profile: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.LibertyRiskProfile ?
        //                $"Risk Profile: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.RecordOfAdvice ?
        //                $"Record of Advice: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.FSPMandate ?
        //                $"FSP Mandate: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.MentonovaFSP ?
        //                $"FSP Mandate: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.StanlibSwitchForm ?
        //                $"Stanlib Portfolio Switch: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.Dividends ?
        //                $"Dividend Tax: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.FW8BENE ?
        //                $"IRS-W8-BEN-E: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.IRSW9E ?
        //                $"IRS-W9: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.TermsAndConditions_Nedbank ?
        //                $"Nedbank Terms and Conditions:  {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.SACompanyEnterpriseGeneralDeclaration ?
        //                $"Enterprise General Declaration, Company: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.TrustEnterpriseGeneralDeclaration ?
        //                $"Enterprise General Declaration, Trust: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.PartnershipEnterpriseGeneralDeclaration ?
        //                $"Enterprise General Declaration, Partnership: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.CCEnterpriseGeneralDeclaration ?
        //                $"Enterprise General Declaration, CC: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.Resolution ?
        //                $"Resolution: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.SACompanySoleShareholderDeclaration ?
        //                $"Sole Shareholder Declaration, Company: {documentName}.pdf" :
        //            doc.DocumentType == DocumentTypesEnum.DOA ?
        //                $"Deed of Adherence:  {documentName}.pdf" : null;

        //        if (appl.Documents.Any(c => c.DocumentType == doc.DocumentType))
        //            _context.ApplicationDocuments.Update(doc);
        //        else
        //            _context.ApplicationDocuments.Add(doc);

        //        _context.SaveChanges();
        //    });

        //    appl.DocumentsCreated = true;
        //    _context.Applications.Update(appl);
        //    _context.SaveChanges();
        //}

        //private ApplicationDocumentsModel CreateDisclosureLetter(Guid applicationId, AdvisorModel advisor, UserModel client, MarketingModel marketing)
        //{
        //    //DisclosuresRepo discRepo = new DisclosuresRepo(_context, _host, _config);

        //    //create new disclosure record
        //    DisclosuresModel newDisc = new DisclosuresModel()
        //    {
        //        ApplicationID = applicationId,
        //        Created = DateTime.UtcNow,
        //        Modified = DateTime.UtcNow,

        //        //Advisor Details
        //        AdvisorID = advisor.Id,

        //        //Marketing
        //        AlumaOffers = marketing.AlumaOffers,
        //        OtherOffers = marketing.OtherOffers,
        //        ReputableOrg = marketing.ReputableOrg,
        //        MethodOfCommunication = marketing.MethodOfCommunication,
        //        OtherMethodOfCommunication = marketing.OtherMethodOfCommunication,

        //        //Client details
        //        ClientName = client.FirstName,
        //        ClientSurname = client.LastName,
        //        ClientIDNumber = client.IdNumber,
        //        ClientEmail = client.Email,
        //        ClientMobile = client.MobileNumber,
        //        ClientCapacity = "Self",
        //        ClientSignature = client.Signature
        //    };

        //    //var adviser = _context.Advisors
        //    //    .Where(a => a.Id == disclosure.AdvisorID)
        //    //    .Include(a => a.BrokerDetails)
        //    //    .Include(a => a.User)
        //    //    .First();

        //    Dictionary<string, string> d = new Dictionary<string, string>();

        //    //Advisor Details
        //    d["advisor"] = advisor.User.FirstName + " " + advisor.User.LastName;
        //    d["dofa"] = advisor.AppointmentDate.ToString("yyyy/MM/dd") ?? string.Empty;

        //    //Physical Address
        //    d["resAddressLine1"] = advisor.BrokerDetails.ResidentialAddressLine1 ?? string.Empty;
        //    d["resAddressLine2"] = advisor.BrokerDetails.ResidentialAddressLine2 ?? string.Empty;
        //    d["resAddressLine3"] = advisor.BrokerDetails.ResidentialAddressLine3 ?? string.Empty;
        //    d["resAddressLine4"] = advisor.BrokerDetails.ResidentialAddressLine4 ?? string.Empty;
        //    d["resAddressPostalCode"] = advisor.BrokerDetails.ResidentialAddressPostalCode ?? string.Empty;

        //    //Postal Address
        //    d["postAddressLine1"] = advisor.BrokerDetails.PostalAddressLine1 ?? string.Empty;
        //    d["postAddressLine2"] = advisor.BrokerDetails.PostalAddressLine2 ?? string.Empty;
        //    d["postAddressLine3"] = advisor.BrokerDetails.PostalAddressLine3 ?? string.Empty;
        //    d["postAddressLine4"] = advisor.BrokerDetails.PostalAddressLine4 ?? string.Empty;
        //    d["postAddressPostalCode"] = advisor.BrokerDetails.PostalAddressPostalCode ?? string.Empty;

        //    //Contact Details
        //    d["adviserBusTel"] = advisor.BrokerDetails.BusinessTel ?? string.Empty;
        //    d["adviserHomeTel"] = advisor.BrokerDetails.HomeTel ?? string.Empty;
        //    d["adviserCell"] = advisor.User.MobileNumber ?? string.Empty;
        //    d["adviserFax"] = advisor.BrokerDetails.Fax ?? string.Empty;
        //    d["adviserEmail"] = advisor.User.Email ?? string.Empty;

        //    //Financial Services

        //    d["LongTermSubA_A"] = advisor.AdviceLTSubCatA ? "X" : null;

        //    d["LongTermSubA_S"] = advisor.SupervisedLTSubCatA ? "X" : null;

        //    d["ShortTermPersonal_A"] = advisor.AdviceSTPersonal ? "X" : null;

        //    d["ShortTermPersonal_S"] = advisor.SupervisedSTPersonal ? "X" : null;

        //    d["LongTermSubB1_A"] = advisor.AdviceLTSubCatB1 ? "X" : null; ;

        //    d["LongTermSubB1_S"] = advisor.SupervisedLTSubCatB1 ? "X" : null;

        //    d["LongTermSubC_A"] = advisor.AdviceLTSubCatC ? "X" : null;

        //    d["LongTermSubC_S"] = advisor.SupervisedLTSubCatC ? "X" : null;

        //    d["RetailPension_A"] = advisor.AdviceRetailPension ? "X" : null;

        //    d["RetailPension_S"] = advisor.SupervisedRetailPension ? "X" : null;

        //    d["ShortTermCommercial_A"] = advisor.AdviceSTCommercial ? "X" : null;

        //    d["ShortTermCommercial_S"] = advisor.SupervisedSTCommercial ? "X" : null;

        //    d["PensionFundsBenefits_A"] = advisor.AdvicePensionFunds ? "X" : null;

        //    d["PensionFundsBenefits_S"] = advisor.SupervisedPensionFunds ? "X" : null;

        //    d["Shares_A"] = advisor.AdviceShares ? "X" : null;

        //    d["Shares_S"] = advisor.SupervisedShares ? "X" : null;

        //    d["MoneyMarket_A"] = advisor.AdviceMoneyMarket ? "X" : null;

        //    d["MoneyMarket_S"] = advisor.SupervisedMoneyMarket ? "X" : null;

        //    d["Debentures_A"] = advisor.AdviceDebentures ? "X" : null;

        //    d["Debentures_S"] = advisor.SupervisedDebentures ? "X" : null;

        //    d["Warrants_A"] = advisor.AdviceWarrants ? "X" : null;

        //    d["Warrants_S"] = advisor.SupervisedWarrants ? "X" : null;

        //    d["Bonds_A"] = advisor.AdviceBonds ? "X" : null;

        //    d["Bonds_S"] = advisor.SupervisedBonds ? "X" : null;

        //    d["DerivativeInstruments_A"] = advisor.AdviceDerivatives ? "X" : null;

        //    d["DerivativeInstruments_S"] = advisor.SupervisedDerivatives ? "X" : null;

        //    d["CollectiveInvestmentScheme_A"] = advisor.AdviceParticipatoryInterestCollective ? "X" : null;

        //    d["CollectiveInvestmentScheme_S"] = advisor.SupervisedParticipatoryInterestCollective ? "X" : null;

        //    //d["MedicalAid_A"] = adviser.AdviceMedicalAid ? "X" : null;

        //    //d["MedicalAid_S"] = adviser.SupervisedMedicalAid ? "X" : null;

        //    d["LongTermDeposits_A"] = advisor.AdviceLTDeposits ? "X" : null;

        //    d["LongTermDeposits_S"] = advisor.SupervisedLTDeposits ? "X" : null;

        //    d["ShortTermDeposits_A"] = advisor.AdviceSTDeposits ? "X" : null;

        //    d["ShortTermDeposits_S"] = advisor.SupervisedSTDeposits ? "X" : null;

        //    d["LongTermSubB2_A"] = advisor.AdviceLTSubCatB2 ? "X" : null;

        //    d["LongTermSubB2_S"] = advisor.SupervisedLTSubCatB2 ? "X" : null;

        //    d["LongTermSubB2A_A"] = advisor.AdviceLTSubCatB2A ? "X" : null;

        //    d["LongTermSubB2A_S"] = advisor.SupervisedLTSubCatB2A ? "X" : null;

        //    d["LongTermSubB1A_A"] = advisor.AdviceLTSubCatB1A ? "X" : null;

        //    d["LongTermSubB1A_S"] = advisor.SupervisedLTSubCatB1A ? "X" : null;

        //    d["ShortTermPersonalA1_A"] = advisor.AdviceSTPersonalA1 ? "X" : null;

        //    d["ShortTermPersonalA1_S"] = advisor.SupervisedSTPersonalA1 ? "X" : null;

        //    d["StructuredDeposits_A"] = advisor.AdviceStructuredDeposits ? "X" : null;

        //    d["StructuredDeposits_S"] = advisor.SupervisedStructuredDeposits ? "X" : null;

        //    d["Securities_A"] = advisor.AdviceSecurities ? "X" : null;

        //    d["Securities_S"] = advisor.SupervisedSecurities ? "X" : null;

        //    d["HedgeFunds_A"] = advisor.AdviceParticipatoryInterestHedge ? "X" : null;

        //    d["HedgeFunds_S"] = advisor.SupervisedParticipatoryInterestHedge ? "X" : null;

        //    //CPA Options
        //    if (marketing.AlumaOffers)
        //        d["cpaDis1Y"] = "X";
        //    else
        //        d["cpaDis1N"] = "X";

        //    if (marketing.OtherOffers)
        //        d["cpaDis2Y"] = "X";
        //    else
        //        d["cpaDis2N"] = "X";

        //    if (marketing.ReputableOrg)
        //        d["cpaDis3Y"] = "X";
        //    else
        //        d["cpaDis3N"] = "X";

        //    if (marketing.MethodOfCommunication != null)
        //        d["comm" + marketing.MethodOfCommunication] = "X";

        //    if (marketing.OtherMethodOfCommunication)
        //        d["cpaDis4Y"] = "X";
        //    else
        //        d["cpaDis4N"] = "X";

        //    //Service Level Agreement
        //    d["clientName"] = client.FirstName + " " + client.LastName;
        //    d["clientID"] = client.IdNumber;
        //    d["clientCapacity"] = "self";

        //    //d["authUsers"] = disclosure.AuthorisedUsers ?? string.Empty;

        //    d["date"] = DateTime.Now.ToString("yyyy/MM/dd");
        //    //d["date2"] = DateTime.Now.ToString("yyyy/MM/dd");

        //    ////Broker Appointment
        //    //if (disclosure.AdvisorAuthority != null)
        //    //{
        //    //    if (!disclosure.AdvisorAuthority)
        //    //        d["authorityAll"] = "X";
        //    //    else
        //    //    {
        //    //        d["authoritySome"] = "X";
        //    //        d["authorityProducts"] = disclosure.AdvisorAuthorityProducts;
        //    //    }

        //    //    d["date2"] = DateTime.Now.ToString("yyyy/MM/dd");
        //    //}

        //    //BackgroundJob.Enqueue(() => discRepo.GenerateDisclosure(newDisc));
        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("DisclosureLetter.pdf", d),
        //        DocumentType = DocumentTypesEnum.DisclosureLetter
        //    };

        //}

        ////DOA
        //private ApplicationDocumentsModel CreateDOA(string application, PrimaryIndividualModel schedule, string entityName, string entityNumber, string entityVat,
        //    AddressDto address,
        //    UserModel client, UserModel adviser, AdvisorAdviseModel products, BankVerificationsModel bv)
        //{
        //    var d = new Dictionary<string, string>();
        //    var ClientDetails = schedule.ContactDetails;
        //    var Advisor = $"{adviser.FirstName} {adviser.LastName}";

        //    // applicant information
        //    d["nameSurname"] = $"{client.FirstName} {client.LastName}";
        //    d["individual"] = "x";
        //    //d["committedCapital"] = Product.AcceptedLumpSum.ToString();

        //    foreach (var product in products.AdvisedProducts)
        //    {
        //        //d[$"committedCapital"] = product.AcceptedLumpSum > 0 ?
        //        //    product.AcceptedLumpSum.ToString() : string.Empty;
        //        d[$"committedCapital"] = product.LumpSum.ToString();

        //    }

        //    d["address"] = $"{ClientDetails.UnitNumber} {ClientDetails.Complex}" + " "
        //           + $"{ClientDetails.StreetNumber} {ClientDetails.StreetName}" + ", " +
        //            $"{ClientDetails.Suburb}" + ", " +
        //            $"{ClientDetails.City} " + ", " +
        //            $"{ClientDetails.PostalCode}";
        //    d["country"] = schedule.ContactDetails.Country;

        //    d["taxpayer_True"] = "x";
        //    d["taxNo"] = schedule.TaxResidency.TaxNumber ?? " ";
        //    d["mobile"] = "0" + schedule.ContactDetails.MobileNumber;
        //    d["email"] = schedule.ContactDetails.Email;
        //    d["bank"] = bv.BankName;
        //    d["accountNo"] = bv.AccountNumber;
        //    d["accountHolder"] = $"{bv.Initials} {bv.Surname}";

        //    // signature
        //    d["onBehalfOf"] = "self";
        //    d["nameSurname_1"] = $"{client.FirstName} {client.LastName}";
        //    d["signDate_1"] = DateTime.Today.ToString("yyyy/MM/dd");
        //    d["signAt_1"] = ClientDetails.City;
        //    d["nameSurname_2"] = "";
        //    d["signDate_2"] = "";
        //    d["signAt_2"] = "";
        //    d["nameSurname_3"] = "";
        //    d["signDate_3"] = "";
        //    d["signAt_3"] = "";

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("DOA.pdf", d),
        //        DocumentType = DocumentTypesEnum.DOA
        //    };

        //}

        //private ApplicationDocumentsModel CreateLibertyRiskProfile(RiskProfileModel r, UserModel adviser, AdvisorAdviseModel advise, string firstName, string lastName, string idNumber)
        //{
        //    Dictionary<string, string> d = new Dictionary<string, string>();

        //    d["User"] = $"{firstName} {lastName}";
        //    d["IdNo"] = idNumber;
        //    d["Advisor"] = $"{adviser.FirstName ?? string.Empty} {adviser.LastName ?? string.Empty}";
        //    d["Created"] = DateTime.Today.ToString("yyyy/MM/dd");
        //    d["Goal"] = r.Goal;

        //    d[r.DerivedProfile] = "x";

        //    d["DerivedProfile"] = r.DerivedProfile;

        //    var agreeStr = r.AgreeWithOutcome == true ? "agree_True" : "agree_False";
        //    if (!r.AgreeWithOutcome)
        //    {
        //        d["Reason"] = r.Reason ?? string.Empty;
        //        d["agree_False"] = "x";
        //    }
        //    else
        //        d["agree_True"] = "x";
        //    d["Date"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    d[r.LibertyInvestmentTerm] = "x";
        //    d[r.LibertyRequiredRisk] = "x";
        //    d[r.LibertyRiskTolerance] = "x";
        //    d[r.LibertyRiskCapacity] = "x";

        //    double score = 0;
        //    score += r.LibertyRequiredRisk == "RequiredRisk_a" ? 1.25 : r.LibertyRequiredRisk == "RequiredRisk_b" ? 2.5 : 3.75;
        //    score += r.LibertyInvestmentTerm == "InvestTerm_a" ? 2.5 : r.LibertyInvestmentTerm == "InvestTerm_b" ? 10 : r.LibertyInvestmentTerm == "InvestTerm_c" ? 20 : r.LibertyInvestmentTerm == "InvestTerm_d" ? 26.5 : 42.5;
        //    score += r.LibertyRiskTolerance == "RiskTolerance_a" ? 1.25 : r.LibertyRiskTolerance == "RiskTolerance_b" ? 2.5 : 3.75;
        //    score += r.LibertyRiskCapacity == "RiskCapacity_a" ? 10 : r.LibertyRiskCapacity == "RiskCapacity_b" ? 20 : 30;

        //    d["Total"] = score.ToString();
        //    // advisor notes
        //    d["advisorNotes"] = r.AdvisorNotes ?? string.Empty;

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("LibertyRisk.pdf", d),
        //        DocumentType = DocumentTypesEnum.LibertyRiskProfile
        //    };
        //}

        ////SWITCH FSP
        //private ApplicationDocumentsModel CreateSwitchFSP(AdvisorModel adviser, UserModel clientUser, SwitchPortfolioClientModel switchClient, AdvisorAdviseModel advise, RiskProfileModel risk, BankVerificationsModel bankValidation, FSPMandateModel fsp)
        //{
        //    var d = new Dictionary<string, string>();
        //    var clientDetails = $"{switchClient.FirstNames} {switchClient.Surname}";
        //    var advisor = $"{adviser.User.FirstName} {adviser.User.LastName}";

        //    //used for all client name fields
        //    d["clientDetails"] = clientDetails;

        //    d["bankName"] = "";//bankValidation.BankName;
        //    d["bankBranch"] = "";//bankValidation.BranchCode;
        //    d["bankAccNo"] = "";//bankValidation.AccountNumber;

        //    //used for all date fields
        //    d["effectiveDate"] = DateTime.Now.ToString("yyyy/MM/dd");

        //    //d["addressLine1"] = $"{switchClient.ResidentialAddressLine1}";
        //    //d["addressLine2"] = $"{switchClient.ResidentialAddressLine2}, {switchClient.ResidentialAddressLine3}";
        //    //d["addressLine3"] = $"{switchClient.ResidentialAddressLine4}, {switchClient.ResidentialAddressPostalCode}";
        //    d["clientEmail"] = $"{clientUser.Email}";

        //    d["atFsp"] = switchClient.ResidentialAddressLine3;

        //    d[fsp.Objective.ToString()] = "x";
        //    d[$"{risk.DerivedProfile}"] = "x";

        //    d["static"] = "x";

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("MentenovaFSP.pdf", d),
        //        DocumentType = DocumentTypesEnum.MentonovaFSP
        //    };
        //}

        ////SWITCH FORM
        //private ApplicationDocumentsModel CreateSwitchForm(AdvisorModel advisor, UserModel clientUser, SwitchPortfolioClientModel switchClient, AdvisorAdviseModel advise) // AdvisorAdvisedProductsModel advise)
        //{
        //    var d = new Dictionary<string, string>();

        //    int i = 0;
        //    foreach (char c in switchClient.InvestmentAccountNumber)
        //    {
        //        d[$"accNumber_{i}"] = c.ToString();
        //        i++;
        //    }

        //    i = 0;
        //    foreach (char c in switchClient.FirstNames + " " + switchClient.Surname)
        //    {
        //        d[$"name_{i}"] = c.ToString();
        //        i++;
        //    }

        //    i = 0;
        //    foreach (char c in clientUser.IdNumber)
        //    {
        //        d[$"id_{i}"] = c.ToString();
        //        i++;
        //    }

        //    i = 0;
        //    foreach (char c in clientUser.IdNumber)
        //    {
        //        d[$"id_{i}"] = c.ToString();
        //        i++;
        //    }

        //    d["cellNo_0"] = "0";
        //    i = 1;
        //    foreach (char c in clientUser.MobileNumber)
        //    {
        //        d[$"cellNo_{i}"] = c.ToString();
        //        i++;
        //    }

        //    i = 0;
        //    foreach (char c in clientUser.Email)
        //    {
        //        d[$"email_{i}"] = c.ToString();
        //        i++;
        //    }

        //    var e = advise.AdvisedProducts.First();
        //    d[$"inst{e.SwitchInstruction}"] = "x";

        //    i = 0;
        //    double totalPerc = 0;
        //    foreach (var v in e.SwitchAllocations)
        //    {
        //        d[$"porfolio{e.SwitchInstruction}_{i}"] = v.PortfolioName;
        //        d[$"perc{e.SwitchInstruction}_{i}"] = v.PortfolioPercentage.ToString();
        //        totalPerc = totalPerc + v.PortfolioPercentage;

        //        if (e.SwitchInstruction != "Rebalance")
        //        {
        //            d[$"amt{e.SwitchInstruction}_{i}"] = v.PortfolioAmount.ToString();
        //        }
        //        i++;
        //    }
        //    d[$"perc{e.SwitchInstruction}_total"] = totalPerc.ToString();

        //    i = 0;
        //    foreach (char c in DateTime.Today.ToString("ddMMyyyy"))
        //    {
        //        d[$"clientDate_0{i}"] = c.ToString();
        //        i++;
        //    }
        //    i = 0;
        //    foreach (char c in DateTime.Today.ToString("ddMMyyyy"))
        //    {
        //        d[$"adviserDate_0{i}"] = c.ToString();
        //        i++;
        //    }

        //    d["clientAt"] = switchClient.ResidentialAddressLine3;
        //    d["adviserAt"] = "Pretoria";
        //    d["capacity"] = "self";

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("StanlibSwitchForm.pdf", d),
        //        DocumentType = DocumentTypesEnum.StanlibSwitchForm
        //    };
        //}

        ////TERMS AND CONDITIONS
        //private ApplicationDocumentsModel CreateTermsAndConditions(AdvisorModel advisor, UserModel clientUser, AddressDto address)
        //{
        //    //throw new NotImplementedException();

        //    var d = new Dictionary<string, string>();

        //    d["name"] = $"{clientUser.FirstName} {clientUser.LastName}";
        //    d["id"] = clientUser.IdNumber;
        //    d["signedAtClient"] = address.City;
        //    d["signedAtDistributor"] = "Pretoria";
        //    d["signedDateClient"] = DateTime.Today.ToString("dd/MM/yyyy");
        //    d["signedDateAdviser"] = DateTime.Today.ToString("dd/MM/yyyy");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("TermsAndConditions_Nedbank.pdf", d),
        //        DocumentType = DocumentTypesEnum.TermsAndConditions_Nedbank
        //    };
        //}

        ////IRSW9
        //private ApplicationDocumentsModel CreateIRSW9(IRSW9Model irsw9)
        //{
        //    //throw new NotImplementedException();

        //    var d = new Dictionary<string, string>();

        //    d["name"] = irsw9.Name;
        //    d["businessName"] = irsw9.BusinessName;

        //    if (irsw9.FederalTaxClass == "Individual")
        //    {
        //        d["classification_1"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "C Corporation")
        //    {
        //        d["classification_2"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "S Corporation")
        //    {
        //        d["classification_3"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "Partnership")
        //    {
        //        d["classification_4"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "Trust")
        //    {
        //        d["classification_5"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "Limited liability company")
        //    {
        //        d["classification_6"] = "x";
        //    }
        //    if (irsw9.FederalTaxClass == "Other")
        //    {
        //        d["classification_7"] = "x";
        //    }

        //    d["companyOption"] = irsw9.LimitedTaxClass;
        //    d["other"] = irsw9.OtherFederal;
        //    d["exemptionCode"] = irsw9.ExemptionCode;
        //    d["fatca"] = irsw9.FatcaCode;
        //    d["address1"] = irsw9.Address1;
        //    d["address2"] = irsw9.Address2;
        //    d["accountNumbers"] = irsw9.AccountNumbers;
        //    d["date"] = DateTime.Now.ToString("yyyy/MM/dd");

        //    int i = 0;
        //    foreach (char tin in irsw9.SocialSecurity)
        //    {
        //        d[$"tin1_{i}"] = tin.ToString();
        //        i++;
        //    }

        //    i = 0;
        //    foreach (char tin in irsw9.EmployerIdNumber)
        //    {
        //        d[$"tin2_{i}"] = tin.ToString();
        //        i++;
        //    }

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("IRSW9.pdf", d),
        //        DocumentType = DocumentTypesEnum.IRSW9E
        //    };
        //}

        ////IRSW8
        //private ApplicationDocumentsModel CreateIRSW8(IRSW8Model irsw8, UserModel clientUser)
        //{
        //    //throw new NotImplementedException();
        //    var d = new Dictionary<string, string>();

        //    //part 1
        //    d["businessName"] = irsw8.BusinessName;
        //    d["country"] = irsw8.ResidentialAddressCountry;
        //    d["entityName"] = irsw8.DisregardedName;

        //    d[irsw8.TreatyClaim == true ? "hybgridY" : "hybgridN"] = "x";

        //    d["static"] = "x";

        //    if (irsw8.EntitySelectedType == "Corporation")
        //    {
        //        d["entityCorporation"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "DisregardedEntity")
        //    {
        //        d["entityDisregarded"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "Partnership")
        //    {
        //        d["entityPartnership"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "SimpleTrust")
        //    {
        //        d["entitySimpleTrust"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "GrantorTrust")
        //    {
        //        d["entityGrantorTrust"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "ComplexTrust")
        //    {
        //        d["entityComplexTrust"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "Estate")
        //    {
        //        d["entityEstate"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "Government")
        //    {
        //        d["entityGovernment"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "CentralBank")
        //    {
        //        d["entityCentralBank"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "TaxExempt")
        //    {
        //        d["entityTaxExempt"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "PrivateFoundation")
        //    {
        //        d["entityPrivate"] = "x";
        //    }
        //    if (irsw8.EntitySelectedType == "InternationalOrg")
        //    {
        //        d["entityInternational"] = "x";
        //    }

        //    if (irsw8.FatcaSelectedStatus == "NonParticipatingFFI")
        //    {
        //        d["fatcaNonparticipate"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ParticipatingFFI")
        //    {
        //        d["fatcaParticipate"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ReportingModel1FFI")
        //    {
        //        d["fatcaModel1"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ReportingModel2FFI")
        //    {
        //        d["fatcaModel2"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "RegisteredDeemedCompliantFFI")
        //    {
        //        d["fatcaDeemedCompliantFFI"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "SponsoredFFI")
        //    {
        //        d["fatcaSponsored"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "CertifiedCompliantNonReg")
        //    {
        //        d["fatcaDeemedCompliantBank"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "CertifiedCompliantLowValue")
        //    {
        //        d["fatcaLowValue"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "CertifiedCompliantInvestmentVehicle")
        //    {
        //        d["fatcaCloselyHeld"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "CertifiedCompliantLimitedLife")
        //    {
        //        d["fatcaLimitedLife"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "NotMaintainFinAccountsEntity")
        //    {
        //        d["fatcaNoFinancialAccounts"] = "x";
        //    }
        //    if
        //    (irsw8.FatcaSelectedStatus == "OwnerDocumentedFFI")
        //    {
        //        d["fatcaOwnerDocumented"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "RestrictedDistributor")
        //    {
        //        d["fatcaRestricted"] = "x";
        //    }

        //    if (irsw8.FatcaSelectedStatus == "NonReportingFFI")
        //    {
        //        d["fatcaNonreportIGA"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ForeignGov")
        //    {
        //        d["fatcaForeignGovernment"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "InternationalOrg")
        //    {
        //        d["fatcaInternational"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExemptRetirementPlan")
        //    {
        //        d["fatcaExemptRetirement"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "WhollyOwnedEntity")
        //    {
        //        d["fatcaWhollyOwned"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "TerritoryFI")
        //    {
        //        d["fatcaTerritory"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExceptedNonFinGroup")
        //    {
        //        d["fatcaGroupEntity"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExceptedNonFinStartUp")
        //    {
        //        d["fatcaStartupCompany"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExceptedNonFinLiquidating")
        //    {
        //        d["fatcaLiquidation"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "Org501")
        //    {
        //        d["fatca501"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "NonProfit")
        //    {
        //        d["fatcaNonprofit"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "PubliclyTradedNFFE")
        //    {
        //        d["fatcaPubliclyTraded"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExceptedTerritoryNFFE")
        //    {
        //        d["fatcaExceptedTerritory"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ActiveNFFE")
        //    {
        //        d["fatcaActiveNFFE"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "PassiveNFFE")
        //    {
        //        d["fatcaPassiveNFFE"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "ExceptedInterAffiliated")
        //    {
        //        d["fatcaInterAffiliate"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "DirectReporting")
        //    {
        //        d["fatcaDirectReporting"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "SponsorDirectReporting")
        //    {
        //        d["fatcaSponsoredDirect"] = "x";
        //    }
        //    if (irsw8.FatcaSelectedStatus == "NotFinAccount")
        //    {
        //        d["fatcaNonFinancialAccount"] = "x";
        //    }

        //    d["resAddress1"] = irsw8.ResidentialAddress1;
        //    d["resAddress2"] = irsw8.ResidentialAddress2;
        //    d["resCountry"] = irsw8.ResidentialAddressCountry;
        //    d["postalAddress1"] = irsw8.PostalAddress1;
        //    d["postalAddress2"] = irsw8.PostalAddress2;
        //    d["postalCountry"] = irsw8.PostalAddressCountry;

        //    d["TIN"] = irsw8.TIN;
        //    d["GIIN"] = irsw8.GIIN;
        //    d["foreignTIN"] = irsw8.ForeignTIN;
        //    d["reference"] = irsw8.ReferenceNumber;

        //    if (irsw8.DisregardedFatcaStatus == "BranchNonParticipatingFFI")
        //    {
        //        d["fatcaBranchNonparticipating"] = "x";
        //    }
        //    if (irsw8.DisregardedFatcaStatus == "ReportingModel1FFI")
        //    {
        //        d["fatcaReportingModel1"] = "x";
        //    }
        //    if (irsw8.DisregardedFatcaStatus == "ReportingModel2FFI")
        //    {
        //        d["fatcaReportingModel2"] = "x";
        //    }
        //    if (irsw8.DisregardedFatcaStatus == "UsBranch")
        //    {
        //        d["fatcaUSBranch"] = "x";
        //    }
        //    if (irsw8.DisregardedFatcaStatus == "ParticipatingFFI")
        //    {
        //        d["fatcaPartcipatingFFI"] = "x";
        //    }

        //    //part 2
        //    d["disregardedAddress1"] = irsw8.DisregardedResidentialAddress1;
        //    d["disregardedAddress2"] = irsw8.DisregardedResidentialAddress2;
        //    d["disregardedCountry"] = irsw8.DisregardedResidentialAddressCountry;
        //    d["disregardedGIIN"] = irsw8.DisregardedGIIN;

        //    //part 3
        //    d["treatyCountry"] = irsw8.TaxTreatyAddress;

        //    if (irsw8.TaxTreatyCheck1)
        //    {
        //        d["treatyResident"] = "x";
        //    }
        //    if (irsw8.TaxTreatyCheck2)
        //    {
        //        d["treatyItemRequirements"] = "x";
        //    }
        //    if (irsw8.TaxTreatyCheck3)
        //    {
        //        d["treatyResidentStatus"] = "x";
        //    }

        //    if (irsw8.TaxTreatySelectedBenefit == "Government")
        //    {
        //        d["treatyGovernment"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "TaxExemptPension")
        //    {
        //        d["treatyPension"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "OtherTaxExempt")
        //    {
        //        d["treatyOtherExempt"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "PubliclyTradedCorp")
        //    {
        //        d["treatyTraded"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "SubsidiaryPubliclyTradedCorp")
        //    {
        //        d["treatySubsidiary"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "ErosionTestCompany")
        //    {
        //        d["treatyOwnershipTest"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "DerivativeTestCompany")
        //    {
        //        d["treatyDerivativeTest"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "BusinessTestCompany")
        //    {
        //        d["treatyTradeTest"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "USCompetentCompany")
        //    {
        //        d["treatyFavorable"] = "x";
        //    }
        //    if (irsw8.TaxTreatySelectedBenefit == "Other")
        //    {
        //        d["treatyOther"] = "x";
        //    }

        //    d["treatyOtherSpecify"] = irsw8.TaxTreatySelectedBenefitOther;

        //    d["treatyParagraph"] = irsw8.TaxTreatyClaimArticle;
        //    d["withholdRate"] = irsw8.TaxTreatyClaimPercent;
        //    d["withholdIncome"] = irsw8.TaxTreatyClaimIncome;
        //    d["withholdConditions"] = irsw8.WithholdingConditions;

        //    //part 4
        //    d["sponsorEntityFFI"] = irsw8.SponsorEntityName;

        //    if (irsw8.SponsorEntityRadio == "1")
        //        d["certifyIV1"] = "x";
        //    else if (irsw8.SponsorEntityRadio == "2")
        //        d["certifyIV2"] = "x";

        //    //part 5 - 6
        //    if (irsw8.CertifiedNonRegCheck1)
        //        d["certifyV"] = "x";
        //    if (irsw8.CertifiedLowValueCheck1)
        //        d["certifyVI"] = "x";

        //    //part 7
        //    if (irsw8.CertifiedInvestVehicleCheck1)
        //        d["certifyVII"] = "x";

        //    d["sponsorEntityCHIV"] = irsw8.CertifiedInvestVehicleSponsorName;

        //    //part 8 - 9
        //    if (irsw8.CertifiedInvestVehicleCheck1)
        //        d["certifyVIII"] = "x";
        //    if (irsw8.InvestmentEntityNotAccountsCheck1)
        //        d["certifyIX"] = "x";

        //    //part 10
        //    if (irsw8.OwnerDocumentedCheck1)
        //        d["certifyX1"] = "x";

        //    if (irsw8.OwnerDocumentedRadio == "1")
        //        d["certifyX2"] = "x";
        //    else if (irsw8.OwnerDocumentedRadio == "2")
        //        d["certifyX3"] = "x";

        //    if (irsw8.OwnerDocumentedCheck2)
        //        d["certifyX4"] = "x";

        //    //part 11
        //    if (irsw8.RestrictedDistributorCheck1)
        //        d["certifyXI1"] = "x";

        //    if (irsw8.RestrictedDistributorRadio == "1")
        //        d["certifyXI2"] = "x";
        //    else if (irsw8.RestrictedDistributorRadio == "2")
        //        d["certifyXI3"] = "x";

        //    //part 12
        //    if (irsw8.NonReportingCheck1)
        //        d["certifyXII"] = "x";

        //    d["nonReportingCountry"] = irsw8.NonReportingCountry;
        //    if (irsw8.NonReportingRadio1 == "model1IGA")
        //    {
        //        d["model1IGA"] = "x";
        //    }
        //    if (irsw8.NonReportingRadio1 == "model2IGA")
        //    {
        //        d["model2IGA"] = "x";
        //    }

        //    d["treatedIGA"] = irsw8.NonReportingTreatedAs;
        //    d["trustee"] = irsw8.NonReportingTrusteeName;

        //    if (irsw8.NonReportingRadio2 == "trusteeUS")
        //    {
        //        d["trusteeUS"] = "x";
        //    }
        //    if (irsw8.NonReportingRadio1 == "trusteeForeign")
        //    {
        //        d["trusteeForeign"] = "x";
        //    }

        //    //part 13
        //    if (irsw8.ForeignGovCheck1)
        //        d["certifyXIII"] = "x";

        //    //part 14
        //    if (irsw8.InternationalOrgRadio == "1")
        //        d["certifyXIV1"] = "x";
        //    else if (irsw8.InternationalOrgRadio == "2")
        //        d["certifyXIV2"] = "x";

        //    //part 15
        //    if (irsw8.ExemptRetirementRadio == "1")
        //        d["certifyXV1"] = "x";
        //    else if (irsw8.ExemptRetirementRadio == "2")
        //        d["certifyXV2"] = "x";
        //    else if (irsw8.ExemptRetirementRadio == "3")
        //        d["certifyXV3"] = "x";
        //    else if (irsw8.ExemptRetirementRadio == "4")
        //        d["certifyXV4"] = "x";
        //    else if (irsw8.ExemptRetirementRadio == "5")
        //        d["certifyXV5"] = "x";
        //    else if (irsw8.ExemptRetirementRadio == "6")
        //        d["certifyXV6"] = "x";

        //    //part 16 - 18
        //    if (irsw8.ExemptBeneficialOwnerCheck1)
        //        d["certifyXVI"] = "x";
        //    if (irsw8.TerritoryFinancialInstituteCheck1)
        //        d["certifyXVII"] = "x";
        //    if (irsw8.ExceptedNonFinancialGroupCheck1)
        //        d["certifyXVIII"] = "x";

        //    //part 19
        //    if (irsw8.ExceptedNonFinancialStartUpCheck1)
        //        d["certifyXIX"] = "x";

        //    d["entityFormedDate"] = irsw8.ExceptedNonFinancialStartUpDate;

        //    //part 20
        //    if (irsw8.ExceptedNonFinancialLiquidationCheck1)
        //        d["certifyXX"] = "x";

        //    d["entityBankruptDate"] = irsw8.ExceptedNonFinancialLiquidationDate;

        //    //part 21
        //    if (irsw8.Organization501Check1)
        //        d["certifyXXI"] = "x";

        //    d["entityIRSDate"] = irsw8.Organization501Date;

        //    //part 22
        //    if (irsw8.NonProfitCheck1)
        //        d["certifyXXII"] = "x";

        //    //part 23
        //    if (irsw8.PublicTradedNFFERadio == "1")
        //        d["certifyXXIII1"] = "x";
        //    else if (irsw8.PublicTradedNFFERadio == "2")
        //        d["certifyXXIII2"] = "x";

        //    d["securityExchange"] = irsw8.SecuritiesMarket;
        //    d["entityStockName"] = irsw8.AffiliateEntityName;
        //    d["securityMarket"] = irsw8.AffiliateSecurityExchange;

        //    //part 24 - 25
        //    if (irsw8.ExceptedTerritoryNFFECheck1)
        //        d["certifyXXIV"] = "X";
        //    if (irsw8.ActiveNFFECheck1)
        //        d["certifyXXV"] = "X";
        //    //d["certifyXXIV"] = irsw8.ExceptedTerritoryNFFECheck1; // "x";
        //    //d["certifyXXV"] = irsw8.ActiveNFFECheck1; // "x";

        //    //part 26
        //    if (irsw8.PassiveNFFECheck1)
        //        d["certifyXXVI1"] = "x";

        //    if (irsw8.PublicTradedNFFERadio == "1")
        //        d["certifyXXVI2"] = "x";
        //    else if (irsw8.PublicTradedNFFERadio == "2")
        //        d["certifyXXVI3"] = "x";

        //    //part 27
        //    if (irsw8.ExceptedInterAffiliatedCheck1)
        //        d["certifyXXVII"] = "x";

        //    //part 28
        //    if (irsw8.SponsoredDirectReportNFFECheck1)
        //        d["certifyXXVIII"] = "x";

        //    d["sponsorEntityNFFE"] = irsw8.SponsoredDirectReportNFFEName;

        //    //part 29
        //    int c = 0;
        //    foreach (var e in irsw8.USPersons)
        //    {
        //        c++;
        //        d[$"name_{c}"] = e.UsOwnerPassiveNFFEName ?? string.Empty;
        //        d[$"tin_{c}"] = e.UsOwnerPassiveNFFETin ?? string.Empty;
        //        d[$"address_{c}"] = e.UsOwnerPassiveNFFEAddress1 ?? string.Empty;
        //    }

        //    d["name"] = clientUser.FirstName + " " + clientUser.LastName;
        //    d["date"] = DateTime.Today.ToString("yyyy/MM/dd"); ;

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("FW8BENE.pdf", d),
        //        DocumentType = DocumentTypesEnum.FW8BENE
        //    };
        //}

        ////DIVIDEND TAX
        //private ApplicationDocumentsModel CreateDividendTax(DividendTaxModel dividend, AddressDto address, AddressDto postal,
        //    ClientDto client, string firstName, string lastName, string idNumber, Guid applicationId)
        //{
        //    var d = new Dictionary<string, string>();

        //    d["nameSurname"] = dividend.NameSurname; //removed new doc
        //    //d["tradingName"] = dividend.TradingName;
        //    d["static"] = "x";
        //    d["investorName"] = dividend.TradingName != null ? dividend.NameSurname + " " + dividend.TradingName : dividend.NameSurname;
        //    d["accountReference"] = dividend.AccountReference ?? string.Empty;
        //    d[dividend.NatureOfEntity] = "x";
        //    d["idNo"] = idNumber;
        //    d["nationality"] = address.Country;

        //    d["dateOfBirth"] = client.DateOfBirth;
        //    d["taxNo"] = client.TaxNumber;
        //    d["address"] = $"{address.UnitNumber} {address.Complex}" + " "
        //           + $"{address.StreetNumber} {address.StreetName}" + ", " +
        //            $"{address.Suburb}" + ", " +
        //            $"{address.City} " + ", " +
        //            $"{address.PostalCode}";

        //    if (dividend.NatureOfEntity == "Individual")
        //    {
        //        d["postal"] = $"{postal.UnitNumber} {postal.Complex}" + " "
        //            + $"{postal.StreetNumber} {postal.StreetName}" + ", " +
        //            $"{postal.Suburb}" + ", " +
        //            $"{postal.City} " + ", " +
        //            $"{postal.PostalCode}";
        //        d["exemption_8"] = "x";
        //    }

        //    if (dividend.NatureOfEntity == "Partnership")
        //    {
        //        var schedule = _context.PrimaryPartnership
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.PartnershipDetails)
        //                .ThenInclude(d => d.AddressItems)
        //            .Include(c => c.EntityDetails)
        //            .Include(c => c.PartnershipTaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .First();

        //        var postalAddress = schedule.PartnershipDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Postal).FirstOrDefault();

        //        d["postal"] = $"{postalAddress.StreetNumber} {postalAddress.StreetName}" + ", " +
        //        $"{postalAddress.Suburb}" + ", " +
        //        $"{postalAddress.City} " + ", " +
        //        $"{postalAddress.PostalCode}";

        //        d["Other"] = "x";
        //        d["natureOther"] = "Partnership";

        //    }

        //    if (dividend.NatureOfEntity == "CC")
        //    {
        //        var schedule = _context.PrimaryCC
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.CCDetails)
        //                .ThenInclude(d => d.AddressItems)
        //            .Include(c => c.EntityDetails)
        //            .Include(c => c.CCTaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .First();

        //        var postalAddress = schedule.CCDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Postal).FirstOrDefault();

        //        d["postal"] = $"{postalAddress.StreetNumber} {postalAddress.StreetName}" + ", " +
        //        $"{postalAddress.Suburb}" + ", " +
        //        $"{postalAddress.City} " + ", " +
        //        $"{postalAddress.PostalCode}";

        //        d["Other"] = "x";
        //        d["natureOther"] = "Close Corporation";

        //    }

        //    if (dividend.NatureOfEntity == "Company")
        //    {
        //        var schedule = _context.PrimarySaCompany
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.SaCompanyDetails)
        //                .ThenInclude(d => d.AddressItems)
        //            .Include(c => c.EntityDetails)
        //            .Include(c => c.SaCompanyTaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .First();

        //        var postalAddress = schedule.SaCompanyDetails.AddressItems.Where(c => c.Type == AddressTypesEnum.Postal).FirstOrDefault();

        //        d["postal"] = $"{postalAddress.StreetNumber} {postalAddress.StreetName}" + ", " +
        //        $"{postalAddress.Suburb}" + ", " +
        //        $"{postalAddress.City} " + ", " +
        //        $"{postalAddress.PostalCode}";

        //        if (schedule.SaCompanyDetails.SaCompanyType == "ListedSA" || schedule.SaCompanyDetails.SaCompanyType == "OwnedSubsidiaryListedSA")
        //        {
        //            d["ListedCompany"] = "x";
        //        }
        //        else if (schedule.SaCompanyDetails.SaCompanyType == "NonListedSAPreMay" || schedule.SaCompanyDetails.SaCompanyType == "NonListedSAPostMay")
        //        {
        //            d["UnlistedCompany"] = "x";
        //        }
        //        else if (schedule.SaCompanyDetails.SaCompanyType == "NPOPreMay" || schedule.SaCompanyDetails.SaCompanyType == "NPOPostMay")
        //        {
        //            d["Other"] = "x";
        //            d["natureOther"] = "Non-profit company";
        //        };

        //    }

        //    d["country"] = address.Country;
        //    d["sigCap1"] = "Self";
        //    d["sigDate1"] = DateTime.Now.ToString("yyyy/MM/dd");

        //    if (dividend.NatureOfEntity != "Individual")
        //    {
        //        d["titleSurname"] = $"{dividend.Title} {dividend.Surname}";
        //        d["initialsFirstName"] = $"{dividend.Initials} {dividend.FirstName}";
        //        d["idNoPassport"] = dividend.IdNoPassport;
        //        d["work"] = dividend.Work ?? string.Empty;
        //        d["home"] = dividend.Home ?? string.Empty;
        //        d["mobile"] = "0" + dividend.Mobile ?? string.Empty;
        //        d["email"] = dividend.Email ?? string.Empty;
        //        d["sigName1"] = $"{firstName} {lastName}";

        //        if (dividend.Exemption == "exemption_1")
        //            d["exemption_1"] = "x";

        //        else if (dividend.Exemption == "exemption_2")
        //            d["exemption_2"] = "x";

        //        else if (dividend.Exemption == "exemption_3")
        //            d["exemption_3"] = "x";

        //        else if (dividend.Exemption == "exemption_4")
        //            d["exemption_4"] = "x";

        //        else if (dividend.Exemption == "exemption_5")
        //            d["exemption_5"] = "x";

        //        else if (dividend.Exemption == "exemption_6")
        //            d["exemption_6"] = "x";

        //        else if (dividend.Exemption == "exemption_7")
        //            d["exemption_7"] = "x";

        //        else if (dividend.Exemption == "exemption_8")
        //            d["exemption_8"] = "x";

        //        else if (dividend.Exemption == "exemption_9")
        //            d["exemption_9"] = "x";

        //        else if (dividend.Exemption == "exemption_10")
        //            d["exemption_10"] = "x";

        //    }

        //    else if (dividend.NatureOfEntity == "Individual")
        //    {
        //        var schedule = _context.PrimaryIndividuals
        //            .Where(c => c.ApplicationId == applicationId)
        //            .Include(c => c.ContactDetails)
        //            .Include(c => c.ClientDetails)
        //                .ThenInclude(c => c.IdentityDetails)
        //            .Include(c => c.TaxResidency)
        //                .ThenInclude(c => c.TaxResidencyItems)
        //            .Include(c => c.PurposeAndFunding)
        //            .First();

        //        var clientDetails = schedule.ClientDetails;
        //        var contactDetails = schedule.ContactDetails;

        //        d["titleSurname"] = $"{clientDetails.Title} {clientDetails.Surname}";
        //        d["initialsFirstName"] = $"{clientDetails.Initials} {clientDetails.FirstNames}";
        //        d["idNoPassport"] = idNumber;
        //        d["work"] = contactDetails.WorkNumber ?? string.Empty;
        //        //d["home"] = contactDetails. ?? string.Empty;
        //        d["mobile"] = "0" + contactDetails.MobileNumber ?? string.Empty;
        //        d["email"] = contactDetails.Email ?? string.Empty;
        //    }

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("Dividend.pdf", d),
        //        DocumentType = DocumentTypesEnum.Dividends
        //    };
        //}

        ////OLD FSP MANDATE
        ////private ApplicationDocumentsModel CreateFspMandate(string firstname, string lastname,
        ////    RiskProfileModel risk, FSPMandateModel man, AddressDto address,
        ////    UserModel client, UserModel adviser, RecordOfAdviseModel roa, BankVerificationsModel bv)
        ////{
        ////    var d = new Dictionary<string, string>();
        ////    var clientDetails = $"{firstname} {lastname}";
        ////    var Advisor = $"{adviser.FirstName} {adviser.LastName}";

        ////    d["ClientDetails_a"] = clientDetails;

        ////    var p2 = string.Empty;
        ////    foreach (var p in roa.SelectedProducts)
        ////        p2 += p.ProductName;
        ////    d["Products"] = p2;

        ////    d["Bank"] = bv.BankName;
        ////    d["Branch"] = bv.BranchCode;
        ////    d["AccNo"] = bv.AccountNumber;

        ////    d["StartDate"] = DateTime.Now.ToString("yyyy/MM/dd");

        ////    d["Address_1"] = $"{address.UnitNumber} {address.Complex}";
        ////    d["Address_2"] = $"{address.StreetNumber} {address.StreetName}," +
        ////        $"{address.Suburb} {address.PostalCode}";
        ////    d["Address_3"] = $"{address.City} {address.Country}";
        ////    d["Email"] = client.Email;
        ////    d["FspSignatory"] = Advisor;
        ////    d["ClientDetails_b"] = clientDetails;

        ////    // location
        ////    d["dateFsp"] = DateTime.Now.ToString("yyyy/MM/dd");
        ////    d["dateClient"] = DateTime.Now.ToString("yyyy/MM/dd");
        ////    d["atFsp"] = "Pretoria";//adviser.BrokerDetails.City;
        ////    d["atClient"] = address.City;
        ////    //d["LAt"] = adviser.BrokerDetails.City;
        ////    //d["FAt"] = adviser.BrokerDetails.City;

        ////    // full / limited discretion
        ////    d[man.Objective.ToString()] = "X";
        ////    d[$"{man.Objective[0]}{risk.DerivedProfile}"] = "X";
        ////    d["instruction_personal"] = man.InstructionPersonal ?? string.Empty;
        ////    d["instruction_advisor"] = man.InstructionAdvisor ?? string.Empty;
        ////    d["Adviser"] = man.Advisor ?? string.Empty;
        ////    d["instruction_fsp"] = man.InstructionFsp ?? string.Empty;
        ////    d[man.PayoutOption] = "X";

        ////    // client details d
        ////    d[$"{man.Objective[0]}ClientDetails"] = clientDetails;
        ////    d[$"{man.Objective[0]}At"] = address.City;//"Pretoria";//adviser.BrokerDetails.City;
        ////    d[$"{man.Objective[0]}Date"] = DateTime.Now.ToString("yyyy/MM/dd");

        ////    var na = man.Objective[0].ToString() == "L" ? "LNA" : "FNA";
        ////    d[na] = "N/A";

        ////    return new ApplicationDocumentsModel()
        ////    {
        ////        DocumentData = PopulateDocument("FspMandate.pdf", d),
        ////        DocumentType = DocumentTypesEnum.FSPMandate
        ////    };
        ////}

        ////NEW FSP MANDATE
        //private ApplicationDocumentsModel CreateFspMandate(string application, PrimaryIndividualModel schedule, string entityName, string entityNumber, string entityVat,
        //    RiskProfileModel risk, FSPMandateModel man, AddressDto address,
        //    UserModel client, UserModel adviser, RecordOfAdviseModel roa, BankVerificationsModel bv)
        //{
        //    var d = new Dictionary<string, string>();
        //    var Advisor = $"{adviser.FirstName} {adviser.LastName}";

        //    d["static"] = "x";

        //    //individual
        //    if (application == "Individual")
        //    {
        //        var clientDetails = schedule.ClientDetails ?? null;
        //        var contactDetails = schedule.ContactDetails ?? null;
        //        //var names = clientDetails.FirstNames.Split();
        //        var names = clientDetails.FirstNames.Split(' ', 2);

        //        d["surname"] = clientDetails.Surname;
        //        d["firstName"] = names[0];

        //        if (names.Length > 1)
        //            d["middleName"] = names[1];

        //        d["title"] = clientDetails.Title;
        //        //d["dateOfBirth"] = clientDetails.DateOfBirth;
        //        d["dateOfBirth_day"] = clientDetails.DateOfBirth.ToString().Substring(8, 2);
        //        d["dateOfBirth_month"] = clientDetails.DateOfBirth.ToString().Substring(5, 2);
        //        d["dateOfBirth_year"] = clientDetails.DateOfBirth.ToString().Substring(0, 4);

        //        if (clientDetails.NonResidentialAccount == true)
        //            d["nonResidential_Y"] = "x";
        //        else
        //            d["nonResidential_N"] = "x";

        //        d["idNo"] = client.IdNumber;
        //        d["nationality"] = clientDetails.Nationality;
        //        d["taxNo"] = schedule.TaxResidency.TaxNumber ?? " ";

        //        d["address1"] = $"{contactDetails.StreetNumber} " + $"{contactDetails.StreetName}, " +
        //            $"{contactDetails.UnitNumber} " + $"{contactDetails.Complex}";
        //        d["address2"] = $"{contactDetails.Suburb}, " + $"{contactDetails.City}, " +
        //            $"{contactDetails.Country}";
        //        d["postalCode"] = address.PostalCode;

        //        d["yearsAtAddress"] = contactDetails.YearsAtAddress;

        //        if (contactDetails.PostalSameAsResidential == true)
        //            d["postalSameAsResidential"] = "x";

        //        d["p_address1"] = $"{contactDetails.PA_StreetNumber} " + $"{contactDetails.PA_StreetName}, " +
        //            $"{contactDetails.PA_UnitNumber} " + $"{contactDetails.PA_Complex}";
        //        d["p_address2"] = $"{contactDetails.PA_Suburb}, " + $"{contactDetails.PA_City}, " +
        //            $"{contactDetails.PA_Country}";
        //        d["p_postalCode"] = contactDetails.PA_PostalCode;

        //        d["businessTel"] = contactDetails.WorkNumber;
        //        //d["homeTel"] = " ";
        //        d["mobile"] = "0" + contactDetails.MobileNumber ?? string.Empty;
        //        //d["fax"] = " ";
        //        d["email"] = contactDetails.Email;

        //        if (clientDetails.MaritalStatus == "Single")
        //            d["single"] = "x";

        //        else if (clientDetails.MaritalStatus == "MarriedInCommunity")
        //            d["inCommunity"] = "x";

        //        else if (clientDetails.MaritalStatus == "MarriedOutCommunity")
        //            d["outCommunity"] = "x";

        //        if (clientDetails.MaritalStatus != "Single")
        //        {
        //            d["dateOfMarriage_day"] = clientDetails.DateOfMarriage.ToString().Substring(8, 2);
        //            d["dateOfMarriage_month"] = clientDetails.DateOfMarriage.ToString().Substring(5, 2);
        //            d["dateOfMarriage_year"] = clientDetails.DateOfMarriage.ToString().Substring(0, 4);

        //            if (clientDetails.ForeignMarriage == true)
        //                d["foreignMarriage"] = "x";

        //            d["countryOfMarriage"] = clientDetails.CountryOfMarriage;
        //            d["spouseName"] = clientDetails.SpouseName;
        //            d["maidenName"] = clientDetails.MaidenName;
        //            //d["spouseDateOfBirth"] = clientDetails.SpouseDateOfBirth;
        //            d["spouseDateOfBirth_day"] = clientDetails.SpouseDateOfBirth.ToString().Substring(8, 2);
        //            d["spouseDateOfBirth_month"] = clientDetails.SpouseDateOfBirth.ToString().Substring(5, 2);
        //            d["spouseDateOfBirth_year"] = clientDetails.SpouseDateOfBirth.ToString().Substring(0, 4);

        //            if (clientDetails.PowerOfAttorney == true)
        //                d["powerOfAttorney_Y"] = "x";
        //            else
        //                d["powerOfAttorney_N"] = "x";
        //        }
        //    }
        //    else
        //    {
        //        // institutions
        //        if (application == "Trust")
        //            d["trust"] = "x";
        //        else if (application == "Partnership")
        //            d["partnership"] = "x";
        //        else if (application == "Company")
        //            d["company"] = "x";
        //        else if (application == "CC")
        //            d["cc"] = "x";

        //        d["regName"] = entityName;
        //        d["regNumber"] = entityNumber;
        //        d["vatRegNumber"] = entityVat;

        //        d["regAddress1"] = $"{address.StreetNumber} " + $"{address.StreetName}, " +
        //                        $"{address.UnitNumber} " + $"{address.Complex}";
        //        d["regAddress2"] = $"{address.Suburb}, " + $"{address.City}, " +
        //            $"{address.Country}";
        //        d["regPostalCode"] = address.PostalCode;

        //        d["appointedName"] = $"{client.FirstName} {client.LastName}";
        //        d["appointedIdNo"] = client.IdNumber;
        //        //d["contactBusinessTel"] = "";
        //        //d["contactHomeTel"] = "";
        //        //d["contactFax"] = "";
        //        d["contactMobile"] = "0" + client.MobileNumber ?? string.Empty;
        //        d["contactEmail"] = client.Email;
        //    }

        //    // bank details
        //    d["accountHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    d["bank"] = bv.BankName ?? string.Empty;
        //    d["branchNo"] = bv.BranchCode ?? string.Empty;
        //    d["branchName"] = bv.BranchCode ?? string.Empty;
        //    d["accountNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    d["accountType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    //fsp
        //    if (man.Discretion == "full")
        //    {
        //        d["dividendInstruction_full"] = man.DividendInstruction ?? string.Empty;
        //        d["monthlyFee_full"] = man.MonthlyFee ?? string.Empty;
        //        d["additionalFee"] = man.AdditionalFee ?? string.Empty;
        //        d["initialFee_full"] = man.InitialFee ?? string.Empty;
        //        d["annualFee_full"] = man.AnnualFee ?? string.Empty;

        //        if (man.Objective == "IncomeGeneration")
        //            d["maximumDividend"] = "x";
        //        else if (man.Objective == "CapitalGrowth")
        //            d["maximumCapital"] = "x";

        //    }

        //    else if (man.Discretion == "limited_DE")
        //    {
        //        //deal and execution
        //        d["dealAndExecution"] = "x";
        //        d["instruction_personal_DE"] = man.InstructionPersonal ?? null;
        //        d["instruction_adviser_DE"] = man.InstructionAdvisor ?? null;
        //        d["dividendInstruction_limited"] = man.DividendInstruction ?? string.Empty;

        //        if (man.Vote == "full")
        //            d["voteFull"] = "x";
        //        else if (man.Vote == "limited")
        //            d["voteLimited"] = "x";

        //        d["monthlyFee_limited"] = man.MonthlyFee ?? string.Empty;
        //        d["initialFee_limited"] = man.InitialFee ?? string.Empty;
        //        d["annualFee_limited"] = man.AnnualFee ?? string.Empty;

        //    }
        //    else if (man.Discretion == "limited_RM")
        //    {
        //        //referral managed
        //        d["referralManaged"] = "x";
        //        d["instruction_personal_RM"] = man.InstructionPersonal ?? null;
        //        d["instruction_adviser_RM"] = man.InstructionAdvisor ?? null;
        //        d["dividendInstruction_limited"] = man.DividendInstruction ?? string.Empty;

        //        if (man.Vote == "full")
        //            d["voteFull"] = "x";
        //        else if (man.Vote == "limited")
        //            d["voteLimited"] = "x";

        //        d["monthlyFee_limited"] = man.MonthlyFee ?? string.Empty;
        //        d["initialFee_limited"] = man.InitialFee ?? string.Empty;
        //        d["annualFee_limited"] = man.AnnualFee ?? string.Empty;

        //    }

        //    d["adminFee"] = man.AdminFee ?? string.Empty;

        //    //signature details
        //    d["clientSignAt"] = address.City;
        //    d["witnessSignName"] = Advisor;
        //    //d["signedOnDay"] = (DateTime.Today).ToString().Substring(8, 2);
        //    //d["signedOnMonth"] = DateTime.Today.ToString().Substring(5, 2);
        //    //d["signedOnYear"] = (DateTime.Today).ToString().Substring(2, 2);
        //    d["signedOnDay"] = DateTime.UtcNow.Day.ToString();
        //    d["signedOnMonth"] = DateTime.UtcNow.Month.ToString();
        //    d["signedOnYear"] = DateTime.UtcNow.Year.ToString().Substring(2, 2);

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("FspMandate.pdf", d),
        //        DocumentType = DocumentTypesEnum.FSPMandate
        //    };
        //}

        ////ROA
        //private ApplicationDocumentsModel CreateRecordOfAdvice(RecordOfAdviseModel roa, string firstname,
        //    string lastname, string idnumber, AddressDto address, AdvisorModel advisor, RiskProfileModel risk,
        //    AdvisorAdviseModel advise)
        //{
        //    var data = new Dictionary<string, string>();
        //    var date = DateTime.Today.ToString("yyyy/MM/dd");
        //    var nameSurname = $"{firstname} {lastname}";

        //    data["bdaNumber"] = string.Empty;
        //    data["date"] = date;
        //    //data["clientSignedAt"] = address.City;                    //removed in new doc
        //    //data["signedAt"] = "Pretoria";//address.City;             //removed in new doc
        //    data["nameSurname"] = nameSurname;
        //    //data["nameSurname_B"] = nameSurname;
        //    data["idNo"] = idnumber;
        //    data["adviserName"] = $"{advisor.User.FirstName} {advisor.User.LastName}";
        //    data["distributorName"] = "Stefan Griesel";
        //    data["adviserEmail"] = advisor.User.Email;
        //    data["adviserMobile"] = "0" + advisor.User.MobileNumber;

        //    var advDetails = advisor.BrokerDetails;

        //    data["adviserAddress"] = $"{advDetails.ResidentialAddressLine1}, " +
        //        $"{advDetails.ResidentialAddressLine2}, " +
        //        $"{advDetails.ResidentialAddressLine3}, " +
        //        $"{advDetails.ResidentialAddressLine4}" + $"{advDetails.ResidentialAddressPostalCode}";
        //    data["introduction"] = advise.Introduction;
        //    data["materialInformation"] = advise.MaterialInformation;
        //    data["replacementReason"] = roa.ReplacementReason ?? string.Empty;
        //    data["derivedProfile"] = risk.DerivedProfile;

        //    var prodProperties = new List<string>() {
        //        "productName", "recommendedLumpSum", "acceptedLumpSum",
        //        "recommendedRecurringPremium", "acceptedRecurringPremium" , "deveationReason"
        //    };

        //    if (roa.Replacement_A)
        //        data["replacement_A"] = "x";

        //    if (roa.Replacement_B)
        //        data["replacement_B"] = "x";

        //    if (roa.Replacement_C)
        //        data["replacement_C"] = "x";

        //    if (roa.Replacement_D)
        //        data["replacement_D"] = "x";

        //    foreach (var product in roa.SelectedProducts)
        //    {
        //        data[$"{product.ProductName}_productName"] = product.ProductName; //not used?

        //        data[$"{product.ProductName}_recommendedLumpSum"] = product.RecommendedLumpSum > 0 ?
        //            product.RecommendedLumpSum.ToString() : string.Empty;

        //        data[$"{product.ProductName}_acceptedLumpSum"] = product.AcceptedLumpSum > 0 ?
        //            product.AcceptedLumpSum.ToString() : string.Empty;

        //        //data[$"{product.ProductName}_recommendedRecurringPremium"] = product.RecommendedRecurringPremium > 0 ?            //removed new doc
        //        //    product.RecommendedRecurringPremium.ToString() : string.Empty; ;

        //        //data[$"{product.ProductName}_acceptedRecurringPremium"] = product.AcceptedRecurringPremium > 0 ?                  //removed new doc
        //        //    product.AcceptedRecurringPremium.ToString() : string.Empty; ;

        //        data[$"{product.ProductName}_deveationReason"] = product.DeveationReason ?? string.Empty; ;
        //    }

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("ROA.pdf", data),
        //        DocumentType = DocumentTypesEnum.RecordOfAdvice
        //    };
        //}

        ////RISK PROFILE
        //private ApplicationDocumentsModel CreateRiskProfile(RiskProfileModel r, UserModel adviser,
        //    AdvisorAdviseModel advise, string firstName, string lastName, string idNumber)
        //{
        //    Dictionary<string, string> d = new Dictionary<string, string>();

        //    d["User"] = $"{firstName} {lastName}";
        //    d["IdNo"] = idNumber;
        //    d["Advisor"] = $"{adviser.FirstName ?? string.Empty} {adviser.LastName ?? string.Empty}";
        //    d["Created"] = DateTime.Today.ToString("yyyy/MM/dd");
        //    d["Goal"] = r.Goal;

        //    d[r.RiskAge] = "x";
        //    d[r.RiskTerm] = "x";
        //    d[r.RiskInflation] = "x";
        //    d[r.RiskReaction] = "x";
        //    d[r.RiskExample] = "x";

        //    d["DerivedProfile"] = r.DerivedProfile;

        //    var agreeStr = r.AgreeWithOutcome == true ? "agree_True" : "agree_False";
        //    if (!r.AgreeWithOutcome)
        //    {
        //        d["Reason"] = r.Reason ?? string.Empty;
        //        d["agree_False"] = "x";
        //    }
        //    else
        //        d["agree_True"] = "x";
        //    d["Date"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    // advisor notes
        //    d["advisorNotes"] = r.AdvisorNotes ?? string.Empty;

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("RiskProfile.pdf", d),
        //        DocumentType = DocumentTypesEnum.RiskProfile
        //    };
        //}

        ////RESOLUTION
        //private ApplicationDocumentsModel CreateResolution(RequiredSecondarySchedulesModel associates, AddressDto address, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var userDetails = user;

        //    DateTime meetingDate = DateTime.Parse(associates.ResolutionMeetingDate);

        //    var employmentOptions = new List<string>() { };

        //    data["meetingHeldAt"] = associates.ResolutionMeetingLocation;
        //    data["meetingHeldOn"] = meetingDate.ToString("d MMMM");
        //    //data["meetingHeldYear"] = meetingDate.ToString("y");
        //    data["meetingHeldYear"] = associates.ResolutionMeetingDate.Substring(2, 2);

        //    data["signedAt"] = address.City;

        //    data["signedOnDay"] = DateTime.Today.ToString().Substring(8, 2);
        //    data["signedOnMonth"] = DateTime.Today.ToString("MMMM");
        //    data["signedOnYear"] = DateTime.Today.ToString().Substring(2, 2);

        //    data["signatory_Name"] = $"{user.FirstName} {user.LastName}";

        //    int c = 0;
        //    foreach (var e in associates.Contacts)
        //    {
        //        data[$"signatory_{c}_Name"] = $"{e.Name ?? string.Empty} {e.Surname ?? string.Empty}";
        //        c++;
        //    }

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("Resolution.pdf", data),
        //        DocumentType = DocumentTypesEnum.Resolution
        //    };
        //}

        ////INDIVIDUAL
        //private ApplicationDocumentsModel CreatePrimaryIndividualSchedule(PrimaryIndividualModel schedule, BankVerificationsModel bv, MarketingModel marketing)
        //{
        //    var data = new Dictionary<string, string>();
        //    var clientDetails = schedule.ClientDetails;
        //    var contactDetails = schedule.ContactDetails;
        //    var funding = schedule.PurposeAndFunding;
        //    var taxResidency = schedule.TaxResidency;

        //    var employmentOptions = new List<string>() { };

        //    // client details
        //    data[clientDetails.ClientType] = "x" ?? string.Empty;
        //    data["title"] = clientDetails.Title ?? string.Empty;
        //    data["dateOfBirth"] = clientDetails.DateOfBirth ?? string.Empty;
        //    data["initials"] = clientDetails.Initials ?? string.Empty;
        //    data["firstNames"] = clientDetails.FirstNames ?? string.Empty;
        //    data["surname"] = clientDetails.Surname ?? string.Empty;

        //    data["countryOfBirth"] = clientDetails.CountryOfBirth ?? string.Empty;
        //    data["cityOfBirth"] = clientDetails.CityOfBirth ?? string.Empty;
        //    data["nationality"] = clientDetails.Nationality ?? string.Empty;
        //    data[clientDetails.EmploymentStatus] = "x";
        //    data["employer"] = clientDetails.Employer ?? string.Empty;
        //    data["businessNature"] = clientDetails.Industry ?? string.Empty;
        //    data["occupation"] = clientDetails.Occupation ?? string.Empty;

        //    var c = 1;
        //    foreach (var e in clientDetails.IdentityDetails)
        //    {
        //        data[$"Type_{c}"] = e.IdentificationType ?? string.Empty;
        //        data[$"Country_{c}"] = e.CountryOfIssure ?? string.Empty;
        //        data[$"IdNo_{c}"] = e.IdentificationNumber ?? string.Empty;
        //        data[$"Expiry_{c}"] = e.ExpiryDate ?? string.Empty;
        //        c += 1;
        //    }

        //    // contact details
        //    data["unitNo"] = contactDetails.UnitNumber ?? string.Empty;
        //    data["streetNo"] = contactDetails.StreetNumber ?? string.Empty;
        //    data["suburb"] = contactDetails.Suburb ?? string.Empty;
        //    data["postalCode"] = contactDetails.PostalCode ?? string.Empty;
        //    data["complex"] = contactDetails.Complex ?? string.Empty;
        //    data["streetName"] = contactDetails.StreetName ?? string.Empty;
        //    data["city"] = contactDetails.City ?? string.Empty;
        //    data["country"] = contactDetails.Country ?? string.Empty;

        //    if (contactDetails.PostalSameAsResidential == false)
        //        data["postalNotSame"] = "x";
        //    else
        //        data["postalSame"] = "x";

        //    if (contactDetails.PostalInCareAddress == false)
        //        data["postalCare_False"] = "x";
        //    else
        //        data["postalCare_True"] = "x";
        //    data["careName"] = contactDetails.PostalInCareName ?? string.Empty;

        //    data["postalAddress_1"] = $"{contactDetails.PA_UnitNumber ?? string.Empty} {contactDetails.PA_Complex ?? string.Empty}";
        //    data["postalAddress_2"] = $"{contactDetails.PA_StreetNumber ?? string.Empty} {contactDetails.PA_StreetName ?? string.Empty}";
        //    data["postalAddress_3"] = $"{contactDetails.PA_Suburb ?? string.Empty}";
        //    data["postalAddress_4"] = $"{contactDetails.PA_City ?? string.Empty}";
        //    data["p_postalCode"] = $"{contactDetails.PA_PostalCode ?? string.Empty}";
        //    data["p_country"] = $"{contactDetails.PA_Country ?? string.Empty}";
        //    //data["work"] = contactDetails.WorkNumber ?? string.Empty;
        //    if (contactDetails.WorkNumber != "")
        //    { data["work"] = "0" + contactDetails.WorkNumber ?? string.Empty; }

        //    data["mobile"] = "0" + contactDetails.MobileNumber ?? string.Empty;
        //    data["email"] = contactDetails.Email ?? string.Empty;

        //    // bank details
        //    data["accountHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bank"] = bv.BankName ?? string.Empty;
        //    data["branchNo"] = bv.BranchCode ?? string.Empty;
        //    data["accountNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["accountType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // static fields
        //    data["static"] = "x";

        //    // employed
        //    if (funding.fundsEmployedSalary)
        //        data["fundsEmployedSalary"] = "x";

        //    if (funding.fundsEmployedCommission)
        //        data["fundsEmployedCommission"] = "x";

        //    if (funding.fundsEmployedBonus)
        //        data["fundsEmployedBonus"] = "x";

        //    // self-employed
        //    if (funding.fundsSelfTurnover)
        //        data["fundsSelfTurnover"] = "x";

        //    // retired
        //    if (funding.fundsRetiredAnnuity)
        //        data["fundsRetiredAnnuity"] = "x";

        //    if (funding.fundsRetiredOnceOff)
        //        data["fundsRetiredOnceOff"] = "x";

        //    // director
        //    if (funding.fundsDirectorSalary)
        //        data["fundsDirectorSalary"] = "x";

        //    if (funding.fundsDirectorDividend)
        //        data["fundsDirectorDividend"] = "x";

        //    if (funding.fundsDirectorInterest)
        //        data["fundsDirectorInterest"] = "x";

        //    if (funding.fundsDirectorBonus)
        //        data["fundsDirectorBonus"] = "x";

        //    data["fundsOther"] = funding.fundsOther ?? string.Empty;

        //    // source of wealth
        //    if (funding.wealthIncome)
        //        data["wealthIncome"] = "x";

        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthInheritance)
        //        data["wealthInheritance"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    data["wealthOther"] = funding.wealthOther ?? string.Empty;

        //    // tax residency
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;
        //    data[taxResidency.TaxObligations == true ? "obligations_True" : "obligations_False"] = "x";
        //    data[taxResidency.UsCitizen == true ? "usCititzen_True" : "usCititzen_False"] = "x";
        //    data[taxResidency.UsRelinquished == true ? "usRelinquished_True" : "usRelinquished_False"] = "x";
        //    data[taxResidency.UsOther == true ? "usOther_True" : "usOther_False"] = "x";

        //    c = 1;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        data[$"oblcountry_{c}"] = e.Country ?? string.Empty;
        //        data[$"tinNo_{c}"] = e.TinNumber ?? string.Empty;
        //        data[$"txReason_{c}"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    // marketing
        //    data[marketing.NedbankOffers == true ? "product_True" : "product_False"] = "x";
        //    data[marketing.OtherOffers == true ? "offers_True" : "offers_False"] = "x";
        //    data[marketing.ReputableOrg == true ? "research_True" : "research_False"] = "x";
        //    data[marketing.OtherMethodOfCommunication == true ? "otherMethod_True" : "otherMethod_False"] = "x";

        //    if (marketing.NedbankOffers || marketing.OtherOffers || marketing.ReputableOrg || marketing.OtherMethodOfCommunication)
        //        data["comm" + marketing.MethodOfCommunication] = "X";

        //    // declaration
        //    data["nameSurname"] = $"{clientDetails.FirstNames} {clientDetails.Surname}";
        //    data["signerCapacity"] = "Self";
        //    data["dateSigned"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("PrimaryIndividual.pdf", data),
        //        DocumentType = DocumentTypesEnum.PrimarySchedule
        //    };
        //}

        ////TRUST
        //private ApplicationDocumentsModel CreatePrimaryTrustSchedule(PrimaryTrustModel schedule, BankVerificationsModel bv, UserModel user, MarketingModel marketing)
        //{
        //    var data = new Dictionary<string, string>();
        //    var trustDetails = schedule.TrustDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.TrustFunding;
        //    var taxResidency = schedule.TrustTaxResidency;
        //    var userDetails = user;

        //    var employmentOptions = new List<string>() { };

        //    //  TRUST details
        //    data[trustDetails.TrustType] = "X";
        //    data["trustName"] = trustDetails.Name ?? string.Empty;
        //    data["trustNumber"] = trustDetails.Number ?? string.Empty;
        //    data["trustCountry"] = trustDetails.Country ?? string.Empty;
        //    data["trustIndustry"] = trustDetails.Industry ?? string.Empty;
        //    data["trustOperations"] = trustDetails.Purpose ?? string.Empty;
        //    data["trustPlaceOfManagement"] = trustDetails.Location ?? string.Empty;

        //    //  ADDRESS DETAILS
        //    if (trustDetails.InCareAddress == false)
        //        data["postalAddressInCareOfNo"] = "x";
        //    else
        //        data["postalAddressInCareOfYes"] = "x";
        //    data["postalAddressInCareOfName"] = trustDetails.InCareName ?? string.Empty;

        //    foreach (var address in trustDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // postal code details
        //            data["postalAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["postalAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["postalAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["postalAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["postalAddressCity"] = address.City ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Residential)
        //        {
        //            // high Court details
        //            data["trustCourtAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["trustCourtAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["trustCourtAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["trustCourtAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["trustCourtAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["trustCourtAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["trustCourtAddressCity"] = address.City ?? string.Empty;
        //            data["trustCourtAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = trustDetails.ContactPerson ?? string.Empty;
        //    data["contactWork"] = trustDetails.Number ?? string.Empty;
        //    data["contactEmail"] = trustDetails.Email ?? string.Empty;
        //    //data["contactCellphone"] = trustDetails.Mobile ?? string.Empty;
        //    data["contactCellphone"] = "0" + trustDetails.Mobile ?? string.Empty;

        //    //  BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankName"] = bv.BankName ?? string.Empty;
        //    data["bankBranchNo"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    //  STATIC FIELDS
        //    data["static"] = "x";

        //    //  SOURCE OF FUNDS
        //    data["sourceFundsExpected"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditional"] = funding.SourceOfAdditional;
        //    data["sourceFundsAdditionalDetails"] = funding.DonorDetails;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    //var sourceOfFundsOptions = new List<string>()
        //    //{
        //    //    "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //    //    "salary", "Dividends", "interest", "bonuses"
        //    //};

        //    //if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //    //    data[funding.SourceOfFunds] = "x";
        //    //else
        //    //    data["other"] = funding.SourceOfFunds;

        //    /*
        //    var sourceOfWealthOptions = new List<string>()
        //    {
        //        "savings", "investments", "shareSales", "propertySales",
        //        "companySales", "inheritance", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "X";
        //    else
        //        data["Wealth_Other"] = funding.SourceOfWealth;
        //    */

        //    //  SOURCE OF WEALTH
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    if (funding.wealthOtherOption)
        //        data["wealthOtherOption"] = "x";

        //    data["wealthOther"] = funding.wealthOther ?? string.Empty; ;

        //    // TAX DETAILS
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxObligationsY" : "taxObligationsN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    //not in Primary Schedule
        //    //data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "X";

        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "trustCreatedUSAY" : "trustCreatedUSAN"] = "x";
        //    //us jurisdiction
        //    data[taxResidency.UsJurisdiction == true ? "trustUSACourtY" : "trustUSACourtN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "trustPersonUSAY" : "trustPersonUSAN"] = "x";
        //    //us tax obligations trust
        //    data[taxResidency.TrustUsTaxObligations == true ? "trustUSATaxY" : "trustUSATaxN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "trustUSAIndicatorsY" : "trustUSAIndicatorsN"] = "x";

        //    // MARKETING
        //    data[marketing.NedbankOffers == true ? "product_True" : "product_False"] = "x";
        //    data[marketing.OtherOffers == true ? "offers_True" : "offers_False"] = "x";
        //    data[marketing.ReputableOrg == true ? "research_True" : "research_False"] = "x";
        //    data[marketing.OtherMethodOfCommunication == true ? "otherMethod_True" : "otherMethod_False"] = "x";

        //    data["comm" + marketing.MethodOfCommunication] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.TrustFATCA == true ? "trustFIFACTAY" : "trustFIFACTAN"] = "x";
        //    data[entityDetails.TrustInvestNotResident == true ? "trustInvestmentNotResidentY" : "trustInvestmentNotResidentN"] = "x";
        //    data[entityDetails.TrustOtherInvestment == true ? "trustInvestmentOtherY" : "trustInvestmentOtherN"] = "x";
        //    data[entityDetails.TrustDepo == true ? "trustDepositoryY" : "trustDepositoryN"] = "x";
        //    if (entityDetails.TrustDepo == true)
        //    {
        //        data["trustDepositoryGIIN"] = entityDetails.TrustGIIN;
        //    }
        //    if (entityDetails.FATCAStatus == "Trustee Documented Trust")
        //    {
        //        data["trustDocumented"] = "x";
        //        data["trustManagingFIName"] = entityDetails.FIName;
        //        data["trustManagingFIGIIN"] = entityDetails.FIGIIN;
        //        data["trustManagingFICountry"] = entityDetails.FICountry;
        //    }
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["trustCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["trustExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["trustDocumentedFI"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["trustNonReportingFI"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["trustGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["trustLimitedFI"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Participating FI")
        //    {
        //        data["trustNonParticipating"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["trustOtherEntity"] = "x";
        //        data["trustOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "trustRelatedStockEntityY" : "trustRelatedStockEntityN"] = "X";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["trustRelatedEntityName"] = entityDetails.RelatedName;
        //        data["trustRelatedEntityXchange"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "trustGOVTY" : "trustGOVTN"] = "X";
        //    data[entityDetails.NFEIntOrg == true ? "trustNFEInternationalY" : "trustNFEInternationalN"] = "X";
        //    data[entityDetails.NFELiquidating == true ? "trustNFELiquidatingY" : "trustNFELiquidatingN"] = "X";
        //    data[entityDetails.NFETreasury == true ? "trustNFETreasuryY" : "trustNFETreasuryN"] = "X";
        //    data[entityDetails.DirectNFE == true ? "trustNFEDirectY" : "trustNFEDirectN"] = "X";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["trustNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["trustNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["trustNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "trustNPOPBOY" : "trustNPOPBON"] = "X";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["trustNPOPBONo"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "trustGrossPassiveY" : "trustGrossPassiveN"] = "X";

        //    data["signaturePersonName"] = userDetails.FirstName + " " + userDetails.LastName;
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");
        //    data["signaturePersonCapacity"] = "Self";

        //    // DECLARATIONS
        //    data["NameSurname"] = $"{user.FirstName} {user.LastName}";
        //    data["SignerCapacity"] = "Self";
        //    data["DateSigned"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("ScheduleTrust.pdf", data),
        //        DocumentType = DocumentTypesEnum.PrimarySchedule

        //    };
        //}

        ////SECONDARY TRUST
        //private ApplicationDocumentsModel CreateSecondaryTrustSchedule(PrimaryTrustModel schedule, BankVerificationsModel bv, UserModel primaryUser, UserModel associatedUser, RequiredSecondarySchedulesContactsModel associatedParty)
        //{
        //    var data = new Dictionary<string, string>();
        //    var trustDetails = schedule.TrustDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.TrustFunding;
        //    var taxResidency = schedule.TrustTaxResidency;

        //    //Primary Client Details
        //    data["clientName"] = primaryUser.FirstName + " " + primaryUser.LastName ?? string.Empty;
        //    data["clientRegNo"] = trustDetails.Number ?? string.Empty;

        //    //Association Details
        //    data[$"assoc{associatedParty.AssociationType}"] = "X";

        //    //TRUST details
        //    data[trustDetails.TrustType] = "X";
        //    data["trustName"] = trustDetails.Name ?? string.Empty;
        //    data["trustNumber"] = trustDetails.Number ?? string.Empty;
        //    data["trustCountry"] = trustDetails.Country ?? string.Empty;
        //    data["trustIndustry"] = trustDetails.Industry ?? string.Empty;
        //    data["trustOperations"] = trustDetails.Purpose ?? string.Empty;
        //    data["trustPlaceOfManagement"] = trustDetails.Location ?? string.Empty;

        //    //ADDRESS DETAILS
        //    if (trustDetails.InCareAddress == false)
        //        data["postalAddressInCareOfNo"] = "X";
        //    else
        //        data["postalAddressInCareOfYes"] = "X";
        //    data["postalAddressInCareOfName"] = trustDetails.InCareName ?? string.Empty;

        //    foreach (var address in trustDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // postal code details
        //            data["postalAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["postalAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["postalAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["postalAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["postalAddressCity"] = address.City ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Residential)
        //        {
        //            // High Court details
        //            data["trustCourtAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["trustCourtAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["trustCourtAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["trustCourtAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["trustCourtAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["trustCourtAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["trustCourtAddressCity"] = address.City ?? string.Empty;
        //            data["trustCourtAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //CONTACT DETAILS
        //    data["contactPerson"] = trustDetails.ContactPerson ?? string.Empty;
        //    data["contactWork"] = trustDetails.Number ?? string.Empty;
        //    data["contactEmail"] = trustDetails.Email ?? string.Empty;
        //    //data["contactCellphone"] = trustDetails.Mobile ?? string.Empty;
        //    data["contactCellphone"] = "0" + trustDetails.Mobile ?? string.Empty;

        //    //BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankName"] = bv.BankName ?? string.Empty;
        //    data["bankBranchNo"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    //SOURCE OF FUNDS
        //    data["sourceFundsExpected"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditional"] = funding.SourceOfAdditional;
        //    data["sourceFundsAdditionalDetails"] = funding.DonorDetails;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    /*
        //    var sourceOfFundsOptions = new List<string>()
        //    {
        //        "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //        "salary", "Dividends", "interest", "bonuses"
        //    };

        //    if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //        data[funding.SourceOfFunds] = "X";
        //    else
        //        data["other"] = funding.SourceOfFunds;

        //    var sourceOfWealthOptions = new List<string>()
        //    {
        //        "savings", "investments", "shareSales", "propertySales",
        //        "companySales", "inheritance", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "X";
        //    else
        //        data["Wealth_Other"] = funding.SourceOfWealth;*/

        //    //SOURCE OF WEALTH
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "X";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "X";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "X";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "X";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "X";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "X";

        //    if (funding.wealthOtherOption)
        //        data["wealthOtherOption"] = "X";

        //    data["wealthOther"] = funding.wealthOther;

        //    // TAX DETAILS
        //    //data["TaxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxObligationsY" : "taxObligationsN"] = "X";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "X";

        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "trustCreatedUSAY" : "trustCreatedUSAN"] = "X";
        //    //us jurisdiction
        //    data[taxResidency.UsJurisdiction == true ? "trustUSACourtY" : "trustUSACourtN"] = "X";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "trustPersonUSAY" : "trustPersonUSAN"] = "X";
        //    //us tax obligations trust
        //    data[taxResidency.TrustUsTaxObligations == true ? "trustUSATaxY" : "trustUSATaxN"] = "X";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "trustUSAIndicatorsY" : "trustUSAIndicatorsN"] = "X";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.TrustFATCA == true ? "trustFIFACTAY" : "trustFIFACTAN"] = "X";
        //    data[entityDetails.TrustInvestNotResident == true ? "trustInvestmentNotResidentY" : "trustInvestmentNotResidentN"] = "X";
        //    data[entityDetails.TrustOtherInvestment == true ? "trustInvestmentOtherY" : "trustInvestmentOtherN"] = "X";
        //    data[entityDetails.TrustDepo == true ? "trustDepositoryY" : "trustDepositoryN"] = "X";
        //    if (entityDetails.TrustDepo == true)
        //    {
        //        data["trustDepositoryGIIN"] = entityDetails.TrustGIIN;
        //    }
        //    if (entityDetails.FATCAStatus == "Trustee Documented Trust")
        //    {
        //        data["trustDocumented"] = "x";
        //        data["trustManagingFIName"] = entityDetails.FIName;
        //        data["trustManagingFIGIIN"] = entityDetails.FIGIIN;
        //        data["trustManagingFICountry"] = entityDetails.FICountry;
        //    }

        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["trustCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["trustExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["trustDocumentedFI"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["trustNonReportingFI"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["trustGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["trustLimitedFI"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Participating FI")
        //    {
        //        data["trustNonParticipating"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["trustOtherEntity"] = "x";
        //        data["trustOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "trustRelatedStockEntityY" : "trustRelatedStockEntityN"] = "x";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["trustRelatedEntityName"] = entityDetails.RelatedName;
        //        data["trustRelatedEntityXchange"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "trustGOVTY" : "trustGOVTN"] = "x";
        //    data[entityDetails.NFEIntOrg == true ? "trustNFEInternationalY" : "trustNFEInternationalN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "trustNFELiquidatingY" : "trustNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "trustNFETreasuryY" : "trustNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "trustNFEDirectY" : "trustNFEDirectN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["trustNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["trustNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["trustNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "trustNPOPBOY" : "trustNPOPBON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["trustNPOPBONo"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "trustGrossPassiveY" : "trustGrossPassiveN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{associatedUser.FirstName} {associatedUser.LastName}";
        //    data["signaturePersonCapacity"] = associatedParty.Capacity;
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SecondaryScheduleTrust.pdf", data),
        //        DocumentType = DocumentTypesEnum.SecondarySchedule,
        //        Name = $"Associated Party, Trust  : {associatedUser.FirstName} {associatedUser.LastName}.pdf"
        //    };
        //}

        ////TRUST ENTERPRISE GENERAL
        //private ApplicationDocumentsModel CreateTrustEnterpriseGeneralDeclaration(PrimaryTrustModel schedule, RequiredSecondarySchedulesModel associates, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var trustDetails = schedule.TrustDetails;
        //    var userDetails = user;

        //    var employmentOptions = new List<string>() { };

        //    if (associates.Contacts.Count() > 1)
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}" + ", " + $"{ associates.Contacts.ElementAt(1).Name ?? string.Empty} {associates.Contacts.ElementAt(1).Surname ?? string.Empty}";
        //        data["capacity2"] = associates.Contacts.ElementAt(1).Capacity ?? string.Empty;
        //        data["signDate2"] = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}";
        //        data["capacity2"] = "";
        //        data["signDate2"] = "";
        //    }
        //    data["capacity1"] = associates.Contacts.First().Capacity;
        //    data["entityName"] = trustDetails.Name ?? string.Empty;
        //    data["signDate1"] = DateTime.Today.ToString("dd/MM/yyyy");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("EnterpriseGeneralDeclaration.pdf", data),
        //        DocumentType = DocumentTypesEnum.TrustEnterpriseGeneralDeclaration
        //    };
        //}

        ////COMPANY
        //private ApplicationDocumentsModel CreatePrimarySaCompanySchedule(PrimarySaCompanyModel schedule, BankVerificationsModel bv, UserModel user, MarketingModel marketing)
        //{
        //    var data = new Dictionary<string, string>();
        //    var saCompanyDetails = schedule.SaCompanyDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.SaCompanyFunding;
        //    var taxResidency = schedule.SaCompanyTaxResidency;
        //    var userDetails = user;

        //    var employmentOptions = new List<string>() { };

        //    //company details
        //    data[saCompanyDetails.SaCompanyType] = "x";

        //    if (saCompanyDetails.SaCompanyType == "NonListedSAPreMay")
        //    {
        //        data["nonListedBeforeMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NonListedSAPostMay")
        //    {
        //        data["nonListedAfterMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "ListedSA")
        //    {
        //        data["listed"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "OwnedSubsidiaryListedSA")
        //    {
        //        data["subsidiaryListed"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NPOPreMay")
        //    {
        //        data["npcBeforeMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NPOPostMay")
        //    {
        //        data["npcAfterMay"] = "x";
        //    }

        //    data["companyRegName"] = saCompanyDetails.RegisteredName ?? string.Empty;
        //    data["companyTradeName"] = saCompanyDetails.TradingName ?? string.Empty;
        //    data["companyRegNo"] = saCompanyDetails.RegistrationNumber ?? string.Empty;
        //    data["companyCountry"] = saCompanyDetails.Country ?? string.Empty;
        //    data["companyIndustry"] = saCompanyDetails.Industry ?? string.Empty;
        //    data["companyTradeArea"] = saCompanyDetails.TradeArea ?? string.Empty;
        //    data["companyOperation"] = saCompanyDetails.Operations ?? string.Empty;
        //    data["companyPlaceOfManagement"] = saCompanyDetails.Location ?? string.Empty;
        //    data["companyVatNo"] = saCompanyDetails.VATNumber ?? string.Empty;

        //    //  address details
        //    if (saCompanyDetails.InCareAddress == false)
        //        data["tradeAddressCareOfN"] = "x";
        //    else
        //        data["tradeAddressCareOfY"] = "x";
        //    data["tradeAddressInCareOfName"] = saCompanyDetails.InCareName ?? string.Empty;

        //    if (saCompanyDetails.PostalInCareAddress == false)
        //        data["postalAddressCareOfN"] = "x";
        //    else
        //        data["postalAddressCareOfY"] = "x";
        //    data["postalAddressInCareOfName"] = saCompanyDetails.PostalInCareName ?? string.Empty;

        //    if (saCompanyDetails.TradingSameAsRegistered == false)
        //        data["tradingAddressSameAsRegAddressN"] = "x";
        //    else
        //        data["tradingAddressSameAsRegAddressY"] = "x";

        //    if (saCompanyDetails.HeadOfficeSameAsRegistered == false)
        //        data["hoAddressSameAsRegAddressN"] = "x";
        //    else
        //        data["hoAddressSameAsRegAddressY"] = "x";

        //    if (saCompanyDetails.HeadOfficeSameAsTrading == false)
        //        data["hoAddressSameAsTradeAddressN"] = "x";
        //    else
        //        data["hoAddressSameAsTradeAddressY"] = "x";

        //    if (saCompanyDetails.PostalSameAsTrading == false)
        //        data["postalAddressSameAsTradeAddressN"] = "x";
        //    else
        //        data["postalAddressSameAsTradeAddressY"] = "x";

        //    foreach (var address in saCompanyDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Registered)
        //        {
        //            // registered details
        //            data["regAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["regAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["regAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["regAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["regAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["regAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["regAddressCity"] = address.City ?? string.Empty;
        //            data["regAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.HeadOffice)
        //        {
        //            // head office details
        //            data["hoAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["hoAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["hoAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["hoAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["hoAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["hoAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["hoAddressCity"] = address.City ?? string.Empty;
        //            data["hoAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // postal details
        //            data["postalAddressLine1"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressLine2"] = address.StreetName ?? string.Empty;
        //            data["postalAddressLine3"] = address.Suburb ?? string.Empty;
        //            data["postalAddressLine4"] = address.City ?? string.Empty;
        //            data["postalAddressCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  contact details
        //    data["contactPerson"] = saCompanyDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = saCompanyDetails.Email ?? string.Empty;
        //    data["contactCellPhoneNo"] = saCompanyDetails.Mobile != null ? "0" + saCompanyDetails.Mobile : string.Empty;

        //    // bank details
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankAccName"] = bv.BankName ?? string.Empty;
        //    data["bankAccBranchCode"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // static fields
        //    data["static"] = "x";

        //    // source of funds
        //    data["sourceFundsExpectedTurnover"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditionalFunds"] = funding.SourceOfAdditional;
        //    //data["sourceFundsAdditionalDetails"] = funding.DonorDetails;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    var sourceOfFundsOptions = new List<string>()
        //    {
        //        "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //        "salary", "Dividends", "interest", "bonuses"
        //    };

        //    if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //        data[funding.SourceOfFunds] = "x";
        //    else
        //        data["other"] = funding.SourceOfFunds;

        //    //source of wealth
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    data["wealthOther"] = funding.wealthOther;

        //    var sourceOfWealthOptions = new List<string>()
        //    {
        //        "investments", "shareSales", "propertySales",
        //        "companySales", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "x";
        //    else
        //        data["wealth_Other"] = funding.SourceOfWealthOther;

        //    // tax details
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxOutsideSAY" : "taxOutsideSAN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "x";

        //    // us registered
        //    data[taxResidency.UsRegistered == true ? "companyCreatedUSAY" : "companyCreatedUSAN"] = "x";
        //    // us jurisdiction
        //    data[taxResidency.UsJurisdiction == true ? "companyUSACourtY" : "companyUSACourtN"] = "x";
        //    // us persons
        //    data[taxResidency.AnyUsPersons == true ? "companyPersonUSAY" : "companyPersonUSAN"] = "x";
        //    // us tax obligations company
        //    data[taxResidency.SaCompanyUsTaxObligations == true ? "companyUSATaxY" : "companyUSATaxN"] = "x";
        //    // other us indicators
        //    data[taxResidency.UsOther == true ? "companyUSAIndicatorsY" : "companyUSAIndicatorsN"] = "x";

        //    // marketing
        //    data[marketing.NedbankOffers == true ? "product_True" : "product_False"] = "x";
        //    data[marketing.OtherOffers == true ? "offers_True" : "offers_False"] = "x";
        //    data[marketing.ReputableOrg == true ? "research_True" : "research_False"] = "x";
        //    data[marketing.OtherMethodOfCommunication == true ? "otherMethod_True" : "otherMethod_False"] = "x";

        //    data["comm" + marketing.MethodOfCommunication] = "x";

        //    //entity classification

        //    //section1
        //    data[entityDetails.SACompanyFATCA == true ? "entityFinInstYes" : "entityFinInstNo"] = "x";
        //    data[entityDetails.SaCompanyInvestNotResident == true ? "entityInvestEntityNotResidentY" : "entityInvestEntityNotResidentN"] = "x";
        //    data[entityDetails.SaCompanyOtherInvestment == true ? "entityOtherInvestmentY" : "entityOtherInvestmentN"] = "x";
        //    data[entityDetails.SaCompanyDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.SaCompanyDepo == true)
        //    {
        //        data["entityGIINNp"] = entityDetails.SaCompanyGIIN;
        //    }
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.StockExchange == true || entityDetails.RelatedEntity == true ? "companyStockExchangeY" : "companyStockExchangeN"] = "x";
        //    if (entityDetails.StockExchange)
        //    {
        //        data["companyStockExchangeName"] = entityDetails.ExchangeName;
        //    }
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["companyRelatedName"] = entityDetails.RelatedName;
        //        data["companyRelatedExchangeName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "companyGovernmentY" : "companyGovernmentN"] = "x";
        //    data[entityDetails.CentralBank == true ? "companyCentralBankY" : "companyCentralBankN"] = "x";
        //    data[entityDetails.NFEIntOrg == true ? "companyNFEInternationalY" : "companyNFEInternationalN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "companyNFELiquidatingY" : "companyNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "companyNFETreasuryY" : "companyNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "companyNFEDirectY" : "companyNFEDirectN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["companyNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["companyNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["companyNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "companyPBOY" : "companyPBON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["companyPBONumber"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "companyPassiveAssetsY" : "companyPassiveAssetsN"] = "x";

        //    // declarations
        //    data["signaturePersonName"] = $"{user.FirstName} {user.LastName}";
        //    data["signaturePersonCapacity"] = "Self";
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("ScheduleSACompany.pdf", data),
        //        DocumentType = DocumentTypesEnum.PrimarySchedule
        //    };
        //}

        ////SECONDARY COMPANY
        //private ApplicationDocumentsModel CreateSecondarySaCompanySchedule(PrimarySaCompanyModel schedule, BankVerificationsModel bv, UserModel primaryUser, UserModel associatedUser, RequiredSecondarySchedulesContactsModel associatedParty)
        //{
        //    var data = new Dictionary<string, string>();
        //    var saCompanyDetails = schedule.SaCompanyDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.SaCompanyFunding;
        //    var taxResidency = schedule.SaCompanyTaxResidency;

        //    var employmentOptions = new List<string>() { };

        //    //Primary Client Details
        //    data["companyPrimaryClientName"] = primaryUser.FirstName + " " + primaryUser.LastName ?? string.Empty;
        //    data["companyClientRegNumber"] = saCompanyDetails.RegistrationNumber ?? string.Empty;

        //    //Association Details
        //    data[$"assoc{associatedParty.AssociationType}"] = "x";

        //    //COMPANY details
        //    data[saCompanyDetails.SaCompanyType] = "x";

        //    if (saCompanyDetails.SaCompanyType == "NonListedSAPreMay")
        //    {
        //        data["nonListedBeforeMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NonListedSAPostMay")
        //    {
        //        data["nonListedAfterMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "ListedSA")
        //    {
        //        data["listed"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "OwnedSubsidiaryListedSA")
        //    {
        //        data["subsidiaryListed"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NPOPreMay")
        //    {
        //        data["npcBeforeMay"] = "x";
        //    }
        //    if (saCompanyDetails.SaCompanyType == "NPOPostMay")
        //    {
        //        data["npcAfterMay"] = "x";
        //    }

        //    data["companyRegName"] = saCompanyDetails.RegisteredName ?? string.Empty;
        //    data["companyTradeName"] = saCompanyDetails.TradingName ?? string.Empty;
        //    data["companyRegNo"] = saCompanyDetails.RegistrationNumber ?? string.Empty;
        //    data["companyCountry"] = saCompanyDetails.Country ?? string.Empty;
        //    data["companyIndustry"] = saCompanyDetails.Industry ?? string.Empty;
        //    data["companyTradeArea"] = saCompanyDetails.TradeArea ?? string.Empty;
        //    data["companyOperation"] = saCompanyDetails.Operations ?? string.Empty;
        //    data["companyPlaceOfManagement"] = saCompanyDetails.Location ?? string.Empty;
        //    data["companyVatNo"] = saCompanyDetails.VATNumber ?? string.Empty;

        //    //  ADDRESS DETAILS
        //    if (saCompanyDetails.InCareAddress == false)
        //        data["tradeAddressCareOfN"] = "x";
        //    else
        //        data["tradeAddressCareOfY"] = "x";
        //    data["tradeAddressInCareOfName"] = saCompanyDetails.InCareName ?? string.Empty;

        //    if (saCompanyDetails.PostalInCareAddress == false)
        //        data["postalAddressCareOfN"] = "x";
        //    else
        //        data["postalAddressCareOfY"] = "x";
        //    data["postalAddressInCareOfName"] = saCompanyDetails.PostalInCareName ?? string.Empty;

        //    if (saCompanyDetails.TradingSameAsRegistered == false)
        //        data["tradingAddressSameAsRegAddressN"] = "x";
        //    else
        //        data["tradingAddressSameAsRegAddressY"] = "x";

        //    if (saCompanyDetails.HeadOfficeSameAsRegistered == false)
        //        data["hoAddressSameAsRegAddressN"] = "x";
        //    else
        //        data["hoAddressSameAsRegAddressY"] = "x";

        //    if (saCompanyDetails.HeadOfficeSameAsTrading == false)
        //        data["hoAddressSameAsTradeAddressN"] = "x";
        //    else
        //        data["hoAddressSameAsTradeAddressY"] = "x";

        //    if (saCompanyDetails.PostalSameAsTrading == false)
        //        data["postalAddressSameAsTradeAddressN"] = "x";
        //    else
        //        data["postalAddressSameAsTradeAddressY"] = "x";

        //    foreach (var address in saCompanyDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Registered)
        //        {
        //            // Registered details
        //            data["regAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["regAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["regAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["regAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["regAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["regAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["regAddressCity"] = address.City ?? string.Empty;
        //            data["regAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // Trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.HeadOffice)
        //        {
        //            // Head Office details
        //            data["hoAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["hoAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["hoAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["hoAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["hoAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["hoAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["hoAddressCity"] = address.City ?? string.Empty;
        //            data["hoAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // Postal details
        //            data["postalAddressLine1"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressLine2"] = address.StreetName ?? string.Empty;
        //            data["postalAddressLine3"] = address.Suburb ?? string.Empty;
        //            data["postalAddressLine4"] = address.City ?? string.Empty;
        //            data["postalAddressCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = saCompanyDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = saCompanyDetails.Email ?? string.Empty;
        //    data["contactCellPhoneNo"] = saCompanyDetails.Mobile != null ? "0" + saCompanyDetails.Mobile : string.Empty;

        //    // BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankAccName"] = bv.BankName ?? string.Empty;
        //    data["bankAccBranchCode"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // SOURCE OF FUNDS
        //    data["sourceFundsExpectedTurnover"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditionalFunds"] = funding.SourceOfAdditional;
        //    //data["sourceFundsAdditionalDetails"] = funding.DonorDetails;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    //SOURCE OF WEALTH
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    data["wealthOther"] = funding.wealthOther;
        //    /*
        //    var sourceOfFundsOptions = new List<string>()
        //    {
        //        "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //        "salary", "Dividends", "interest", "bonuses"
        //    };

        //    if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //        data[funding.SourceOfFunds] = "X";
        //    else
        //        data["other"] = funding.SourceOfFunds;

        //    var sourceOfWealthOptions = new List<string>()
        //    {
        //        "investments", "shareSales", "propertySales",
        //        "companySales", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "X";
        //    else
        //        data["wealth_Other"] = funding.SourceOfWealth;
        //    data["wealth_Other"] = funding.SourceOfWealthOther;
        //    */

        //    // TAX DETAILS
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxOutsideSAY" : "taxOutsideSAN"] = "X";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "x";

        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "companyCreatedUSAY" : "companyCreatedUSAN"] = "x";
        //    //us jurisdiction
        //    data[taxResidency.UsJurisdiction == true ? "companyUSACourtY" : "companyUSACourtN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "companyPersonUSAY" : "companyPersonUSAN"] = "x";
        //    //us tax obligations company
        //    data[taxResidency.SaCompanyUsTaxObligations == true ? "companyUSATaxY" : "companyUSATaxN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "companyUSAIndicatorsY" : "companyUSAIndicatorsN"] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.SACompanyFATCA == true ? "entityFinInstYes" : "entityFinInstNo"] = "x";
        //    data[entityDetails.SaCompanyInvestNotResident == true ? "entityInvestEntityNotResidentY" : "entityInvestEntityNotResidentN"] = "x";
        //    data[entityDetails.SaCompanyOtherInvestment == true ? "entityOtherInvestmentY" : "entityOtherInvestmentN"] = "x";
        //    data[entityDetails.SaCompanyDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.SaCompanyDepo == true)
        //    {
        //        data["entityGIINNp"] = entityDetails.SaCompanyGIIN;
        //    }
        //    /*if (entityDetails.FATCAStatus == "Trustee Documented Trust")
        //    {
        //        data["trustDocumented"] = "X";
        //        data["trustManagingFIName"] = entityDetails.FIName;
        //        data["trustManagingFIGIIN"] = entityDetails.FIGIIN;
        //        data["trustManagingFICountry"] = entityDetails.FICountry;
        //    }
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["trustCompliant"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["trustExempt"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["trustDocumentedFI"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["trustNonReportingFI"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["trustGIINNotYetReceived"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["trustLimitedFI"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Participating FI")
        //    {
        //        data["trustNonParticipating"] = "X";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["trustOtherEntity"] = "X";
        //    }*/
        //    /*if (entityDetails.FATCAStatus == "Trustee Documented Trust")
        //    {
        //        data["trustDocumented"] = "X";
        //        data["trustManagingFIName"] = entityDetails.FIName;
        //        data["trustManagingFIGIIN"] = entityDetails.FIGIIN;
        //        data["trustManagingFICountry"] = entityDetails.FICountry;
        //    }*/
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    //data[entityDetails.StockExchange == true || entityDetails.RelatedEntity == true ? "companyStockExchangeY" : "companyStockExchangeN"] = "X";
        //    data[entityDetails.StockExchange == true || entityDetails.RelatedEntity == true ? "companyStockExchangeY" : "companyStockExchangeN"] = "x";
        //    if (entityDetails.StockExchange)
        //    {
        //        data["companyStockExchangeName"] = entityDetails.ExchangeName;
        //    }
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["companyRelatedName"] = entityDetails.RelatedName;
        //        data["companyRelatedExchangeName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "companyGovernmentY" : "companyGovernmentN"] = "x";
        //    data[entityDetails.CentralBank == true ? "companyCentralBankY" : "companyCentralBankN"] = "x";
        //    data[entityDetails.NFEIntOrg == true ? "companyNFEInternationalY" : "companyNFEInternationalN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "companyNFELiquidatingY" : "companyNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "companyNFETreasuryY" : "companyNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "companyNFEDirectY" : "companyNFEDirectN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["companyNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["companyNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["companyNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "companyPBOY" : "companyPBON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["companyPBONumber"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "companyPassiveAssetsY" : "companyPassiveAssetsN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{associatedUser.FirstName} {associatedUser.LastName}";
        //    data["signaturePersonCapacity"] = associatedParty.Capacity;
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SecondaryScheduleSACompany.pdf", data),
        //        DocumentType = DocumentTypesEnum.SecondarySchedule,
        //        Name = $"Associated Party, Company : {associatedUser.FirstName} {associatedUser.LastName}.pdf"
        //    };
        //}

        ////COMPANY ENTERPRISE GENERAL
        //private ApplicationDocumentsModel CreateSACompanyEnterpriseGeneralDeclaration(PrimarySaCompanyModel schedule, RequiredSecondarySchedulesModel associates, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var saCompanyDetails = schedule.SaCompanyDetails;

        //    var employmentOptions = new List<string>() { };

        //    if (associates.Contacts.Count() > 1)
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}" + ", " + $"{ associates.Contacts.ElementAt(1).Name ?? string.Empty} {associates.Contacts.ElementAt(1).Surname ?? string.Empty}";
        //        data["capacity2"] = associates.Contacts.ElementAt(1).Capacity ?? string.Empty;
        //        data["signDate2"] = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}";
        //        data["capacity2"] = "";
        //        data["signDate2"] = "";
        //    }
        //    data["capacity1"] = associates.Contacts.First().Capacity;
        //    data["entityName"] = saCompanyDetails.RegisteredName ?? string.Empty;
        //    data["signDate1"] = DateTime.Today.ToString("dd/MM/yyyy");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("EnterpriseGeneralDeclaration.pdf", data),
        //        DocumentType = DocumentTypesEnum.SACompanyEnterpriseGeneralDeclaration
        //    };
        //}

        ////COMPANY SHAREHOLDER
        //private ApplicationDocumentsModel CreateSACompanySoleShareholderDeclaration(PrimarySaCompanyModel schedule, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var saCompanyDetails = schedule.SaCompanyDetails;
        //    //var entityDetails = schedule.EntityDetails;
        //    //var funding = schedule.SaCompanyFunding;
        //    //var taxResidency = schedule.SaCompanyTaxResidency;
        //    //var employmentOptions = new List<string>() { };

        //    data["entityName"] = saCompanyDetails.RegisteredName;
        //    data["regNumber"] = saCompanyDetails.RegistrationNumber;
        //    data["industry"] = saCompanyDetails.Industry;
        //    data["nature"] = saCompanyDetails.Operations;
        //    data["accNumber"] = saCompanyDetails.AccNumber;
        //    data["accDescription"] = saCompanyDetails.AccDescription;
        //    data["shareholderID"] = user.IdNumber;
        //    data["shareholderName"] = $"{user.FirstName} {user.LastName}";
        //    data["signDate"] = DateTime.Today.ToString("ddMMyyyy");

        //    foreach (var address in saCompanyDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Residential)
        //        {
        //            data["shareholderAddress1"] = $"{address.UnitNumber} {address.ComplexName}" + " "
        //          + $"{address.StreetNumber} {address.StreetName}";

        //            data["shareholderAddress2"] = $"{address.Suburb}" + ", " +
        //               $"{address.City}";

        //            data["shareholderAddressPost"] = address.PostalCode ?? string.Empty;
        //        }
        //    }

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SACompanySoleShareholderDeclaration.pdf", data),
        //        DocumentType = DocumentTypesEnum.SACompanySoleShareholderDeclaration
        //    };
        //}

        ////PARTNERSHIP
        //private ApplicationDocumentsModel CreatePrimaryPartnershipSchedule(PrimaryPartnershipModel schedule, BankVerificationsModel bv, UserModel user, MarketingModel marketing)
        //{
        //    var data = new Dictionary<string, string>();
        //    var partnershipDetails = schedule.PartnershipDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.PartnershipFunding;
        //    var taxResidency = schedule.PartnershipTaxResidency;
        //    var userDetails = user;
        //    var employmentOptions = new List<string>() { };

        //    //PARTNERSHIP details
        //    data[partnershipDetails.ProfessionalPartnership == true ? "partnershipProfessionalY" : "partnershipProfessionalN"] = "x";

        //    data["partnershipProfessionalType"] = partnershipDetails.PartnershipType ?? string.Empty;
        //    data["partnershipProfessionalBody"] = partnershipDetails.ProfessionalBody ?? string.Empty;
        //    data["partnershipName"] = partnershipDetails.Name ?? string.Empty;
        //    data["partnershipIndustry"] = partnershipDetails.Industry ?? string.Empty;
        //    data["partnershipTradeArea"] = partnershipDetails.TradeArea ?? string.Empty;
        //    data["partnershipOps"] = partnershipDetails.Operations ?? string.Empty;
        //    data["partnershipPlaceOfManagement"] = partnershipDetails.Location ?? string.Empty;

        //    //  ADDRESS DETAILS
        //    if (partnershipDetails.InCareAddress == false)
        //        data["tradeAddressCareOfN"] = "x";
        //    else
        //        data["tradeAddressCareOfY"] = "x";

        //    data["tradeAddressInCareOfName"] = partnershipDetails.InCareName ?? string.Empty;

        //    if (partnershipDetails.PostalSameAsTrading == false)
        //        data["postalAddressSameAsTradeN"] = "x";
        //    else
        //        data["postalAddressSameAsTradeY"] = "x";

        //    if (partnershipDetails.PostalInCareAddress == false)
        //        data["postalAddressCareOfN"] = "x";
        //    else
        //        data["postalAddressCareOfY"] = "x";

        //    data["postalAddressInCareOfName"] = partnershipDetails.PostalInCareName ?? string.Empty;

        //    foreach (var address in partnershipDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // Trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // Postal details
        //            data["postalAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["postalAddressComplexName"] = address.ComplexName ?? string.Empty;
        //            data["postalAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressStreetName"] = address.StreetName ?? string.Empty;
        //            data["postalAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["postalAddressCity"] = address.City ?? string.Empty;
        //            data["postalAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = partnershipDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = partnershipDetails.Email ?? string.Empty;
        //    //data["contactCellPhoneNo"] = partnershipDetails.Mobile ?? string.Empty;
        //    data["contactCellPhoneNo"] = "0" + partnershipDetails.Mobile ?? string.Empty;

        //    // BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankAccName"] = bv.BankName ?? string.Empty;
        //    data["bankAccBranchCode"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // STATIC FIELDS
        //    data["static"] = "X";

        //    // SOURCE OF FUNDS
        //    data["sourceFundsExpectedTurnover"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditionalFunds"] = funding.SourceOfAdditional;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    /*var sourceOfFundsOptions = new List<string>()
        //    {
        //        "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //        "salary", "Dividends", "interest", "bonuses"
        //    };

        //    if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //        data[funding.SourceOfFunds] = "X";
        //    else
        //        data["other"] = funding.SourceOfFunds;*/

        //    /*var sourceOfWealthOptions = new List<string>()
        //    {
        //        "investments", "shareSales", "propertySales",
        //        "companySales", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "X";
        //    else
        //    {
        //        data["wealth_Other"] = funding.SourceOfWealthOther;
        //    }*/

        //    //Source of Wealth
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    data["wealthOther"] = funding.wealthOther;

        //    // TAX DETAILS
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxOutsideSAY" : "taxOutsideSAN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    //us tax obligations persons
        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "x";
        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "partnershipCreatedUSAY" : "partnershipCreatedUSAN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "partnershipPersonUSAY" : "partnershipPersonUSAN"] = "x";
        //    //us tax obligations company
        //    data[taxResidency.PartnershipUsTaxObligations == true ? "partnershipUSATaxY" : "partnershipUSATaxN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "partnershipUSAIndicatorsY" : "partnershipUSAIndicatorsN"] = "x";

        //    // MARKETING
        //    data[marketing.NedbankOffers == true ? "product_True" : "product_False"] = "x";
        //    data[marketing.OtherOffers == true ? "offers_True" : "offers_False"] = "x";
        //    data[marketing.ReputableOrg == true ? "research_True" : "research_False"] = "x";
        //    data[marketing.OtherMethodOfCommunication == true ? "otherMethod_True" : "otherMethod_False"] = "x";

        //    data["comm" + marketing.MethodOfCommunication] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.PartnershipFATCA == true ? "entityFinInstYes" : "entityFinInstNo"] = "x";
        //    data[entityDetails.PartnershipInvestNotResident == true ? "entityInvestEntityNotResidentY" : "entityInvestEntityNotResidentN"] = "x";
        //    data[entityDetails.PartnershipOtherInvestment == true ? "entityOtherInvestmentY" : "entityOtherInvestmentN"] = "x";
        //    data[entityDetails.PartnershipDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.PartnershipDepo == true)
        //    {
        //        data["entityGIIN"] = entityDetails.PartnershipGIIN;
        //    }
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOther"] = "x";
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "partnershipStockExchangeY" : "partnershipStockExchangeN"] = "x";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["partnershipRelatedName"] = entityDetails.RelatedName;
        //        data["partnershipRelatedExchangeName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "partnershipGovernmentY" : "partnershipGovernmentN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "partnershipNFELiquidatingY" : "partnershipNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "partnershipNFETreasuryY" : "partnershipNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "partnershipNFEDirectY" : "partnershipNFEDirectN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["partnershipNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["partnershipNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["partnershipNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "partnershipPBOY" : "partnershipPBON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["partnershipPBONumber"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "partnershipPassiveAssetsY" : "partnershipPassiveAssetsN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{user.FirstName} {user.LastName}";
        //    data["signaturePersonCapacity"] = "Self";
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SchedulePartnership.pdf", data),
        //        DocumentType = DocumentTypesEnum.PrimarySchedule
        //    };
        //}

        ////SECONDARY PARTNERSHIP
        //private ApplicationDocumentsModel CreateSecondaryPartnershipSchedule(PrimaryPartnershipModel schedule, BankVerificationsModel bv, UserModel primaryUser, UserModel associatedUser, RequiredSecondarySchedulesContactsModel associatedParty)
        //{
        //    var data = new Dictionary<string, string>();
        //    var partnershipDetails = schedule.PartnershipDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.PartnershipFunding;
        //    var taxResidency = schedule.PartnershipTaxResidency;

        //    //Primary Client Details
        //    data["partnershipPrimaryClientName"] = primaryUser.FirstName + " " + primaryUser.LastName ?? string.Empty;
        //    data["partnershipClientRegNumber"] = "N/A";

        //    //Association Details
        //    data[$"assoc{associatedParty.AssociationType}"] = "x";

        //    //Partnership Details
        //    data[partnershipDetails.ProfessionalPartnership == true ? "partnershipProfessionalY" : "partnershipProfessionalN"] = "x";
        //    data["partnershipProfessionalType"] = partnershipDetails.PartnershipType ?? string.Empty;
        //    data["partnershipProfessionalBody"] = partnershipDetails.ProfessionalBody ?? string.Empty;
        //    data["partnershipName"] = partnershipDetails.Name ?? string.Empty;
        //    data["partnershipIndustry"] = partnershipDetails.Industry ?? string.Empty;
        //    data["partnershipTradeArea"] = partnershipDetails.TradeArea ?? string.Empty;
        //    data["partnershipOps"] = partnershipDetails.Operations ?? string.Empty;
        //    data["partnershipPlaceOfManagement"] = partnershipDetails.Location ?? string.Empty;

        //    //Address Details
        //    if (partnershipDetails.InCareAddress == false)
        //        data["tradeAddressCareOfN"] = "x";
        //    else
        //        data["tradeAddressCareOfY"] = "x";

        //    data["tradeAddressInCareOfName"] = partnershipDetails.InCareName ?? string.Empty;

        //    if (partnershipDetails.PostalSameAsTrading == false)
        //        data["postalAddressSameAsTradeN"] = "x";
        //    else
        //        data["postalAddressSameAsTradeY"] = "x";

        //    if (partnershipDetails.PostalInCareAddress == false)
        //        data["postalAddressCareOfN"] = "x";
        //    else
        //        data["postalAddressCareOfY"] = "x";

        //    data["postalAddressInCareOfName"] = partnershipDetails.PostalInCareName ?? string.Empty;

        //    foreach (var address in partnershipDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // Trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // Postal details
        //            data["postalAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["postalAddressComplexName"] = address.ComplexName ?? string.Empty;
        //            data["postalAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["postalAddressStreetName"] = address.StreetName ?? string.Empty;
        //            data["postalAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["postalAddressCity"] = address.City ?? string.Empty;
        //            data["postalAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["postalAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = partnershipDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = partnershipDetails.Email ?? string.Empty;
        //    data["contactCellPhoneNo"] = partnershipDetails.Mobile != null ? "0" + partnershipDetails.Mobile : string.Empty;

        //    // BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankAccName"] = bv.BankName ?? string.Empty;
        //    data["bankAccBranchCode"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // SOURCE OF FUNDS
        //    data["sourceFundsExpectedTurnover"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditionalFunds"] = funding.SourceOfAdditional;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";
        //    /*
        //    var sourceOfFundsOptions = new List<string>()
        //    {
        //        "monthlySalary", "commission", "bonus", "turnover", "annuity", "onceOffPayment",
        //        "salary", "Dividends", "interest", "bonuses"
        //    };

        //    if (sourceOfFundsOptions.Contains(funding.SourceOfFunds))
        //        data[funding.SourceOfFunds] = "X";
        //    else
        //        data["other"] = funding.SourceOfFunds;
        //    */
        //    var sourceOfWealthOptions = new List<string>()
        //    {
        //        "investments", "shareSales", "propertySales",
        //        "companySales", "loan", "gift"
        //    };

        //    if (sourceOfWealthOptions.Contains(funding.SourceOfWealth))
        //        data[funding.SourceOfWealth] = "x";
        //    else
        //        data["wealth_Other"] = funding.SourceOfWealthOther;

        //    // TAX DETAILS
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxOutsideSAY" : "taxOutsideSAN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    //us tax obligations persons
        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonObligationY" : "taxPersonObligationN"] = "x";
        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "partnershipCreatedUSAY" : "partnershipCreatedUSAN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "partnershipPersonUSAY" : "partnershipPersonUSAN"] = "x";
        //    //us tax obligations company
        //    data[taxResidency.PartnershipUsTaxObligations == true ? "partnershipUSATaxY" : "partnershipUSATaxN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "partnershipUSAIndicatorsY" : "partnershipUSAIndicatorsN"] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.PartnershipFATCA == true ? "entityFinInstYes" : "entityFinInstNo"] = "x";
        //    data[entityDetails.PartnershipInvestNotResident == true ? "entityInvestEntityNotResidentY" : "entityInvestEntityNotResidentN"] = "x";
        //    data[entityDetails.PartnershipOtherInvestment == true ? "entityOtherInvestmentY" : "entityOtherInvestmentN"] = "x";
        //    data[entityDetails.PartnershipDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.PartnershipDepo == true)
        //    {
        //        data["entityGIIN"] = entityDetails.PartnershipGIIN;
        //    }
        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOther"] = "x";
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "partnershipStockExchangeY" : "partnershipStockExchangeN"] = "x";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["partnershipRelatedName"] = entityDetails.RelatedName;
        //        data["partnershipRelatedExchangeName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "partnershipGovernmentY" : "partnershipGovernmentN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "partnershipNFELiquidatingY" : "partnershipNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "partnershipNFETreasuryY" : "partnershipNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "partnershipNFEDirectY" : "partnershipNFEDirectN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["partnershipNFEDirectGIIN"] = entityDetails.NFEGIIN;
        //        data["partnershipNFEDirectSponsor"] = entityDetails.NFESponsorName;
        //        data["partnershipNFEDirectSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "partnershipPBOY" : "partnershipPBON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["partnershipPBONumber"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "partnershipPassiveAssetsY" : "partnershipPassiveAssetsN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{associatedUser.FirstName} {associatedUser.LastName}";
        //    data["signaturePersonCapacity"] = associatedParty.Capacity;
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SecondarySchedulePartnership.pdf", data),
        //        DocumentType = DocumentTypesEnum.SecondarySchedule,
        //        Name = $"Associated Party, Partnership : {associatedUser.FirstName} {associatedUser.LastName}.pdf"
        //    };
        //}

        ////PARTNERSHIP ENTERPRISE GENERAL
        //private ApplicationDocumentsModel CreatePartnershipEnterpriseGeneralDeclaration(PrimaryPartnershipModel schedule, RequiredSecondarySchedulesModel associates, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var partnershipDetails = schedule.PartnershipDetails;
        //    var userDetails = user;

        //    var employmentOptions = new List<string>() { };

        //    if (associates.Contacts.Count() > 1)
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}" + ", " + $"{ associates.Contacts.ElementAt(1).Name ?? string.Empty} {associates.Contacts.ElementAt(1).Surname ?? string.Empty}";
        //        data["capacity2"] = associates.Contacts.ElementAt(1).Capacity ?? string.Empty;
        //        data["signDate2"] = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}";
        //        data["capacity2"] = "";
        //        data["signDate2"] = "";
        //    }
        //    data["capacity1"] = associates.Contacts.First().Capacity;
        //    data["entityName"] = partnershipDetails.Name ?? string.Empty;
        //    data["signDate1"] = DateTime.Today.ToString("dd/MM/yyyy");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("EnterpriseGeneralDeclaration.pdf", data),
        //        DocumentType = DocumentTypesEnum.PartnershipEnterpriseGeneralDeclaration
        //    };
        //}

        ////CC
        //private ApplicationDocumentsModel CreatePrimaryCCSchedule(PrimaryCCModel schedule, BankVerificationsModel bv, UserModel clientUser, MarketingModel marketing)
        //{
        //    var data = new Dictionary<string, string>();
        //    var ccDetails = schedule.CCDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.CCFunding;
        //    var taxResidency = schedule.CCTaxResidency;
        //    var userDetails = clientUser;

        //    var employmentOptions = new List<string>() { };

        //    data["ccRegName"] = ccDetails.RegisteredName ?? string.Empty;
        //    data["ccTradeName"] = ccDetails.TradingName ?? string.Empty;
        //    data["ccRegNo"] = ccDetails.RegistrationNumber ?? string.Empty;
        //    data["ccIndustry"] = ccDetails.Industry ?? string.Empty;
        //    data["ccTradeArea"] = ccDetails.TradeArea ?? string.Empty;
        //    data["ccOps"] = ccDetails.Purpose ?? string.Empty;
        //    data["ccPlaceOfManagement"] = ccDetails.Location ?? string.Empty;
        //    data["ccVATNo"] = ccDetails.VATNumber ?? string.Empty;

        //    //  ADDRESS DETAILS
        //    if (!ccDetails.TradeInCareAddress)
        //        data["tradeAddressInCareOfN"] = "x";
        //    else
        //        data["tradeAddressInCareOfY"] = "x";
        //    data["tradeAddressInCareOfName"] = ccDetails.TradeInCareName ?? string.Empty;

        //    if (!ccDetails.PostalInCareAddress)
        //        data["postAddressInCareOfN"] = "x";
        //    else
        //        data["postAddressInCareOfY"] = "x";
        //    data["postAddressInCareOfName"] = ccDetails.PostalInCareName ?? string.Empty;

        //    if (ccDetails.TradeSameAsReg == false)
        //        data["tradeAddressSameAsRegN"] = "x";
        //    else
        //        data["tradeAddressSameAsRegY"] = "x";

        //    if (ccDetails.PostalSameAsReg == false)
        //        data["postAddressSameAsTradeN"] = "x";
        //    else
        //        data["postAddressSameAsTradeY"] = "x";

        //    foreach (var address in ccDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Registered)
        //        {
        //            // Registered details
        //            data["regAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["regAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["regAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["regAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["regAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["regAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["regAddressCity"] = address.City ?? string.Empty;
        //            data["regAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // Trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        else if (address.Type == AddressTypesEnum.Postal)
        //        {
        //            // Postal details
        //            data["postAddressLine1"] = address.StreetNumber ?? string.Empty;
        //            data["postAddressLine2"] = address.StreetName ?? string.Empty;
        //            data["postAddressLine3"] = address.Suburb ?? string.Empty;
        //            data["postAddressLine4"] = address.City ?? string.Empty;
        //            data["postAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["postAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = ccDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = ccDetails.Email ?? string.Empty;
        //    data["contactCell"] = "0" + ccDetails.Mobile ?? string.Empty;

        //    // BANK DETAILS
        //    data["bankAccHolder"] = $"{bv.Initials ?? string.Empty} {bv.Surname ?? string.Empty}";
        //    data["bankName"] = bv.BankName ?? string.Empty;
        //    data["bankBranchCode"] = bv.BranchCode ?? string.Empty;
        //    data["bankAccNo"] = bv.AccountNumber ?? string.Empty;
        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };
        //    data["bankAccType"] = AccountTypes[bv.AccountType] ?? string.Empty;

        //    // STATIC FIELDS
        //    data["static"] = "X";

        //    // SOURCE OF FUNDS
        //    data["sourceFundsExpectedMonthly"] = funding.ExpectedMonthlyTurnover;
        //    data["sourceFundsAdditional"] = funding.SourceOfAdditional;
        //    //data["sourceFundsAdditionalDetails"] = funding.DonorDetails;
        //    data["sourceFundsAdditionalAmount"] = funding.DonorAmount;
        //    //data["sourceFundsInternational"] = funding.InternationalTransactions;

        //    if (funding.InternationalTransactions)
        //        data["sourceFundsInternational"] = "Yes";
        //    else data["sourceFundsInternational"] = "No";

        //    //Source of Wealth
        //    if (funding.wealthInvestments)
        //        data["wealthInvestments"] = "x";

        //    if (funding.wealthShares)
        //        data["wealthShares"] = "x";

        //    if (funding.wealthProperty)
        //        data["wealthProperty"] = "x";

        //    if (funding.wealthCompany)
        //        data["wealthCompany"] = "x";

        //    if (funding.wealthLoan)
        //        data["wealthLoan"] = "x";

        //    if (funding.wealthGift)
        //        data["wealthGift"] = "x";

        //    data["wealthOther"] = funding.wealthOther;

        //    // TAX DETAILS
        //    data["taxNo"] = taxResidency.TaxNumber ?? string.Empty;

        //    data[taxResidency.InternationalObligations == true ? "taxInternationalY" : "taxInternationalN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonInternationalY" : "taxPersonInternationalN"] = "x";

        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "taxRegUSAY" : "taxRegUSAN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "taxPersonUSAY" : "taxPersonUSAN"] = "x";
        //    //us tax obligations company
        //    data[taxResidency.CCUsTaxObligations == true ? "taxUSAY" : "taxUSAN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "taxUSAIndicatorY" : "taxUSAIndicatorN"] = "x";

        //    // MARKETING
        //    data[marketing.NedbankOffers == true ? "product_True" : "product_False"] = "x";
        //    data[marketing.OtherOffers == true ? "offers_True" : "offers_False"] = "x";
        //    data[marketing.ReputableOrg == true ? "research_True" : "research_False"] = "x";
        //    data[marketing.OtherMethodOfCommunication == true ? "otherMethod_True" : "otherMethod_False"] = "x";

        //    data["comm" + marketing.MethodOfCommunication] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.CCFATCA == true ? "entityFIFactaY" : "entityFIFactaN"] = "x";
        //    data[entityDetails.CCInvestNotResident == true ? "entityInvestNotResidentY" : "entityInvestNotResidentN"] = "x";
        //    data[entityDetails.CCOtherInvestment == true ? "entityOtherInvestY" : "entityOtherInvestN"] = "x";
        //    data[entityDetails.CCDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.CCDepo == true)
        //    {
        //        data["entityGIIN"] = entityDetails.CCGIIN;
        //    }

        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "ccRelatedStockY" : "ccRelatedStockN"] = "x";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["ccRelatedName"] = entityDetails.RelatedName;
        //        data["ccRelatedStockName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "ccGOVTY" : "ccGOVTN"] = "x";
        //    data[entityDetails.CentralBank == true ? "ccNFECentralY" : "ccNFECentralN"] = "x";
        //    data[entityDetails.NFEIntOrg == true ? "ccNFEInternationalY" : "ccNFEInternationalN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "ccNFELiquidatingY" : "ccNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "ccNFETreasuryY" : "ccNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "ccDirectNFEY" : "ccDirectNFEN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["ccDirectNFEGIIN"] = entityDetails.NFEGIIN;
        //        data["ccSponsorName"] = entityDetails.NFESponsorName;
        //        data["ccSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "ccNPOY" : "ccNPON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["ccNPOPBONO"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "ccPassiveIncomeY" : "ccPassiveIncomeN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{clientUser.FirstName} {clientUser.LastName}";
        //    data["signaturePersonCapacity"] = "Self";
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("ScheduleCC.pdf", data),
        //        DocumentType = DocumentTypesEnum.PrimarySchedule
        //    };
        //}

        ////SECONDARY CC
        //private ApplicationDocumentsModel CreateSecondaryCCSchedule(PrimaryCCModel schedule, BankVerificationsModel bv, UserModel clientUser, UserModel associatedUser, RequiredSecondarySchedulesContactsModel contact)
        //{
        //    var data = new Dictionary<string, string>();
        //    var ccDetails = schedule.CCDetails;
        //    var entityDetails = schedule.EntityDetails;
        //    var funding = schedule.CCFunding;
        //    var taxResidency = schedule.CCTaxResidency;

        //    var employmentOptions = new List<string>() { };

        //    //Primary Client Details
        //    data["ccPrimaryClientName"] = clientUser.FirstName + " " + clientUser.LastName ?? string.Empty;
        //    data["ccClientRegNumber"] = ccDetails.RegistrationNumber ?? string.Empty;

        //    //Association Details
        //    data[$"assoc{contact.AssociationType}"] = "x";

        //    data["ccRegName"] = ccDetails.RegisteredName ?? string.Empty;
        //    data["ccTradeName"] = ccDetails.TradingName ?? string.Empty;
        //    data["ccRegNo"] = ccDetails.RegistrationNumber ?? string.Empty;
        //    data["ccIndustry"] = ccDetails.Industry ?? string.Empty;
        //    data["ccTradeArea"] = ccDetails.TradeArea ?? string.Empty;
        //    data["ccOps"] = ccDetails.Purpose ?? string.Empty;
        //    data["ccPlaceOfManagement"] = ccDetails.Location ?? string.Empty;
        //    data["ccVATNo"] = ccDetails.VATNumber ?? string.Empty;
        //    data["static"] = "x";

        //    if (ccDetails.TradeSameAsReg == false)
        //        data["tradeAddressSameAsRegN"] = "x";
        //    else
        //        data["tradeAddressSameAsRegY"] = "x";

        //    if (!ccDetails.TradeInCareAddress)
        //        data["tradeAddressInCareOfN"] = "x";
        //    else
        //        data["tradeAddressInCareOfY"] = "x";
        //    data["tradeAddressInCareOfName"] = ccDetails.TradeInCareName ?? string.Empty;

        //    foreach (var address in ccDetails.AddressItems)
        //    {
        //        if (address.Type == AddressTypesEnum.Registered)
        //        {
        //            // Registered details
        //            data["regAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["regAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["regAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["regAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["regAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["regAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["regAddressCity"] = address.City ?? string.Empty;
        //            data["regAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        if (address.Type == AddressTypesEnum.Trading)
        //        {
        //            // Trading details
        //            data["tradeAddressUnitNo"] = address.UnitNumber ?? string.Empty;
        //            data["tradeAddressStreetNo"] = address.StreetNumber ?? string.Empty;
        //            data["tradeAddressSuburb"] = address.Suburb ?? string.Empty;
        //            data["tradeAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //            data["tradeAddressComplex"] = address.ComplexName ?? string.Empty;
        //            data["tradeAddressStreet"] = address.StreetName ?? string.Empty;
        //            data["tradeAddressCity"] = address.City ?? string.Empty;
        //            data["tradeAddressCountry"] = address.Country ?? string.Empty;
        //        }
        //        //else if (address.Type == AddressTypesEnum.Postal)
        //        //{
        //        //    // Postal details
        //        //    data["postAddressLine1"] = address.StreetNumber ?? string.Empty;
        //        //    data["postAddressLine2"] = address.StreetName ?? string.Empty;
        //        //    data["postAddressLine3"] = address.Suburb ?? string.Empty;
        //        //    data["postAddressLine4"] = address.City ?? string.Empty;
        //        //    data["postAddressPostalCode"] = address.PostalCode ?? string.Empty;
        //        //    data["postAddressCountry"] = address.Country ?? string.Empty;
        //        //}
        //    }

        //    //  CONTACT DETAILS
        //    data["contactPerson"] = ccDetails.ContactPerson ?? string.Empty;
        //    data["contactEmail"] = ccDetails.Email ?? string.Empty;
        //    data["contactCell"] = ccDetails.Mobile != null ? "0" + ccDetails.Mobile : string.Empty;

        //    // TAX DETAILS
        //    data[taxResidency.InternationalObligations == true ? "taxInternationalY" : "taxInternationalN"] = "x";

        //    int c = 0;
        //    foreach (var e in taxResidency.TaxResidencyItems)
        //    {
        //        c++;
        //        data[$"taxResidence{c}Country"] = e.Country ?? string.Empty;
        //        data[$"taxResidence{c}TIN"] = e.TinNumber ?? string.Empty;
        //        data[$"taxResidence{c}Reason"] = e.TinUnavailableReason ?? string.Empty;
        //    }

        //    data[taxResidency.PersonUsTaxObligations == true ? "taxPersonInternationalY" : "taxPersonInternationalN"] = "x";
        //    //us registered
        //    data[taxResidency.UsRegistered == true ? "taxRegUSAY" : "taxRegUSAN"] = "x";
        //    //us persons
        //    data[taxResidency.AnyUsPersons == true ? "taxPersonUSAY" : "taxPersonUSAN"] = "x";
        //    //us tax obligations company
        //    data[taxResidency.CCUsTaxObligations == true ? "taxUSAY" : "taxUSAN"] = "x";
        //    //other us indicators
        //    data[taxResidency.UsOther == true ? "taxUSAIndicatorY" : "taxUSAIndicatorN"] = "x";

        //    //ENTITY CLASSIFICATION

        //    //section1
        //    data[entityDetails.CCFATCA == true ? "entityFIFactaY" : "entityFIFactaN"] = "x";
        //    data[entityDetails.CCInvestNotResident == true ? "entityInvestNotResidentY" : "entityInvestNotResidentN"] = "x";
        //    data[entityDetails.CCOtherInvestment == true ? "entityOtherInvestY" : "entityOtherInvestN"] = "x";
        //    data[entityDetails.CCDepo == true ? "entityDepositoryY" : "entityDepositoryN"] = "x";
        //    if (entityDetails.CCFATCA == true)
        //    {
        //        data["entityGIIN"] = entityDetails.CCGIIN;
        //    }

        //    if (entityDetails.FATCAStatus == "Certified Deemed Compliant FI")
        //    {
        //        data["entityCompliant"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Exempt Beneficial Owner")
        //    {
        //        data["entityExempt"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Owner Documented FI")
        //    {
        //        data["entityDocumented"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Non Reporting FI")
        //    {
        //        data["entityNonReporting"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Other")
        //    {
        //        data["entityOther"] = "x";
        //        data["entityOtherReason"] = entityDetails.FATCAOther;
        //    }
        //    if (entityDetails.FATCAStatus == "Participating FI")
        //    {
        //        data["entityGIINNotYetReceived"] = "x";
        //    }
        //    if (entityDetails.FATCAStatus == "Limited FI")
        //    {
        //        data["entityLimited"] = "x";
        //    }

        //    //section2
        //    data[entityDetails.RelatedEntity == true ? "ccRelatedStockY" : "ccRelatedStockN"] = "x";
        //    if (entityDetails.RelatedEntity)
        //    {
        //        data["ccRelatedName"] = entityDetails.RelatedName;
        //        data["ccRelatedStockName"] = entityDetails.RelatedExchangeName;
        //    }
        //    data[entityDetails.GovEntity == true ? "ccGOVTY" : "ccGOVTN"] = "x";
        //    data[entityDetails.CentralBank == true ? "ccNFECentralY" : "ccNFECentralN"] = "x";
        //    data[entityDetails.NFEIntOrg == true ? "ccNFEInternationalY" : "ccNFEInternationalN"] = "x";
        //    data[entityDetails.NFELiquidating == true ? "ccNFELiquidatingY" : "ccNFELiquidatingN"] = "x";
        //    data[entityDetails.NFETreasury == true ? "ccNFETreasuryY" : "ccNFETreasuryN"] = "x";
        //    data[entityDetails.DirectNFE == true ? "ccDirectNFEY" : "ccDirectNFEN"] = "x";
        //    if (entityDetails.DirectNFE)
        //    {
        //        data["ccDirectNFEGIIN"] = entityDetails.NFEGIIN;
        //        data["ccSponsorName"] = entityDetails.NFESponsorName;
        //        data["ccSponsorGIIN"] = entityDetails.NFESponsorGIIN;
        //    }

        //    data[entityDetails.PBOEntity == true ? "ccNPOY" : "ccNPON"] = "x";
        //    if (entityDetails.PBOEntity)
        //    {
        //        data["ccNPOPBONO"] = entityDetails.PBONumber;

        //    }
        //    data[entityDetails.PassiveIncome == true ? "ccPassiveIncomeY" : "ccPassiveIncomeN"] = "x";

        //    // DECLARATIONS
        //    data["signaturePersonName"] = $"{clientUser.FirstName} {clientUser.LastName}";
        //    data["signaturePersonCapacity"] = contact.Capacity;
        //    data["signatureDate"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("SecondaryScheduleCC.pdf", data),
        //        DocumentType = DocumentTypesEnum.SecondarySchedule,
        //        Name = $"Associated Party, CC : {associatedUser.FirstName} {associatedUser.LastName}.pdf"
        //    };
        //}

        ////CC ENTERPRISE GENERAL
        //private ApplicationDocumentsModel CreateCCEnterpriseGeneralDeclaration(PrimaryCCModel schedule, RequiredSecondarySchedulesModel associates, BankVerificationsModel bv, UserModel user)
        //{
        //    var data = new Dictionary<string, string>();
        //    var ccDetails = schedule.CCDetails;
        //    var userDetails = user;

        //    var employmentOptions = new List<string>() { };

        //    if (associates.Contacts.Count() > 1)
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}" + ", " + $"{ associates.Contacts.ElementAt(1).Name ?? string.Empty} {associates.Contacts.ElementAt(1).Surname ?? string.Empty}";
        //        data["capacity2"] = associates.Contacts.ElementAt(1).Capacity ?? string.Empty;
        //        data["signDate2"] = DateTime.Today.ToString("dd/MM/yyyy");
        //    }
        //    else
        //    {
        //        data["attestingName"] = $"{user.FirstName} {user.LastName}";
        //        data["capacity2"] = "";
        //        data["signDate2"] = "";
        //    }
        //    data["capacity1"] = associates.Contacts.First().Capacity;
        //    data["entityName"] = ccDetails.RegisteredName ?? string.Empty;
        //    data["signDate1"] = DateTime.Today.ToString("dd/MM/yyyy");

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("EnterpriseGeneralDeclaration.pdf", data),
        //        DocumentType = DocumentTypesEnum.CCEnterpriseGeneralDeclaration
        //    };
        //}

        ////BANK VALIDATION
        //private ApplicationDocumentsModel CreateBankValidationReport(BankVerificationsModel bv, string firstName, string lastName)
        //{
        //    var data = new Dictionary<string, string>();

        //    //data["bankName"] = bv.BankName ?? string.Empty;
        //    data["name"] = $"{firstName} {lastName}";
        //    data["searchDate"] = DateTime.Now.ToString("yyyy/MM/dd");
        //    data["reference"] = bv.Reference;
        //    data["branchCode"] = bv.BranchCode ?? string.Empty;
        //    data["accountNumber"] = bv.AccountNumber ?? string.Empty;
        //    //data["accountId"] = bv.AccountType;

        //    Dictionary<string, string> AccountTypes = new Dictionary<string, string>()
        //        {
        //            {"00","Unknown"},
        //            {"01","Current / Cheque Account"},
        //            {"02","Savings Account"},
        //            {"03","Transmission Account"},
        //            {"04","Bond Account"},
        //            {"06","Subscription Share"}
        //        };

        //    data["accountType"] = AccountTypes[bv.AccountType];
        //    data["idNumber"] = bv.IdNumber ?? string.Empty;
        //    data["initials"] = bv.Initials ?? string.Empty;
        //    data["surname"] = bv.Surname ?? string.Empty;
        //    data["foundAtBank"] = bv.FoundAtBank == "00" ? "Yes" : bv.FoundAtBank == "01" ? "No" : "Undefined";
        //    data["accOpen"] = bv.AccOpen == "00" ? "Yes" : bv.AccOpen == "01" ? "No" : "Undefined";
        //    data["olderThan3Months"] = bv.OlderThan3Months == "00" ? "Yes" : bv.OlderThan3Months == "01" ? "No" : "Undefined";
        //    data["typeCorrect"] = bv.TypeCorrect == "00" ? "Yes" : bv.TypeCorrect == "01" ? "No" : "Undefined";
        //    data["idNumberMatch"] = bv.IdNumberMatch == "00" ? "Yes" : bv.IdNumberMatch == "01" ? "No" : "Undefined";
        //    data["namesMatch"] = bv.NamesMatch == "00" ? "Yes" : bv.NamesMatch == "01" ? "No" : "Undefined";
        //    data["acceptDebits"] = bv.AcceptDebits == "00" ? "Yes" : bv.AcceptDebits == "01" ? "No" : "Undefined";
        //    data["acceptCredits"] = bv.AcceptCredits == "00" ? "Yes" : bv.AcceptCredits == "01" ? "No" : "Undefined";

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("BankVerificationReport.pdf", data),
        //        DocumentType = DocumentTypesEnum.BankVerification
        //    };
        //}

        ////ENTERPRISE BO GUIDELINE
        //private ApplicationDocumentsModel CreateEnterpriseBOGuideline()
        //{
        //    var data = new Dictionary<string, string>();

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("EnterpriseBOGuideline.pdf", data),
        //        DocumentType = DocumentTypesEnum.EnterpriseBOGuideline
        //    };
        //}

        ////FNA
        //private ApplicationDocumentsModel CreateFinancialNeedsAnalysis(RecordOfAdviseModel roa, string city,
        //    string firstName, string lastname, string idNumber, UserModel advisor)
        //{
        //    var data = new Dictionary<string, string>();

        //    int c = 1;
        //    foreach (var prod in roa.SelectedProducts)
        //    {
        //        if (prod.DeveationReason == null)
        //            data[$"prod{c}"] = prod.RecommendedLumpSum > 0 ?
        //                $"{prod.ProductName}: R {prod.RecommendedLumpSum}, R {prod.RecommendedRecurringPremium}" :
        //                $"{prod.ProductName}: R {prod.RecommendedLumpSum}";
        //        else
        //            data[$"prod{c}"] = prod.AcceptedRecurringPremium > 0 ?
        //                $"{prod.ProductName}: R {prod.AcceptedLumpSum}, R {prod.AcceptedRecurringPremium}" :
        //                $"{prod.ProductName}: R {prod.AcceptedLumpSum}";
        //    }

        //    data["reason"] = $"After discussions with the client, and according to the risk profile " +
        //        $"the above investment/s were decided and agreed on.";
        //    data["signedAt"] = "Pretoria"; //advisor.BrokerDetails.City;
        //    data["day"] = DateTime.Today.Day.ToString();
        //    data["monthYear"] = $"{DateTime.Today.Month}-{DateTime.Today.Year}";
        //    data["client"] = $"{firstName} {lastname}";
        //    data["clientId"] = $"{idNumber}";
        //    data["advisor"] = $"{advisor.FirstName} {advisor.LastName}";
        //    data["advisorId"] = $"{advisor.IdNumber}";

        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("FNA.pdf", data),
        //        DocumentType = DocumentTypesEnum.FinancialNeedsAnalysis
        //    };
        //}

        ////INTRO LETTER
        //private ApplicationDocumentsModel CreateIntroLetter(
        //    AdvisorModel advisor, string firstName, string lastName, string IdNumber)
        //{
        //    // create data dictionary
        //    var data = new Dictionary<string, string>();
        //    data["advisor"] = $"{advisor.User.FirstName} {advisor.User.LastName}";
        //    data["employmentDate"] = advisor.AppointmentDate.ToString("yyyy/MM/dd") ?? string.Empty;
        //    data["mobile"] = "0" + advisor.User.MobileNumber;
        //    data["email"] = advisor.User.Email;
        //    data["supervised"] = advisor.Supervised == true ?
        //        "supervised" : "not supervised";
        //    data["confirmation"] = $"{advisor.User.FirstName} {advisor.User.LastName}";
        //    data["client"] = $"{firstName} {lastName}";
        //    data["idNumber"] = IdNumber;
        //    data["date"] = DateTime.Today.ToString("yyyy/MM/dd");

        //    // create document
        //    return new ApplicationDocumentsModel()
        //    {
        //        DocumentData = PopulateDocument("IntroLetter.pdf", data),
        //        DocumentType = DocumentTypesEnum.IntroLetter
        //    };
        //}

        ////TEST
        //public ApplicationDocumentsModel PopulateTestDocument()
        //{
        //    var data = new Dictionary<string, string>() {
        //        { "Text1", "Text box 1" },
        //        { "Text2", "Text box 2" },
        //        { "Text3", "Text box 3" },
        //    };

        //    return new ApplicationDocumentsModel() { DocumentData = PopulateDocument("FormIdTest.pdf", data) };
        //}

        //public byte[] PopulateDocument(string teplateName, Dictionary<string, string> formData)
        //{
        //    char slash = Path.DirectorySeparatorChar;
        //    string templatePath = $"{_host.WebRootPath}{slash}pdf{slash}{teplateName}";

        //    var ms = new MemoryStream();
        //    var pdf = new PdfDocument(new PdfReader(templatePath), new PdfWriter(ms));
        //    var form = PdfAcroForm.GetAcroForm(pdf, true);
        //    IDictionary<string, PdfFormField> fields = form.GetFormFields();
        //    PdfFormField toSet;

        //    foreach (var d in formData)
        //    {
        //        try
        //        {
        //            fields.TryGetValue(d.Key, out toSet);
        //            toSet.SetValue(d.Value);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine($"Form Field Error:  {d.Key}, {d.Value}");
        //        }
        //    }

        //    form.FlattenFields();
        //    pdf.Close();

        //    byte[] file = ms.ToArray();
        //    ms.Close();

        //    return file;
        //}

        //public async Task<string> SendNewApplicationNotification()
        //{
        //    var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

        //    try
        //    {
        //        var message = new MailMessage
        //        {
        //            From = new MailAddress(mailSettings.Username),
        //            Subject = "Aluma Application: New",
        //            IsBodyHtml = true
        //        };

        //        message.To.Add(new MailAddress("sales@aluma.co.za"));
        //        message.Bcc.Add(new MailAddress("johankoster@ymail.com"));

        //        message.Body = "A new application has been submitted on the client portal";

        //        var smtpClient = new SmtpClient
        //        {
        //            Host = "mail.administr8it.co.za",
        //            Port = 25,
        //            EnableSsl = false,
        //            Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
        //            Timeout = 1000000
        //        };

        //        smtpClient.Send(message);

        //        return "Success";

        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        NLog.LogManager.Shutdown();
        //    }
        //}
        //private async Task<string> SendApplicationDocumentsToBroker(ApplicationsModel app, AdvisorModel broker)
        //{
        //    var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

        //    UserMail um = new UserMail()
        //    {
        //        Email = broker.User.Email,
        //        Name = app.User.FirstName + " " + app.User.LastName,
        //        Subject = "Aluma Capital: Application Complete " + app.User.FirstName + " " + app.User.LastName,
        //        Template = "ApplicationComplete"
        //    };

        //    try
        //    {
        //        var message = new MailMessage
        //        {
        //            From = new MailAddress(mailSettings.Username),
        //            Subject = um.Subject,
        //            IsBodyHtml = true
        //        };

        //        message.To.Add(new MailAddress(broker.User.Email));
        //        message.Bcc.Add(new MailAddress("johankoster@ymail.com"));
        //        message.Bcc.Add(new MailAddress("nadine@aluma.co.za"));

        //        foreach (var doc in app.Documents)
        //        {
        //            var stream = new MemoryStream(doc.DocumentData);

        //            var attachment = new Attachment(stream, doc.Name);

        //            message.Attachments.Add(attachment);
        //        };

        //        message.Body = "Application Completed: " + um.Name;

        //        var smtpClient = new SmtpClient
        //        {
        //            Host = "mail.administr8it.co.za",
        //            Port = 25,
        //            EnableSsl = false,
        //            Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
        //            Timeout = 1000000
        //        };

        //        smtpClient.Send(message);

        //        return "Success";

        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        NLog.LogManager.Shutdown();
        //    }
        //}
        //public async Task<string> SendSwitchPortfoliolOTP(UserModel user, AdvisorModel adviser, string otp, ApplicationsModel app)
        //{
        //    var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();

        //    try
        //    {
        //        UserMail um = new UserMail()
        //        {
        //            Email = user.Email,
        //            Name = user.FirstName + " " + user.LastName,
        //            Template = "SwitchPortfolio"
        //        };

        //        char slash = Path.DirectorySeparatorChar;
        //        string templatePath = $"{_host.WebRootPath}{slash}htmlTemplates{slash}{um.Template}.html";

        //        var systemSettings = _config.GetSection("SystemSettingsDto").Get<SystemSettingsDto>();

        //        // Create Body Builder
        //        MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

        //        // Create streamreader to read content of the the given template
        //        using (StreamReader sr = File.OpenText(templatePath))
        //        {
        //            bb.HtmlBody = sr.ReadToEnd();
        //        }

        //        bb.HtmlBody = string.Format(bb.HtmlBody, user.FirstName + " " + user.LastName, otp);

        //        string msg = bb.HtmlBody;

        //        var message = new MailMessage
        //        {
        //            From = new MailAddress(mailSettings.Username),
        //            Subject = "Aluma Capital: Portfolio Switch OTP " + otp,
        //            IsBodyHtml = true,
        //            Body = msg.Replace(Environment.NewLine, "<br/>"),
        //            BodyEncoding = System.Text.Encoding.ASCII
        //        };

        //        message.To.Add(new MailAddress(user.Email));
        //        message.Bcc.Add(new MailAddress("johankoster@ymail.com"));

        //        foreach (var doc in app.Documents)
        //        {
        //            var stream = new MemoryStream(doc.DocumentData);

        //            var attachment = new Attachment(stream, doc.Name);

        //            message.Attachments.Add(attachment);
        //        };

        //        var smtpClient = new SmtpClient
        //        {
        //            Host = "mail.administr8it.co.za",
        //            Port = 587,
        //            EnableSsl = false,
        //            Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
        //            Timeout = 1000000
        //        };

        //        //var smtpClient = new SmtpClient
        //        //{
        //        //    Host = "smtp.office365.com",
        //        //    Port = 587,
        //        //    EnableSsl = true,
        //        //    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
        //        //    Timeout = 1000000
        //        //};

        //        smtpClient.Send(message);

        //        return "Success";

        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        NLog.LogManager.Shutdown();
        //    }
        //}
        //public async Task<string> SendSwitchPortfolioSignedDocs(UserModel user, AdvisorModel adviser, ApplicationsModel app)
        //{
        //    var mailSettings = _config.GetSection("MailServerSettings").Get<MailServerSettingsDto>();
        //    try
        //    {
        //        UserMail um = new UserMail()
        //        {
        //            Email = user.Email,
        //            Name = user.FirstName + " " + user.LastName,
        //            Template = "SwitchPortfolioSigned"
        //        };

        //        char slash = Path.DirectorySeparatorChar;
        //        string templatePath = $"{_host.WebRootPath}{slash}htmlTemplates{slash}{um.Template}.html";

        //        var systemSettings = _config.GetSection("SystemSettingsDto").Get<SystemSettingsDto>();

        //        // Create Body Builder
        //        MimeKit.BodyBuilder bb = new MimeKit.BodyBuilder();

        //        // Create streamreader to read content of the the given template
        //        using (StreamReader sr = File.OpenText(templatePath))
        //        {
        //            bb.HtmlBody = sr.ReadToEnd();
        //        }

        //        bb.HtmlBody = string.Format(bb.HtmlBody, user.FirstName + " " + user.LastName);

        //        string msg = bb.HtmlBody;

        //        var message = new MailMessage
        //        {
        //            From = new MailAddress(mailSettings.Username),
        //            Subject = "Aluma Capital: Portfolio Switch Complete",
        //            IsBodyHtml = true,
        //            Body = msg.Replace(Environment.NewLine, "<br/>"),
        //            BodyEncoding = System.Text.Encoding.ASCII
        //        };

        //        message.To.Add(new MailAddress(user.Email));
        //        message.Bcc.Add(new MailAddress(adviser.User.Email));
        //        message.Bcc.Add(new MailAddress("johankoster@ymail.com"));
        //        message.Bcc.Add(new MailAddress("nadine@aluma.co.za"));

        //        foreach (var doc in app.Documents)
        //        {
        //            var stream = new MemoryStream(doc.DocumentData);

        //            var attachment = new Attachment(stream, doc.Name);

        //            message.Attachments.Add(attachment);
        //        };

        //        var smtpClient = new SmtpClient
        //        {
        //            Host = "mail.administr8it.co.za",
        //            Port = 587,
        //            EnableSsl = false,
        //            Credentials = new NetworkCredential("uloans@administr8it.co.za", "4?E$)hzUNW+v"),
        //            Timeout = 1000000
        //        };

        //        //var smtpClient = new SmtpClient
        //        //{
        //        //    Host = "smtp.office365.com",
        //        //    Port = 587,
        //        //    EnableSsl = true,
        //        //    Credentials = new NetworkCredential(mailSettings.Username, mailSettings.Password),
        //        //    Timeout = 1000000
        //        //};

        //        smtpClient.Send(message);

        //        return "Success";

        //    }
        //    catch (System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //    finally
        //    {
        //        // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
        //        NLog.LogManager.Shutdown();
        //    }
        //}
    }
}