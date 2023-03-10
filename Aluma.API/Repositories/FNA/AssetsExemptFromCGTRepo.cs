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

        string DeleteAssetsExemptFromCGTItem(int id);
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

                    if (_context.AssetsExemptFromCGT.Where(a => a.Id == pModel.Id).Any())
                    {
                        _context.Entry(pModel).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            asset.Status = "Success";
                            asset.Message = "Asset Exempted Form CGT Updated";
                        }
                    }
                    else
                    {
                        _context.AssetsExemptFromCGT.Add(pModel);
                        if (_context.SaveChanges() > 0)
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

        public string DeleteAssetsExemptFromCGTItem(int id)
        {
            try
            {
                using (AlumaDBContext db = new())
                {

                    AssetsExemptFromCGTModel item = _context.AssetsExemptFromCGT.Where(a => a.Id == id).First();

                    _context.AssetsExemptFromCGT.Remove(item);

                    if (_context.SaveChanges() > 0)
                    {
                        return "Asset Deleted Successfully";
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