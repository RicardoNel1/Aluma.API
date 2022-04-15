using Aluma.API.Helpers;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Enum;
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
    public interface IRiskProfileRepo : IRepoBase<RiskProfileModel>
    {
        RiskProfileDto GetRiskProfile(int clientId);

        bool DoesClientHaveRiskProfile(RiskProfileDto dto);

        RiskProfileDto CreateRiskProfile(RiskProfileDto dto);

        RiskProfileDto UpdateRiskProfile(RiskProfileDto dto);

        bool DeleteRiskProfile(RiskProfileDto dto);
        Task GenerateRiskProfile(ClientModel client, AdvisorModel advisor, RiskProfileModel riskProfile);
    }

    public class RiskProfileRepo : RepoBase<RiskProfileModel>, IRiskProfileRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        public RiskProfileRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper, IFileStorageRepo fileStorage) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public RiskProfileDto CreateRiskProfile(RiskProfileDto dto)
        {
            RiskProfileModel newRiskProfile = _mapper.Map<RiskProfileModel>(dto);

            _context.RiskProfiles.Add(newRiskProfile);
            _context.SaveChanges();

            dto = _mapper.Map<RiskProfileDto>(newRiskProfile);

            return dto;

        }

        public bool DeleteRiskProfile(RiskProfileDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesClientHaveRiskProfile(RiskProfileDto dto)
        {
            var rm = _context.RiskProfiles.Where(r => r.ClientId == dto.ClientId);
            if (rm.Any())
            {
                return true;
            }
            return false;

        }

        public RiskProfileDto GetRiskProfile(int clientId)
        {
            var riskProfileModel = _context.RiskProfiles.Where(r => r.ClientId == clientId);

            if (riskProfileModel.Any())
            {
                return _mapper.Map<RiskProfileDto>(riskProfileModel.First());
            }
            return null;

        }

        public RiskProfileDto UpdateRiskProfile(RiskProfileDto dto)
        {
            RiskProfileModel newRiskProfile = _mapper.Map<RiskProfileModel>(dto);

            _context.RiskProfiles.Update(newRiskProfile);
            _context.SaveChanges();

            dto = _mapper.Map<RiskProfileDto>(newRiskProfile);

            return dto;
        }

        public async Task GenerateRiskProfile(ClientModel client, AdvisorModel advisor, RiskProfileModel riskProfile)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();

            d["User"] = $"{client.User.FirstName} {client.User.LastName}";
            d["IdNo"] = client.User.RSAIdNumber;
            d["Advisor"] = $"{advisor.User.FirstName ?? string.Empty} {advisor.User.LastName ?? string.Empty}";
            d["Created"] = DateTime.Today.ToString("yyyy/MM/dd");
            d["Goal"] = riskProfile.Goal ?? "Capital Growth";

            d["RiskAge_" + riskProfile.RiskAge] = "x";
            d["RiskTerm_" + riskProfile.RiskTerm] = "x";
            d["RiskInflation_" + riskProfile.RiskInflation] = "x";
            d["RiskReaction_" + riskProfile.RiskReaction] = "x";
            d["RiskExample_" + riskProfile.RiskExample] = "x";

            d["DerivedProfile"] = riskProfile.DerivedProfile;

            var agreeStr = riskProfile.AgreeWithOutcome == true ? "agree_True" : "agree_False";
            if (!riskProfile.AgreeWithOutcome)
            {
                d["Reason"] = riskProfile.DisagreeReason ?? string.Empty;
                d["agree_False"] = "x";
            }
            else
                d["agree_True"] = "x";
            d["Date"] = DateTime.Today.ToString("yyyy/MM/dd");

            //advisor notes
            d["advisorNotes"] = riskProfile.AdvisorNotes ?? string.Empty;

            DocumentHelper dh = new DocumentHelper(_context,_config, _fileStorage, _host);

            await dh.PopulateAndSaveDocument(DocumentTypesEnum.RiskProfile, d, client.User);
            
        }
    }
}