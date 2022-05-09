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
        #region Public Methods

        bool DeleteLiquidAssetsItem(int id);

        bool DoesLiquidAssetsExist(LiquidAssetsDto dto);
        List<LiquidAssetsDto> GetLiquidAssets(int clientId);
        LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto[] dtoArray);

        #endregion Public Methods
    }

    public class LiquidAssetsRepo : RepoBase<LiquidAssetsModel>, ILiquidAssetsRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public LiquidAssetsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors


        #region Public Methods

        public bool DeleteLiquidAssetsItem(int id)
        {
            LiquidAssetsModel item = _context.LiquidAssets.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.LiquidAssets.Remove(item);
            _context.SaveChanges();

            return true;
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
            List<LiquidAssetsDto> assets = new();

            foreach (var item in data)
            {
                LiquidAssetsDto asset = new()
                {
                    Id = item.Id,
                    ClientId = item.ClientId,
                    Description = item.Description,
                    Value = item.Value,
                    AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo)
                };

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

        #endregion Public Methods
    }
}