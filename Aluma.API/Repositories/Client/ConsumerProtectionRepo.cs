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
    public interface IConsumerProtectionRepo : IRepoBase<ConsumerProtectionModel>
    {
        #region Public Methods

        ConsumerProtectionDto CreateConsumerProtection(ConsumerProtectionDto dto);

        bool DoesConsumerProtectionExist(ConsumerProtectionDto dto);

        ConsumerProtectionDto GetConsumerProtection(int clientId);

        ConsumerProtectionDto UpdateConsumerProtection(ConsumerProtectionDto dto);

        #endregion Public Methods



    }

    public class ConsumerProtectionRepo : RepoBase<ConsumerProtectionModel>, IConsumerProtectionRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public ConsumerProtectionRepo(AlumaDBContext databaseContext, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(databaseContext)
        {
            _context = databaseContext;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public ConsumerProtectionDto CreateConsumerProtection(ConsumerProtectionDto dto)
        {

            ConsumerProtectionModel consumerProtection = _mapper.Map<ConsumerProtectionModel>(dto);
            _context.ConsumerProtection.Add(consumerProtection);
            _context.SaveChanges();
            dto = _mapper.Map<ConsumerProtectionDto>(consumerProtection);

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
            var consumerProtectionModel = _context.ConsumerProtection.Where(a => a.ClientId == clientId);
            var result = consumerProtectionModel.Any() ? _mapper.Map<ConsumerProtectionDto>(consumerProtectionModel.First()) : new ConsumerProtectionDto();
            return result;

        }

        public ConsumerProtectionDto UpdateConsumerProtection(ConsumerProtectionDto dto)
        {
            ConsumerProtectionModel data = _context.ConsumerProtection.Where(a => a.ClientId == dto.ClientId).FirstOrDefault();
            Enum.TryParse(dto.PreferredComm, true, out DataService.Enum.CommEnum parsedComm);

            //set fields to be updated            
            data.InformProducts = dto.InformProducts;
            data.InformOffers = dto.InformOffers;
            data.RequestResearch = dto.RequestResearch;
            data.PreferredComm = parsedComm;
            data.OtherCommMethods = dto.OtherCommMethods;

            _context.ConsumerProtection.Update(data);
            _context.SaveChanges();
            dto = _mapper.Map<ConsumerProtectionDto>(data);
            return dto;

        }

        #endregion Public Methods       

    }
}