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
    public interface ITaxLumpsumRepo : IRepoBase<TaxLumpsumModel>
    {
        TaxLumpsumDto CreateTaxLumpsum(TaxLumpsumDto dto);
        TaxLumpsumDto GetTaxLumpsum(int id);
        TaxLumpsumDto UpdateTaxLumpsum(TaxLumpsumDto accrual);
        TaxLumpsumDto DeleteTaxLumpsum(int id);
    }

    public class TaxLumpsumRepo : RepoBase<TaxLumpsumModel>, ITaxLumpsumRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public TaxLumpsumRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public TaxLumpsumDto CreateTaxLumpsum(TaxLumpsumDto dto)
        {
            throw new NotImplementedException();
        }

        public TaxLumpsumDto GetTaxLumpsum(int id)
        {
            TaxLumpsumModel taxLumpsum = _context.TaxLumpsum.Where(c => c.ClientId == id).FirstOrDefault();

            if (taxLumpsum == null)
            {
                return new TaxLumpsumDto() { ClientId = id };
            }
            else
            {
                return _mapper.Map<TaxLumpsumDto>(taxLumpsum);
            }
        }

        public TaxLumpsumDto UpdateTaxLumpsum(TaxLumpsumDto accrual)
        {
            throw new NotImplementedException();
        }

        public TaxLumpsumDto DeleteTaxLumpsum(int id)
        {
            throw new NotImplementedException();
        }
    }
}
