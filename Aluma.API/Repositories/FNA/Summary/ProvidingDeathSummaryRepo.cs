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
    public interface IProvidingDeathSummaryRepo : IRepoBase<ProvidingDeathSummaryModel>
    {
        ProvidingDeathSummaryDto GetProvidingDeathSummary(int fnaId);
        ProvidingDeathSummaryDto UpdateProvidingDeathSummary(ProvidingDeathSummaryDto dto);
    }

    public class ProvidingDeathSummaryRepo : RepoBase<ProvidingDeathSummaryModel>, IProvidingDeathSummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProvidingDeathSummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProvidingDeathSummaryDto GetProvidingDeathSummary(int fnaId)
        {
            ProvidingDeathSummaryModel summaryValues = new() { FNAId = fnaId};
            var summaryValuesExist = _context.ProvidingDeathSummary.AsNoTracking().Where(a => a.FNAId == fnaId);
            if (summaryValuesExist.Any())
            {
                summaryValues = summaryValuesExist.FirstOrDefault();
            }

            return _mapper.Map<ProvidingDeathSummaryDto>(summaryValues);
        }

        public ProvidingDeathSummaryDto UpdateProvidingDeathSummary(ProvidingDeathSummaryDto dto)
        {
            ProvidingDeathSummaryModel newValues = _mapper.Map<ProvidingDeathSummaryModel>(dto);
            ProvidingDeathSummaryModel currValues = new();
            var currValuesExist = _context.ProvidingDeathSummary.Where(a => a.FNAId == dto.FNAId);

            if (!currValuesExist.Any())
            {
                _context.ProvidingDeathSummary.Add(newValues);
            }
            else
            {
                currValues = currValuesExist.FirstOrDefault();
                currValues.TotalAvailable = dto.TotalAvailable;
                currValues.TotalNeeds = dto.TotalNeeds;

                _context.ProvidingDeathSummary.Update(currValues);

            }
            _context.SaveChanges();

            return _mapper.Map<ProvidingDeathSummaryDto>(currValues);
        }
    }
}