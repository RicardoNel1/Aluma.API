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
        bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto);
        List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int fnaId);
        AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto[] dtoArray, string update_type);

        bool DeleteAssetsExemptFromCGTItem(int id);
    }


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
                

        public bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto)
        {
           bool assetsExemptFromCGTExist = false;
            assetsExemptFromCGTExist = _context.AssetsExemptFromCGT.Where(a => a.FNAId == dto.FNAId).Any();
            return assetsExemptFromCGTExist;

        }

        public List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int fnaId)
        {
            ICollection<AssetsExemptFromCGTModel> data = _context.AssetsExemptFromCGT.Where(c => c.FNAId == fnaId).ToList();
            List<AssetsExemptFromCGTDto> assets = new List<AssetsExemptFromCGTDto>();

            foreach (var item in data)
            {
                AssetsExemptFromCGTDto asset = new AssetsExemptFromCGTDto();

                asset.Id = item.Id;
                asset.FNAId = item.FNAId;
                asset.Description = item.Description;
                asset.Value = item.Value;
                asset.AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo);

                assets.Add(asset);

            }

            return assets;
        }

        public AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto[] dtoArray, string update_type)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.AssetsExemptFromCGT.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    AssetsExemptFromCGTModel updateItem = _context.AssetsExemptFromCGT.Where(a => a.Id == item.Id).FirstOrDefault();

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

                    _context.AssetsExemptFromCGT.Update(updateItem);

                }
                else
                {
                    AssetsExemptFromCGTModel newItem = new AssetsExemptFromCGTModel();

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

                    _context.AssetsExemptFromCGT.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        public bool DeleteAssetsExemptFromCGTItem(int id)
        {
            AssetsExemptFromCGTModel item = _context.AssetsExemptFromCGT.Where(a => a.Id == id).First();
            _context.AssetsExemptFromCGT.Remove(item);
            _context.SaveChanges();

            return true;
        }

    }
}