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
    public interface IAssetsExemptFromCGTRepo : IRepoBase<AssetsExemptFromCGTModel>
    {
        AssetsExemptFromCGTDto CreateAssetsExemptFromCGT(AssetsExemptFromCGTDto dto);
        bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto);
        AssetsExemptFromCGTDto GetAssetsExemptFromCGT(int clientId);
        AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto dto);
        

        //bool DeleteAsset(int id);


    }

    /// <summary>
    /// </summary>
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

        public AssetsExemptFromCGTDto CreateAssetsExemptFromCGT(AssetsExemptFromCGTDto dto)
        {

            AssetsExemptFromCGTModel assetsExemptFromCGT = _mapper.Map<AssetsExemptFromCGTModel>(dto);
            _context.AssetsExemptFromCGT.Add(assetsExemptFromCGT);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsExemptFromCGTDto>(assetsExemptFromCGT);

            return dto;
        }


        public bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto)
        {
            bool assetsExemptFromCGTExist = false;
            assetsExemptFromCGTExist = _context.AssetsExemptFromCGT.Where(a => a.ClientId == dto.ClientId).Any();
            return assetsExemptFromCGTExist;

        }

        public AssetsExemptFromCGTDto GetAssetsExemptFromCGT(int clientId)
        {
            AssetsExemptFromCGTModel data = _context.AssetsExemptFromCGT.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<AssetsExemptFromCGTDto>(data);

        }

        public AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto dto)
        {
            AssetsExemptFromCGTModel data = _context.AssetsExemptFromCGT.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();            
            Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

            //set fields to be updated       
            data.Description = dto.Description;
            data.AllocateTo = parsedAllocation;
            data.Value = dto.Value;


            _context.AssetsExemptFromCGT.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsExemptFromCGTDto>(data);
            return dto;

        }

       

    }
}