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
    public interface IRetirementPlanningRepo : IRepoBase<RetirementPlanningModel>
    {
        RetirementPlanningDto CreateRetirementPlanning(RetirementPlanningDto dto);
        bool DoesRetirementPlanningExist(RetirementPlanningDto dto);
        RetirementPlanningDto GetRetirementPlanning(int fnaId);
        RetirementPlanningDto UpdateRetirementPlanning(RetirementPlanningDto dto);


    }

    public class RetirementPlanningRepo : RepoBase<RetirementPlanningModel>, IRetirementPlanningRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RetirementPlanningRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

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
            retirementPlanningExist = _context.RetirementPlanning.Where(a => a.FNAId == dto.FNAId).Any();
            return retirementPlanningExist;

        }

        public RetirementPlanningDto GetRetirementPlanning(int fnaId)
        {
            try
            {
                RetirementPlanningModel data = _context.RetirementPlanning.Where(c => c.FNAId == fnaId).First();
                return _mapper.Map<RetirementPlanningDto>(data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public RetirementPlanningDto UpdateRetirementPlanning(RetirementPlanningDto dto)
        {
            RetirementPlanningModel data = _context.RetirementPlanning.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();

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



    }
}