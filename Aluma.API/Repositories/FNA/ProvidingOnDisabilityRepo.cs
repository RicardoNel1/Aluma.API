using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IProvidingOnDisabilityRepo : IRepoBase<ProvidingOnDisabilityModel>
    {
        ProvidingOnDisabilityDto CreateProvidingOnDisability(ProvidingOnDisabilityDto dto);
        bool DoesProvidingOnDisabilityExist(ProvidingOnDisabilityDto dto);
        ProvidingOnDisabilityDto GetProvidingOnDisability(int clientId);
        ProvidingOnDisabilityDto UpdateProvidingOnDisability(ProvidingOnDisabilityDto dto);


    }

    public class ProvidingOnDisabilityRepo : RepoBase<ProvidingOnDisabilityModel>, IProvidingOnDisabilityRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProvidingOnDisabilityRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProvidingOnDisabilityDto CreateProvidingOnDisability(ProvidingOnDisabilityDto dto)
        {

            ProvidingOnDisabilityModel providingOnDisability = _mapper.Map<ProvidingOnDisabilityModel>(dto);
            _context.ProvidingOnDisability.Add(providingOnDisability);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDisabilityDto>(providingOnDisability);

            return dto;
        }


        public bool DoesProvidingOnDisabilityExist(ProvidingOnDisabilityDto dto)
        {
            bool providingOnDisabilityExist = false;
            providingOnDisabilityExist = _context.ProvidingOnDisability.Where(a => a.FNAId == dto.FNAId).Any();
            return providingOnDisabilityExist;

        }

        public ProvidingOnDisabilityDto GetProvidingOnDisability(int fnaId)
        {
            ProvidingOnDisabilityModel data = _context.ProvidingOnDisability.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<ProvidingOnDisabilityDto>(data);

        }

        public ProvidingOnDisabilityDto UpdateProvidingOnDisability(ProvidingOnDisabilityDto dto)
        {

            ProvidingOnDisabilityModel data = _context.ProvidingOnDisability.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();

            //set fields to be updated       
            data.ShortTermProtection = dto.ShortTermProtection;
            data.IncomeProtectionTerm_Months = dto.IncomeProtectionTerm_Months;
            data.LongTermProtection = dto.LongTermProtection;
            data.IncomeNeeds = dto.IncomeNeeds;
            data.NeedsTerm_Years = dto.NeedsTerm_Years;
            data.LiabilitiesToClear = dto.LiabilitiesToClear;
            data.CapitalNeeds = dto.CapitalNeeds;

            _context.ProvidingOnDisability.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDisabilityDto>(data);
            return dto;

        }



    }
}
