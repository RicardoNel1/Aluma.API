using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.Advisor;
using DataService.Enum;
using DataService.Model;
using DataService.Model.Advisor;
using DataService.Model.Client;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace FintegrateSharedAstuteService
{
    public interface IFSASRepo
    {
        SubmitCCPResponseDto SubmitClientCCPRequest(ClientDto dto, AdvisorAstuteDto advisorCredentials, bool refresh);
        public ClientCCPResponseDto GetClientCCP(int clientId, AdvisorAstuteDto astuteCredentials);
        public ClientCCPResponseDto DeleteCCP(int clientId, AdvisorAstuteDto astuteCredentials);

    }
    public class FSASRepo : IFSASRepo
    {
        public readonly FSASConfigDto _settings;
        private readonly AlumaDBContext _context;
        private readonly IMapper _mapper;

        public FSASRepo(AlumaDBContext databaseContext, IMapper mapper)
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("FSAS").Get<FSASConfigDto>();
            _context = databaseContext;
            _mapper = mapper;
        }

        public ClientCCPResponseDto GetClientCCP(int clientId, AdvisorAstuteDto astuteCredentials)
        {            
            var client = new RestClient($"{_settings.BaseUrl}api/CCP/retrieveCCP");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            GetCCPRequestDto requestDto = new();
            requestDto.SystemRef = clientId.ToString();
            requestDto.AstuteCredentials = _mapper.Map<AdvisorCredentials>(astuteCredentials);
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to retrieve client CCP");

            ClientCCPResponseDto responseData = JsonConvert.DeserializeObject<ClientCCPResponseDto>(response.Content);

            return responseData;
        }

        

        public SubmitCCPResponseDto SubmitClientCCPRequest(ClientDto dto, AdvisorAstuteDto astuteCredentials, bool refresh)
        {

            SubmitCCPRequestDto requestDto = new()
            {
                Client = new RequestClientDetails(),
                AstuteCredentials = new AdvisorCredentials(),
                YourReference = "",
                Refresh = refresh,

            };

            requestDto.Client.FirstName = dto.User.FirstName;
            requestDto.Client.LastName = dto.User.LastName;
            requestDto.Client.IdNumber = dto.User.RSAIdNumber;
            requestDto.Client.Email = dto.User.Email;
            requestDto.Client.IdType = "RSAId";
            requestDto.Client.MobileNumber = dto.User.MobileNumber;
            requestDto.Client.DateOfBirth = DateTime.Parse(dto.User.DateOfBirth);//DateTime.ParseExact(dto.User.DateOfBirth, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            requestDto.YourReference = dto.Id.ToString();
            requestDto.AstuteCredentials = _mapper.Map<AdvisorCredentials>(astuteCredentials);
            List<int> providerList = _context.ClientConsentModels.Include(c => c.ConsentedProviders).Where(c => c.ClientId == dto.Id).OrderByDescending(c => c.Id).First().ConsentedProviders.Select(c => c.FinancialProviderId).ToList();

            //List<string> consentedProvidersList = _context.FinancialProviders.Where(f => providerList.Contains(f.Id)).Select(f => f.Code).ToList(); //change name to code

            List<string> consentedProvidersList = _context.FinancialProviders.Where(f => providerList.Contains(f.Id)).Select(f => f.Code).ToList();
            for (var i = 0; i < consentedProvidersList.Count; i++)      //UAT 
            {
                consentedProvidersList[i] = consentedProvidersList[i] + "L";
            }

            requestDto.Client.ConsentedProviders = consentedProvidersList.ToArray();

            var client = new RestClient($"{_settings.BaseUrl}api/CCP/submitCCP");
            if (refresh) {
                client = new RestClient($"{_settings.BaseUrl}api/CCP/refreshCCP");
            }
            
            
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
           
            //request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to submitting client for new CCP");

            SubmitCCPResponseDto responseData = JsonConvert.DeserializeObject<SubmitCCPResponseDto>(response.Content);

            return responseData;
        }










        public ClientCCPResponseDto DeleteCCP(int clientId, AdvisorAstuteDto astuteCredentials)
        {
            var client = new RestClient($"{_settings.BaseUrl}api/CCP/deleteCCP");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            GetCCPRequestDto requestDto = new();
            requestDto.SystemRef = clientId.ToString();
            requestDto.AstuteCredentials = _mapper.Map<AdvisorCredentials>(astuteCredentials);
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to retrieve client CCP");

            ClientCCPResponseDto responseData = JsonConvert.DeserializeObject<ClientCCPResponseDto>(response.Content);

            return responseData;
        }
    }
}