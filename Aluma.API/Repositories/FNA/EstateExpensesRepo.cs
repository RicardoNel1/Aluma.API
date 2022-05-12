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
    public interface IEstateExpensesRepo : IRepoBase<EstateExpensesModel>
    {
        EstateExpensesDto CreateEstateExpenses(EstateExpensesDto dto);
        bool DoesEstateExpensesExist(EstateExpensesDto dto);
        EstateExpensesDto GetEstateExpenses(int fnaId);
        EstateExpensesDto UpdateEstateExpenses(EstateExpensesDto dto);


    }

    public class EstateExpensesRepo : RepoBase<EstateExpensesModel>, IEstateExpensesRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public EstateExpensesRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public EstateExpensesDto CreateEstateExpenses(EstateExpensesDto dto)
        {

            EstateExpensesModel estateExpenses = _mapper.Map<EstateExpensesModel>(dto);
            _context.EstateExpenses.Add(estateExpenses);
            _context.SaveChanges();
            dto = _mapper.Map<EstateExpensesDto>(estateExpenses);

            return dto;
        }


        public bool DoesEstateExpensesExist(EstateExpensesDto dto)
        {
            bool estateExpensesExist = false;
            estateExpensesExist = _context.EstateExpenses.Where(a => a.FNAId == dto.FNAId).Any();
            return estateExpensesExist;

        }

        public EstateExpensesDto GetEstateExpenses(int fnaId)
        {
            EstateExpensesModel data = _context.EstateExpenses.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<EstateExpensesDto>(data);

        }

        public EstateExpensesDto UpdateEstateExpenses(EstateExpensesDto dto)
        {
            EstateExpensesModel data = _context.EstateExpenses.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();
            
            //set fields to be updated       
            data.AdminCosts = dto.AdminCosts;
            data.FuneralExpenses = dto.FuneralExpenses;
            data.CashBequests = dto.CashBequests;
            data.Other = dto.Other;
            data.CapitalLosses = dto.CapitalLosses;


            _context.EstateExpenses.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<EstateExpensesDto>(data);
            return dto;

        }



    }
}