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
    public interface IRetirementPreservationFundsRepo : IRepoBase<RetirementPreservationFundsModel>
    {
        #region Public Methods

        bool DeleteRetirementPreservationFundsItem(int id);

        List<RetirementPreservationFundsDto> GetRetirementPreservationFunds(int clientId);
        RetirementPreservationFundsDto UpdateRetirementPreservationFunds(RetirementPreservationFundsDto[] dtoArray);

        #endregion Public Methods
    }

    public class RetirementPreservationFundsRepo : RepoBase<RetirementPreservationFundsModel>, IRetirementPreservationFundsRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public RetirementPreservationFundsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors


        #region Public Methods

        public bool DeleteRetirementPreservationFundsItem(int id)
        {
            RetirementPreservationFundsModel item = _context.RetirementPreservationFunds.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.RetirementPreservationFunds.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public List<RetirementPreservationFundsDto> GetRetirementPreservationFunds(int clientId)
        {
            ICollection<RetirementPreservationFundsModel> data = _context.RetirementPreservationFunds.Where(c => c.ClientId == clientId).ToList();
            List<RetirementPreservationFundsDto> funds = new List<RetirementPreservationFundsDto>();

            foreach (var item in data)
            {
                RetirementPreservationFundsDto fund = new RetirementPreservationFundsDto();

                fund.Id = item.Id;
                fund.ClientId = item.ClientId;
                fund.Description = item.Description;
                fund.Value = item.Value;


                funds.Add(fund);

            }

            return funds;
        }

        public RetirementPreservationFundsDto UpdateRetirementPreservationFunds(RetirementPreservationFundsDto[] dtoArray)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.RetirementPreservationFunds.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    RetirementPreservationFundsModel updateItem = _context.RetirementPreservationFunds.Where(a => a.Id == item.Id).FirstOrDefault();
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;

                    _context.RetirementPreservationFunds.Update(updateItem);

                }
                else
                {
                    RetirementPreservationFundsModel newItem = new RetirementPreservationFundsModel();

                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;

                    _context.RetirementPreservationFunds.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        #endregion Public Methods
    }
}