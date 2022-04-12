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
        AssetsAttractingCGTDto CreateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray);
        bool DoesAssetsAttractingCGTExist(AssetsAttractingCGTDto dto);
        AssetsAttractingCGTDto GetAssetsAttractingCGT(int clientId);
        AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray);

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

        public AssetsAttractingCGTDto CreateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray)
        {
            foreach (var item in dtoArray)
            {

                AssetsAttractingCGTModel newItem = new AssetsAttractingCGTModel();
                //newItem = _mapper.Map<AssetsAttractingCGTModel>(item);

                Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                //newItem.Id = item.Id;
                newItem.ClientId = item.ClientId;
                newItem.Description = item.Description;
                newItem.Value = item.Value;
                newItem.AllocateTo = parsedAllocation;
                newItem.BaseCost = item.BaseCost;


                //_context.AssetsAttractingCGT.Add(assetsAttractingCGT);
                _context.AssetsAttractingCGT.Add(newItem);
                //_context.SaveChanges();
                //return _mapper.Map<AssetsAttractingCGTDto>(newItem);
            }
            _context.SaveChanges();
            return null;

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

        public AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray)
        {
            //AssetsAttractingCGTModel data = _context.AssetsAttractingCGT.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            //Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

            ////set fields to be updated       
            //data.Description = dto.Description;
            //data.AllocateTo = parsedAllocation;
            //data.Value = dto.Value;
            //data.BaseCost = dto.BaseCost;


            //_context.AssetsAttractingCGT.Update(data);
            //_context.SaveChanges();
            //dto = _mapper.Map<AssetsAttractingCGTDto>(data);
            //return dto;
            return null;

        }



    }
}