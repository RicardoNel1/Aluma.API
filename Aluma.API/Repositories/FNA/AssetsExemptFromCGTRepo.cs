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
    public interface IAssetsExemptFromCGTRepo : IRepoBase<AssetsExemptFromCGTModel>
    {
        bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto);
        List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int fnaId);
        List<AssetsExemptFromCGTDto>  UpdateAssetsExemptFromCGT(List<AssetsExemptFromCGTDto>  dtoArray);

        bool DeleteAssetsExemptFromCGTItem(int id);
    }


    public class AssetsExemptFromCGTRepo : RepoBase<AssetsExemptFromCGTModel>, IAssetsExemptFromCGTRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AssetsExemptFromCGTRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto)
        {
            bool assetsExemptFromCGTExist = false;
            assetsExemptFromCGTExist = _context.AssetsExemptFromCGT.Where(a => a.FNAId == dto.FNAId).Any();
            return assetsExemptFromCGTExist;

        }

        public List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int fnaId)
        {
            List<AssetsExemptFromCGTModel> data = _context.AssetsExemptFromCGT.Where(c => c.FNAId == fnaId).ToList();
            var assets = _mapper.Map<List<AssetsExemptFromCGTDto>>(data);

            return assets;
        }

        public List<AssetsExemptFromCGTDto>  UpdateAssetsExemptFromCGT(List<AssetsExemptFromCGTDto> dtoArray)
        {
            foreach (var asset in dtoArray)
            {
                try
                {
                    using AlumaDBContext db = new();
                    var pModel = _mapper.Map<AssetsExemptFromCGTModel>(asset);

                    if (db.AssetsExemptFromCGT.Where(a => a.Id == pModel.Id).Any())
                    {
                        db.Entry(pModel).State = EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            asset.Status = "Success";
                            asset.Message = "Asset Exempted Form CGT Updated";
                        }
                    }
                    else
                    {
                        db.AssetsExemptFromCGT.Add(pModel);
                        if (db.SaveChanges() > 0)
                        {
                            asset.Id = _mapper.Map<AssetsExemptFromCGTDto>(pModel).Id;
                            asset.Status = "Success";
                            asset.Message = "Asset Exempted Form CGT Created";
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

        public bool DeleteAssetsExemptFromCGTItem(int id)
        {
            AssetsExemptFromCGTModel item = _context.AssetsExemptFromCGT.Where(a => a.Id == id).First();
            _context.AssetsExemptFromCGT.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}