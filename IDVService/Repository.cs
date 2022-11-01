using DataService.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Runtime;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;

namespace IDVService

{

    public interface IIDVServiceRepo
    {
        string StartAuthentication();

        IDVRealTimeResponseDto StartIDVerification(ClientDto dto, string token);

        //BankValidationResponseDto StartBankValidation(BankDetailsDto dto);

        //VerificationStatusResponse GetBankValidationStatus(string jobId);
    }
    public class IDVServiceRepo : IIDVServiceRepo
    {
        private IDVSettingsDto _settings;

        public IDVServiceRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("PbVerifyIDV").Get<IDVSettingsDto>();
        }

        public IDVSettingsDto settings { get => _settings; }

        public string StartAuthentication()
        {
            var client = new RestClient($"{_settings.BaseUrl}/api/Authentication");         


            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            AuthenticationDto _authDto = new AuthenticationDto() {
            UserName = _settings.UserName,
            Password = _settings.Password,
            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(_authDto), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start PB Authentication");

            AuthResponseObject responseData = JsonConvert.DeserializeObject<AuthResponseObject>(response.Content);

            return responseData.Token;
        }

        public IDVRealTimeResponseDto StartIDVerification(ClientDto dto, string token)
        {
            var client = new RestClient($"{_settings.BaseUrl}/api/PBSAIDV/realtime");


            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic ${token}");

            IDVRequestDto _requestDto = new()
            {
                IdNumber = dto.User.RSAIdNumber,
                YourReference = "ALM" + dto.User.LastName

            };

            request.AddParameter("application/json", JsonConvert.SerializeObject(_requestDto), ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start ID Verification");

            IDVRealTimeResponseDto responseData = JsonConvert.DeserializeObject<IDVRealTimeResponseDto>(response.Content);



            
            return responseData;
        }
    }
}
