using DataService.Dto;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileStorageService;
using DataService.Context;
using Microsoft.Extensions.Configuration;
using DataService.Model;
using DataService.Enum;
using Azure.Storage.Files.Shares;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Aluma.API.Helpers
{

    public interface IDocumentHelper
    {
        Task PopulateAndSaveDocument(DocumentTypesEnum fileType, Dictionary<string, string> formData, UserModel user, ApplicationModel application = null);
        byte[] GetDocumentData(string url, string name);
        Task<byte[]> GetDocumentDataAsync(string url, string name);
        void UploadSignedUserFile(byte[] fileBytes, UserDocumentModel document);
        Task UploadSignedUserFileAsync(byte[] fileBytes, UserDocumentModel document);
        void UploadSignedApplicationFile(byte[] fileBytes, ApplicationDocumentModel document, UserModel user);
        Task UploadSignedApplicationFileAsync(byte[] fileBytes, ApplicationDocumentModel document, UserModel user);
        void DeleteAllDocuments();
        Task<List<DocumentListDto>> GetUserDocListAsync(int userId);
        Task<List<DocumentListDto>> GetApplicationDocListAsync(int applicationId, int userId);
    }

    public class DocumentHelper : IDocumentHelper
    {
        private readonly AlumaDBContext _context;
        private readonly IConfiguration _config;
        private readonly IFileStorageRepo _fileStorageRepo;
        private readonly IWebHostEnvironment _host;

        public DocumentHelper(AlumaDBContext context, IConfiguration config, IFileStorageRepo fileStorage, IWebHostEnvironment host)
        {
            _context = context;
            _config = config;
            _fileStorageRepo = fileStorage;
            _host = host;
        }

        public Dictionary<DocumentTypesEnum, string> DocumentNames = new()
                {
                    {DocumentTypesEnum.RiskProfile,     "Aluma Capital - Risk Profile.pdf"},
                    {DocumentTypesEnum.RecordOfAdvice,  "Aluma Capital - Record of Advice.pdf"},
                    {DocumentTypesEnum.FSPMandate,      "Aluma Capital - Discretionary Mandate.pdf"},
                    {DocumentTypesEnum.ClientConsent,   "Aluma Capital - Client Consent.pdf"},
                    {DocumentTypesEnum.DisclosureLetter,"Aluma Capital - Disclosure Letter.pdf"},
                    {DocumentTypesEnum.PEFDOA,          "Aluma Capital - Private Equity - Growth - Deed of Adherence.pdf"},
                    {DocumentTypesEnum.PEF2DOA,         "Aluma Capital - Private Equity - Income - Deed of Adherence.pdf"},
                    {DocumentTypesEnum.FIDOA,         "Aluma Capital - Fixed Income - Deed of Adherence.pdf"},
                    {DocumentTypesEnum.PEFQuote,        "Aluma Capital - Private Equity - Growth - Quote.pdf"},
                    {DocumentTypesEnum.PEF2Quote,       "Aluma Capital - Private Equity - Income - Quote.pdf"},
                    {DocumentTypesEnum.FIQuote,       "Aluma Capital - Fixed Income - Quote.pdf"},
                };

        public Dictionary<DocumentTypesEnum, string> DocumentTemplates = new()
                {
                    {DocumentTypesEnum.RiskProfile,"RiskProfile.pdf"},
                    {DocumentTypesEnum.RecordOfAdvice,"ROA.pdf"},
                    {DocumentTypesEnum.FSPMandate,"FspMandate.pdf"},
                    {DocumentTypesEnum.ClientConsent,"ClientConsent.pdf"},
                    {DocumentTypesEnum.DisclosureLetter,"DisclosureLetter.pdf"},
                    {DocumentTypesEnum.PEFDOA,"DOA.pdf"},
                    {DocumentTypesEnum.PEF2DOA,"DOA2.pdf"},
                    {DocumentTypesEnum.FIDOA,"DOA.pdf"}, // Change when FI DOA arrives.
                    {DocumentTypesEnum.PEFQuote,"PEFQuote.pdf"},
                    {DocumentTypesEnum.PEF2Quote,"PEF2Quote.pdf"},
                    {DocumentTypesEnum.FIQuote,"FIQuote.pdf"},
                };

        public async Task<List<DocumentListDto>> GetApplicationDocListAsync(int applicationId, int userId)
        {
            List<DocumentListDto> doc = new();

            ApplicationModel app = _context.Applications.Include(a => a.Client).Where(a => a.Id == applicationId).FirstOrDefault();

            if (app.Client.UserId == userId)
            {
                foreach (var item in _context.ApplicationDocuments.Where(d => d.ApplicationId == app.Id))
                {
                    doc.Add(new DocumentListDto() { ApplicationId = app.Id, UserId = userId, DocumentId = item.Id, DocumentName = item.Name, DocumentType = "ApplicationDocument" });
                }
            }

            return doc;
        }

        public async Task<List<DocumentListDto>> GetUserDocListAsync(int userId)
        {
            List<DocumentListDto> doc = new();

            UserModel user = _context.Users.Where(a => a.Id == userId).FirstOrDefault();

            if (user != null)
            {
                foreach (var item in _context.UserDocuments.Where(d => d.UserId == user.Id))
                {
                    doc.Add(new DocumentListDto() { ApplicationId = 0, UserId = userId, DocumentId = item.Id, DocumentName = item.Name, DocumentType = "UserDocument" });
                }
            }

            return doc;
        }


        public async Task PopulateAndSaveDocument(DocumentTypesEnum fileType, Dictionary<string, string> formData, UserModel user, ApplicationModel application = null)
        {
            byte[] docPopulated = PopulateDocument(fileType, formData);

            await UploadFile(docPopulated, fileType, user, application);
        }

        private byte[] PopulateDocument(DocumentTypesEnum documentType, Dictionary<string, string> formData)
        {
            char slash = Path.DirectorySeparatorChar;

            string templatePath = $"{_host.WebRootPath}{slash}pdf{slash}{DocumentTemplates[documentType]}";

            var ms = new MemoryStream();
            var pdf = new PdfDocument(new PdfReader(templatePath), new PdfWriter(ms));
            var form = PdfAcroForm.GetAcroForm(pdf, true);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;

            foreach (var d in formData)
            {
                try
                {
                    fields.TryGetValue(d.Key, out toSet);
                    toSet.SetValue(d.Value);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Form Field Error:  {d.Key}, {d.Value}");
                }
            }

            form.FlattenFields();
            pdf.Close();

            byte[] file = ms.ToArray();
            ms.Close();

            return file;
        }


        public byte[] GetDocumentData(string url, string name)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();
            var dto = new FileStorageDto()
            {
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare,
                FileName = name,
                FileDirectory = url,
            };

            return _fileStorageRepo.DownloadAsync(dto).Result;
        }

        public async Task<byte[]> GetDocumentDataAsync(string url, string name)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();
            var dto = new FileStorageDto()
            {
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare,
                FileName = name,
                FileDirectory = url,
            };

            return await _fileStorageRepo.DownloadAsync(dto);
        }

        public void UploadSignedUserFile(byte[] fileBytes, UserDocumentModel document)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{document.UserId}";


            document.URL = fileDirectory;
            document.Modified = DateTime.UtcNow;
            document.Size = fileBytes.Length;
            document.IsSigned = true;
            _context.UserDocuments.Update(document);

            _context.SaveChanges();

            var dtoNew = new FileStorageDto()
            {
                FileName = document.Name,
                FileBytes = fileBytes,
                FileDirectory = fileDirectory,
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare
            };

            FileStorageRepo storage = new(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            if (document.URL == fileDirectory)
            {
                var dtoOld = new FileStorageDto()
                {
                    BaseDocumentPath = storageSettings.DocumentsRootPath,
                    BaseShare = storageSettings.BaseShare,
                    FileName = document.Name,
                    FileDirectory = document.URL,
                };
                storage.DeleteAsync(dtoOld).Wait();
            }

            storage.UploadAsync(dtoNew).Wait();
        }

        public async Task UploadSignedUserFileAsync(byte[] fileBytes, UserDocumentModel document)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{document.UserId}";


            document.URL = fileDirectory;
            document.Modified = DateTime.UtcNow;
            document.Size = fileBytes.Length;
            document.IsSigned = true;
            _context.UserDocuments.Update(document);

            _context.SaveChanges();

            var dtoNew = new FileStorageDto()
            {
                FileName = document.Name,
                FileBytes = fileBytes,
                FileDirectory = fileDirectory,
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare
            };

            FileStorageRepo storage = new(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            if (document.URL == fileDirectory)
            {
                var dtoOld = new FileStorageDto()
                {
                    BaseDocumentPath = storageSettings.DocumentsRootPath,
                    BaseShare = storageSettings.BaseShare,
                    FileName = document.Name,
                    FileDirectory = document.URL,
                };
                await storage.DeleteAsync(dtoOld);
            }

            await storage.UploadAsync(dtoNew);
        }


        public void UploadSignedApplicationFile(byte[] fileBytes, ApplicationDocumentModel document, UserModel user)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{user.Id}/{document.ApplicationId}";


            document.URL = fileDirectory;
            document.Modified = DateTime.UtcNow;
            document.Size = fileBytes.Length;
            document.IsSigned = true;
            _context.ApplicationDocuments.Update(document);

            _context.SaveChanges();

            var dtoNew = new FileStorageDto()
            {
                FileName = document.Name,
                FileBytes = fileBytes,
                FileDirectory = fileDirectory,
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare
            };

            FileStorageRepo storage = new(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            if (document.URL == fileDirectory)
            {
                var dtoOld = new FileStorageDto()
                {
                    BaseDocumentPath = storageSettings.DocumentsRootPath,
                    BaseShare = storageSettings.BaseShare,
                    FileName = document.Name,
                    FileDirectory = document.URL,
                };
                storage.DeleteAsync(dtoOld).Wait();
            }

            storage.UploadAsync(dtoNew).Wait();
        }


        public async Task UploadSignedApplicationFileAsync(byte[] fileBytes, ApplicationDocumentModel document, UserModel user)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{user.Id}/{document.ApplicationId}";


            document.URL = fileDirectory;
            document.Modified = DateTime.UtcNow;
            document.Size = fileBytes.Length;
            document.IsSigned = true;
            _context.ApplicationDocuments.Update(document);

            _context.SaveChanges();

            var dtoNew = new FileStorageDto()
            {
                FileName = document.Name,
                FileBytes = fileBytes,
                FileDirectory = fileDirectory,
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare
            };

            FileStorageRepo storage = new(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            if (document.URL == fileDirectory)
            {
                var dtoOld = new FileStorageDto()
                {
                    BaseDocumentPath = storageSettings.DocumentsRootPath,
                    BaseShare = storageSettings.BaseShare,
                    FileName = document.Name,
                    FileDirectory = document.URL,
                };
                await storage.DeleteAsync(dtoOld);
            }

            await storage.UploadAsync(dtoNew);


        }

        private async Task UploadFile(byte[] fileBytes, DocumentTypesEnum fileType, UserModel user, ApplicationModel application)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{user.Id}";

            if (application != null)
            {
                fileDirectory += $"/{application.Id}";
                ApplicationDocumentModel adm = new();
                var documentExist = _context.ApplicationDocuments.Where(d => d.Name == DocumentNames[fileType].ToString() && d.ApplicationId == application.Id);

                if (documentExist.Any())
                {
                    adm = documentExist.First();
                    if (adm.URL != fileDirectory)
                    {
                        adm.URL = fileDirectory;
                        adm.Modified = DateTime.UtcNow;
                        adm.Size = fileBytes.Length;
                        _context.ApplicationDocuments.Update(adm);
                    }
                }
                else
                {
                    adm = new ApplicationDocumentModel()
                    {
                        DocumentType = fileType,
                        FileType = FileTypesEnum.Pdf,
                        Name = DocumentNames[fileType].ToString(),
                        URL = fileDirectory,
                        ApplicationId = application.Id,
                        Size = fileBytes.Length,
                    };
                    _context.ApplicationDocuments.Add(adm);
                }

            }
            else
            {
                UserDocumentModel udm = new();
                var documentExist = _context.UserDocuments.Where(d => d.Name == DocumentNames[fileType].ToString() && d.UserId == user.Id);

                if (documentExist.Any())
                {
                    udm = documentExist.First();
                    if (udm.URL != fileDirectory)
                    {
                        udm.URL = fileDirectory;
                        udm.Modified = DateTime.UtcNow;
                        udm.Size = fileBytes.Length;
                        _context.UserDocuments.Update(udm);
                    }
                }
                else
                {
                    udm = new UserDocumentModel()
                    {
                        DocumentType = fileType,
                        FileType = FileTypesEnum.Pdf,
                        Name = DocumentNames[fileType].ToString(),
                        URL = fileDirectory,
                        UserId = user.Id,
                        Size = fileBytes.Length
                    };

                    _context.UserDocuments.Add(udm);
                }
            }
            _context.SaveChanges();

            var dto = new FileStorageDto()
            {
                FileName = DocumentNames[fileType].ToString(),
                FileBytes = fileBytes,
                FileDirectory = fileDirectory,
                BaseDocumentPath = storageSettings.DocumentsRootPath,
                BaseShare = storageSettings.BaseShare
            };

            FileStorageRepo storage = new(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            await storage.UploadAsync(dto);


        }

        internal async Task<List<ApplicationDocumentDto>> GetAllApplicationDocuments(ApplicationModel application)
        {
            var azureSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            List<ApplicationDocumentModel> appDocs = _context.ApplicationDocuments.Where(d => d.ApplicationId == application.Id).ToList();

            List<ApplicationDocumentDto> response = new();
            foreach (var doc in appDocs)
            {

                FileStorageDto fileDto = new()
                {
                    BaseDocumentPath = azureSettings.DocumentsRootPath,
                    BaseShare = azureSettings.BaseShare,
                    FileDirectory = doc.URL,
                    FileName = doc.Name
                };

                byte[] bytes = await _fileStorageRepo.DownloadAsync(fileDto);

                ApplicationDocumentDto dto = new()
                {
                    Id = doc.Id,
                    DocumentName = doc.Name,
                    b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                };

                response.Add(dto);
            }

            return response;
        }

        internal async Task<List<UserDocumentDto>> GetAllUserDocuments(UserModel user)
        {
            var azureSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            List<UserDocumentModel> userDocs = _context.UserDocuments.Where(d => d.UserId == user.Id).ToList();

            List<UserDocumentDto> response = new();
            foreach (var doc in userDocs)
            {

                FileStorageDto fileDto = new()
                {
                    BaseDocumentPath = azureSettings.DocumentsRootPath,
                    BaseShare = azureSettings.BaseShare,
                    FileDirectory = doc.URL,
                    FileName = doc.Name
                };

                byte[] bytes = await _fileStorageRepo.DownloadAsync(fileDto);

                UserDocumentDto dto = new()
                {
                    Id = doc.Id,
                    DocumentName = doc.Name,
                    b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                };

                response.Add(dto);
            }

            return response;
        }

        public void DeleteAllDocuments()
        {
            var azureSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();
            FileStorageDto fileDto = new()
            {
                BaseDocumentPath = azureSettings.DocumentsRootPath,
                BaseShare = azureSettings.BaseShare,
            };

            _fileStorageRepo.DeleteAllAsync(fileDto);
        }
    }
}