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
    public interface IEconomyVariablesSummaryRepo : IRepoBase<EconomyVariablesModel>
    {
        EconomyVariablesDto GetEconomyVariablesSummary(int fnaId);
        EconomyVariablesDto UpdateEconomyVariablesSummary(EconomyVariablesDto dto);
    }

    public class EconomyVariablesSummaryRepo : RepoBase<EconomyVariablesModel>, IEconomyVariablesSummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public EconomyVariablesSummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public EconomyVariablesDto GetEconomyVariablesSummary(int fnaId)
        {
            EconomyVariablesModel summaryValues = new EconomyVariablesModel();
            summaryValues = _context.EconomyVariables.AsNoTracking().FirstOrDefault();

            return _mapper.Map<EconomyVariablesDto>(summaryValues);
        }

        public EconomyVariablesDto UpdateEconomyVariablesSummary(EconomyVariablesDto dto)
        {
            EconomyVariablesModel newValues = _mapper.Map<EconomyVariablesModel>(dto);
            EconomyVariablesModel currValues = _context.EconomyVariables.Where(a => a.Id == dto.Id).FirstOrDefault();
            currValues = newValues;
            _context.EconomyVariables.Update(currValues);
            _context.SaveChanges();

            return _mapper.Map<EconomyVariablesDto>(currValues);
        }

    }
}
