using WeatherApp.Models.DTOs;

namespace WeatherApp.Services.Clients
{
    public interface IWeatherApiClient
    {
        Task<WeatherResponseDto> GetWeatherAsync(double latitude, double longitude);
    }
}
