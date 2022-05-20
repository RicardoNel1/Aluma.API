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
    public interface ICapitalGainsTaxRepo : IRepoBase<CapitalGainsTaxModel>
    {
        CapitalGainsTaxDto CreateCapitalGainsTax(CapitalGainsTaxDto dto);
        bool DoesCapitalGainsTaxExist(CapitalGainsTaxDto dto);
        CapitalGainsTaxDto GetCapitalGainsTax(int clientId);
        CapitalGainsTaxDto UpdateCapitalGainsTax(CapitalGainsTaxDto dto);

    }

    public class CapitalGainsTaxRepo : RepoBase<CapitalGainsTaxModel>, ICapitalGainsTaxRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public CapitalGainsTaxRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public CapitalGainsTaxDto CreateCapitalGainsTax(CapitalGainsTaxDto dto)
        {

            CapitalGainsTaxModel CapitalGainsTax = _mapper.Map<CapitalGainsTaxModel>(dto);
            _context.CapitalGainsTax.Add(CapitalGainsTax);
            _context.SaveChanges();
            dto = _mapper.Map<CapitalGainsTaxDto>(CapitalGainsTax);

            return dto;
        }


        public bool DoesCapitalGainsTaxExist(CapitalGainsTaxDto dto)
        {
            bool CapitalGainsTaxExist = false;
            CapitalGainsTaxExist = _context.CapitalGainsTax.Where(a => a.ClientId == dto.ClientId).Any();
            return CapitalGainsTaxExist;

        }

        public CapitalGainsTaxDto GetCapitalGainsTax(int clientId)
        {
            CapitalGainsTaxModel data = _context.CapitalGainsTax.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<CapitalGainsTaxDto>(data);

        }

        public CapitalGainsTaxDto UpdateCapitalGainsTax(CapitalGainsTaxDto dto)
        {
            CapitalGainsTaxModel data = _context.CapitalGainsTax.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            data.PreviousCapitalLosses = dto.PreviousCapitalLosses;
            data.TotalCGTPayable = dto.TotalCGTPayable;


            _context.CapitalGainsTax.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<CapitalGainsTaxDto>(data);
            return dto;

        }

    }
}