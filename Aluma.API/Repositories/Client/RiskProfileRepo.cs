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
    public interface IRiskProfileRepo : IRepoBase<RiskProfileModel>
    {
        RiskProfileDto GetRiskProfile(int clientId);

        bool DoesClientHaveRiskProfile(RiskProfileDto dto);

        RiskProfileDto CreateRiskProfile(RiskProfileDto dto);

        RiskProfileDto UpdateRiskProfile(RiskProfileDto dto);

        bool DeleteRiskProfile(RiskProfileDto dto);
    }

    public class RiskProfileRepo : RepoBase<RiskProfileModel>, IRiskProfileRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RiskProfileRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public RiskProfileDto CreateRiskProfile(RiskProfileDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteRiskProfile(RiskProfileDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesClientHaveRiskProfile(RiskProfileDto dto)
        {
            throw new System.NotImplementedException();
        }

        public RiskProfileDto GetRiskProfile(int clientId)
        {
            RiskProfileModel riskProfileModel = _context.RiskProfiles.Where(r => r.ClientId == clientId).First();
            return _mapper.Map<RiskProfileDto>(riskProfileModel);

        }

        public RiskProfileDto UpdateRiskProfile(RiskProfileDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}