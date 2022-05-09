using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net.Http;

namespace BankValidationService
{
    public interface IBankValidationServiceRepo
    {
        #region Public Methods

        VerificationStatusResponse GetBankValidationStatus(string jobId);

        BankValidationResponseDto StartBankValidation(BankDetailsDto dto);

        #endregion Public Methods
    }

    public class BankValidationServiceRepo : IBankValidationServiceRepo
    {
        #region Public Fields

        public readonly SettingsDto _settings;

        #endregion Public Fields

        #region Public Constructors

        public BankValidationServiceRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("PbVerifyBankValidation").Get<SettingsDto>();
        }

        #endregion Public Constructors

        #region Public Properties

        public SettingsDto settings { get => _settings; }

        #endregion Public Properties

        #region Public Methods

        public VerificationStatusResponse GetBankValidationStatus(string jobId)
        {
            var client = new RestClient($"{_settings.BaseUrl}pbverify-bank-account-verification-job-status-v3");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Basic {_settings.Authorization}");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("memberkey", _settings.Memberkey.ToString());
            request.AddParameter("password", _settings.Password.ToString());
            request.AddParameter("jobID", jobId.ToString());
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start Bank Account Validation Status");

            return JsonConvert.DeserializeObject<VerificationStatusResponse>(response.Content);
        }

        public BankValidationResponseDto StartBankValidation(BankDetailsDto dto)
        {
            var client = new RestClient($"{_settings.BaseUrl}pbverify-bank-account-verification-v3");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Authorization", $"Basic ${_settings.Authorization}");
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AlwaysMultipartFormData = true;
            request.AddParameter("memberkey", _settings.Memberkey);
            request.AddParameter("password", _settings.Password);
            request.AddParameter("bvs_details[accountNumber]", dto.AccountNumber);
            request.AddParameter("bvs_details[accountType]", dto.AccountType);
            request.AddParameter("bvs_details[branchCode]", dto.BranchCode);
            request.AddParameter("bvs_details[idNumber]", dto.IdNumber);
            request.AddParameter("bvs_details[initial]", dto.Initials);
            request.AddParameter("bvs_details[lastname]", dto.Surname);
            request.AddParameter("bvs_details[yourReference]", dto.Reference);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start Bank Account Validation");

            BankValidationResponseDto responseData = JsonConvert.DeserializeObject<BankValidationResponseDto>(response.Content);

            return responseData;
        }

        #endregion Public Methods
    }
}