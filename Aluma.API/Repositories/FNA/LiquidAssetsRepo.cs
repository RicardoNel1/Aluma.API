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
    public interface ILiquidAssetsRepo : IRepoBase<LiquidAssetsModel>
    {
        bool DoesLiquidAssetsExist(LiquidAssetsDto dto);
        List<LiquidAssetsDto> GetLiquidAssets(int fnaId);
        List<LiquidAssetsDto> UpdateLiquidAssets(List<LiquidAssetsDto> dtoArray);

        bool DeleteLiquidAssetsItem(int id);

    }

    public class LiquidAssetsRepo : RepoBase<LiquidAssetsModel>, ILiquidAssetsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LiquidAssetsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public bool DoesLiquidAssetsExist(LiquidAssetsDto dto)
        {
            bool liquidAssetsExist = false;
            liquidAssetsExist = _context.LiquidAssets.Where(a => a.FNAId == dto.FNAId).Any();
            return liquidAssetsExist;

        }

        public List<LiquidAssetsDto> GetLiquidAssets(int fnaId)
        {
            List<LiquidAssetsModel> data = _context.LiquidAssets.Where(c => c.FNAId == fnaId).ToList();
            var assets = _mapper.Map<List<LiquidAssetsDto>>(data);

            return assets;
        }

        public List<LiquidAssetsDto> UpdateLiquidAssets(List<LiquidAssetsDto>dtoArray)
        {
            foreach (var asset in dtoArray)
            {
                try
                {
                    using (AlumaDBContext db = new())
                    {
                        var pModel = _mapper.Map<LiquidAssetsModel>(asset);

                        if (_context.LiquidAssets.Where(a => a.Id == pModel.Id).Any())
                        {
                            _context.Entry(pModel).State = EntityState.Modified;
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Status = "Success";
                                asset.Message = "Liquid Asset Updated";
                            }
                        }
                        else
                        {
                            _context.LiquidAssets.Add(pModel);
                            if (_context.SaveChanges() > 0)
                            {
                                asset.Id = _mapper.Map<LiquidAssetsDto>(pModel).Id;
                                asset.Status = "Success";
                                asset.Message = "Liquid Asset Created";
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    asset.Status = "Server Error";
                    asset.Message = ex.Message;
                }

                // bool existingItem = _context.LiquidAssets.Where(a => a.Id == item.Id).Any();

                // if (existingItem)
                // {
                //     LiquidAssetsModel updateItem = _context.LiquidAssets.Where(a => a.Id == item.Id).FirstOrDefault();

                //     Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                //     updateItem.Description = item.Description;
                //     updateItem.Value = item.Value;
                //     updateItem.AllocateTo = parsedAllocation;

                //     updateItem.DisposedOnDisability = item.DisposedOnDisability;
                //     updateItem.DisposedAtRetirement = item.DisposedAtRetirement;
                //     updateItem.Growth = item.Growth;

                //     _context.LiquidAssets.Update(updateItem);

                // }
                // else
                // {
                //     LiquidAssetsModel newItem = new LiquidAssetsModel();

                //     Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                //     newItem.FNAId = item.FNAId;
                //     newItem.Description = item.Description;
                //     newItem.Value = item.Value;
                //     newItem.AllocateTo = parsedAllocation;

                //     newItem.DisposedOnDisability = item.DisposedOnDisability;
                //     newItem.DisposedAtRetirement = item.DisposedAtRetirement;
                //     newItem.Growth = item.Growth;

                //     _context.LiquidAssets.Add(newItem);

                // }
            }

            return dtoArray;

        }

        public bool DeleteLiquidAssetsItem(int id)
        {
            LiquidAssetsModel item = _context.LiquidAssets.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.LiquidAssets.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}