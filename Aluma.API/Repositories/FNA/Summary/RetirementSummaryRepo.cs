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
            RetirementSummaryModel summaryValues = new( ){ FNAId = fnaId};

            var summaryValuesExist = _context.RetirementSummary.AsNoTracking().Where(a => a.FNAId == fnaId);
            if (summaryValuesExist.Any())
            {
                summaryValues = summaryValuesExist.FirstOrDefault();
            }

            return _mapper.Map<RetirementSummaryDto>(summaryValues);
        }

        public RetirementSummaryDto UpdateRetirementSummary(RetirementSummaryDto dto)
        {
            RetirementSummaryModel newValues = _mapper.Map<RetirementSummaryModel>(dto);
            RetirementSummaryModel currValues = new();
            var currValuesExist = _context.RetirementSummary.Where(a => a.FNAId == dto.FNAId);

            if (!currValuesExist.Any())
            {
                _context.RetirementSummary.Add(newValues);
            }
            else
            {
                currValues = currValuesExist.FirstOrDefault();
                currValues.TotalPensionFund = dto.TotalPensionFund;
                currValues.TotalPreservation = dto.TotalPreservation;
                currValues.SavingsRequiredPremium = dto.SavingsRequiredPremium;
                currValues.TotalAvailable = dto.TotalAvailable;
                currValues.TotalNeeds = dto.TotalNeeds;

                _context.RetirementSummary.Update(currValues);
            }

            _context.SaveChanges();

            return _mapper.Map<RetirementSummaryDto>(currValues);
        }


    }
}