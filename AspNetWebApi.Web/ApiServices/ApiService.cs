using AspNetWebApi.Web.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AspNetWebApi.Web.ApiServices
{
    public class ApiService
    {
        private readonly HttpClient _client;

        public ApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> dtos;
            var response = await _client.GetAsync("categories");
            if (response.IsSuccessStatusCode)
            {
                dtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                dtos = null;
            }
            return dtos;
        }
        public async Task<CategoryDto> AddAsync(CategoryDto dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto),Encoding.UTF8,"application/json");
            var response = await _client.PostAsync("categories",stringContent);
            if (response.IsSuccessStatusCode)
            {
                dto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
                return dto;
            }
            else
            {
                return null;
            }
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> Update(CategoryDto dto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("categories", stringContent);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Remove(int id)
        {
            var response = await _client.DeleteAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
