using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WeatherApp.Models.DTOs;

namespace WeatherApp.Services.Clients
{
    public class OpenWeatherApiClient : IWeatherApiClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _apiKey;

        public OpenWeatherApiClient(IHttpClientFactory clientFactory, IConfiguration configuration) 
        { 
            _clientFactory = clientFactory;
            _apiKey = configuration["OpenWeather:ApiKey"] ?? throw new InvalidOperationException("OpenWeather API key is missing.");
        }

        public async Task<WeatherResponseDto> GetWeatherAsync(double latitude, double longitude)
        {
            var endpoint = $"weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";

            var client = _clientFactory.CreateClient("OpenWeatherClient");
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var rawJson = await response.Content.ReadAsStringAsync();

            var contentStream = await response.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<WeatherResponseDto>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}
