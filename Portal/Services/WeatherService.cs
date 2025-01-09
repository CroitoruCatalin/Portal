using Microsoft.Extensions.Options;
using Portal.Models;

namespace Portal.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public WeatherService(HttpClient httpClient, string apiKey)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
        }
        public async Task<WeatherResponse> GetCurrentWeatherAsync(double latitude, double longitude)
        {
            string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";


            return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
        }
    }
}
