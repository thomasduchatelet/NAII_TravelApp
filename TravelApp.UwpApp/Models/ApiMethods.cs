﻿using Newtonsoft.Json;
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

namespace TravelApp.UwpApp.Models
{
    public static class ApiMethods
    {
        private static readonly string baseUrl = "https://localhost:44372/api";
        private static HttpClient client = new HttpClient();

        public static async Task<T> ApiCall<T>(string uri, BaseFilterDto filter = null)
        {
            var uriBuilder = new UriBuilder(baseUrl + uri);
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
            return await ApiCall<List<TripDto>>("/Trip/GetAll", filter);

        }

        public static async Task<List<ItemDto>> GetItemsEager(ItemTodoFilterDto filter = null)
        {
            return await ApiCall<List<ItemDto>>("/Item/GetAllEager", filter);
        }

        public static async Task<List<ItemDto>> GetToDosEager(ItemTodoFilterDto filter = null)
        {
            return await ApiCall<List<ItemDto>>("/ToDo/GetAllEager", filter);
        }

        public static async Task<List<CategoryDto>> GetCategories(CategoryFilterDto filter = null)
        {
            return await ApiCall<List<CategoryDto>>("/Category/GetAll", filter);
        }
    }
}
