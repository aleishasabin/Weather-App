using WeatherApp.Models.DTOs;
using WeatherApp.Services.Shared;

namespace WeatherApp.Services
{
    public interface ICityService
    {
        Task<Result<List<CityDto>>> SearchCitiesAsync(string prefix);
        Task<Result<List<CityDto>>> GetRecentCitiesAsync(int count);
    }
}
