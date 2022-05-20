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
    public interface IProvidingOnDeathRepo : IRepoBase<ProvidingOnDeathModel>
    {
        ProvidingOnDeathDto CreateProvidingOnDeath(ProvidingOnDeathDto dto);
        bool DoesProvidingOnDeathExist(ProvidingOnDeathDto dto);
        ProvidingOnDeathDto GetProvidingOnDeath(int clientId);
        ProvidingOnDeathDto UpdateProvidingOnDeath(ProvidingOnDeathDto dto);


    }

    public class ProvidingOnDeathRepo : RepoBase<ProvidingOnDeathModel>, IProvidingOnDeathRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProvidingOnDeathRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProvidingOnDeathDto CreateProvidingOnDeath(ProvidingOnDeathDto dto)
        {

            ProvidingOnDeathModel providingOnDeath = _mapper.Map<ProvidingOnDeathModel>(dto);
            _context.ProvidingOnDeath.Add(providingOnDeath);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDeathDto>(providingOnDeath);

            return dto;
        }


        public bool DoesProvidingOnDeathExist(ProvidingOnDeathDto dto)
        {
            bool providingOnDeathExist = false;
            providingOnDeathExist = _context.ProvidingOnDeath.Where(a => a.FNAId == dto.FNAId).Any();
            return providingOnDeathExist;

        }

        public ProvidingOnDeathDto GetProvidingOnDeath(int fnaId)
        {
            ProvidingOnDeathModel data = _context.ProvidingOnDeath.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<ProvidingOnDeathDto>(data);

        }

        public ProvidingOnDeathDto UpdateProvidingOnDeath(ProvidingOnDeathDto dto)
        {

            ProvidingOnDeathModel data = _context.ProvidingOnDeath.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();

            //set fields to be updated       
            data.IncomeNeeds = dto.IncomeNeeds;
            data.IncomeTerm_Years = dto.IncomeTerm_Years;
            data.CapitalNeeds = dto.CapitalNeeds;
            data.Available_InsuranceDescription = dto.Available_InsuranceDescription;
            data.Available_Insurance_Amount = dto.Available_Insurance_Amount;
            data.Available_PreTaxIncome_Amount = dto.Available_PreTaxIncome_Amount;
            data.Available_PreTaxIncome_Term = dto.Available_PreTaxIncome_Term;
            data.RetirementFunds = dto.RetirementFunds;

            _context.ProvidingOnDeath.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDeathDto>(data);
            return dto;

        }



    }
}