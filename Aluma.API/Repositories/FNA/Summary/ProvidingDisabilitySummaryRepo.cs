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
    public interface IProvidingDisabilitySummaryRepo : IRepoBase<ProvidingDisabilitySummaryModel>
    {
        ProvidingDisabilitySummaryDto GetProvidingDisabilitySummary(int fnaId);
        ProvidingDisabilitySummaryDto UpdateProvidingDisabilitySummary(ProvidingDisabilitySummaryDto dto);
    }

    public class ProvidingDisabilitySummaryRepo : RepoBase<ProvidingDisabilitySummaryModel>, IProvidingDisabilitySummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProvidingDisabilitySummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProvidingDisabilitySummaryDto GetProvidingDisabilitySummary(int fnaId)
        {
            ProvidingDisabilitySummaryModel summaryValues = new() { FNAId = fnaId};
            var summaryValuesExist = _context.ProvidingDisabilitySummary.AsNoTracking().Where(a => a.FNAId == fnaId);

            if (summaryValuesExist.Any())
            {
                summaryValues = summaryValuesExist.FirstOrDefault();
            }

            return _mapper.Map<ProvidingDisabilitySummaryDto>(summaryValues);
        }

        public ProvidingDisabilitySummaryDto UpdateProvidingDisabilitySummary(ProvidingDisabilitySummaryDto dto)
        {
            ProvidingDisabilitySummaryModel newValues = _mapper.Map<ProvidingDisabilitySummaryModel>(dto);
            ProvidingDisabilitySummaryModel currValues = new();
            var currValuesExist = _context.ProvidingDisabilitySummary.Where(a => a.FNAId == dto.FNAId);

            if (!currValuesExist.Any())
            {
                _context.ProvidingDisabilitySummary.Add(newValues);
            }
            else
            {
                currValues = currValuesExist.FirstOrDefault();
                currValues.TotalAvailable = dto.TotalAvailable;
                currValues.TotalNeeds = dto.TotalNeeds;
                currValues.TotalExistingLongTermIncome = dto.TotalExistingLongTermIncome;
                currValues.TotalExistingShortTermIncome = dto.TotalExistingShortTermIncome;
                currValues.TotalIncomeNeed = dto.TotalIncomeNeed;

                _context.ProvidingDisabilitySummary.Update(currValues);

            }
            _context.SaveChanges();

            return _mapper.Map<ProvidingDisabilitySummaryDto>(currValues);
        }

    }
}