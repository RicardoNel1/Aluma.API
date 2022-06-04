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
    public interface IInsuranceSummaryRepo : IRepoBase<InsuranceSummaryModel>
    {
        InsuranceSummaryDto GetInsuranceSummary(int fnaId);
        InsuranceSummaryDto UpdateInsuranceSummary(InsuranceSummaryDto dto);
    }

    public class InsuranceSummaryRepo : RepoBase<InsuranceSummaryModel>, IInsuranceSummaryRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public InsuranceSummaryRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }


        public InsuranceSummaryDto GetInsuranceSummary(int fnaId)
        {
            InsuranceSummaryModel summaryValues = new InsuranceSummaryModel();
            summaryValues = _context.InsuranceSummary.AsNoTracking().Where(a => a.FNAId == fnaId).FirstOrDefault();

            return _mapper.Map<InsuranceSummaryDto>(summaryValues);
        }

        public InsuranceSummaryDto UpdateInsuranceSummary(InsuranceSummaryDto dto)
        {
            InsuranceSummaryModel newValues = _mapper.Map<InsuranceSummaryModel>(dto);
            InsuranceSummaryModel currValues = _context.InsuranceSummary.Where(a => a.Id == dto.Id).FirstOrDefault();
            currValues = newValues;
            _context.InsuranceSummary.Update(currValues);
            _context.SaveChanges();

            return _mapper.Map<InsuranceSummaryDto>(currValues);
        }

    }
}