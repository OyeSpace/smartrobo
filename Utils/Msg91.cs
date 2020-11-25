using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ChampService.Utils
{
    public static class Msg91
    {
        //Old Key  
       // private const string MSG91_AUTH_KEY = "299584AbXf2pbFGju5da97f8b";
        private const string MSG91_AUTH_KEY = "299584AmGYbF96Yvz5e103c5bP1"; 
       // private const string MSG91_AUTH_KEY = "296091AhM9msCc6z5da42d87";
        // private const string MSG91_AUTH_KEY = "189845ArLTw3HnRwFs5a509fc5";
        private const string SEND_OTP_PREFIX = "http://control.msg91.com/api/sendotp.php";
        private const string VERIFY_OTP_PREFIX = "https://control.msg91.com/api/verifyRequestOTP.php";
        private const string RESEND_OTP_PREFIX = "http://control.msg91.com/api/retryotp.php";

        static HttpClient client = new HttpClient();

        public static async Task<bool> SendOTP(string mobileNumber)
        {
          //http://control.msg91.com/api/sendotp.php?otp_length=&authkey=&message=&sender=&mobile=&otp=&otp_expiry=&email=
            var url = new StringBuilder();
            url.Append(SEND_OTP_PREFIX + "?");
            url.AppendFormat("otp_length={0}&", 6);
            url.AppendFormat("authkey={0}&", MSG91_AUTH_KEY);
            //url.AppendFormat("message={0}&", "Your OTP for CHAMP Login is ");
            url.AppendFormat("sender={0}&", "SCUARX");
            url.AppendFormat("mobile={0}", mobileNumber);

            HttpResponseMessage response = await client.PostAsJsonAsync(url.ToString(), "");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic jsonDe = JsonConvert.DeserializeObject(responseBody);
            if ("error".Equals((string)jsonDe["type"]))
                return false;
            return true;
        }
        public static async Task<bool> ResendOTP(string mobileNumber)
        {
          // http://control.msg91.com/api/retryotp.php?authkey=&mobile=

           
            var url = new System.Text.StringBuilder();
           url.Append(RESEND_OTP_PREFIX + "?");
            //url.AppendFormat("otp_length={0}&", 6);
            url.AppendFormat("authkey={0}&", MSG91_AUTH_KEY);
            //url.AppendFormat("message={0}&", "Your OTP for CHAMP Login is ");
          // url.AppendFormat("sender={0}&", "OTPCoH");
            url.AppendFormat("mobile={0}&", mobileNumber);
            url.AppendFormat("retrytype=text");
            HttpResponseMessage response = await client.PostAsJsonAsync(url.ToString(), "");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic jsonDe = JsonConvert.DeserializeObject(responseBody);
            if ("error".Equals((string)jsonDe["type"]))
                return false;
            return true;
        }
        public static async Task<bool> VerifyOTP(string mobileNumber, string otpNumber)
        {
            // https://control.msg91.com/api/verifyRequestOTP.php?authkey=&mobile=&otp=
            var url = new StringBuilder();
            url.Append(VERIFY_OTP_PREFIX + "?");
            //url.AppendFormat("otp_length={0}&", 6);
            url.AppendFormat("authkey={0}&", MSG91_AUTH_KEY);
            url.AppendFormat("mobile={0}&", mobileNumber);
            url.AppendFormat("otp={0}", otpNumber);

            HttpResponseMessage response = await client.PostAsJsonAsync(url.ToString(), "");
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            dynamic jsonDe = JsonConvert.DeserializeObject(responseBody);
            if ("error".Equals((string)jsonDe["type"]))
                return false;
            return true;
        }


        public static void SendMsg(string mobileNumber, string message)
        {
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", MSG91_AUTH_KEY);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", "SCUARX");
            sbPostData.AppendFormat("&country={0}", "91"); // Hardcoded to 91 for India
            sbPostData.AppendFormat("&route={0}", "4"); // 4 means transactional

            try
            {
                //Call Send SMS API
                string sendSMSUri = "https://control.msg91.com/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                // Need to add trace msg;
            }
        }



        public static void SendRandomOTP(string mobileNumber, string message)
        {
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("authkey={0}", MSG91_AUTH_KEY);
            sbPostData.AppendFormat("&mobiles={0}", mobileNumber);
            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&sender={0}", "OyeSpc");
            sbPostData.AppendFormat("&country={0}", "91"); // Hardcoded to 91 for India
            sbPostData.AppendFormat("&route={0}", "4"); // 4 means transactional

            try
            {
                //Call Send SMS API
                string sendSMSUri = "https://control.msg91.com/api/sendhttp.php";
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string responseString = reader.ReadToEnd();

                //Close the response
                reader.Close();
                response.Close();
            }
            catch (SystemException ex)
            {
                // Need to add trace msg;
            }
        }

       // public static async Task<bool> VerifyIDs(string id_number)
       // {
       //     string authorization = "Bearer" +
       //" eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
       //"eyJ0eXBlIjoiYWNjZXNzIiwiZXhwIjoxODk1NTUwMDMy" +
       //"LCJpZGVudGl0eSI6ImRldi5veWVzcGFjZUBhYWRoYWFy" +
       //"YXBpLmlvIiwiaWF0IjoxNTgwMTkwMDMyLCJuYmYiOjE1" +
       //"ODAxOTAwMzIsImp0aSI6ImY4OTU2NWQ4LWE0MTAtNDExNi" +
       //"1hOGEwLWMzMTc0NzQ2Nzg4MCIsInVzZXJfY2xhaW1zIjp7" +
       //"InNjb3BlcyI6WyJyZWFkIl19LCJmcmVzaCI6ZmFsc2V9." +
       //"CVIkRnZbaJD11f_ahLdAciV0XEIsgFdo4sfX78I3Ios";
       //     string pan = "https://kyc-api.aadhaarapi.io/api/v1/pan/pan";
           
       //     //url.Append(pan );

       //     //url.Append(authorization);

       //     //HttpResponseMessage response = await client.PostAsJsonAsync(url.ToString(),id_number);
       //     //response.EnsureSuccessStatusCode();

       //     //string responseBody = await response.Content.ReadAsStringAsync();

       //     //dynamic jsonDe = JsonConvert.DeserializeObject(responseBody);
       //     //if ("error".Equals((string)jsonDe["type"]))
       //     //    return false;
       //     //return true;
       // }

    }
}