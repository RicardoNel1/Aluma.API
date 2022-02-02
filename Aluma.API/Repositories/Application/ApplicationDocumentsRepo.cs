using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Aluma.API.Repositories
{
    public interface IApplicationDocumentsRepo : IRepoBase<ApplicationDocumentModel>
    {
        public void SignDisclosure(UserDocumentModel dto);

        object GetDocuments(ApplicationDto dto);

        ICollection<ApplicationDocumentDto> GetDocumentsList(ApplicationDto dto);

        object GetDocument(ApplicationDocumentDto dto);
    }

    public class ApplicationDocumentsRepo : RepoBase<ApplicationDocumentModel>, IApplicationDocumentsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ApplicationDocumentsRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public object GetDocument(ApplicationDocumentDto dto)
        {
            throw new NotImplementedException();
        }

        public object GetDocuments(ApplicationDto dto)
        {
            throw new NotImplementedException();
        }

        public ICollection<ApplicationDocumentDto> GetDocumentsList(ApplicationDto dto)
        {
            throw new NotImplementedException();
        }

        public void SignDisclosure(UserDocumentModel dto)
        {
            try
            {
                //ApplicationDocumentModel SignedDisclosureDocument = SignatureRepo;
                //_context.ApplicationDocuments.Add(SignedDisclosureDocument);
                //_context.SaveChanges();

                return;
            }
            catch (Exception ex)
            {
                //log error
                return;
            }
        }
    }
}