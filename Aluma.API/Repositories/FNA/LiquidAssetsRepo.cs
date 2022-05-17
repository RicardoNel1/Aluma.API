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
    public interface ILiquidAssetsRepo : IRepoBase<LiquidAssetsModel>
    {
        bool DoesLiquidAssetsExist(LiquidAssetsDto dto);
        List<LiquidAssetsDto> GetLiquidAssets(int fnaId);
        LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto[] dtoArray, string update_type);

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
            ICollection<LiquidAssetsModel> data = _context.LiquidAssets.Where(c => c.FNAId == fnaId).ToList();
            List<LiquidAssetsDto> assets = new();

            foreach (var item in data)
            {
                LiquidAssetsDto asset = new()
                {
                    Id = item.Id,
                    FNAId = item.FNAId,
                    Description = item.Description,
                    Value = item.Value,
                    AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo)
                };

                assets.Add(asset);

            }

            return assets;
        }

        public LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto[] dtoArray, string update_type)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.LiquidAssets.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    LiquidAssetsModel updateItem = _context.LiquidAssets.Where(a => a.Id == item.Id).FirstOrDefault();

                    //Update All fields or Retirement or Disability
                    if (update_type == "retirement")
                    {
                        updateItem.DisposedAtRetirement = item.DisposedAtRetirement;
                        updateItem.Growth = item.Growth;
                    }
                    else
                    {
                        if (update_type == "disability")
                        {
                            updateItem.DisposedOnDisability = item.DisposedOnDisability;
                        }
                        else
                        {
                            Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                            updateItem.Description = item.Description;
                            updateItem.Value = item.Value;
                            updateItem.AllocateTo = parsedAllocation;
                        }
                    }

                    _context.LiquidAssets.Update(updateItem);

                }
                else
                {
                    LiquidAssetsModel newItem = new LiquidAssetsModel();

                    //Add fields or Retirement or Disability
                    if (update_type == "retirement")
                    {
                        newItem.DisposedAtRetirement = item.DisposedAtRetirement;
                        newItem.Growth = item.Growth;
                    }
                    else
                    {
                        if (update_type == "disability")
                        {
                            newItem.DisposedOnDisability = item.DisposedOnDisability;
                        }
                        else
                        {
                            Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                            newItem.FNAId = item.FNAId;
                            newItem.Description = item.Description;
                            newItem.Value = item.Value;
                            newItem.AllocateTo = parsedAllocation;
                        }
                    }

                    _context.LiquidAssets.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

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