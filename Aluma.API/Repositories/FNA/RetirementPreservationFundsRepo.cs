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
    public interface IRetirementPreservationFundsRepo : IRepoBase<RetirementPreservationFundsModel>
    {
        List<RetirementPreservationFundsDto> GetRetirementPreservationFunds(int clientId);
        List<RetirementPreservationFundsDto> UpdateRetirementPreservationFunds(List<RetirementPreservationFundsDto> dtoArray);

        bool DeleteRetirementPreservationFundsItem(int id);

    }

    public class RetirementPreservationFundsRepo : RepoBase<RetirementPreservationFundsModel>, IRetirementPreservationFundsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RetirementPreservationFundsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public List<RetirementPreservationFundsDto> GetRetirementPreservationFunds(int fnaId)
        {
            List<RetirementPreservationFundsModel> data = _context.RetirementPreservationFunds.Where(c => c.FNAId == fnaId).ToList();
            var funds = _mapper.Map<List<RetirementPreservationFundsDto>>(data);

            return funds;
        }

        public List<RetirementPreservationFundsDto> UpdateRetirementPreservationFunds(List<RetirementPreservationFundsDto> dtoArray)
        {
            foreach (var asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<RetirementPreservationFundsModel>(asset);

                        if (db.RetirementPreservationFunds.Where(a => a.Id == pModel.Id).Any())
                        {
                            db.Entry(pModel).State = EntityState.Modified;
                            if (db.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Retirement Preservation Fund Updated";
                            }
                        }
                        else
                        {
                            db.RetirementPreservationFunds.Add(pModel);
                            if (db.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<RetirementPreservationFundsDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Retirement Preservation Fund Created";
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



        public bool DeleteRetirementPreservationFundsItem(int id)
        {
            RetirementPreservationFundsModel item = _context.RetirementPreservationFunds.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.RetirementPreservationFunds.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}