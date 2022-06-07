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
    public interface IRetirementPensionFundsRepo : IRepoBase<RetirementPensionFundsModel>
    {
        List<RetirementPensionFundsDto> GetRetirementPensionFunds(int fnaId);
        List<RetirementPensionFundsDto> UpdateRetirementPensionFunds(List<RetirementPensionFundsDto> dtoArray);

        string DeleteRetirementPensionFunds(int Id);

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


        public List<RetirementPensionFundsDto> GetRetirementPensionFunds(int fnaId)
        {
            List<RetirementPensionFundsModel> data = _context.RetirementPensionFunds.Where(c => c.FNAId == fnaId).ToList();
            var funds = _mapper.Map<List<RetirementPensionFundsDto>>(data);

            return funds;
        }

        public List<RetirementPensionFundsDto> UpdateRetirementPensionFunds(List<RetirementPensionFundsDto> dtoArray)
        {

            foreach (var asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<RetirementPensionFundsModel>(asset);

                        if (_context.RetirementPensionFunds.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Asset Retirement Pension Fund Updated";
                            }
                        }
                        else
                        {
                            _context.RetirementPensionFunds.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<RetirementPensionFundsDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Asset Retirement Pension Fund Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    asset.Status = "Server Error";
                    asset.Message = ex.Message;
                }
            }

            return dtoArray;

        }

        public string DeleteRetirementPensionFunds(int Id)
        {
            try
            {
                using (AlumaDBContext db = new())
                {

                    RetirementPensionFundsModel item = _context.RetirementPensionFunds.Where(a => a.Id == Id).First();

                    _context.RetirementPensionFunds.Remove(item);

                    if (_context.SaveChanges() > 0)
                    {
                        return "Asset Retirement Pension Fund Deleted Successfully";
                    }
                    else
                    {
                        return "Unsuccesful";
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

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