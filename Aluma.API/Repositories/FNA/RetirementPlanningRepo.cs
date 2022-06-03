using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                using (AlumaDBContext db = new())
                {
                    var pModel = _mapper.Map<RetirementPlanningModel>(dto);

                    if (_context.RetirementPlanning.Where(a => a.Id == pModel.Id).Any())
                    {
                        _context.Entry(pModel).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Status = "Success";
                            dto.Message = "Asset Attracting CGT Updated";
                        }
                    }
                    else
                    {
                        _context.RetirementPlanning.Add(pModel);
                        if (_context.SaveChanges() > 0)
                        {
                            dto.Id = _mapper.Map<RetirementPlanningDto>(pModel).Id;
                            dto.Status = "Success";
                            dto.Message = "Asset Attracting CGT Created";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                dto.Status = "Server Error";
                dto.Message = ex.Message;
            }

            return dto;
        }



    }
}