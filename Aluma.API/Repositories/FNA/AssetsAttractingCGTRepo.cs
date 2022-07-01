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
    public interface IAssetsAttractingCGTRepo : IRepoBase<AssetsAttractingCGTModel>
    {
        List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int fnaId);
        List<AssetsAttractingCGTDto> UpdateAssetsAttractingCGT(List<AssetsAttractingCGTDto> dtoArray);

        bool DeleteAssetsAttractingCGTItem(int id);

    }

    public class AssetsAttractingCGTRepo : RepoBase<AssetsAttractingCGTModel>, IAssetsAttractingCGTRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AssetsAttractingCGTRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int fnaId)
        {
            List<AssetsAttractingCGTModel> data = _context.AssetsAttractingCGT.Where(c => c.FNAId == fnaId).ToList();
            var assets = _mapper.Map<List<AssetsAttractingCGTDto>>(data);

            return assets;
        }

        public List<AssetsAttractingCGTDto> UpdateAssetsAttractingCGT(List<AssetsAttractingCGTDto> dtoArray)
        {
            foreach (AssetsAttractingCGTDto asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<AssetsAttractingCGTModel>(asset);

                        if (_context.AssetsAttractingCGT.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Asset Attracting CGT Updated";
                            }
                        }
                        else
                        {
                            _context.AssetsAttractingCGT.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<AssetsAttractingCGTDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Asset Attracting CGT Created";
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

        public bool DeleteAssetsAttractingCGTItem(int id)
        {
            AssetsAttractingCGTModel item = _context.AssetsAttractingCGT.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.AssetsAttractingCGT.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}