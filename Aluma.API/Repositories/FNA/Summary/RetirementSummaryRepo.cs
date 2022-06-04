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
    public interface IRetirementSummaryRepo : IRepoBase<RetirementSummaryModel>
    {
        RetirementSummaryDto GetRetirementSummary(int fnaId);
        RetirementSummaryDto UpdateRetirementSummary(RetirementSummaryDto dto);

    }

    public class RetirementSummaryRepo : RepoBase<RetirementSummaryModel>, IRetirementSummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public RetirementSummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public RetirementSummaryDto GetRetirementSummary(int fnaId)
        {
            RetirementSummaryModel summaryValues = new RetirementSummaryModel();
            summaryValues = _context.RetirementSummary.AsNoTracking().Where(a => a.FNAId == fnaId).FirstOrDefault();

            return _mapper.Map<RetirementSummaryDto>(summaryValues);
        }

        public RetirementSummaryDto UpdateRetirementSummary(RetirementSummaryDto dto)
        {
            RetirementSummaryModel newValues = _mapper.Map<RetirementSummaryModel>(dto);
            RetirementSummaryModel currValues = _context.RetirementSummary.Where(a => a.Id == dto.Id).FirstOrDefault();
            currValues = newValues;
            _context.RetirementSummary.Update(currValues);
            _context.SaveChanges();

            return _mapper.Map<RetirementSummaryDto>(currValues);
        }


    }
}