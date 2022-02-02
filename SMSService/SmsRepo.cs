using DataService.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace SmsService
{
    public interface ISmsRepo
    {
        string CreateOtp();

        bool SendOtp(string mobile, string message);
    }

    public class SmsRepo : ISmsRepo
    {
        public readonly SmsSettings _settings;

        public SmsRepo()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _settings = root.GetSection("SMSPortalSettings").Get<SmsSettings>();
        }

        public SmsSettings settings { get => _settings; }

        private static readonly Random random = new Random();

        public string CreateOtp()
        {
            var chars = "0123456789";

            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public bool SendOtp(string mobile, string message)
        {
            // get last 9 digits from the string: mystring.Substring(mystring.Length - 4);
            var mobileNo = mobile.Substring(mobile.Length - 9);

            var client = new RestClient($"{_settings.BaseUrl}");
            client.Timeout = -1;

            var authToken = "";

            var authRequest = new RestRequest("Authentication", Method.GET);

            authRequest.AddHeader("Authorization", $"Basic {_settings.ApiToken}");

            var authResponse = client.Execute(authRequest);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                var authResponseBody = JObject.Parse(authResponse.Content);
                authToken = authResponseBody["token"].ToString();
            }
            else
            {
                Console.WriteLine(authResponse.ErrorMessage);
                return false;
            }

            var sendRequest = new RestRequest("bulkmessages", Method.POST);

            var authHeader = $"Bearer {authToken}";
            sendRequest.AddHeader("Authorization", $"{authHeader}");

            sendRequest.AddJsonBody(new
            {
                Messages = new[]
                {
                        new
                        {
                            content = message,
                            destination = mobileNo
                        }
                }
            });

            var sendResponse = client.Execute(sendRequest);
            return sendResponse.StatusCode == HttpStatusCode.OK ? true : false;
        }

        
    }
}