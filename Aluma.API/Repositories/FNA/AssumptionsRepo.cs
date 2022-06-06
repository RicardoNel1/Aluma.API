using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IAssumptionsRepo : IRepoBase<AssumptionsModel>
    {
        AssumptionsDto CreateAssumptions(AssumptionsDto dto);
        bool DoesAssumptionsExist(AssumptionsDto dto);
        AssumptionsDto GetAssumptions(int fnaId);
        AssumptionsDto UpdateAssumptions(AssumptionsDto dto);


    }

    public class AssumptionsRepo : RepoBase<AssumptionsModel>, IAssumptionsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AssumptionsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

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
            assumptionsExist = _context.Assumptions.Where(a => a.FNAId == dto.FNAId).Any();
            return assumptionsExist;

        }

        public AssumptionsDto GetAssumptions(int fnaId)
        {
            AssumptionsModel data = _context.Assumptions.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<AssumptionsDto>(data);

        }

        public AssumptionsDto UpdateAssumptions(AssumptionsDto dto)
        {
            try
            {
                using (AlumaDBContext db = new())
                {
                    var pModel = _mapper.Map<AssumptionsModel>(dto);

                    if (_context.Assumptions.Where(a => a.Id == pModel.Id).Any())
                    {
                        _context.Entry(pModel).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Status = "Success";
                            dto.Message = "Assumptions Updated";
                        }
                    }
                    else
                    {
                        _context.Assumptions.Add(pModel);
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Id = _mapper.Map<AssumptionsDto>(pModel).Id;
                            dto.Status = "Success";
                            dto.Message = "Assumptions Created";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                dto.Status = "Server Error";
                dto.Message = ex.Message;
            }

            //Update according to screen
            //if (update_type == "retirement")
            //{
            //    Enum.TryParse(dto.RetirementInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedRetirement);
            //    data.RetirementInvestmentRisk = parsedRetirement;
            //    data.RetirementAge = dto.RetirementAge;
            //}
            //else if (update_type == "death")
            //{
            //    Enum.TryParse(dto.DeathInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDeath);
            //    data.DeathInvestmentRisk = parsedDeath;
            //    data.CurrentGrossIncome = dto.CurrentGrossIncome;
            //}
            //else if (update_type == "disability")
            //{
            //    Enum.TryParse(dto.DisabilityInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDisability);
            //    data.DisabilityInvestmentRisk = parsedDisability;
            //    data.RetirementAge = dto.RetirementAge;
            //    data.CurrentGrossIncome = dto.CurrentGrossIncome;
            //}
            //else if (update_type == "dread")
            //{
            //    data.CurrentGrossIncome = dto.CurrentGrossIncome;
            //}

            // Enum.TryParse(dto.RetirementInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedRetirement);
            // Enum.TryParse(dto.DeathInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDeath);
            // Enum.TryParse(dto.DisabilityInvestmentRisk, true, out DataService.Enum.InvestmentRiskEnum parsedDisability);
            // data.RetirementInvestmentRisk = parsedRetirement;
            // data.DeathInvestmentRisk = parsedDeath;
            // data.DisabilityInvestmentRisk = parsedDisability;
            // data.RetirementAge = dto.RetirementAge;            
            // data.CurrentGrossIncome = dto.CurrentGrossIncome;           

            return dto;

        }



    }
}