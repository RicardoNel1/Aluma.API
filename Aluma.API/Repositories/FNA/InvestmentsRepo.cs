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
    public interface IInvestmentsRepo : IRepoBase<InvestmentsModel>
    {
        bool DoesInvestmentsExist(InvestmentsDto dto);
        List<InvestmentsDto> GetInvestments(int fnaId);
        List<InvestmentsDto> UpdateInvestments(List<InvestmentsDto> dtoArray);
        bool DeleteInvestmentsItem(int id);

    }

    public class InvestmentsRepo : RepoBase<InvestmentsModel>, IInvestmentsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public InvestmentsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public bool DoesInvestmentsExist(InvestmentsDto dto)
        {
            bool investmentsExist = false;
            investmentsExist = _context.Investments.Where(a => a.FNAId == dto.FNAId).Any();
            return investmentsExist;

        }

        public List<InvestmentsDto> GetInvestments(int fnaId)
        {
            List<InvestmentsModel> data = _context.Investments.Where(c => c.FNAId == fnaId).ToList();
            var investments = _mapper.Map<List<InvestmentsDto>>(data);

            return investments;
        }

        public List<InvestmentsDto> UpdateInvestments(List<InvestmentsDto> dtoArray)
        {
            foreach (var investment in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<InvestmentsModel>(investment);

                        if (_context.Investments.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                investment.Status = "Success";
                                investment.Message = "Investment Updated";
                            }
                        }
                        else
                        {
                            _context.Investments.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                investment.Id = _mapper.Map<InvestmentsDto>(pModel).Id;
                                investment.Status = "Success";
                                investment.Message = "Investment Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    investment.Status = "Server Error";
                    investment.Message = ex.Message;
                }
                
            }

            return dtoArray;

        }

        public bool DeleteInvestmentsItem(int id)
        {
            InvestmentsModel item = _context.Investments.Where(a => a.Id == id).First();
            _context.Investments.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}