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
    public interface ILiabilitiesRepo : IRepoBase<LiabilitiesModel>
    {
        List<LiabilitiesDto> GetLiabilities(int fnaId);
        List<LiabilitiesDto> UpdateLiabilities(List<LiabilitiesDto> dtoArray);
        string DeleteLiabilities(int Id);
    }

    public class LiabilitiesRepo : RepoBase<LiabilitiesModel>, ILiabilitiesRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LiabilitiesRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }
                

        public List<LiabilitiesDto> GetLiabilities(int fnaId)
        {
            ICollection<LiabilitiesModel> data = _context.Liabilities.Where(c => c.FNAId == fnaId).ToList();
            List<LiabilitiesDto> liabilities = new();

            foreach (var item in data)
            {
                LiabilitiesDto liability = new();

                liability.Id = item.Id;
                liability.FNAId = item.FNAId;
                liability.Description = item.Description;
                liability.Value = item.Value;

                liabilities.Add(liability);

            }

            return liabilities;
        }

        public List<LiabilitiesDto> UpdateLiabilities(List<LiabilitiesDto> dtoArray)
        {

            foreach (var asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<LiabilitiesModel>(asset);

                        if (_context.Liabilities.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Asset Liability Updated";
                            }
                        }
                        else
                        {
                            _context.Liabilities.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<LiabilitiesDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Asset Liability Created";
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

        public string DeleteLiabilities(int Id)
        {
            try
            {
                using (AlumaDBContext db = new())
                {

                    LiabilitiesModel item = _context.Liabilities.Where(a => a.Id == Id).First();

                    _context.Liabilities.Remove(item);

                    if (_context.SaveChanges() > 0)
                    {
                        return "Liability Deleted Successfully";
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

    }
}