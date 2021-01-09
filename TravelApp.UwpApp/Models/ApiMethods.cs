using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Models;
using Windows.Storage;
using Windows.System;
using Windows.Web.Http;
using TravelApp.Models;
using TravelApp.Shared.Dto;
using System.Net.Http.Headers;
using System.Web;

namespace TravelApp.UwpApp.Models
{
    public static class ApiMethods
    {
        private static readonly string baseUrl = "https://localhost:44372/api";
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

        public static async Task<Boolean> AuthenticateUser(string username, string password)
        {
            client = new HttpClient();
            string token;
            try
            {
                Uri uri = new Uri($"{baseUrl}/Account");

                var Body = new
                    {
                        email = username,
                        password = password
                    };
               
                var response = await client.PostAsync(uri, JsonHelpers.ObjectToHttpContent(Body));
                token = await response.Content.ReadAsStringAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
            catch (Exception)
            {
                throw;
            }
            return token != null;
        }
        public static async Task<List<TripDto>> GetTrips(string titleFilter = null, DateTime? startsAfter = null, DateTime? startsBefore = null, long? id = null)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/Trip/GetAll");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if(titleFilter != null && titleFilter != "")
                query["Title"] = titleFilter;
            if (startsAfter.HasValue)
                query["Startsafter"] = startsAfter.Value.ToString();
            if (startsBefore.HasValue)
                query["StartsBefore"] = startsBefore.Value.ToString();
            if (id.HasValue)
                query["Id"] = id.Value.ToString();
            uriBuilder.Query = query.ToString();
            List<TripDto> trips = new List<TripDto>();
            try
            {
                HttpResponseMessage httpResponse = await client.GetAsync(uriBuilder.Uri);
                httpResponse.EnsureSuccessStatusCode();
                trips = JsonConvert.DeserializeObject<List<TripDto>>(httpResponse.Content.ToString());
            }
            catch (Exception ex)
            {
                
            }
            return trips;
        }
    }
}
