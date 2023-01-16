using Aluma.API.RepoWrapper;
using DataService.Model;
using System.Linq;
using System;
using System.Collections.Generic;
using DataService.Context;
using DataService.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Aluma.API.Repositories
{
    public interface ICompletedFNARepo : IRepoBase<CompletedFNADto>
    {
        List<CompletedFNADto> GetCompletedFNA();
        //EconomyVariablesDto UpdateEconomyVariablesSummary(EconomyVariablesDto dto);
    }
    public class CompletedFNARepo : RepoBase<CompletedFNADto>, ICompletedFNARepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public CompletedFNARepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public List<CompletedFNADto> GetCompletedFNA()
        {
            var query = (from f in _context.clientFNA
                         join c in _context.Clients on f.ClientId equals c.Id
                         join u in _context.Users on c.UserId equals u.Id
                         join a in _context.Advisors on f.AdvisorId equals a.Id
                         join b in _context.Users on a.UserId equals b.Id
                         where b.FirstName != "System"
                         orderby f.Created descending
                         select new CompletedFNADto
                         {
                             Client = u.FirstName + " " + u.LastName,
                             Advisor = b.FirstName + " " + b.LastName,
                             Created = f.Created
                         }).ToList();
            return query;
        }

    }
}
