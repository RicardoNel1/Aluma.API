using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IIRSW9Repo : IRepoBase<IRSW9Model>
    {
        IRSW9Dto GetIRSW9(IRSW9Dto dto);

        bool DoesApplicationHaveIRSW9(IRSW9Dto dto);

        IRSW9Dto CreateIRSW9(IRSW9Dto dto);

        IRSW9Dto UpdateIRSW9(IRSW9Dto dto);

        bool DeleteIRSW9(IRSW9Dto dto);
    }

    public class IRSW9Repo : RepoBase<IRSW9Model>, IIRSW9Repo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public IRSW9Repo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _host = host;
            _config = config;
            _mapper = mapper;
            _context = context;
        }

        public IRSW9Dto CreateIRSW9(IRSW9Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteIRSW9(IRSW9Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveIRSW9(IRSW9Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public IRSW9Dto GetIRSW9(IRSW9Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public IRSW9Dto UpdateIRSW9(IRSW9Dto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}