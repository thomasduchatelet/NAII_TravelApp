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
using TravelApp.Shared.Dto;
using System.Net.Http.Headers;
using System.Web;
using TravelApp.Shared.Dto.FilterDto;

namespace TravelApp.UwpApp.Models
{
    public static class ApiMethods
    {
        private static readonly string baseUrl = "https://localhost:44372/api";
        private static HttpClient client = new HttpClient();

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

        public static async Task<Boolean> AuthenticateUser(string email, string password)
        {
            string token;
            try
            {
                Uri uri = new Uri($"{baseUrl}/Account");

                LoginDto Body = new LoginDto
                    {
                        Email = email,
                        Password = password
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

        public static async Task<Boolean> RegisterUser(string username, string email, string password)
        {
            try
            {
                Uri uri = new Uri($"{baseUrl}/Account/Register");

                RegisterDto body = new RegisterDto
                {
                    Username = username,
                    Email = email,
                    Password = password
                };

                var response = await client.PostAsync(uri, JsonHelpers.ObjectToHttpContent(body));
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }
        public static async Task<List<TripDto>> GetTrips(TripFilterDto filter = null)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/Trip/GetAll");
            if(filter != null) uriBuilder.Query = filter.ParseQuery();
            try
            {
                List<TripDto> trips = new List<TripDto>();

                HttpResponseMessage httpResponse = await client.GetAsync(uriBuilder.Uri);
                httpResponse.EnsureSuccessStatusCode();
                trips = JsonConvert.DeserializeObject<List<TripDto>>(httpResponse.Content.ToString());
                return trips;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<ItemDto>> GetItems(ItemTodoFilterDto filter = null)
        {
            var uriBuilder = new UriBuilder($"{baseUrl}/Item/GetAll");
            if (filter != null) uriBuilder.Query = filter.ParseQuery();
            try
            {
                List<ItemDto> items = new List<ItemDto>();

                HttpResponseMessage httpResponse = await client.GetAsync(uriBuilder.Uri);
                httpResponse.EnsureSuccessStatusCode();
                items = JsonConvert.DeserializeObject<List<ItemDto>>(httpResponse.Content.ToString());
                return items;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
