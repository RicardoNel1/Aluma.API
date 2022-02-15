using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using DataService.Dto;
using DataService.Model;
using System;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IPurposeAndFundingRepo : IRepoBase<PurposeAndFundingModel>
    {
        bool DoesPurposeAndFundingExist(PurposeAndFundingDto dto);

        PurposeAndFundingDto CreatePurposeAndFunding(PurposeAndFundingDto dto);

        PurposeAndFundingDto UpdatePurposeAndFunding(PurposeAndFundingDto dto);

        PurposeAndFundingDto GetPurposeAndFunding(int applicationId);

    }

    public class PurposeAndFundingRepo : RepoBase<PurposeAndFundingModel>, IPurposeAndFundingRepo
    {
        private AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;


        public PurposeAndFundingRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;       
            _config = config;   
            _mapper = mapper;   

        }

        public bool DoesPurposeAndFundingExist(PurposeAndFundingDto dto)
        {
            bool purposeAndFundingExist = false;

            purposeAndFundingExist = _context.PurposeAndFunding.Where(a => a.ApplicationId == dto.ApplicationId).Any();

            return purposeAndFundingExist;
        }

        public PurposeAndFundingDto CreatePurposeAndFunding(PurposeAndFundingDto dto)
        {
            PurposeAndFundingModel details = _mapper.Map<PurposeAndFundingModel>(dto);
            _context.PurposeAndFunding.Add(details);
            _context.SaveChanges();
            dto = _mapper.Map<PurposeAndFundingDto>(details);
            return dto;
         
        }
                      

        public PurposeAndFundingDto UpdatePurposeAndFunding(PurposeAndFundingDto dto)
        {            
            PurposeAndFundingModel details = _context.PurposeAndFunding.Where(a => a.ApplicationId == dto.ApplicationId).FirstOrDefault();
                        
            details = _mapper.Map<PurposeAndFundingModel>(dto);
            _context.PurposeAndFunding.Update(details);
            _context.SaveChanges();
            dto = _mapper.Map<PurposeAndFundingDto>(details);
            return dto;

        }

        public PurposeAndFundingDto GetPurposeAndFunding(int applicationId)
        {
            PurposeAndFundingModel purposeAndFunding = _context.PurposeAndFunding.Where(c => c.ApplicationId == applicationId).First();
            return _mapper.Map<PurposeAndFundingDto>(purposeAndFunding);
        }

    }
}