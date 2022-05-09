using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IIRSW8Repo : IRepoBase<IRSW8Model>
    {
        #region Public Methods

        IRSW8Dto CreateIRSW8(IRSW8Dto dto);

        bool DeleteIRSW8(IRSW8Dto dto);

        bool DoesApplicationHaveIRSW8(IRSW8Dto dto);

        IRSW8Dto GetIRSW8(IRSW8Dto dto);
        IRSW8Dto UpdateIRSW8(IRSW8Dto dto);

        #endregion Public Methods
    }

    public class IRSW8Repo : RepoBase<IRSW8Model>, IIRSW8Repo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public IRSW8Repo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _config = config;
            _host = host;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public IRSW8Dto CreateIRSW8(IRSW8Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteIRSW8(IRSW8Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesApplicationHaveIRSW8(IRSW8Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public IRSW8Dto GetIRSW8(IRSW8Dto dto)
        {
            throw new System.NotImplementedException();
        }

        public IRSW8Dto UpdateIRSW8(IRSW8Dto dto)
        {
            throw new System.NotImplementedException();
        }

        #endregion Public Methods
    }
}