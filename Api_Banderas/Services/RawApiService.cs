using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using RawGames.Model;
using System.Net.WebSockets;

namespace RawGames.Services
{
    public class CountryApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://restcountries.com/v3.1/all";

        private const string ApiKey = "bfcce58028994581bd74f00182f33433";

        private readonly JsonSerializerOptions _jsonOptions;

        public CountryApiService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            _httpClient.DefaultRequestHeaders.Add("API_Key", ApiKey);
        }

        public async Task<List<Country>?> GetAllCountriesAsync()
        {
            try
            {
                string url = $"{BaseUrl}/all";

                var json = await _httpClient.GetStringAsync(url);

                return JsonSerializer.Deserialize<List<Country>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Country>?> SearchCountryAsync(string nombre)
        {
            try
            {
                string url = $"{BaseUrl}/name/{nombre}";

                var json = await _httpClient.GetStringAsync(url);

                return JsonSerializer.Deserialize<List<Country>>(json,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }
    }
}