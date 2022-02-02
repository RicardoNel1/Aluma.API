using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Aluma.API.Repositories
{
    public interface IKycDataRepo : IRepoBase<KycDataModel>
    {
        public List<KycDataDto> GetAllKycEvents();

        public KycDataDto GetClientKycEvent(ClientDto dto);

        public KycDataDto CreateClientKycEvent(ClientDto dto);

        public KycDataDto UpdateClientKycEvent(ClientDto dto);

        bool DoesClientHaveKYC(ClientDto dto);

        bool DeleteKycEvent(ClientDto dto);
    }

    public class KycDataRepo : RepoBase<KycDataModel>, IKycDataRepo
    {
        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public KycDataRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        public List<KycDataDto> GetAllKycEvents()
        {
            List<KycDataModel> kycs = _context.KycData.Include(c => c.Client).ToList();
            return _mapper.Map<List<KycDataDto>>(kycs);
        }

        public KycDataDto CreateClientKycEvent(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }

        public KycDataDto GetClientKycEvent(ClientDto dto)
        {
            KycDataModel kyc = _context.KycData.Where(k => k.ClientId == dto.Id).First();
            return _mapper.Map<KycDataDto>(kyc);
        }

        public KycDataDto UpdateClientKycEvent(ClientDto dto)
        {
            throw new System.NotImplementedException();

            //var dto = _repo.KycService.GetConsumerKycMetaData(new FactoryDetailsDto()
            //        {
            //            idNumber = application.User.IdNumber,
            //            factoryId = application.Steps
            //               .First(c => c.StepType == ApplicationStepTypesEnum.DigitalKyc)
            //               .FactoryId
            //        });

            //var kycData = _mapper.Map<KycMetaDataModel>(dto);
            //        kycData.ApplicationId = currentStep.ApplicationId;

            //        _repo.KycMetaData.Create(kycData);
        }

        public bool DoesClientHaveKYC(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteKycEvent(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }
    }
}