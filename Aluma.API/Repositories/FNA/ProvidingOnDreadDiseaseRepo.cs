using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IProvidingOnDreadDiseaseRepo : IRepoBase<ProvidingOnDreadDiseaseModel>
    {
        ProvidingOnDreadDiseaseDto CreateProvidingOnDreadDisease(ProvidingOnDreadDiseaseDto dto);
        bool DoesProvidingOnDreadDiseaseExist(ProvidingOnDreadDiseaseDto dto);
        ProvidingOnDreadDiseaseDto GetProvidingOnDreadDisease(int clientId);
        ProvidingOnDreadDiseaseDto UpdateProvidingOnDreadDisease(ProvidingOnDreadDiseaseDto dto);


    }

    public class ProvidingOnDreadDiseaseRepo : RepoBase<ProvidingOnDreadDiseaseModel>, IProvidingOnDreadDiseaseRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ProvidingOnDreadDiseaseRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ProvidingOnDreadDiseaseDto CreateProvidingOnDreadDisease(ProvidingOnDreadDiseaseDto dto)
        {

            ProvidingOnDreadDiseaseModel providingOnDreadDisease = _mapper.Map<ProvidingOnDreadDiseaseModel>(dto);
            _context.ProvidingOnDreadDisease.Add(providingOnDreadDisease);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDreadDiseaseDto>(providingOnDreadDisease);

            return dto;
        }


        public bool DoesProvidingOnDreadDiseaseExist(ProvidingOnDreadDiseaseDto dto)
        {
            bool providingOnDreadDiseaseExist = false;
            providingOnDreadDiseaseExist = _context.ProvidingOnDreadDisease.Where(a => a.FNAId == dto.FNAId).Any();
            return providingOnDreadDiseaseExist;

        }

        public ProvidingOnDreadDiseaseDto GetProvidingOnDreadDisease(int fnaId)
        {
            ProvidingOnDreadDiseaseModel data = new() { FNAId = fnaId };
            var entryExist = _context.ProvidingOnDreadDisease.Where(c => c.FNAId == fnaId);

            if (entryExist.Any())
            {
                data = entryExist.First();
            }
            return _mapper.Map<ProvidingOnDreadDiseaseDto>(data);

        }

        public ProvidingOnDreadDiseaseDto UpdateProvidingOnDreadDisease(ProvidingOnDreadDiseaseDto dto)
        {

            ProvidingOnDreadDiseaseModel data = _context.ProvidingOnDreadDisease.Where(a => a.FNAId == dto.FNAId).FirstOrDefault();

            //set fields to be updated       
            data.Needs_CapitalNeeds = dto.Needs_CapitalNeeds;
            data.Needs_GrossAnnualSalaryMultiple = dto.Needs_GrossAnnualSalaryMultiple;
            data.Available_DreadDiseaseDescription = dto.Available_DreadDiseaseDescription;
            data.Available_DreadDiseaseAmount = dto.Available_DreadDiseaseAmount;
            data.TotalDreadDisease = dto.TotalDreadDisease;

            _context.ProvidingOnDreadDisease.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<ProvidingOnDreadDiseaseDto>(data);
            return dto;

        }



    }
}
