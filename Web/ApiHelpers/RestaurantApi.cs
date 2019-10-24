using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Web.DTO;
using Web.Models;

namespace Web.ApiHelpers
{
    public class RestaurantApi
    {
        string baseUrl = "https://localhost:44390/api/v1/";
        public async Task CreateRestaurant(CreateRestaurantViewModel data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                var httpContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                HttpResponseMessage msg = await client.PostAsync("restaurants", httpContent);
                msg.EnsureSuccessStatusCode();
            }
        }
        public async Task<List<RestaurantBasicResponseDTO>> GetAllRestaurants()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/basic-details");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<RestaurantBasicResponseDTO>>();
                return result;
            }
        }
        public async Task<List<MealCategoryResponseDTO>> GetAllMealCategories()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync("restaurants/meal-categories");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealCategoryResponseDTO>>();
                return result;
            }
        }
        public async Task<List<MealTypeResponseDTO>> GetMealTypesForRestaurant(int restaurantId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);
                HttpResponseMessage msg = await client.GetAsync($"restaurants/meal-types/{restaurantId}");
                msg.EnsureSuccessStatusCode();
                var result = await msg.Content.ReadAsAsync<List<MealTypeResponseDTO>>();
                return result;
            }
        }

        public async Task AddnewMeal(AddMealViewModel meal)
        {

        }
    }
}
