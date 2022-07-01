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
    public interface IPrimaryResidenceRepo : IRepoBase<PrimaryResidenceModel>
    {
        PrimaryResidenceDto CreatePrimaryResidence(PrimaryResidenceDto dto);
        bool DoesPrimaryResidenceExist(PrimaryResidenceDto dto);
        PrimaryResidenceDto GetPrimaryResidence(int fnaId);
        PrimaryResidenceDto UpdatePrimaryResidence(PrimaryResidenceDto dto);

    }

    public class PrimaryResidenceRepo : RepoBase<PrimaryResidenceModel>, IPrimaryResidenceRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public PrimaryResidenceRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public PrimaryResidenceDto CreatePrimaryResidence(PrimaryResidenceDto dto)
        {

            PrimaryResidenceModel primaryResidence = _mapper.Map<PrimaryResidenceModel>(dto);
            _context.PrimaryResidence.Add(primaryResidence);
            _context.SaveChanges();
            dto = _mapper.Map<PrimaryResidenceDto>(primaryResidence);

            return dto;
        }


        public bool DoesPrimaryResidenceExist(PrimaryResidenceDto dto)
        {
            bool primaryResidenceExist = false;
            primaryResidenceExist = _context.PrimaryResidence.Where(a => a.FNAId == dto.FNAId).Any();
            return primaryResidenceExist;

        }

        public PrimaryResidenceDto GetPrimaryResidence(int fnaId)
        {
            PrimaryResidenceModel data = _context.PrimaryResidence.Where(c => c.FNAId == fnaId).First();
            return _mapper.Map<PrimaryResidenceDto>(data);

        }

        public PrimaryResidenceDto UpdatePrimaryResidence(PrimaryResidenceDto dto)
        {
            PrimaryResidenceModel data = _context.PrimaryResidence.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();

            Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);
            data.Description = dto.Description;
            data.AllocateTo = parsedAllocation;
            data.Value = dto.Value;
            data.BaseCost = dto.BaseCost;

            data.DisposedOnDisability = dto.DisposedOnDisability;
            data.DisposedAtRetirement = dto.DisposedAtRetirement;
            data.Growth = dto.Growth;

            _context.PrimaryResidence.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<PrimaryResidenceDto>(data);
            return dto;

        }

    }
}