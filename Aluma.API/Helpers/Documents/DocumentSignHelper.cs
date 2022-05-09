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
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Helpers
{

    public interface IDocumentSignHelper
    {
        #region Public Methods

        Task SendAdvisorEmails();

        Task SignDocuments(int applicationId);

        #endregion Public Methods
    }

    public class DocumentSignHelper : IDocumentSignHelper
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IWebHostEnvironment _host;
        DocumentHelper _dh;
        MailSender _ms;

        #endregion Private Fields

        #region Public Constructors

        public DocumentSignHelper(AlumaDBContext context, IConfiguration config, IFileStorageRepo fileStorage, IWebHostEnvironment host)
        {
            _context = context;
            _config = config;
            _fileStorageRepo = fileStorage;
            _host = host;
            _dh = new DocumentHelper(_context, _config, _fileStorageRepo, _host);
            _ms = new MailSender(_context, _config, _fileStorageRepo, _host);
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task SendAdvisorEmails()
        {
            //var items = _context.Advisors.Include(a => a.User).ToList();
            var items = _context.Clients.Include(c => c.User).ToList();
            foreach (var item in items)
            {
                //await _ms.SendAdvisorWelcomeEmail(item);
                await _ms.SendClientWelcomeEmail(item);
            }
        }

        public async Task SignDocuments(int applicationId)
        {
            SignatureRepo _signRepo = new SignatureRepo();

            ApplicationModel application = _context.Applications.SingleOrDefault(a => a.Id == applicationId);
            ClientModel client = _context.Clients.Include(c => c.User).ThenInclude(u => u.Address).Include(c => c.TaxResidency).Include(c => c.BankDetails).Include(c => c.FspMandate).SingleOrDefault(c => c.Id == application.ClientId);
            AdvisorModel advisor = _context.Advisors.Include(a => a.User).ThenInclude(u => u.Address).SingleOrDefault(ad => ad.Id == client.AdvisorId);

            List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == client.UserId && !d.IsSigned).ToList();
            List<ApplicationDocumentModel> appDocs = _context.ApplicationDocuments.Where(d => d.ApplicationId == applicationId && !d.IsSigned).ToList();


            List<DocumentTypesEnum> docTypeRequireSignature = new List<DocumentTypesEnum>();
            Console.WriteLine("User docs to sign : " + userDocs.Count);
            Console.WriteLine("Application docs to sign: " + appDocs.Count);

            if (application.ApplicationType == ApplicationTypesEnum.Individual)
            {
                docTypeRequireSignature = new List<DocumentTypesEnum>()
                        {
                            DocumentTypesEnum.ClientConsent,
                            DocumentTypesEnum.DisclosureLetter,
                            DocumentTypesEnum.RiskProfile,
                            DocumentTypesEnum.RecordOfAdvice,
                            DocumentTypesEnum.FSPMandate,
                            DocumentTypesEnum.PEFDOA,
                            DocumentTypesEnum.PEFQuote,
                            DocumentTypesEnum.PEF2DOA,
                            DocumentTypesEnum.PEF2Quote,
                            DocumentTypesEnum.FIDOA,
                            DocumentTypesEnum.FIQuote
                        };
            }

            if (appDocs.Count > 0)
            {
                Console.WriteLine("Application docs started");

                Parallel.ForEach(appDocs, item =>
                {
                    if (docTypeRequireSignature.Contains(item.DocumentType))
                    {
                        List<SignerListItemDto> signers = item.DocumentType switch
                        {
                            DocumentTypesEnum.RecordOfAdvice => RecordOfAdviceSigningList(application, client, advisor),
                            DocumentTypesEnum.PEFDOA => PEFDOASigningList(application, client, advisor),
                            DocumentTypesEnum.PEF2DOA => PEF2DOASigningList(application, client, advisor),
                            DocumentTypesEnum.PEFQuote => PEFQuoteSigningList(application, client, advisor),
                            DocumentTypesEnum.PEF2Quote => PEF2QuoteSigningList(application, client, advisor),
                            DocumentTypesEnum.FIDOA => FIDOASigningList(application, client, advisor),
                            DocumentTypesEnum.FIQuote => FIQuoteSigningList(application, client, advisor),
                        };


                        byte[] docB64 = _dh.GetDocumentData(item.URL, item.Name);
                        Console.WriteLine("Signing - " + item.Name);
                        var ceremony = _signRepo.CreateMultipleSignersCeremony(docB64,
                                item.Name, signers);

                        docB64 = Convert.FromBase64String(
                            _signRepo.RunMultiSignerCeremony(ceremony));


                        if (docB64.Length > 0)
                        {
                            Console.WriteLine("Signed - " + item.Name);
                            _dh.UploadSignedApplicationFile(docB64, item, client.User);
                            Console.WriteLine("Uploaded - " + item.Name);

                        }

                    }
                });
            }

            Console.WriteLine("Application Docs done");

            if (userDocs.Count > 0)
            {
                Console.WriteLine("User Docs started");

                Parallel.ForEach(userDocs, item =>
                {
                    if (docTypeRequireSignature.Contains(item.DocumentType))
                    {
                        List<SignerListItemDto> signers = item.DocumentType switch
                        {
                            DocumentTypesEnum.ClientConsent => ClientConsentSigningList(application, client, advisor),
                            DocumentTypesEnum.RiskProfile => RiskProfileSigningList(application, client, advisor),
                            DocumentTypesEnum.DisclosureLetter => DisclosureLetterSigningList(application, client, advisor),
                            DocumentTypesEnum.FSPMandate => FspMandateSigningList(application, client, advisor),
                        };

                        byte[] docB64 = _dh.GetDocumentData(item.URL, item.Name);
                        Console.WriteLine("Signing - " + item.Name);
                        var ceremony = _signRepo.CreateMultipleSignersCeremony(docB64,
                                item.Name, signers);

                        docB64 = Convert.FromBase64String(
                            _signRepo.RunMultiSignerCeremony(ceremony));

                        if (docB64.Length > 0)
                        {
                            Console.WriteLine("Signed - " + item.Name);
                            _dh.UploadSignedUserFile(docB64, item);
                            Console.WriteLine("Uploaded - " + item.Name);
                        }

                    }
                });

            }

            Console.WriteLine("User Docs done");

            application.ApplicationStatus = ApplicationStatusEnum.Completed;
            application.DocumentsSigned = true;
            _context.Applications.Update(application);
            _context.SaveChanges();


            _ms.SendApplicationDocumentsToBroker(application, advisor, client);
        }

        #endregion Public Methods

        #region Private Methods

        private List<SignerListItemDto> ClientConsentSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 99, 677, 30, 120, 1)));

            return signerList;
        }

        private SignerDto CreateSignItem(UserModel user, int x, int y, int h, int w, int p)
        {
            return new SignerDto()
            {
                Signature = System.Text.Encoding.ASCII.GetString(user.Signature),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IdNo = user.RSAIdNumber,
                Mobile = user.MobileNumber,
                XField = x,
                YField = y,
                HField = h,
                WField = w,
                Page = p
            };
        }

        private List<SignerListItemDto> DisclosureLetterSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2, 3 };



            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 25, 770, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 98, 463, 30, 120, 4)));

            //when broker appointment is added.
            //signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 98, 463, 30, 120, 4)));

            return signerList;
        }

        private List<SignerListItemDto> FIDOASigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 529, 788, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 170, 299, 30, 120, 10)));
            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 170, 703, 30, 120, 10)));

            return signerList;
        }

        private List<SignerListItemDto> FIQuoteSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 175, 587, 30, 120, 1)));

            return signerList;
        }

        private List<SignerListItemDto> FspMandateSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 20, 767, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 113, 205, 30, 120, 11)));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 400, 205, 30, 120, 11)));

            if (client.FspMandate.DiscretionType == "full")
            {
                signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 117, 530, 30, 120, 11)));
            }
            else if (client.FspMandate.DiscretionType == "limited_DE")
            {
                signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 370, 162, 30, 120, 12)));
            }
            else if (client.FspMandate.DiscretionType == "limited_RM")
            {
                signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 370, 296, 30, 120, 12)));
            }

            return signerList;
        }
        private List<SignerListItemDto> PEF2DOASigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 529, 788, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 170, 299, 30, 120, 10)));
            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 170, 703, 30, 120, 10)));

            return signerList;
        }

        private List<SignerListItemDto> PEF2QuoteSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 175, 587, 30, 120, 1)));

            return signerList;
        }

        private List<SignerListItemDto> PEFDOASigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 529, 788, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 170, 299, 30, 120, 10)));
            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 170, 703, 30, 120, 10)));

            return signerList;
        }

        private List<SignerListItemDto> PEFQuoteSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 190, 597, 30, 120, 1)));

            return signerList;
        }

        private List<SignerListItemDto> RecordOfAdviceSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 99, 560, 30, 120, 4)));
            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(advisor.User, 385, 560, 30, 120, 4)));

            return signerList;
        }

        private List<SignerListItemDto> RiskProfileSigningList(ApplicationModel application, ClientModel client, AdvisorModel advisor)
        {
            SignatureRepo _signRepo = new SignatureRepo();
            var signerList = new List<SignerListItemDto>();

            var pageList = new List<int> { 1, 2 };

            pageList.ForEach(p => signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 18, 769, 20, 60, p))));

            signerList.Add(_signRepo.CreateSignerListItem(CreateSignItem(client.User, 115, 436, 30, 120, 2)));

            return signerList;

        }

        #endregion Private Methods
    }
}