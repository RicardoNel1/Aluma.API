using AutoMapper;
using DataService.Context;
using DataService.Dto;
using DataService.Dto.ClientVerification;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;

namespace ClientVerificationService
{
    public interface IClientVerificationServiceRepo
    {
        AMLScreeningSubmitResponseDto SubmitAMLScreening(AMLScreeningSubmitRequestDto request);

        AMLScreeningResultResponseDto ResultAMLScreening(AMLScreeningResultRequestDto request);
    }
    public class ClientVerificationServiceRepo : IClientVerificationServiceRepo
    {
        public readonly ClientVerificationServiceDto _settings;
        private readonly AlumaDBContext _context;
        private readonly IMapper _mapper;

        public ClientVerificationServiceRepo(AlumaDBContext databaseContext, IMapper mapper)
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("ClientVerificationService").Get<ClientVerificationServiceDto>();
            _context = databaseContext;
            _mapper = mapper;
        }

        public AMLScreeningSubmitResponseDto SubmitAMLScreening(AMLScreeningSubmitRequestDto requestDto)
        {
            string tokenResponse = Authenticate(_settings.Memberkey, _settings.Password);

            var client = new RestClient($"{_settings.BaseUrl}api/PBSAAMLScreening/submit");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AlwaysMultipartFormData = true;
            request.AddHeader("Authorization", $"Basic {tokenResponse}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to submit AML screening");

            AMLScreeningSubmitResponseDto responseData = JsonConvert.DeserializeObject<AMLScreeningSubmitResponseDto>(response.Content);

            return responseData;
        }

        public AMLScreeningResultResponseDto ResultAMLScreening(AMLScreeningResultRequestDto requestDto)
        {
            string tokenResponse = Authenticate(_settings.Memberkey, _settings.Password);

            var client = new RestClient($"{_settings.BaseUrl}api/PBSAAMLScreening/result");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");

            request.AddHeader("Authorization", $"Basic {tokenResponse}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestDto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error occurred while retrieving AML result");

            AMLScreeningResultResponseDto responseData = JsonConvert.DeserializeObject<AMLScreeningResultResponseDto>(response.Content);

            return responseData;
        }

        public string Authenticate(string _Username, string _Password)
        {
            var client = new RestClient($"{_settings.BaseUrl}api/Authentication");

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            AuthenticationDto _authDto = new AuthenticationDto()
            {
                UserName = _Username,
                Password = _Password,
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(_authDto), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException("Error while trying to start Client Verification Authentication");
                return "Error";
            }


            AuthResponseObject responseData = JsonConvert.DeserializeObject<AuthResponseObject>(response.Content);

            //if (accessToken == "Connection attempt failed")
            //{
            //    response.Source = "";
            //    response.Details = "CIF Server Unavailable";
            //    response.StatusCode = "503";
            //}

            return responseData.Token;

        }
    }
}
