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
using Aluma.API.Helpers;
using System.Threading.Tasks;
using FileStorageService;
using iText.Layout.Element;

namespace Aluma.API.Repositories
{
    public interface ICompletedFNARepo : IRepoBase<CompletedFNADto>
    {
        public Task<List<CompletedFNACountDto>> GetCompletedFNA();
        //EconomyVariablesDto UpdateEconomyVariablesSummary(EconomyVariablesDto dto);
    }
    public class CompletedFNARepo : RepoBase<CompletedFNADto>, ICompletedFNARepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IFileStorageRepo _fileStorage;
        MailSender _ms;


        public CompletedFNARepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
            _ms = new MailSender(_context, _config, _fileStorage, _host);
        }

        public async Task<List<CompletedFNACountDto>> GetCompletedFNA()
        {
            var query = (from f in _context.clientFNA
                         join c in _context.Clients on f.ClientId equals c.Id
                         join u in _context.Users on c.UserId equals u.Id
                         join a in _context.Advisors on f.AdvisorId equals a.Id
                         join b in _context.Users on a.UserId equals b.Id
                         where b.FirstName != "System"
                         && f.Created.Month == DateTime.Now.Month
                         //group f by b.FirstName + " " + b.LastName into g
                         orderby f.Created descending
                         select new CompletedFNADto
                         {
                             Client = u.FirstName + " " + u.LastName,
                             Advisor = b.FirstName + " " + b.LastName
                             //Created = f.Created
                         }).ToList();

   
            var distinctAdvisor = new List<string>();
            var fnaCount = new List<int>();

            //CompletedFNACountDto fnaCountDto = new CompletedFNACountDto
            //{
            //    Advisor = "",
            //    FNAsCompleted = 0,
            //};

            List<CompletedFNACountDto> fNAsCompleted = new List<CompletedFNACountDto>();

            foreach (string advisor in query.Select(x => x.Advisor).Distinct())
            {
                distinctAdvisor.Add(advisor);
            }                      

            foreach (var advisor in distinctAdvisor)
            {
                var count = query.Where(x => x != null && x.Advisor == advisor).Count();

                //distinctAdvisor               

                CompletedFNACountDto fnaCountDto = new CompletedFNACountDto {
                    Advisor = advisor,
                    FNAsCompleted = count
                };

                fNAsCompleted.Add(fnaCountDto);


            }

            await _ms.SendWeeklyFNAReport(fNAsCompleted);

            return fNAsCompleted;
        }

    }
}
