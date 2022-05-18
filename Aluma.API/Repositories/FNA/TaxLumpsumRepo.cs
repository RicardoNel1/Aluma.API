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
            TaxLumpsumModel tls = _mapper.Map<TaxLumpsumModel>(dto);

            _context.TaxLumpsum.Add(tls);
            _context.SaveChanges();

            dto = _mapper.Map<TaxLumpsumDto>(tls);

            return dto;
        }

        public TaxLumpsumDto GetTaxLumpsum(int fnaId)
        {
            TaxLumpsumModel taxLumpsum = _context.TaxLumpsum.Where(c => c.FnaId == fnaId).FirstOrDefault();

            if (taxLumpsum == null)
            {
                return new TaxLumpsumDto();
            }
            else
            {
                return _mapper.Map<TaxLumpsumDto>(taxLumpsum);
            }
        }

        public TaxLumpsumDto UpdateTaxLumpsum(TaxLumpsumDto dto)
        {
            TaxLumpsumModel tls = _context.TaxLumpsum.Where(a => a.FnaId == dto.FnaId).FirstOrDefault();

            tls.PreviouslyDisallowed = dto.PreviouslyDisallowed;
            tls.RetirementReceived = dto.RetirementReceived;
            tls.WithdrawalReceived = dto.WithdrawalReceived;
            tls.SeverenceReceived = dto.SeverenceReceived;
            tls.TaxPayable = dto.TaxPayable;

            _context.TaxLumpsum.Update(tls);
            _context.SaveChanges();
            dto = _mapper.Map<TaxLumpsumDto>(dto);
            return dto;
        }

        public TaxLumpsumDto DeleteTaxLumpsum(int id)
        {
            throw new NotImplementedException();
        }
    }
}
