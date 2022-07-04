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
    public interface IClientPortfolioRepo : IRepoBase<ClientPortfolioDto>
    {
        ClientPortfolioDto GetClientPortfolio(int clientId);

    }

    public class ClientPortfolioRepo : RepoBase<ClientPortfolioDto>, IClientPortfolioRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ClientPortfolioRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }



        public ClientPortfolioDto GetClientPortfolio(int clientId)
        {

            ClientPortfolioDto dto = new ClientPortfolioDto();
            dto.Client = GetClient(clientId);
            dto.FNA = GetClientFNA(clientId);
            dto.Investments = GetInvestments(dto.FNA.Id);
            //List<InvestmentsModel> Investments = _context.Investments.Where(a => a.FNAId == dto.FNA.Id).ToList();
            //dto.InvestmentsTotal = Investments.Sum(a => a.Value);
            dto.Retirement = GetRetirement(dto.FNA.Id);
            dto.ProvidingDisability = GetProvidingDisability(dto.FNA.Id);
            dto.ProvidingDeath = GetProvidingDeath(dto.FNA.Id);
            dto.ProvidingDread = GetProvidingDread(dto.FNA.Id);       
            dto.ShortTermInsurance = GetShortTerm(clientId);
            dto.MedicalAid = GetMedical(clientId);

            return dto;

        }
              

        private ClientFNADto GetClientFNA(int clientId)
        {
            FNARepo _fna = new FNARepo(_context, _host, _config, _mapper, null);
            return _fna.GetClientFNA(clientId);
        }
        
        private ClientDto GetClient(int clientId)
        {
            ClientRepo _clientRepo = new ClientRepo(_context, _host, _config, _mapper, null);
            return _clientRepo.GetClient(new ClientDto(){ Id = clientId });
        }

        private List<InvestmentsDto> GetInvestments(int fnaId)
        {
            InvestmentsRepo _investments = new InvestmentsRepo(_context, _host, _config, _mapper);
            return _investments.GetInvestments(fnaId);
        }

        private RetirementSummaryDto GetRetirement(int fnaId)
        {
            RetirementSummaryRepo _retirement = new RetirementSummaryRepo(_context, _host, _config, _mapper);
            return _retirement.GetRetirementSummary(fnaId);
        }

        private ProvidingOnDisabilityDto GetProvidingDisability(int fnaId)
        {
            ProvidingOnDisabilityRepo _disability = new ProvidingOnDisabilityRepo(_context, _host, _config, _mapper);
            return _disability.GetProvidingOnDisability(fnaId);
        }

        private ProvidingOnDeathDto GetProvidingDeath(int fnaId)
        {
            ProvidingOnDeathRepo _death = new ProvidingOnDeathRepo(_context, _host, _config, _mapper);
            return _death.GetProvidingOnDeath(fnaId);
        }

        private ProvidingOnDreadDiseaseDto GetProvidingDread(int fnaId)
        {
            ProvidingOnDreadDiseaseRepo _dread = new ProvidingOnDreadDiseaseRepo(_context, _host, _config, _mapper);
            return _dread.GetProvidingOnDreadDisease(fnaId);
        }

        private List<ShortTermInsuranceDTO> GetShortTerm(int cliendId)
        {
            ShortTermInsuranceRepo _shortTerm = new ShortTermInsuranceRepo(_context, _host, _config, _mapper);
            return _shortTerm.GetSortTermInsurance(cliendId);
        }

        private MedicalAidDTO GetMedical(int clientId)
        {
            MedicalAidRepo _medical = new MedicalAidRepo(_context, _host, _config, _mapper);
            return _medical.GetMedicalAid(clientId);
        }
    }
}