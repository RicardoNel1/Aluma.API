using Microsoft.Extensions.Configuration;
using DataService.Dto.Services;
using DataService.Dto.Services.CreditCheck;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;

namespace FintegrateCreditCheckService
{
    public interface ICreditCheckService
    {
        CreditCheckAuthResponse DoCreditCheckServiceAuthentication(CreditCheckAuthRequest requestDetails);
        ExperianInitialAssessmentResponse GetExperianInitialAssessment(CreditCheckSearchCriteria searchCriteria);
        PBSAInitialAssessmentResponse GetPBSAInitialAssessment(CreditCheckSearchCriteria searchCriteria);
    }

    public class CreditCheckService : ICreditCheckService
    {
        public readonly CreditCheckSettingsDto _settings;
        

        public CreditCheckService()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("CreditCheckSettings").Get<CreditCheckSettingsDto>();
        }

        public CreditCheckSettingsDto Settings { get => _settings; }

        public CreditCheckAuthResponse DoCreditCheckServiceAuthentication(CreditCheckAuthRequest requestDetails)
        {
            var client = new RestClient($"{_settings.BaseUrl}/authentication")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(requestDetails);

            IRestResponse response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Error || response.ResponseStatus == ResponseStatus.None)
                throw new HttpRequestException("Error while trying to authenticate");
            if (response.ResponseStatus == ResponseStatus.TimedOut)
                throw new HttpRequestException("TimeOut error while trying to authenticate");
            if (response.ResponseStatus == ResponseStatus.Aborted)
                throw new HttpRequestException("Abort error while trying to authenticate");

            CreditCheckAuthResponse responseData = JsonConvert.DeserializeObject<CreditCheckAuthResponse>(response.Content);

            return responseData;
        }

        public ExperianInitialAssessmentResponse GetExperianInitialAssessment(CreditCheckSearchCriteria searchCriteria)
        {
            var client = new RestClient($"{_settings.BaseUrl}/experian/initial")
            {
                Timeout = -1
            };

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(searchCriteria);

            IRestResponse response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Error || response.ResponseStatus == ResponseStatus.None)
                throw new HttpRequestException("Error while trying to start initial assessment with PBSA");
            if (response.ResponseStatus == ResponseStatus.TimedOut)
                throw new HttpRequestException("TimeOut error while trying to start initial assessment with PBSA");
            if (response.ResponseStatus == ResponseStatus.Aborted)
                throw new HttpRequestException("Abort error while trying to start initial assessment with PBSA");

            ExperianInitialAssessmentResponse responseData = JsonConvert.DeserializeObject<ExperianInitialAssessmentResponse>(response.Content);

            return responseData;
        }

        public PBSAInitialAssessmentResponse GetPBSAInitialAssessment(CreditCheckSearchCriteria searchCriteria)
        {
            var client = new RestClient($"{_settings.BaseUrl}/pbsa/initial")
            {
                Timeout = -1
            };

            PBSAInitialAssessmentRequest requestBody = new();
            requestBody.ConsumerDetails.IdNumber = searchCriteria.IdNumber;
            requestBody.ConsumerDetails.Reference = searchCriteria.IdNumber;
            requestBody.ConsumerDetails.SearchReason = "Consumer Enquiry";

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddJsonBody(requestBody);

            IRestResponse response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Error || response.ResponseStatus == ResponseStatus.None)
                throw new HttpRequestException("Error while trying to start initial assessment with PBSA");
            if (response.ResponseStatus == ResponseStatus.TimedOut)
                throw new HttpRequestException("TimeOut error while trying to start initial assessment with PBSA");
            if (response.ResponseStatus == ResponseStatus.Aborted)
                throw new HttpRequestException("Abort error while trying to start initial assessment with PBSA");

            PBSAInitialAssessmentResponse responseData = JsonConvert.DeserializeObject<PBSAInitialAssessmentResponse>(response.Content);

            return responseData;
        }

        
    }
}
    