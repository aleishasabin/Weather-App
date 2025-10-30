using WeatherApp.Models.DTOs;
using WeatherApp.Services.Shared;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        Task<Result<WeatherDto>> GetWeatherForCityAsync(int cityId);
    }
}
