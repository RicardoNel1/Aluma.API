using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;

namespace FintegrateSharedAstuteService
{
    public interface IFSASRepo
    {
        SubmitCCPResponseDto SubmitClientCCPRequest(SubmitCCPRequestDto request);

        ClientCCPResponseDto GetClientCCP(int clientId);
    }
    public class FSASRepo : IFSASRepo
    {
        public readonly FSASConfigDto _settings;

        public FSASRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("FSAS").Get<FSASConfigDto>();
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

        public SubmitCCPResponseDto SubmitClientCCPRequest(SubmitCCPRequestDto requestDto)
        {
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