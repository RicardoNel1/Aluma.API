using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IConsumerProtectionRepo : IRepoBase<ConsumerProtectionModel>
    {
        ConsumerProtectionDto CreateConsumerProtection(ConsumerProtectionDto dto);

        bool DoesConsumerProtectionExist(ConsumerProtectionDto dto);

        ConsumerProtectionDto GetConsumerProtection(int clientId);

        ConsumerProtectionDto UpdateConsumerProtection(ConsumerProtectionDto dto);



    }

    public class ConsumerProtectionRepo : RepoBase<ConsumerProtectionModel>, IConsumerProtectionRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public ConsumerProtectionRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public ConsumerProtectionDto CreateConsumerProtection(ConsumerProtectionDto dto)
        {

            ConsumerProtectionModel data = _mapper.Map<ConsumerProtectionModel>(dto);

            _context.ConsumerProtection.Add(data);
            _context.SaveChanges();

            dto = _mapper.Map<ConsumerProtectionDto>(data);
            
            return dto;

        }


        public bool DoesConsumerProtectionExist(ConsumerProtectionDto dto)
        {
            bool consumerProtectionExist = false;
            consumerProtectionExist = _context.ConsumerProtection.Where(a => a.ClientId == dto.ClientId).Any();
            return consumerProtectionExist;

        }

        public ConsumerProtectionDto GetConsumerProtection(int clientId)
        {
            ConsumerProtectionModel consumerProtectionModel = _context.ConsumerProtection.Where(a => a.ClientId == clientId).FirstOrDefault();

            return _mapper.Map<ConsumerProtectionDto>(consumerProtectionModel);

        }

        public ConsumerProtectionDto UpdateConsumerProtection(ConsumerProtectionDto dto)
        {
            ConsumerProtectionModel data = _context.ConsumerProtection.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();

            //set fields to be updated
            
            //details.TaxNumber = dto.TaxNumber;
            //details.TaxObligations = dto.TaxObligations;
            //details.UsCitizen = dto.UsCitizen;
            //details.UsRelinquished = dto.UsRelinquished;
            //details.UsOther = dto.UsOther;

           
            _context.ConsumerProtection.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<ConsumerProtectionDto>(data);
            return dto;

        }       

    }
}