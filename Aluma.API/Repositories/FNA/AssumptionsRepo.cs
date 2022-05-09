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
    public interface IAssumptionsRepo : IRepoBase<AssumptionsModel>
    {
        #region Public Methods

        AssumptionsDto CreateAssumptions(AssumptionsDto dto);
        bool DoesAssumptionsExist(AssumptionsDto dto);
        AssumptionsDto GetAssumptions(int clientId);
        AssumptionsDto UpdateAssumptions(AssumptionsDto dto);

        #endregion Public Methods


    }

    public class AssumptionsRepo : RepoBase<AssumptionsModel>, IAssumptionsRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public AssumptionsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public AssumptionsDto CreateAssumptions(AssumptionsDto dto)
        {

            AssumptionsModel assumptions = _mapper.Map<AssumptionsModel>(dto);
            _context.Assumptions.Add(assumptions);
            _context.SaveChanges();
            dto = _mapper.Map<AssumptionsDto>(assumptions);

            return dto;
        }


        public bool DoesAssumptionsExist(AssumptionsDto dto)
        {
            bool assumptionsExist = false;
            assumptionsExist = _context.Assumptions.Where(a => a.ClientId == dto.ClientId).Any();
            return assumptionsExist;

        }

        public AssumptionsDto GetAssumptions(int clientId)
        {
            AssumptionsModel data = _context.Assumptions.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<AssumptionsDto>(data);

        }

        public AssumptionsDto UpdateAssumptions(AssumptionsDto dto)
        {
            AssumptionsModel data = _context.Assumptions.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            Enum.TryParse(dto.DeathInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDeath);
            Enum.TryParse(dto.DisabilityInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDisability);      

            //set fields to be updated       
            data.RetirementAge = dto.RetirementAge;
            data.CurrentNetIncome = dto.CurrentNetIncome;
            data.DeathInvestmentRisk = parsedDeath;
            data.DisabilityInvestmentRisk = parsedDisability;            

            _context.Assumptions.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AssumptionsDto>(data);
            return dto;

        }

        #endregion Public Methods



    }
}