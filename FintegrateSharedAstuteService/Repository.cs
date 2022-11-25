using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.Advisor;
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
        SubmitCCPResponseDto SubmitClientCCPRequest(ClientDto dto);

        ClientCCPResponseDto GetClientCCP(int clientId);
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
        }

        public ClientCCPResponseDto GetClientCCP(int clientId)
        {
            var client = new RestClient($"{_settings.BaseUrl}api/CCP/retrieveCCP");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;
            //request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("systemRef", clientId.ToString());
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to retrieve client CCP");

            ClientCCPResponseDto responseData = JsonConvert.DeserializeObject<ClientCCPResponseDto>(response.Content);

            return responseData;
        }

        public SubmitCCPResponseDto SubmitClientCCPRequest(ClientDto dto) //****
        {

            AdvisorAstuteModel astuteCredentials = _context.Advisors.Include(c => c.AdvisorAstute).Where(a => a.UserId == advisorCredentials.UserId).First().AdvisorAstute;
            SubmitCCPRequestDto requestDto = new();


            requestDto.Client.FirstName = dto.User.FirstName;
            requestDto.Client.LastName = dto.User.LastName;
            requestDto.Client.IdNumber = dto.User.RSAIdNumber;
            requestDto.Client.Email = dto.User.Email;
            requestDto.Client.IdType = "RSAId";
            requestDto.Client.MobileNumber = dto.User.MobileNumber;
            requestDto.Client.DateOfBirth = DateTime.ParseExact(dto.User.DateOfBirth, "yyyy-mm-dd", CultureInfo.InvariantCulture);
            requestDto.OurReference = dto.Id.ToString();
            requestDto.AstuteCredentials = _mapper.Map<AdvisorCredentials>(astuteCredentials);
            //List<ClientConsentProvidersModel> clientConsentedList = _context.ClientConsentModels.Where(c => c.ClientId == dto.Id).ToList();
            List<int> providerList = _context.ClientConsentModels.Include(c => c.ConsentedProviders).Where(c => c.ClientId == dto.Id).OrderByDescending(c => c.Id).First().ConsentedProviders.Select(c => c.Id).ToList();

            List<string> consentedProvidersList = _context.FinancialProviders.Where(f => providerList.Contains(f.Id)).Select(f => f.Name).ToList(); //change name to code
            requestDto.Client.ConsentedProviders = consentedProvidersList.ToArray();
            //var list = _context.ClientConsentModels.Include(c => c.ConsentedProviders).Join(_context.FinancialProviders, f => f.ConsentedProviders == )

            var client = new RestClient($"{_settings.BaseUrl}api/CCP/submitCCP");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;
            //request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to submitting client for new CCP");

            SubmitCCPResponseDto responseData = JsonConvert.DeserializeObject<SubmitCCPResponseDto>(response.Content);

            return responseData;
        }
    }
}