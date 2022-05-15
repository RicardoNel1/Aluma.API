using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{

    public interface IEstateDutiesRepo : IRepoBase<EstateDutyModel>
    {
        EstateDutyDto CreateEstateDuty(EstateDutyDto dto);
        bool DoesEstateDutyExist(EstateDutyDto dto);
        EstateDutyDto GetEstateDuty(int id);
        EstateDutyDto UpdateEstateDuty(EstateDutyDto dto);
    }

    public class EstateDutyRepo : RepoBase<EstateDutyModel>, IEstateDutiesRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public EstateDutyRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public EstateDutyDto CreateEstateDuty(EstateDutyDto dto)
        {
            EstateDutyModel estateDuty = _mapper.Map<EstateDutyModel>(dto);
            _context.EstateDuty.Add(estateDuty);
            _context.SaveChanges();
            dto = _mapper.Map<EstateDutyDto>(estateDuty);

            return dto;
        }

        public bool DoesEstateDutyExist(EstateDutyDto dto)
        {
            bool estateDutyExist = false;
            estateDutyExist = _context.EstateDuty.Where(a => a.ClientId == dto.ClientId).Any();
            return estateDutyExist;
        }

        public EstateDutyDto GetEstateDuty(int clientId)
        {
            EstateDutyModel data = _context.EstateDuty.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<EstateDutyDto>(data);
        }

        public EstateDutyDto UpdateEstateDuty(EstateDutyDto dto)
        {
            EstateDutyModel data = _context.EstateDuty.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            //data.ClientId = dto.ClientId;                 //never update id
            data.Section4pValue = dto.Section4pValue;
            data.LimitedRights = dto.LimitedRights;
            data.ResidueToSpouse = dto.ResidueToSpouse;
            data.Abatement = dto.Abatement;
            data.TotalDutyPayable = dto.TotalDutyPayable;


            _context.EstateDuty.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<EstateDutyDto>(data);
            return dto;
        }

        IQueryable<EstateDutyModel> IRepoBase<EstateDutyModel>.FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
