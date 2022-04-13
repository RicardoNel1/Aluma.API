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
        AssetsAttractingCGTDto[] CreateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray);
        bool DoesAssetsAttractingCGTExist(AssetsAttractingCGTDto dto);
        List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int clientId);
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

        public AssetsAttractingCGTDto[] CreateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray)
        {
            foreach (var item in dtoArray)
            {
                AssetsAttractingCGTModel asset = new AssetsAttractingCGTModel();
                //newItem = _mapper.Map<AssetsAttractingCGTModel>(item);

                Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                asset.ClientId = item.ClientId;
                asset.Description = item.Description;
                asset.Value = item.Value;
                asset.AllocateTo = parsedAllocation;
                asset.BaseCost = item.BaseCost;

                _context.AssetsAttractingCGT.Add(asset);
                //return _mapper.Map<AssetsAttractingCGTDto>(asset);
            }
            _context.SaveChanges();
            return dtoArray;

        }


        public bool DoesAssetsAttractingCGTExist(AssetsAttractingCGTDto dto)
        {
            bool assetsAttractingCGTExist = false;
            assetsAttractingCGTExist = _context.AssetsAttractingCGT.Where(a => a.ClientId == dto.ClientId).Any();
            return assetsAttractingCGTExist;

        }

        public List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int clientId)
        {
            ICollection<AssetsAttractingCGTModel> data = _context.AssetsAttractingCGT.Where(c => c.ClientId == clientId).ToList();
            List<AssetsAttractingCGTDto> assets = new List<AssetsAttractingCGTDto>();

            foreach (var item in data)
            {                
                AssetsAttractingCGTDto asset = new AssetsAttractingCGTDto();
                //newItem = _mapper.Map<AssetsAttractingCGTModel>(item);

                asset.Id = item.Id;
                asset.ClientId = item.ClientId;
                asset.Description = item.Description;
                asset.Value = item.Value;
                asset.AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo);
                asset.BaseCost = item.BaseCost;

                //_context.AssetsAttractingCGT.Add(asset);
                //return _mapper.Map<AssetsAttractingCGTDto>(asset);
                assets.Add(asset);

            }

            //return _mapper.Map<AssetsAttractingCGTDto[]>(data);
            return assets;
        }

        public AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray)
        {
            
            foreach (var item in dtoArray)
            {

                bool existingItem = _context.AssetsAttractingCGT.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    AssetsAttractingCGTModel updateItem = _context.AssetsAttractingCGT.Where(a => a.Id == item.Id).FirstOrDefault();
                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;
                    updateItem.AllocateTo = parsedAllocation;
                    updateItem.BaseCost = item.BaseCost;

                    _context.AssetsAttractingCGT.Update(updateItem);

                }
                else
                {
                    AssetsAttractingCGTModel newItem = new AssetsAttractingCGTModel();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;
                    newItem.AllocateTo = parsedAllocation;
                    newItem.BaseCost = item.BaseCost;

                    _context.AssetsAttractingCGT.Add(newItem);

                }
            }

            //_context.AssetsAttractingCGT.Update(data);
            _context.SaveChanges();
            //dto = _mapper.Map<AssetsAttractingCGTDto>(data);
            //return dto;
            return null;

        }



    }
}