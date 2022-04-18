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
        EstateExpensesDto GetEstateExpenses(int clientId);
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
            bool estaeExpensesExist = false;
            estaeExpensesExist = _context.EstateExpenses.Where(a => a.ClientId == dto.ClientId).Any();
            return estaeExpensesExist;

        }

        public EstateExpensesDto GetEstateExpenses(int clientId)
        {
            EstateExpensesModel data = _context.EstateExpenses.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<EstateExpensesDto>(data);

        }

        public EstateExpensesDto UpdateEstateExpenses(EstateExpensesDto dto)
        {
            EstateExpensesModel data = _context.EstateExpenses.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            
            //set fields to be updated       
            data.AdminCosts = dto.AdminCosts;
            data.FuneralExpenses = dto.FuneralExpenses;
            data.CashBequests = dto.CashBequests;
            data.Other = dto.Other;


            _context.EstateExpenses.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<EstateExpensesDto>(data);
            return dto;

        }



    }
}