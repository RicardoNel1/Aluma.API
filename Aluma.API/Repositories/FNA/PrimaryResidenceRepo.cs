using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IPrimaryResidenceRepo : IRepoBase<PrimaryResidenceModel>
    {
        PrimaryResidenceDto CreatePrimaryResidence(PrimaryResidenceDto dto);
        bool DoesPrimaryResidenceExist(PrimaryResidenceDto dto);
        PrimaryResidenceDto GetPrimaryResidence(int clientId);
        PrimaryResidenceDto UpdatePrimaryResidence(PrimaryResidenceDto dto, string update_type);

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
            primaryResidenceExist = _context.PrimaryResidence.Where(a => a.ClientId == dto.ClientId).Any();
            return primaryResidenceExist;

        }

        public PrimaryResidenceDto GetPrimaryResidence(int clientId)
        {
            PrimaryResidenceModel data = _context.PrimaryResidence.Where(c => c.ClientId == clientId).First();
            return _mapper.Map<PrimaryResidenceDto>(data);

        }

        public PrimaryResidenceDto UpdatePrimaryResidence(PrimaryResidenceDto dto, string update_type)
        {
            PrimaryResidenceModel data = _context.PrimaryResidence.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();            
            
            //Update All fields or Retirement or Disability
            if (update_type == "retirement")
            {
                data.DisposedAtRetirement = dto.DisposedAtRetirement;
                data.Growth = dto.Growth;
            }
            else
            {
                if (update_type == "disability")
                {
                    data.DisposedOnDisability = dto.DisposedOnDisability;
                }
                else
                {
                    Enum.TryParse(dto.AllocateTo, true, out DataService.Enum.EstateAllocationEnum parsedAllocation);

                    data.Description = dto.Description;
                    data.AllocateTo = parsedAllocation;
                    data.Value = dto.Value;
                    data.BaseCost = dto.BaseCost;
                }
            }

            _context.PrimaryResidence.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<PrimaryResidenceDto>(data);
            return dto;

        }

    }
}