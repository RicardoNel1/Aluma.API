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

        public IDVResponseDto StartIDVerification(ClientDto dto)
        {
            //var client = new RestClient($"{_settings.BaseUrl}/api/PBSAID/realtime");
            var client = new RestClient($"{_settings.BaseUrl}");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authenticate", _settings.User);
            //request.AddHeader("Accept", "application/json");
            //request.AddHeader("Authorization", $"Basic ${_settings.Authorization}");
            //request.AddHeader("Content-Type", "multipart/form-data");
            //request.AlwaysMultipartFormData = true;
            request.AddParameter("idNumber", dto.User.RSAIdNumber);
            request.AddParameter("yourReference", dto.User.LastName);
            //request.AddParameter("memberkey", _settings.Memberkey);
            //request.AddParameter("password", _settings.Password);
            //request.AddParameter("bvs_details[accountNumber]", dto.AccountNumber);
            //request.AddParameter("bvs_details[accountType]", dto.AccountType);
            //request.AddParameter("bvs_details[branchCode]", dto.BranchCode);
            //request.AddParameter("bvs_details[idNumber]", dto.IdNumber);
            //request.AddParameter("bvs_details[initial]", dto.Initials);
            //request.AddParameter("bvs_details[lastname]", dto.Surname);
            //request.AddParameter("bvs_details[yourReference]", dto.Reference);
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
                throw new HttpRequestException("Error while trying to start ID Verification");

            IDVResponseDto responseData = JsonConvert.DeserializeObject<IDVResponseDto>(response.Content);

            return responseData;
        }
    }
}
