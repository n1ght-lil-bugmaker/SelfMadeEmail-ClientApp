using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ClientApp.Model;
using Newtonsoft.Json.Linq;

namespace ClientApp
{
    public static class Api
    {
        private static string _url = "https://localhost:44360/api/";

        private static string ApiRequest(string apiController, string requestMethod, string addData = "")
        {
            var request = (HttpWebRequest)WebRequest.Create(_url + apiController);
            request.Method = requestMethod;
            request.ServerCertificateValidationCallback = (sender, sertificate, chain, sslErrors) => true;
            request.ContentType = "application/json";
            request.ContentLength = addData.Length;

            if (addData != "")
            {
                using(var s = request.GetRequestStream())
                {
                    using(var writer = new StreamWriter(s))
                    {
                        writer.Write(addData);
                    }
                }
            }
            var response = request.GetResponse();
            string res;

            using (var responseStream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    res = reader.ReadToEnd();
                }
                response.Close();
            }
            return res;
        }

        public static void AddEmail(Email email)
        {
            var serialized = JsonConvert.SerializeObject(email);

            ApiRequest("email/", "POST", addData: serialized);
        }

        public static void DeleteEmail(Email email)
        {
            ApiRequest($"email/{email.Id}", "DELETE");
        }

        public static bool CheckUserData(string username, string password)
        {
            var loginData = new LoginData()
            {
                Login = username,
                Password = password
            };
            string data = JsonConvert.SerializeObject(loginData);

            var res = ApiRequest($"users/validate", "POST", addData: data);
            return bool.Parse(res);
        }

        public static User GetUser(string username)
        {
            var serialized = ApiRequest($"users/{username}", "GET");

            return JsonConvert.DeserializeObject<User>(serialized);
        }

        public static IEnumerable<Email>GetReceivedEmails(string username)
        {
            var response = ApiRequest($"email/received/{username}", "GET");
            return JsonConvert.DeserializeObject<Email[]>(response);
        }

        public static IEnumerable<Email>GetSendedEmails(string username)
        {
            var response = ApiRequest($"email/sended/{username}", "GET");
            return JsonConvert.DeserializeObject<Email[]>(response);
        }

        public static void AddUser(User toAdd)
        {
            var serialized = JsonConvert.SerializeObject(toAdd);
            ApiRequest("users/", "POST", addData: serialized);
        }
    }
}
