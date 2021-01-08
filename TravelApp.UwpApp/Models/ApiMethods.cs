using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.Web.Http;

namespace TravelApp.UwpApp.Models
{
    class ApiMethods
    {
        private static readonly string baseUrl = "http://10.0.0.25:45456/";
        private static HttpClient client;

        public static async Task<T> ApiCall<T>(string uri, HttpClient client) where T : new()
        {
            Uri requestUri = new Uri(uri);
            T localObject = default(T);
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
                httpResponse = await client.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                localObject = JsonConvert.DeserializeObject<T>(httpResponseBody);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                throw new Exception(httpResponseBody);
            }
            return localObject;
        }

        public static async Task<String> AuthenticateUser(string username, string password)
        {
            client = new HttpClient();

            try
            {
                 var Body = new
                    {
                        email = username,
                        password = password
                    };

                var jsonBody = JsonConvert.SerializeObject(Body);

                HttpStringContent content = new HttpStringContent(jsonBody, encoding: Windows.Storage.Streams.UnicodeEncoding.Utf8, mediaType: "application/json");

                Uri uri = new Uri($"{baseUrl}"); //url aanvullen
                var response = await client.PostAsync(uri, content);
            }
            catch (Exception)
            {
                throw;
            }
            return "Token";

        }
    }
}
