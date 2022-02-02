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

        ICollection<UserDocumentDto> GetDocuments(UserDto dto);

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

        public UserDocumentsRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
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
            FileStorageDto fileDto = new FileStorageDto()
            {
                BaseDocumentPath = _config.GetSection("AzureSettings:DocumentsRootPath").Value,
                FileDirectory = dto.Url,
                FileName = dto.Name,
            };

            dto.DocumentData = await _fileStorage.DownloadAsync(fileDto);

            return dto;
        }

        public ICollection<UserDocumentDto> GetDocuments(UserDto dto)
        {
            throw new System.NotImplementedException();
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
            FileStorageDto fileDto = new FileStorageDto()
            {
                BaseDocumentPath = _config.GetSection("AzureSettings:DocumentsRootPath").Value,
                FileDirectory = dto.Url,
                FileName = dto.Name,
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