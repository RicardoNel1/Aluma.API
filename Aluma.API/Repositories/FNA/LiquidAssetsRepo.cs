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
        LiquidAssetsDto CreateLiquidAssets(LiquidAssetsDto dto);

        bool DoesLiquidAssetsExist(LiquidAssetsDto dto);
        LiquidAssetsDto GetLiquidAssets(int clientId);
        LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto dto);

        //bool DeleteAsset(int id);


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

        public LiquidAssetsDto CreateLiquidAssets(LiquidAssetsDto dto)
        {

            LiquidAssetsModel liquidAssets = _mapper.Map<LiquidAssetsModel>(dto);
            _context.LiquidAssets.Add(liquidAssets);
            _context.SaveChanges();
            dto = _mapper.Map<LiquidAssetsDto>(liquidAssets);

            return dto;
        }


        public bool DoesLiquidAssetsExist(LiquidAssetsDto dto)
        {
            bool liquidAssetsExist = false;
            liquidAssetsExist = _context.LiquidAssets.Where(a => a.ClientId == dto.ClientId).Any();
            return liquidAssetsExist;

        }

        public LiquidAssetsDto GetLiquidAssets(int clientId)
        {
            LiquidAssetsModel data = _context.LiquidAssets.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<LiquidAssetsDto>(data);

        }

        public LiquidAssetsDto UpdateLiquidAssets(LiquidAssetsDto dto)
        {
            LiquidAssetsModel data = _context.LiquidAssets.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();            
            Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

            //set fields to be updated       
            data.Description = dto.Description;
            data.AllocateTo = parsedAllocation;
            data.Value = dto.Value;


            _context.LiquidAssets.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<LiquidAssetsDto>(data);
            return dto;

        }

       

    }
}