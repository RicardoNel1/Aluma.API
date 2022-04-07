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
    public interface IAssetsRepo : IRepoBase<AssetsModel>
    {
        AssetsDto CreateAssets(AssetsDto dto);

        //bool DoesAssetsExist(AssetsDto dto);

        //AssetDto GetAssets(int clientId);

        AssetsDto UpdateAssets(AssetsDto dto);

        //bool DeleteAsset(int id);


    }

    /// <summary>
    /// /<3
    /// </summary>
    public class AssetsRepo : RepoBase<AssetsModel>, IAssetsRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AssetsRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public AssetsDto CreateAssets(AssetsDto dto)
        {

            AssetsModel assets = _mapper.Map<AssetsModel>(dto);
            _context.Assets.Add(assets);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsDto>(assets);

            return dto;

        }


        //public bool DoesTaxResidencyExist(TaxResidencyDto dto)
        //{
        //    bool taxResidencyExist = false;
        //    taxResidencyExist = _context.TaxResidency.Where(a => a.ClientId == dto.ClientId).Any();
        //    return taxResidencyExist;

        //}

        //public TaxResidencyDto GetTaxResidency(int clientId)
        //{
        //    TaxResidencyModel taxResidencyModel = _context.TaxResidency.Where(a => a.ClientId == clientId).FirstOrDefault();
        //    taxResidencyModel.TaxResidencyItems = _context.TaxResidencyItems.Where(a => a.TaxResidencyId == taxResidencyModel.Id).ToList();


        //    return _mapper.Map<TaxResidencyDto>(taxResidencyModel);

        //}

        public AssetsDto UpdateAssets(AssetsDto dto)
        {
            AssetsModel data = _context.Assets.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            Enum.TryParse(dto.AssetType, true, out DataService.Enum.AddressTypesEnum parsedAssetType);

            //set fields to be updated            
            data.Description = dto.Description;

            _context.Assets.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<AssetsDto>(data);
            return dto;

        }

       

    }
}