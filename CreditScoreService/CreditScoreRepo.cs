using System;

namespace CreditScoreService
{
    public class CreditScoreRepo
    {
        public object DoExperianCheck(string zaid)
        {
            try
            {
                //UAT creds :
                //string username = "77827-1";
                //string password = "devtest";

                //////Live creds
                //string username = "31544-4";
                //string password = "ZV)ENk(6";

                //string username = "31544-1";
                //string password = "-UkrP6/&";

                string username = "31544-api";
                string password = "u{AK!8UC";

                if (!zaid.All(char.IsDigit))
                    return false;

                if (zaid.Trim().Length != 13)
                    return false;

                string json = "{\"auth\": {\"username\" : \"" + username + "\",\"password\" : \"" + password + "\"},\"system_settings\" : {\"version\" : \"1.0\",\"origin\": \"SH\"},\"search_criteria\": { \"identity_number\": \"" + zaid + "\", \"identity_type\": \"SID\" } }";

                string strResponseValue = string.Empty;

                HttpWebRequest request = null;

                try
                {
                    //UAT url
                    //request = (HttpWebRequest)WebRequest.Create("https://webservices-uat.compuscan.co.za:9443/PersonCheckScoreV2/RequestCheckScore");

                    //Live url
                    request = (HttpWebRequest)WebRequest.Create("https://webservices.compuscan.co.za:9443/PersonCheckScoreV2/RequestCheckScore");
                }
                catch (Exception requestException)
                {
                    return "Failed - Reason : " + requestException.Message;
                }

                request.Method = "POST";

                if (request.Method == "POST" && json != string.Empty)
                {
                    request.ContentType = "application/json";
                    using StreamWriter swJSONPayload = new(request.GetRequestStream());
                    swJSONPayload.Write(json);
                    swJSONPayload.Close();
                }

                HttpWebResponse response = null;

                response = (HttpWebResponse)request.GetResponse();

                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using StreamReader reader = new(responseStream);
                        strResponseValue = reader.ReadToEnd();
                    }
                }

                var jsonData = JObject.Parse(strResponseValue);
                if (jsonData["response_status"].ToString() == "Success")
                {
                }

                return GetResult(strResponseValue);
            }
            catch (WebException ex)
            {
                string strResponseValue = "";

                var response = (HttpWebResponse)ex.Response;
                using (Stream responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using StreamReader reader = new(responseStream);
                        strResponseValue = reader.ReadToEndAsync().Result;
                    }
                }

                ExperianResponse experianResponse = JsonConvert.DeserializeObject<ExperianResponse>(strResponseValue);

                return experianResponse.error_description;
            }
        }
    }
}
