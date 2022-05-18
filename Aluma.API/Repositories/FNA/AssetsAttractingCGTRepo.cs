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
        List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int fnaId);
        AssetsAttractingCGTDto UpdateAssetsAttractingCGT(AssetsAttractingCGTDto[] dtoArray);

        bool DeleteAssetsAttractingCGTItem(int id);

    }

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


        public List<AssetsAttractingCGTDto> GetAssetsAttractingCGT(int fnaId)
        {
            ICollection<AssetsAttractingCGTModel> data = _context.AssetsAttractingCGT.Where(c => c.FNAId == fnaId).ToList();
            List<AssetsAttractingCGTDto> assets = new();

            foreach (var item in data)
            {
                AssetsAttractingCGTDto asset = new()
                {
                    Id = item.Id,
                    FNAId = item.FNAId,
                    Description = item.Description,
                    Value = item.Value,
                    PropertyType = Enum.GetName(typeof(DataService.Enum.PropertyTypeEnum), item.PropertyType),
                    AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo),
                    BaseCost = item.BaseCost
                };

                assets.Add(asset);

            }

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
                    Enum.TryParse(item.PropertyType, true, out DataService.Enum.PropertyTypeEnum parsedType);
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;
                    updateItem.PropertyType = parsedType;
                    updateItem.AllocateTo = parsedAllocation;
                    updateItem.BaseCost = item.BaseCost;

                    updateItem.DisposedOnDisability = item.DisposedOnDisability;
                    updateItem.DisposedAtRetirement = item.DisposedAtRetirement;
                    updateItem.Growth = item.Growth;

                    _context.AssetsAttractingCGT.Update(updateItem);

                }
                else
                {
                    AssetsAttractingCGTModel newItem = new();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    Enum.TryParse(item.PropertyType, true, out DataService.Enum.PropertyTypeEnum parsedType);
                    newItem.FNAId = item.FNAId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;
                    newItem.PropertyType = parsedType;
                    newItem.AllocateTo = parsedAllocation;
                    newItem.BaseCost = item.BaseCost;

                    newItem.DisposedOnDisability = item.DisposedOnDisability;
                    newItem.DisposedAtRetirement = item.DisposedAtRetirement;
                    newItem.Growth = item.Growth;

                    _context.AssetsAttractingCGT.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        public bool DeleteAssetsAttractingCGTItem(int id)
        {
            AssetsAttractingCGTModel item = _context.AssetsAttractingCGT.Where(a => a.Id == id).First();
            //item.isDeleted = false;
            _context.AssetsAttractingCGT.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}