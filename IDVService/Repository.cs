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
        public IDVServiceRepo()
        {
            var config = new ConfigurationBuilder();
            // Get current directory will return the root dir of Base app as that is the running application
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            //_settings = root.GetSection("PbVerifyBankValidation").Get<SettingsDto>();
        }

    }
}
