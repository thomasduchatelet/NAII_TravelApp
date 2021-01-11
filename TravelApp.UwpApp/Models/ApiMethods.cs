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
using Windows.Web.Http.Headers;
using TravelApp.Shared;

namespace TravelApp.UwpApp.Models
{
    public static class ApiMethods
    {
        private static readonly string baseUrl = "https://travelapi.azurewebsites.net/api";
        private static HttpClient client = new HttpClient();

        public static async Task<T> ApiCall<T>(string uri, BaseFilterDto filter = null)
        {
            var uriBuilder = new UriBuilder(uri);
            if (filter != null) uriBuilder.Query = filter.ParseQuery();
            try
            {
                T result;

                HttpResponseMessage httpResponse = await client.GetAsync(uriBuilder.Uri);
                httpResponse.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<T>(httpResponse.Content.ToString());
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static async Task<List<CountryCovidResult>> GetCountryCovidData(CountryDto countryDto)
        {
            return await ApiCall<List<CountryCovidResult>>("https://api.covid19api.com/total/dayone/country/" + countryDto.Slug);
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
                client.DefaultRequestHeaders.Authorization = new HttpCredentialsHeaderValue("Bearer", token);
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

        public static async Task<List<ItemDto>> GetItemsEager(ItemTodoFilterDto filter = null)
        {
            return await ApiCall<List<ItemDto>>($"{baseUrl}/Item/GetAllEager", filter);
        }

        public static async Task<List<CountryDto>> GetAllCountries()
        {
           return await ApiCall<List<CountryDto>>("https://api.covid19api.com/countries");
        }
    }
}
