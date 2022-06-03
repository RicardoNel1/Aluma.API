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
        List<InsuranceDto> GetInsurance(int fnaId);
        InsuranceDto UpdateInsurance(InsuranceDto[] dtoArray);
        bool DeleteInsuranceItem(int id);

    }

    public class InsuranceRepo : RepoBase<InsuranceModel>, IInsuranceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public InsuranceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }
               

        public List<InsuranceDto> GetInsurance(int fnaId)
        {
            ICollection<InsuranceModel> data = _context.Insurance.Where(c => c.FNAId == fnaId).ToList();
            List<InsuranceDto> insurance = new();

            foreach (var item in data)
            {
                InsuranceDto insured = new()
                {
                    Id = item.Id,
                    FNAId = item.FNAId,
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
                    InsuranceModel newItem = new();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    newItem.FNAId = item.FNAId;
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

        public bool DeleteInsuranceItem(int id)
        {
            InsuranceModel item = _context.Insurance.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.Insurance.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}