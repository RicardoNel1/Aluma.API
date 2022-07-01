

using System;
using System.Collections.Generic;
using System.Linq;
using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface IShortTermInsuranceRepo : IRepoBase<ShortTermInsuranceModel>
    {
        List<ShortTermInsuranceDTO> GetSortTermInsurance(int clientId);
        List<ShortTermInsuranceDTO> UpdateSortTermInsurance(List<ShortTermInsuranceDTO> dtoArray);

        bool DeleteSortTermInsurance(int id);
    }

    public class ShortTermInsuranceRepo : RepoBase<ShortTermInsuranceModel>, IShortTermInsuranceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ShortTermInsuranceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public List<ShortTermInsuranceDTO> GetSortTermInsurance(int clientId)
        {
            List<ShortTermInsuranceModel> data = _context.SortTermInsurance.Where(c => c.ClientId == clientId).ToList();
            var shortTerms = _mapper.Map<List<ShortTermInsuranceDTO>>(data);

            return shortTerms;
        }

        public List<ShortTermInsuranceDTO> UpdateSortTermInsurance(List<ShortTermInsuranceDTO> dtoArray)
        {
            foreach (ShortTermInsuranceDTO shortTerm in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<ShortTermInsuranceModel>(shortTerm);

                        if (_context.SortTermInsurance.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                shortTerm.Status = "Success";
                                shortTerm.Message = "Sort-term Insurance Updated";
                            }
                        }
                        else
                        {
                            _context.SortTermInsurance.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                shortTerm.Id = _mapper.Map<ShortTermInsuranceDTO>(pModel).Id;
                                shortTerm.Status = "Success";
                                shortTerm.Message = "Sort-term Insurance Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    shortTerm.Status = "Server Error";
                    shortTerm.Message = ex.Message;
                }
            }
            return dtoArray;
        }

        public bool DeleteSortTermInsurance(int id)
        {
            ShortTermInsuranceModel item = _context.SortTermInsurance.Where(a => a.Id == id).First();
            
            _context.SortTermInsurance.Remove(item);
            return _context.SaveChanges() > 0;

        }
    }
}