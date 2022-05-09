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
        #region Public Methods

        bool DeleteAssetsExemptFromCGTItem(int id);

        bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto);
        List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int clientId);
        AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto[] dtoArray);

        #endregion Public Methods
    }


    public class AssetsExemptFromCGTRepo : RepoBase<AssetsExemptFromCGTModel>, IAssetsExemptFromCGTRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public AssetsExemptFromCGTRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors


        #region Public Methods

        public bool DeleteAssetsExemptFromCGTItem(int id)
        {
            AssetsExemptFromCGTModel item = _context.AssetsExemptFromCGT.Where(a => a.Id == id).First();
            _context.AssetsExemptFromCGT.Remove(item);
            _context.SaveChanges();

            return true;
        }

        public bool DoesAssetsExemptFromCGTExist(AssetsExemptFromCGTDto dto)
        {
            bool assetsExemptFromCGTExist = false;
            assetsExemptFromCGTExist = _context.AssetsExemptFromCGT.Where(a => a.ClientId == dto.ClientId).Any();
            return assetsExemptFromCGTExist;

        }

        public List<AssetsExemptFromCGTDto> GetAssetsExemptFromCGT(int clientId)
        {
            ICollection<AssetsExemptFromCGTModel> data = _context.AssetsExemptFromCGT.Where(c => c.ClientId == clientId).ToList();
            List<AssetsExemptFromCGTDto> assets = new List<AssetsExemptFromCGTDto>();

            foreach (var item in data)
            {
                AssetsExemptFromCGTDto asset = new AssetsExemptFromCGTDto();

                asset.Id = item.Id;
                asset.ClientId = item.ClientId;
                asset.Description = item.Description;
                asset.Value = item.Value;
                asset.AllocateTo = Enum.GetName(typeof(DataService.Enum.EstateAllocationEnum), item.AllocateTo);

                assets.Add(asset);

            }

            return assets;
        }

        public AssetsExemptFromCGTDto UpdateAssetsExemptFromCGT(AssetsExemptFromCGTDto[] dtoArray)
        {

            foreach (var item in dtoArray)
            {

                bool existingItem = _context.AssetsExemptFromCGT.Where(a => a.Id == item.Id).Any();

                if (existingItem)
                {
                    AssetsExemptFromCGTModel updateItem = _context.AssetsExemptFromCGT.Where(a => a.Id == item.Id).FirstOrDefault();
                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    updateItem.Description = item.Description;
                    updateItem.Value = item.Value;
                    updateItem.AllocateTo = parsedAllocation;

                    _context.AssetsExemptFromCGT.Update(updateItem);

                }
                else
                {
                    AssetsExemptFromCGTModel newItem = new AssetsExemptFromCGTModel();

                    Enum.TryParse(item.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
                    newItem.ClientId = item.ClientId;
                    newItem.Description = item.Description;
                    newItem.Value = item.Value;
                    newItem.AllocateTo = parsedAllocation;

                    _context.AssetsExemptFromCGT.Add(newItem);

                }
            }

            _context.SaveChanges();
            return null;

        }

        #endregion Public Methods
    }
}