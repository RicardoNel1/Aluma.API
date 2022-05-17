using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface ILeadRepo : IRepoBase<LeadModel>
    {
        LeadDto CreateClientLead(LeadDto dto);
        bool DoesLeadExist(LeadDto dto);
        UserDto ConvertLeadToUser(LeadDto dto);
        bool IsLeadConverted(LeadDto dto);
        LeadDto GetClientLead(int clientId);
    }

    public class LeadRepo : RepoBase<LeadModel>, ILeadRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public LeadRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public LeadDto CreateClientLead(LeadDto dto)
        {

            LeadModel clientLead = _mapper.Map<LeadModel>(dto);
            _context.Leads.Add(clientLead);
            _context.SaveChanges();
            dto = _mapper.Map<LeadDto>(clientLead);

            return dto;

        }
        public bool DoesLeadExist(LeadDto dto)
        {
            bool leadExist = false;
            leadExist = _context.Leads.Where(a => a.ClientId == dto.ClientId && a.AdvisorId == dto.AdvisorId).Any();
            return leadExist;

        }
        public UserDto ConvertLeadToUser(LeadDto dto)
        {
            throw new NotImplementedException();

        }
        public bool IsLeadConverted(LeadDto dto)
        {
            bool leadConverted = false;
            leadConverted = _context.Clients.Where(a => a.Id == dto.ClientId && a.AdvisorId == dto.AdvisorId && a.UserId != 0).Any();
            return leadConverted;
        }

        public LeadDto GetClientLead(int clientId)
        {
            var lead = _context.Leads.Where(a => a.ClientId == clientId);
            LeadDto result = lead.Any() ? _mapper.Map<LeadDto>(lead.First()) : new LeadDto();
            return result;

        }      
    }
}