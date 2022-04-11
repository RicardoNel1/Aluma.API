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
    public interface IAssetsAttractingCGTRepo : IRepoBase<AssetsAttractingCGTModel>
    {
        AssetsAttractingCGTDto CreateAssetsAttractingCGT(AssetsAttractingCGTDto dto);
        bool DoesAssetsAttractingCGTExist(AssetsAttractingCGTDto dto);
        AssetsAttractingCGTDto GetAssetsAttractingCGT(int clientId);
        AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto dto);

        //bool DeleteAsset(int id);


    }

    /// <summary>
    /// </summary>
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

        public AssetsAttractingCGTDto CreateAssetsAttractingCGT(AssetsAttractingCGTDto dto)
        {

            AssetsAttractingCGTModel assetsAttractingCGT = _mapper.Map<AssetsAttractingCGTModel>(dto);
            _context.AssetsAttractingCGT.Add(assetsAttractingCGT);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsAttractingCGTDto>(assetsAttractingCGT);

            return dto;
        }


        public bool DoesAssetsAttractingCGTExist(AssetsAttractingCGTDto dto)
        {
            bool assetsAttractingCGTExist = false;
            assetsAttractingCGTExist = _context.AssetsAttractingCGT.Where(a => a.ClientId == dto.ClientId).Any();
            return assetsAttractingCGTExist;

        }

        public AssetsAttractingCGTDto GetAssetsAttractingCGT(int clientId)
        {
            AssetsAttractingCGTModel data = _context.AssetsAttractingCGT.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<AssetsAttractingCGTDto>(data);

        }

        public AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto dto)
        {
            AssetsAttractingCGTModel data = _context.AssetsAttractingCGT.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();            
            Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

            //set fields to be updated       
            data.Description = dto.Description;
            data.AllocateTo = parsedAllocation;
            data.Value = dto.Value;
            data.BaseCost = dto.BaseCost;


            _context.AssetsAttractingCGT.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsAttractingCGTDto>(data);
            return dto;

        }

       

    }
}