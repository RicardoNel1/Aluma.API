using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Net.Http;

namespace KycService
{
    public interface IKycFactoryRepo
    {
        #region Public Methods

        string GetBusinessComplianceReport(FactoryDetailsDto dto);

        KycResultsDto GetBusinessKycMetaData(FactoryDetailsDto dto);

        string GetConsumerComplianceReport(FactoryDetailsDto dto);

        KycResultsDto GetConsumerKycMetaData(FactoryDetailsDto dto);

        KycInitiationBusinessResponseDto InitiateBusinessKycFactory(KycInitiationDto dto);

        KycInitiationResponseDto InitiateConsumerKycFactory(KycInitiationDto dto);

        #endregion Public Methods
    }

    public class KycFactoryRepo : IKycFactoryRepo
    {
        #region Public Fields

        public readonly KycSettingsDto _settings;

        #endregion Public Fields

        #region Public Constructors

        public KycFactoryRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("KycFactory").Get<KycSettingsDto>();
        }

        #endregion Public Constructors

        #region Public Properties

        public KycSettingsDto settings { get => _settings; }

        #endregion Public Properties

        #region Public Methods

        public string GetBusinessComplianceReport(FactoryDetailsDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}Consumer/get-compliance-report");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Couldn't retreive compliance report");

            var responseData = JsonConvert.DeserializeObject<ComplianceReportDto>(response.Content);

            return responseData.Document;
        }

        public KycResultsDto GetBusinessKycMetaData(FactoryDetailsDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}business/get-compliance-report-metadata");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                throw new HttpRequestException("Error while trying to retreive KYC Meta Data");
            //var response = "{\"idVerify\":{\"status\":\"Success\",\"realTimeResults\":{\"traceId\":18889947,\"idNumber\":\"9012245124082\",\"firstNames\":\"JOHAN\",\"surName\":\"KOSTER\",\"dob\":\"1990-12-24\",\"age\":30,\"gender\":\"Male\",\"citizenship\":\"South African\",\"deceasedStatus\":\"Alive\",\"deceasedDate\":\"\",\"cellNumber\":\"845873986\",\"email\":\"johank@statustechhub.com\",\"idType\":\"RSA_ID\"},\"error\":null,\"kycType\":null},\"facialMatchData\":null,\"addressInfo\":{\"status\":\"Success\",\"sacrrA_Response\":{\"addressHistory\":[{\"address\":\"47 CPOS BUHLE PARK GAUTENG 1428\",\"subscriberName\":\"MTN\",\"lastUpdatedDate\":\"2021-01-16\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD GLEN 0149\",\"subscriberName\":\"Liberty\",\"lastUpdatedDate\":\"2020-12-16\"},{\"address\":\"20 LAVENDER GARDENS AMBERFIELD GLEN CENTURION 0157\",\"subscriberName\":\"DISCOVERY BANK\",\"lastUpdatedDate\":\"2021-01-09\"},{\"address\":\"CPHY 1 PYGMY AMBERFILED GLEN CENTURION CENTURION 157\",\"subscriberName\":\"MTN\",\"lastUpdatedDate\":\"2020-04-07\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD GLEN ELDOGLEN 000157\",\"subscriberName\":\"Nedbank Mortage\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION 0157\",\"subscriberName\":\"FNB Credit Card\",\"lastUpdatedDate\":\"2021-01-29\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD CENTURION 0157\",\"subscriberName\":\"Absa Vehicle Finance\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD CENTURION 0157\",\"subscriberName\":\"Absa Vehicle Finance\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION CENTURION 0157\",\"subscriberName\":\"FNB Credit Card\",\"lastUpdatedDate\":\"2021-01-29\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN AMBERFIELD GLEN 0149\",\"subscriberName\":\"Edgars\",\"lastUpdatedDate\":\"2019-01-21\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN RSA 0149\",\"subscriberName\":\"Edgars\",\"lastUpdatedDate\":\"2019-01-21\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN 0149\",\"subscriberName\":\"Santam\",\"lastUpdatedDate\":\"2020-12-11\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION CENTURION 0157\",\"subscriberName\":\"Discovery Card\",\"lastUpdatedDate\":\"2019-01-19\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION 0157\",\"subscriberName\":\"Discovery Card\",\"lastUpdatedDate\":\"2018-10-12\"},{\"address\":\"20 SS LAVENDER GARDENS PYGMY STREET AMBERFIELD GAUTENG 0157\",\"subscriberName\":\"Momentum Short Term Insurance\",\"lastUpdatedDate\":\"2018-02-28\"},{\"address\":\"20 SS LAVENDER GARDENS AMBERFIELD GAUTENG 0157\",\"subscriberName\":\"Momentum Short Term Insurance\",\"lastUpdatedDate\":\"2018-02-28\"}]}},\"selectedAddress\":{\"success\":true,\"link\":\"http://kycfactory.signiflow.com/images/ghosttown.png\",\"formattedAddress\":\"1 Pygmy Street Amberfield Glen Centurion Gauteng South Africa\",\"generalisedFormat\":{\"street\":\"1 Pygmy Street\",\"suburb\":\"Amberfield Glen\",\"city\":\"Centurion\",\"province\":\"Gauteng\",\"country\":\"South Africa\",\"postalCode\":\"0149\"},\"manualInput\":true,\"uploadProof\":false,\"message\":\"Partial Match\",\"nextStep\":2,\"errorCode\":0,\"errorMessage\":null},\"imageUrl\":null,\"questionResults\":null,\"documentResults\":null}";
            MetaDataDto metaDatadto = JsonConvert.DeserializeObject<MetaDataDto>(response.Content);

            return metaDatadto.IdVerify.RealTimeResults;
        }

        public string GetConsumerComplianceReport(FactoryDetailsDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}Consumer/get-compliance-report");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Couldn't retreive compliance report");

            var responseData = JsonConvert.DeserializeObject<ComplianceReportDto>(response.Content);

            return responseData.Document;
        }

        public KycResultsDto GetConsumerKycMetaData(FactoryDetailsDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}Consumer/get-compliance-report-metadata");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                throw new HttpRequestException("Error while trying to retreive KYC Meta Data");
            //var response = "{\"idVerify\":{\"status\":\"Success\",\"realTimeResults\":{\"traceId\":18889947,\"idNumber\":\"9012245124082\",\"firstNames\":\"JOHAN\",\"surName\":\"KOSTER\",\"dob\":\"1990-12-24\",\"age\":30,\"gender\":\"Male\",\"citizenship\":\"South African\",\"deceasedStatus\":\"Alive\",\"deceasedDate\":\"\",\"cellNumber\":\"845873986\",\"email\":\"johank@statustechhub.com\",\"idType\":\"RSA_ID\"},\"error\":null,\"kycType\":null},\"facialMatchData\":null,\"addressInfo\":{\"status\":\"Success\",\"sacrrA_Response\":{\"addressHistory\":[{\"address\":\"47 CPOS BUHLE PARK GAUTENG 1428\",\"subscriberName\":\"MTN\",\"lastUpdatedDate\":\"2021-01-16\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD GLEN 0149\",\"subscriberName\":\"Liberty\",\"lastUpdatedDate\":\"2020-12-16\"},{\"address\":\"20 LAVENDER GARDENS AMBERFIELD GLEN CENTURION 0157\",\"subscriberName\":\"DISCOVERY BANK\",\"lastUpdatedDate\":\"2021-01-09\"},{\"address\":\"CPHY 1 PYGMY AMBERFILED GLEN CENTURION CENTURION 157\",\"subscriberName\":\"MTN\",\"lastUpdatedDate\":\"2020-04-07\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD GLEN ELDOGLEN 000157\",\"subscriberName\":\"Nedbank Mortage\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION 0157\",\"subscriberName\":\"FNB Credit Card\",\"lastUpdatedDate\":\"2021-01-29\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD CENTURION 0157\",\"subscriberName\":\"Absa Vehicle Finance\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"25 PYGMY ROAD AMBERFIELD CENTURION 0157\",\"subscriberName\":\"Absa Vehicle Finance\",\"lastUpdatedDate\":\"2021-01-03\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION CENTURION 0157\",\"subscriberName\":\"FNB Credit Card\",\"lastUpdatedDate\":\"2021-01-29\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN AMBERFIELD GLEN 0149\",\"subscriberName\":\"Edgars\",\"lastUpdatedDate\":\"2019-01-21\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN RSA 0149\",\"subscriberName\":\"Edgars\",\"lastUpdatedDate\":\"2019-01-21\"},{\"address\":\"20 LEVENDER GARDENS 1 PYGMY STREET AMBERFIELD GLEN 0149\",\"subscriberName\":\"Santam\",\"lastUpdatedDate\":\"2020-12-11\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION CENTURION 0157\",\"subscriberName\":\"Discovery Card\",\"lastUpdatedDate\":\"2019-01-19\"},{\"address\":\"20 LAVENDER GARDEN 1 PYGM CENTURION 0157\",\"subscriberName\":\"Discovery Card\",\"lastUpdatedDate\":\"2018-10-12\"},{\"address\":\"20 SS LAVENDER GARDENS PYGMY STREET AMBERFIELD GAUTENG 0157\",\"subscriberName\":\"Momentum Short Term Insurance\",\"lastUpdatedDate\":\"2018-02-28\"},{\"address\":\"20 SS LAVENDER GARDENS AMBERFIELD GAUTENG 0157\",\"subscriberName\":\"Momentum Short Term Insurance\",\"lastUpdatedDate\":\"2018-02-28\"}]}},\"selectedAddress\":{\"success\":true,\"link\":\"http://kycfactory.signiflow.com/images/ghosttown.png\",\"formattedAddress\":\"1 Pygmy Street Amberfield Glen Centurion Gauteng South Africa\",\"generalisedFormat\":{\"street\":\"1 Pygmy Street\",\"suburb\":\"Amberfield Glen\",\"city\":\"Centurion\",\"province\":\"Gauteng\",\"country\":\"South Africa\",\"postalCode\":\"0149\"},\"manualInput\":true,\"uploadProof\":false,\"message\":\"Partial Match\",\"nextStep\":2,\"errorCode\":0,\"errorMessage\":null},\"imageUrl\":null,\"questionResults\":null,\"documentResults\":null}";
            MetaDataDto metaDatadto = JsonConvert.DeserializeObject<MetaDataDto>(response.Content);

            return metaDatadto.IdVerify.RealTimeResults;
        }

        public KycInitiationBusinessResponseDto InitiateBusinessKycFactory(KycInitiationDto dto)
        {
            dto.BusinessId = _settings.businessId;

            var client = new RestClient($"{_settings.BaseUrl}business/build-business-identity-and-factory");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            //var response = "{\"header\":{\"transactionID\":\"421_56e35cce-d637-4985-a53c-80eca88b71c6\",\"organisation\":\"0\",\"userID\":\"0\",\"systemID\":\"0\",\"timestamp\":\"2021-02-01T03:43:55\",\"portfolioSettings\":null},\"status\":\"Success\",\"message\":null,\"factoryInfo\":[{\"factoryId\":3116,\"factoryLink\":\"https://kyc.pbverify.co.za/easisign/kyc/index?enc=cajZC8PrzhX/1z3Veq39N5z4lksP8ugFKHCZW49Ovvfh1zEILe3r4DfJS68rW+RAph/AiYvkhQC/t8qqLH5zvSdXJ1Ha/Ce60saIigwdDKoBp6r2v7l7aCPxYk8DxvAmrOEDobqmSQuu6PmT+X5djA==\",\"selfKYC\":null}],\"realTimeResults\":null,\"physicalAddresses\":null,\"nextStep\":0,\"factoryLink\":null,\"errorCode\":0,\"errorMessage\":null}";
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("KycFactory could not be started");

            dynamic jsonResult = JValue.Parse(response.Content);

            //dynamic factoryInfo = jsonResult.factoryInfo;

            var res = JsonConvert.DeserializeObject<KycInitiationBusinessResponseDto>(jsonResult);

            return res;
        }

        public KycInitiationResponseDto InitiateConsumerKycFactory(KycInitiationDto dto)
        {
            dto.BusinessId = _settings.businessId;

            var client = new RestClient($"{_settings.BaseUrl}Consumer/build-consumer-identity-and-factory");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddParameter("application/json", JsonConvert.SerializeObject(dto), ParameterType.RequestBody);
            //var response = "{\"header\":{\"transactionID\":\"421_56e35cce-d637-4985-a53c-80eca88b71c6\",\"organisation\":\"0\",\"userID\":\"0\",\"systemID\":\"0\",\"timestamp\":\"2021-02-01T03:43:55\",\"portfolioSettings\":null},\"status\":\"Success\",\"message\":null,\"factoryInfo\":[{\"factoryId\":3116,\"factoryLink\":\"https://kyc.pbverify.co.za/easisign/kyc/index?enc=cajZC8PrzhX/1z3Veq39N5z4lksP8ugFKHCZW49Ovvfh1zEILe3r4DfJS68rW+RAph/AiYvkhQC/t8qqLH5zvSdXJ1Ha/Ce60saIigwdDKoBp6r2v7l7aCPxYk8DxvAmrOEDobqmSQuu6PmT+X5djA==\",\"selfKYC\":null}],\"realTimeResults\":null,\"physicalAddresses\":null,\"nextStep\":0,\"factoryLink\":null,\"errorCode\":0,\"errorMessage\":null}";
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("KycFactory could not be started");

            dynamic jsonResult = JValue.Parse(response.Content);

            dynamic factoryInfo = jsonResult.factoryInfo;

            var factoryId = factoryInfo == null ? 0 : Int32.Parse(factoryInfo[0].factoryId.ToString());
            var message = jsonResult.message.ToString();
            var status = jsonResult.status.ToString();
            var nextStep = jsonResult.nextStep == null ? 0 : Int32.Parse(jsonResult.nextStep.ToString());

            var res = new KycInitiationResponseDto()
            {
                FactoryId = factoryId,
                Message = message,
                Status = status,
                NextStep = nextStep
            };

            return res;
        }

        #endregion Public Methods
    }
}