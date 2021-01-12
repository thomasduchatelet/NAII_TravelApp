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

        public static async Task<T> PutObject<T>(String uri, T body)
        {
            T result;
            try
            {
                var uriBuilder = new UriBuilder(baseUrl + uri);
                var response = await client.PutAsync(uriBuilder.Uri, JsonHelpers.ObjectToHttpContent(body));
                result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public static async Task<T> PostObject<T>(String uri, T body)
        {
            T result;
            try
            {
                var uriBuilder = new UriBuilder(baseUrl + uri);
                HttpStringContent temp = JsonHelpers.ObjectToHttpContent(body);
                var response = await client.PostAsync(uriBuilder.Uri, JsonHelpers.ObjectToHttpContent(body));
                result = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        

        public static Task<ItemDto> UpdateItem(ItemDto item)
        {
            item.Category = null;
            
            return PutObject("/Item/Update", item);
        }

        public static async Task<ItineraryDto> GetItinerary(long tripId)
        {
            var itineraries = await ApiCall<IEnumerable<ItineraryDto>>(baseUrl + "/Itinerary/GetAllEager", new ItineraryFilter() { TripId = tripId });
            return itineraries.FirstOrDefault();
        }

        public static async Task<IEnumerable<LocationDto>> ChangeLocationPosition(int position, LocationDto dto)
        {
            IEnumerable<LocationDto> result;
            dto.Order = position;
            try
            {
                var uriBuilder = new UriBuilder(baseUrl + "/Itinerary/LocationChangeOrder");
                var response = await client.PostAsync(uriBuilder.Uri, JsonHelpers.ObjectToHttpContent(dto));
                result = JsonConvert.DeserializeObject<IEnumerable<LocationDto>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                throw;
            }
            return result;
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

        public static void LogOut()
        {
            client.DefaultRequestHeaders.Remove("Bearer"); //TODO tijdelijke logout X D 
        }

        public static async Task<ItemDto> AddItem(long tripId, string name, int count, long categoryId)
        {
                ItemDto item = new ItemDto
                {
                    TripId = tripId,
                    Name = name,
                    Count = count,
                    CategoryId = categoryId
                };
            return await PutObject("/Item/Create", item);
        }

        public static async Task<TripDto> AddTrip(string title, DateTime startDate, DateTime endDate)
        {
            TripDto trip = new TripDto
            {
                Title = title,
                StartDate=startDate,
                EndDate = endDate
            };
            return await PutObject("/Trip/Create", trip);
        }

        public static async Task<CategoryDto> AddCategory(string name)
        {
            CategoryDto category = new CategoryDto
            {
                Name = name
            };
            return await PutObject<CategoryDto>("/Category/Create", category);

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
            return await ApiCall<List<TripDto>>(baseUrl + "/Trip/GetAll", filter);

        }

        public static async Task<List<ItemDto>> GetItemsEager(ItemTodoFilterDto filter = null)
        {
            return await ApiCall<List<ItemDto>>($"{baseUrl}/Item/GetAllEager", filter);
        }

        public static async Task<List<CountryDto>> GetAllCountries()
        {
           return await ApiCall<List<CountryDto>>("https://api.covid19api.com/countries");
        }

        public static async Task<List<ItemDto>> GetToDosEager(ItemTodoFilterDto filter = null)
        {
            return await ApiCall<List<ItemDto>>(baseUrl + "/ToDo/GetAllEager", filter);
        }

        public static async Task<List<CategoryDto>> GetCategories(CategoryFilterDto filter = null)
        {
            return await ApiCall<List<CategoryDto>>(baseUrl + "/Category/GetAll", filter);
        }

        public static async Task<CategoryDto> DeleteCategory(CategoryDto category)
        {
            return await PostObject("/Category/Delete", category);
        }

        public static async Task<TripDto> DeleteTrip(TripDto trip)
        {
            return await PostObject("/Trip/Delete", trip);
        }

        public static async Task<ItemDto> DeleteItem(ItemDto item)
        {
            return await PostObject("/Item/Delete", item);
        }

        public static async Task<TodoDto> DeleteToDo(TodoDto toDo)
        {
            return await PostObject("/ToDo/Delete", toDo);
        }

        public static async Task<UnsplashResult> GetImageUrl(string keyword)
        {
            var uriBuilder = new UriBuilder("https://api.unsplash.com/search/photos/?client_id=dMXHbxgeklD_WMu1ANAR6r3549ln6W8iNuzQp4Ms_rs&query=" + keyword);
            try
            {
                UnsplashResult result;
                HttpClient unsplashClient = new HttpClient();
                HttpResponseMessage httpResponse = await unsplashClient.GetAsync(uriBuilder.Uri);
                httpResponse.EnsureSuccessStatusCode();
                result = JsonConvert.DeserializeObject<UnsplashResult>(httpResponse.Content.ToString());
                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
