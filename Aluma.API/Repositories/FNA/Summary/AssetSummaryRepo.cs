using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IAssetSummaryRepo : IRepoBase<AssetSummaryModel>
    {
        AssetSummaryDto GetAssetSummary(int fnaId);
        AssetSummaryDto UpdateAssetSummary(AssetSummaryDto dto);

    }

    public class AssetSummaryRepo : RepoBase<AssetSummaryModel>, IAssetSummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AssetSummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public AssetSummaryDto GetAssetSummary(int fnaId)
        {
            AssetSummaryModel summaryValues = new AssetSummaryModel();
            var summaryValuesExist = _context.AssetSummary.AsNoTracking().Where(a => a.FNAId == fnaId);
            if (summaryValuesExist.Any())
            {
                summaryValues = summaryValuesExist.FirstOrDefault();
            }
            return _mapper.Map<AssetSummaryDto>(summaryValues);
        }

        public AssetSummaryDto UpdateAssetSummary(AssetSummaryDto dto)
        {
            AssetSummaryModel newValues = _mapper.Map<AssetSummaryModel>(dto);
            AssetSummaryModel currValues = new();
            var currValuesExist = _context.AssetSummary.Where(a => a.FNAId == dto.FNAId);

            if (!currValuesExist.Any())
            {
                _context.AssetSummary.Add(newValues);
            }
            else
            {
                currValues  = currValuesExist.FirstOrDefault(); 
                currValues.TotalAssetsAttractingCGT = dto.TotalAssetsAttractingCGT;
                currValues.TotalAssetsExcemptCGT = dto.TotalAssetsExcemptCGT;
                currValues.TotalLiquidAssets = dto.TotalLiquidAssets;
                currValues.TotalAccrual = dto.TotalAccrual;
                currValues.TotalLiabilities = dto.TotalLiabilities;

                _context.AssetSummary.Update(currValues);

            }
            //currValues = newValues;
            //_context.AssetSummary.Update(currValues);
            _context.SaveChanges();

            return _mapper.Map<AssetSummaryDto>(currValues);
        }
    }
}