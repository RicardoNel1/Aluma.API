using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IPurposeAndFundingRepo : IRepoBase<PurposeAndFundingModel>
    {
        #region Public Methods

        PurposeAndFundingDto CreatePurposeAndFunding(PurposeAndFundingDto dto);

        bool DoesPurposeAndFundingExist(PurposeAndFundingDto dto);
        PurposeAndFundingDto GetPurposeAndFunding(int applicationId);

        PurposeAndFundingDto UpdatePurposeAndFunding(PurposeAndFundingDto dto);

        #endregion Public Methods
    }

    public class PurposeAndFundingRepo : RepoBase<PurposeAndFundingModel>, IPurposeAndFundingRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly IWebHostEnvironment _host;

        private readonly IMapper _mapper;

        private AlumaDBContext _context;

        #endregion Private Fields

        #region Public Constructors

        public PurposeAndFundingRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;

        }

        #endregion Public Constructors

        #region Public Methods

        public PurposeAndFundingDto CreatePurposeAndFunding(PurposeAndFundingDto dto)
        {
            PurposeAndFundingModel details = _mapper.Map<PurposeAndFundingModel>(dto);
            _context.PurposeAndFunding.Add(details);
            _context.SaveChanges();
            dto = _mapper.Map<PurposeAndFundingDto>(details);
            return dto;

        }

        public bool DoesPurposeAndFundingExist(PurposeAndFundingDto dto)
        {
            bool purposeAndFundingExist = false;

            purposeAndFundingExist = _context.PurposeAndFunding.Where(a => a.ApplicationId == dto.ApplicationId).Any();

            return purposeAndFundingExist;
        }
        public PurposeAndFundingDto GetPurposeAndFunding(int applicationId)
        {
            PurposeAndFundingModel purposeAndFunding = _context.PurposeAndFunding.Where(c => c.ApplicationId == applicationId).First();
            return _mapper.Map<PurposeAndFundingDto>(purposeAndFunding);
        }

        public PurposeAndFundingDto UpdatePurposeAndFunding(PurposeAndFundingDto dto)
        {
            PurposeAndFundingModel details = _context.PurposeAndFunding.Where(a => a.ApplicationId == dto.ApplicationId).FirstOrDefault();

            details = _mapper.Map<PurposeAndFundingModel>(dto);
            _context.PurposeAndFunding.Update(details);
            _context.SaveChanges();
            dto = _mapper.Map<PurposeAndFundingDto>(details);
            return dto;

        }

        #endregion Public Methods
    }
}