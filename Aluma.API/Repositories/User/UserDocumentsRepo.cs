using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using FileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluma.API.Repositories
{
    public interface IUserDocumentsRepo : IRepoBase<UserDocumentModel>
    {
        public UserDocumentModel CreateClientDisclosure(DisclosureModel model);

        public UserDocumentModel UpdateClientDisclosure(DisclosureModel model);

        public UserDocumentModel CreateClientBankVerification(BankDetailsModel model);

        public UserDocumentModel UpdateClientBankVerification(BankDetailsModel model);

        ICollection<UserDocumentDto> GetDocumentsList(UserDto dto);

        Task<List<UserDocumentDto>> GetDocuments(int userId);

        Task<UserDocumentDto> GetDocument(UserDocumentDto dto);

        Task<UserDocumentDto> UploadDocument(UserDocumentDto dto);
    }

    public class UserDocumentsRepo : RepoBase<UserDocumentModel>, IUserDocumentsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        DocumentHelper _dh;

        public UserDocumentsRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _dh = new DocumentHelper(_context, _config, _fileStorage, _host);
        }

        public UserDocumentModel CreateClientBankVerification(BankDetailsModel model)
        {
            throw new System.NotImplementedException();
        }

        public UserDocumentModel CreateClientDisclosure(DisclosureModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserDocumentDto> GetDocument(UserDocumentDto dto)
        {
            FileStorageDto fileDto = new()
            {
                BaseDocumentPath = _config.GetSection("AzureSettings:DocumentsRootPath").Value,
                FileDirectory = dto.Url,
                FileName = dto.DocumentName,
            };

            dto.DocumentData = await _fileStorage.DownloadAsync(fileDto);

            return dto;
        }

        public async Task<List<UserDocumentDto>> GetDocuments(int userId)
        {
            UserModel u = _context.Users.First(a => a.Id == userId);

            List<UserDocumentDto> response = await _dh.GetAllUserDocuments(u);

            return response;

        }

        public ICollection<UserDocumentDto> GetDocumentsList(UserDto dto)
        {
            throw new System.NotImplementedException();
        }

        public UserDocumentModel UpdateClientBankVerification(BankDetailsModel model)
        {
            throw new System.NotImplementedException();
        }

        public UserDocumentModel UpdateClientDisclosure(DisclosureModel model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserDocumentDto> UploadDocument(UserDocumentDto dto)
        {
            FileStorageDto fileDto = new()
            {
                BaseDocumentPath = _config.GetSection("AzureSettings:DocumentsRootPath").Value,
                FileDirectory = dto.Url,
                FileName = dto.DocumentName,
            };

            try
            {
                await _fileStorage.UploadAsync(fileDto);

                UserDocumentModel docModel = _mapper.Map<UserDocumentModel>(dto);

                _context.UserDocuments.Add(docModel);
                _context.SaveChanges();

                dto = _mapper.Map<UserDocumentDto>(docModel);
                return dto;

            }
            catch (Exception ex)
            {
                return null;
            }

        }
    }
}