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
    public interface IRetirementPlanningRepo : IRepoBase<RetirementPlanningModel>
    {
        #region Public Methods

        RetirementPlanningDto CreateRetirementPlanning(RetirementPlanningDto dto);
        bool DoesRetirementPlanningExist(RetirementPlanningDto dto);
        RetirementPlanningDto GetRetirementPlanning(int clientId);
        RetirementPlanningDto UpdateRetirementPlanning(RetirementPlanningDto dto);

        #endregion Public Methods


    }

    public class RetirementPlanningRepo : RepoBase<RetirementPlanningModel>, IRetirementPlanningRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public RetirementPlanningRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public RetirementPlanningDto CreateRetirementPlanning(RetirementPlanningDto dto)
        {

            RetirementPlanningModel retirementPlanning = _mapper.Map<RetirementPlanningModel>(dto);
            _context.RetirementPlanning.Add(retirementPlanning);
            _context.SaveChanges();
            dto = _mapper.Map<RetirementPlanningDto>(retirementPlanning);

            return dto;
        }


        public bool DoesRetirementPlanningExist(RetirementPlanningDto dto)
        {
            bool retirementPlanningExist = false;
            retirementPlanningExist = _context.RetirementPlanning.Where(a => a.ClientId == dto.ClientId).Any();
            return retirementPlanningExist;

        }

        public RetirementPlanningDto GetRetirementPlanning(int clientId)
        {
            RetirementPlanningModel data = _context.RetirementPlanning.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<RetirementPlanningDto>(data);

        }

        public RetirementPlanningDto UpdateRetirementPlanning(RetirementPlanningDto dto)
        {
            RetirementPlanningModel data = _context.RetirementPlanning.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            //set fields to be updated       
            data.MonthlyIncome = dto.MonthlyIncome;
            data.TermPostRetirement_Years = dto.TermPostRetirement_Years;
            data.IncomeEscalation = dto.IncomeEscalation;
            data.IncomeNeeds = dto.IncomeNeeds;
            data.NeedsTerm_Years = dto.NeedsTerm_Years;
            data.IncomeNeedsEscalation = dto.IncomeNeedsEscalation;
            data.CapitalNeeds = dto.CapitalNeeds;
            data.OutstandingLiabilities = dto.OutstandingLiabilities;
            data.SavingsEscalation = dto.SavingsEscalation;

            _context.RetirementPlanning.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<RetirementPlanningDto>(data);
            return dto;

        }

        #endregion Public Methods



    }
}