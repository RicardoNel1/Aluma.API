using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net.Http;

namespace Aluma.API.Repositories
{
    public interface IAVSRepo
    {
        BankValidationResponseDto StartBankValidation(BankDetailsDto dto);

        VerificationStatusResponse GetBankValidationStatus(string jobId);
    }

    public class AVSRepo : IAVSRepo
    {
        public readonly SettingsDto _settings;

        public AVSRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("ClientVerificationService").Get<SettingsDto>();
        }

        public SettingsDto settings { get => _settings; }

        public BankValidationResponseDto StartBankValidation(BankDetailsDto dto)
        {
            string tokenResponse = Authenticate(_settings.Memberkey, _settings.Password);

            var client = new RestClient($"{_settings.BaseUrl}/api/AVS");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Basic ${tokenResponse}");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start Bank Account Validation");

            BankValidationResponseDto responseData = JsonConvert.DeserializeObject<BankValidationResponseDto>(response.Content);

            return responseData;
        }

        public VerificationStatusResponse GetBankValidationStatus(string jobId)
        {
            string tokenResponse = Authenticate(_settings.Memberkey, _settings.Password);

            var client = new RestClient($"{_settings.BaseUrl}/api/AVS");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Basic {tokenResponse}");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("memberkey", _settings.Memberkey.ToString());
            request.AddParameter("password", _settings.Password.ToString());
            request.AddParameter("jobID", jobId.ToString());
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start Bank Account Validation Status");

            return JsonConvert.DeserializeObject<VerificationStatusResponse>(response.Content);
        }

        public string Authenticate(string _Username, string _Password)
        {
            var client = new RestClient($"{_settings.BaseUrl}/api/Authentication");

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

            return responseData.Token;

        }
    }
}
