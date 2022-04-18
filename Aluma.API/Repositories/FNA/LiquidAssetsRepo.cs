﻿using Aluma.API.RepoWrapper;
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
        List<LiquidAssetsDto> GetLiquidAssets(int clientId);
        LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto[] dtoArray);


    }

    /// <summary>
    /// </summary>
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
            liquidAssetsExist = _context.LiquidAssets.Where(a => a.ClientId == dto.ClientId).Any();
            return liquidAssetsExist;

        }

        public List<LiquidAssetsDto> GetLiquidAssets(int clientId)
        {
            ICollection<LiquidAssetsModel> data = _context.LiquidAssets.Where(c => c.ClientId == clientId).ToList();
            List<LiquidAssetsDto> assets = new List<LiquidAssetsDto>();

            foreach (var item in data)
            {
                LiquidAssetsDto asset = new LiquidAssetsDto();

                asset.Id = item.Id;
                asset.ClientId = item.ClientId;
                asset.Description = item.Description;
                asset.Value = item.Value;
                asset.AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo);

                assets.Add(asset);

            }

            return assets;
        }

        public LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto[] dtoArray)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.LiquidAssets.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    LiquidAssetsModel updateItem = _context.LiquidAssets.Where(a => a.Id == item.Id).FirstOrDefault();
                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;
                    updateItem.AllocateTo = parsedAllocation;

                    _context.LiquidAssets.Update(updateItem);

                }
                else
                {
                    LiquidAssetsModel newItem = new LiquidAssetsModel();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;
                    newItem.AllocateTo = parsedAllocation;

                    _context.LiquidAssets.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }



    }
}