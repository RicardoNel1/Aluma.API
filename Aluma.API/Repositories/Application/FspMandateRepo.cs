using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IFspMandateRepo : IRepoBase<FSPMandateModel>
    {
        FSPMandateDto GetFspMandate(FSPMandateDto dto);

        bool DoesApplicationHaveMandate(FSPMandateDto dto);

        FSPMandateDto UpdateFSPMandate(FSPMandateDto dto);

        FSPMandateDto CreateFSPMandate(FSPMandateDto dto);

        FSPMandateDto GetFSPMandate(FSPMandateDto dto);

        bool DeleteFSPMandate(FSPMandateDto dto);
    }

    public class FspMandateRepo : RepoBase<FSPMandateModel>, IFspMandateRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public FspMandateRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public FSPMandateDto CreateFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public FSPMandateDto GetFspMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public FSPMandateDto GetFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }

        public FSPMandateDto UpdateFSPMandate(FSPMandateDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}