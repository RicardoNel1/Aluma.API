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
    public interface IInsuranceRepo : IRepoBase<InsuranceModel>
    {
        #region Public Methods

        bool DeleteInsuranceItem(int id);

        List<InsuranceDto> GetInsurance(int clientId);
        InsuranceDto UpdateInsurance(InsuranceDto[] dtoArray);

        #endregion Public Methods
    }

    public class InsuranceRepo : RepoBase<InsuranceModel>, IInsuranceRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public InsuranceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors


        #region Public Methods

        public bool DeleteInsuranceItem(int id)
        {
            InsuranceModel item = _context.Insurance.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.Insurance.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public List<InsuranceDto> GetInsurance(int clientId)
        {
            ICollection<InsuranceModel> data = _context.Insurance.Where(c => c.ClientId == clientId).ToList();
            List<InsuranceDto> insurance = new();

            foreach (var item in data)
            {
                InsuranceDto insured = new()
                {
                    Id = item.Id,
                    ClientId = item.ClientId,
                    Description = item.Description,
                    Owner = item.Owner,
                    LifeCover = item.LifeCover,
                    Disability = item.Disability,
                    DreadDisease = item.DreadDisease,
                    AbsoluteIpPm = item.AbsoluteIpPm,
                    ExtendedIpPm = item.ExtendedIpPm,
                    AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo)
                };

                insurance.Add(insured);

            }

            return insurance;
        }

        public InsuranceDto UpdateInsurance(InsuranceDto[] dtoArray)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.Insurance.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    InsuranceModel updateItem = _context.Insurance.Where(a => a.Id == item.Id).FirstOrDefault();
                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    updateItem.Description = item.Description;
                    updateItem.Owner = item.Owner;
                    updateItem.LifeCover = item.LifeCover;
                    updateItem.Disability = item.Disability;
                    updateItem.DreadDisease = item.DreadDisease;
                    updateItem.AbsoluteIpPm = item.AbsoluteIpPm;
                    updateItem.ExtendedIpPm = item.ExtendedIpPm;
                    updateItem.AllocateTo = parsedAllocation;

                    _context.Insurance.Update(updateItem);

                }
                else
                {
                    InsuranceModel newItem = new InsuranceModel();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Owner = item.Owner;
                    newItem.LifeCover = item.LifeCover;
                    newItem.Disability = item.Disability;
                    newItem.DreadDisease = item.DreadDisease;
                    newItem.AbsoluteIpPm = item.AbsoluteIpPm;
                    newItem.ExtendedIpPm = item.ExtendedIpPm;
                    newItem.AllocateTo = parsedAllocation;

                    _context.Insurance.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        #endregion Public Methods
    }
}