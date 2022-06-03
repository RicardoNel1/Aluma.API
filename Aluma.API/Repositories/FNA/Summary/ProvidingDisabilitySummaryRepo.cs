using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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
            ProvidingDisabilitySummaryModel summaryValues = new ProvidingDisabilitySummaryModel();
            summaryValues = _context.ProvidingDisabilitySummary.AsNoTracking().Where(a => a.FNAId == fnaId).FirstOrDefault();

            return _mapper.Map<ProvidingDisabilitySummaryDto>(summaryValues);
        }

        public ProvidingDisabilitySummaryDto UpdateProvidingDisabilitySummary(ProvidingDisabilitySummaryDto dto)
        {
            ProvidingDisabilitySummaryModel newValues = _mapper.Map<ProvidingDisabilitySummaryModel>(dto);
            ProvidingDisabilitySummaryModel currValues = _context.ProvidingDisabilitySummary.Where(a => a.Id == dto.Id).FirstOrDefault();
            currValues = newValues;
            _context.ProvidingDisabilitySummary.Update(currValues);
            _context.SaveChanges();

            return _mapper.Map<ProvidingDisabilitySummaryDto>(currValues);
        }

    }
}