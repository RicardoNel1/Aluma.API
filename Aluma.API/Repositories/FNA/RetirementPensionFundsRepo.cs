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
    public interface IRetirementPensionFundsRepo : IRepoBase<RetirementPensionFundsModel>
    {
        List<RetirementPensionFundsDto> GetRetirementPensionFunds(int clientId);
        RetirementPensionFundsDto UpdateRetirementPensionFunds(RetirementPensionFundsDto[] dtoArray);

        bool DeleteRetirementPensionFundsItem(int id);
    }

    public class RetirementPensionFundsRepo : RepoBase<RetirementPensionFundsModel>, IRetirementPensionFundsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RetirementPensionFundsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public List<RetirementPensionFundsDto> GetRetirementPensionFunds(int clientId)
        {
            ICollection<RetirementPensionFundsModel> data = _context.RetirementPensionFunds.Where(c => c.ClientId == clientId).ToList();
            List<RetirementPensionFundsDto> funds = new List<RetirementPensionFundsDto>();

            foreach (var item in data)
            {
                RetirementPensionFundsDto fund = new RetirementPensionFundsDto();

                fund.Id = item.Id;
                fund.ClientId = item.ClientId;
                fund.Description = item.Description;
                fund.Value = item.Value;
                fund.MonthlyContributions = item.MonthlyContributions;
                fund.EscPercent = item.EscPercent;

                funds.Add(fund);

            }

            return funds;
        }

        public RetirementPensionFundsDto UpdateRetirementPensionFunds(RetirementPensionFundsDto[] dtoArray)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.RetirementPensionFunds.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    RetirementPensionFundsModel updateItem = _context.RetirementPensionFunds.Where(a => a.Id == item.Id).FirstOrDefault();                    
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;
                    updateItem.MonthlyContributions = item.MonthlyContributions;
                    updateItem.EscPercent = item.EscPercent;

                    _context.RetirementPensionFunds.Update(updateItem);

                }
                else
                {
                    RetirementPensionFundsModel newItem = new RetirementPensionFundsModel();

                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;
                    newItem.MonthlyContributions = item.MonthlyContributions;
                    newItem.EscPercent = item.EscPercent;

                    _context.RetirementPensionFunds.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        public bool DeleteRetirementPensionFundsItem(int id)
        {
            RetirementPensionFundsModel item = _context.RetirementPensionFunds.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.RetirementPensionFunds.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}