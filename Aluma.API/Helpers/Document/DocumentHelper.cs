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

namespace Aluma.API.Helpers
{
    public class DocumentHelper
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

        public Dictionary<DocumentTypesEnum, string> DocumentNames = new Dictionary<DocumentTypesEnum, string>()
                {
                    {DocumentTypesEnum.RiskProfile,     "Aluma Capital - Risk Profile.pdf"},
                    {DocumentTypesEnum.RecordOfAdvice,  "Aluma Capital - Record of Advice.pdf"},
                    {DocumentTypesEnum.FSPMandate,      "Aluma Capital - Discretionary Mandate.pdf"},
                    {DocumentTypesEnum.ClientConsent,   "Aluma Capital - Client Consent.pdf"},
                    {DocumentTypesEnum.DisclosureLetter,"Aluma Capital - Disclosure Letter.pdf"},
                    {DocumentTypesEnum.PEFDOA,          "Aluma Capital - Private Equity - Growth - Deed of Adherence.pdf"},
                    {DocumentTypesEnum.PEF2DOA,         "Aluma Capital - Private Equity - Income - Deed of Adherence.pdf"},
                    {DocumentTypesEnum.PEFQuote,        "Aluma Capital - Private Equity - Growth - Quote.pdf"},
                    {DocumentTypesEnum.PEF2Quote,       "Aluma Capital - Private Equity - Income - Quote.pdf"},
                };

        public Dictionary<DocumentTypesEnum, string> DocumentTemplates = new Dictionary<DocumentTypesEnum, string>()
                {
                    {DocumentTypesEnum.RiskProfile,"RiskProfile.pdf"},
                    {DocumentTypesEnum.RecordOfAdvice,"ROA.pdf"},
                    {DocumentTypesEnum.FSPMandate,"FspMandate.pdf"},
                    {DocumentTypesEnum.ClientConsent,"ClientConsent.pdf"},
                    {DocumentTypesEnum.DisclosureLetter,"DisclosureLetter.pdf"},
                    {DocumentTypesEnum.PEFDOA,"DOA.pdf"},
                    {DocumentTypesEnum.PEF2DOA,"DOA2.pdf"},
                    {DocumentTypesEnum.PEFQuote,"PEFQuote.pdf"},
                    {DocumentTypesEnum.PEF2Quote,"PEF2Quote.pdf"},
                };

        public void PopulateAndSaveDocument(DocumentTypesEnum fileType, Dictionary<string, string> formData, UserModel user, ApplicationModel application = null)
        {
            byte[] docPopulated = PopulateDocument(fileType, formData);

            UploadFile(docPopulated, fileType, user, application);
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

        private async void UploadFile(byte[] fileBytes, DocumentTypesEnum fileType, UserModel user, ApplicationModel application)
        {
            var storageSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            string fileDirectory = $"{storageSettings.DocumentsRootPath}/{DateTime.UtcNow.Year}/{DateTime.UtcNow.Month}/{user.Id}";

            if (application != null)
            {
                fileDirectory += $"/{application.Id}";
                ApplicationDocumentModel adm = new ApplicationDocumentModel();
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
                UserDocumentModel udm = new UserDocumentModel();
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
                BaseShare = "alumaportal"
            };

            FileStorageRepo storage = new FileStorageRepo(new ShareServiceClient(storageSettings.AzureFileStorageConnection));

            await storage.UploadAsync(dto);


        }

        internal async Task<List<ApplicationDocumentDto>> GetAllApplicationDocuments(ApplicationModel application)
        {
            var azureSettings = _config.GetSection("AzureSettings").Get<AzureSettingsDto>();

            List<ApplicationDocumentModel> appDocs = _context.ApplicationDocuments.Where(d => d.ApplicationId == application.Id).ToList();

            List<ApplicationDocumentDto> response = new List<ApplicationDocumentDto>();
            foreach (var doc in appDocs)
            {

                FileStorageDto fileDto = new FileStorageDto()
                {
                    BaseDocumentPath = azureSettings.DocumentsRootPath,
                    BaseShare = "alumaportal",
                    FileDirectory = doc.URL,
                    FileName = doc.Name
                };

                byte[] bytes = await _fileStorageRepo.DownloadAsync(fileDto);

                ApplicationDocumentDto dto = new ApplicationDocumentDto()
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

            List<UserDocumentDto> response = new List<UserDocumentDto>();
            foreach (var doc in userDocs)
            {

                FileStorageDto fileDto = new FileStorageDto()
                {
                    BaseDocumentPath = azureSettings.DocumentsRootPath,
                    BaseShare = "alumaportal",
                    FileDirectory = doc.URL,
                    FileName = doc.Name
                };

                byte[] bytes = await _fileStorageRepo.DownloadAsync(fileDto);

                UserDocumentDto dto = new UserDocumentDto()
                {
                    Id = doc.Id,
                    DocumentName = doc.Name,
                    b64 = "data:application/pdf;base64," + Convert.ToBase64String(bytes, 0, bytes.Length),
                };

                response.Add(dto);
            }

            return response;
        }
    }
}