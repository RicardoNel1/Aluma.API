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
    public interface IKYCDataRepo : IRepoBase<KYCDataModel>
    {
        #region Public Methods

        public KycDataDto CreateClientKycEvent(ClientDto dto);

        bool DeleteKycEvent(ClientDto dto);

        bool DoesClientHaveKYC(ClientDto dto);

        public List<KycDataDto> GetAllKycEvents();

        public KycDataDto GetClientKycEvent(ClientDto dto);
        public KycDataDto UpdateClientKycEvent(ClientDto dto);

        #endregion Public Methods
    }

    public class KYCDataRepo : RepoBase<KYCDataModel>, IKYCDataRepo
    {
        #region Private Fields

        private readonly IConfiguration _config;

        private readonly AlumaDBContext _context;
        private readonly IWebHostEnvironment _host;
        private readonly IMapper _mapper;

        #endregion Private Fields

        #region Public Constructors

        public KYCDataRepo(AlumaDBContext context, IWebHostEnvironment host, IConfiguration config, IMapper mapper) : base(context)
        {
            _context = context;
            _host = host;
            _config = config;
            _mapper = mapper;
        }

        #endregion Public Constructors

        #region Public Methods

        public KycDataDto CreateClientKycEvent(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteKycEvent(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }

        public bool DoesClientHaveKYC(ClientDto dto)
        {
            throw new System.NotImplementedException();
        }

        public List<KycDataDto> GetAllKycEvents()
        {
            List<KYCDataModel> kycs = _context.KycData.Include(c => c.Client).ToList();
            return _mapper.Map<List<KycDataDto>>(kycs);
        }
        public KycDataDto GetClientKycEvent(ClientDto dto)
        {
            KYCDataModel kyc = _context.KycData.Where(k => k.ClientId == dto.Id).First();
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

        #endregion Public Methods
    }
}