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
        public readonly SmsPortalSettings _smsPortalSettings;
        public readonly SmsConnectMobileSettings _smsConnectMobileSettings;

        public SmsRepo()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _smsPortalSettings = root.GetSection("SMSPortalSettings").Get<SmsPortalSettings>();
            _smsConnectMobileSettings = root.GetSection("SMSConnectMobileSettings").Get<SmsConnectMobileSettings>();


        }

        public SmsPortalSettings smsPortalSettings { get => _smsPortalSettings; }
        public SmsConnectMobileSettings settings { get => _smsConnectMobileSettings; }


        private static readonly Random random = new();

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

            //bool sendResponse = SendSMSPortalMessage(message,mobile);
            bool sendResponse = SendSMSConnectMobileMessage(message, mobile);
            return sendResponse;
        }

        private bool SendSMSPortalMessage(string msg, string mobileNo)
        {
            var client = new RestClient($"{_smsPortalSettings.BaseUrl}");
            client.Timeout = -1;

            var authToken = "";

            var authRequest = new RestRequest("", Method.POST);

            authRequest.AddHeader("Authorization", $"Basic {_smsPortalSettings.ApiToken}");

            var authResponse = client.Execute(authRequest);
            if (authResponse.StatusCode == HttpStatusCode.OK)
            {
                var authResponseBody = JObject.Parse(authResponse.Content);
                authToken = authResponseBody["token"].ToString();

                var sendRequest = new RestRequest("bulkmessages", Method.POST);

                var authHeader = $"Bearer {authToken}";
                sendRequest.AddHeader("Authorization", $"{authHeader}");

                sendRequest.AddJsonBody(new
                {
                    Messages = new[]
                    {
                        new
                        {
                            content = msg,
                            destination = mobileNo
                        }
                }
                });

                var response = client.Execute(sendRequest);
                return response.StatusCode == HttpStatusCode.OK ? true : false;
            }
            else
            {
                Console.WriteLine(authResponse.ErrorMessage);
                return false;
            }

        }

        private bool SendSMSConnectMobileMessage(string msg, string mobileNo)
        {
            if (mobileNo.StartsWith("0"))
                mobileNo = mobileNo[1..];
            else if (mobileNo.StartsWith("27"))
                mobileNo = mobileNo[2..];


            var client = new RestClient($"{_smsConnectMobileSettings.BaseUrl}");
            client.Timeout = -1;
            var sendRequest = new RestRequest("", Method.POST);

            sendRequest.AddQueryParameter("username", _smsConnectMobileSettings.UserName);
            sendRequest.AddQueryParameter("password", _smsConnectMobileSettings.Password);
            sendRequest.AddQueryParameter("account", _smsConnectMobileSettings.Account);
            sendRequest.AddQueryParameter("ud", msg);
            sendRequest.AddQueryParameter("da", "27" + mobileNo);
           

            var response = client.Execute(sendRequest);
            return response.StatusCode == HttpStatusCode.Accepted ? true : false;


        }


    }
}