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
            //
            ClientPortfolioDto dto = new ClientPortfolioDto();
            dto.FNA = GetClientFNA(clientId);
            dto.Investments = GetInvestments(dto.FNA.Id);

            

            return null;

        }

        private List<InvestmentsDto> GetInvestments(int fnaId)
        {
            InvestmentsRepo _investments = new InvestmentsRepo(_context, _host, _config, _mapper);
            return _investments.GetInvestments(fnaId);
        }

        private ClientFNADto GetClientFNA(int clientId)
        {
            FNARepo _fna = new FNARepo(_context, _host, _config, _mapper, null);
            return _fna.GetClientFNA(clientId);
        }

    }
}