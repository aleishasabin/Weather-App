using AutoMapper;
using Microsoft.Extensions.Logging;
using WeatherApp.Data.Repositories;
using WeatherApp.Models.DTOs;
using WeatherApp.Services.Clients;
using WeatherApp.Services.Shared;

namespace WeatherApp.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IWeatherApiClient _weatherApiClient;
        private readonly ILogger<IWeatherService> _logger;

        public WeatherService(IMapper mapper, ICityRepository cityRepository, IWeatherApiClient weatherApiClient, ILogger<IWeatherService> logger)
        { 
            _cityRepository = cityRepository;
            _mapper = mapper;
            _weatherApiClient = weatherApiClient;
            _logger = logger;
        }

        public async Task<Result<WeatherDto>> GetWeatherForCityAsync(int cityId)
        {
            var city = await _cityRepository.GetByIdAsync(cityId);

            if (city == null)
                return Error.CityNotFound;

            city.LastSearched = DateTime.UtcNow;

            await _cityRepository.SaveChangesAsync();

            try 
            {
                var weatherResponse = await _weatherApiClient.GetWeatherAsync(city.Latitude, city.Longitude);

                var weather = _mapper.Map<WeatherDto>(weatherResponse);

                weather.CityName = city.Name;
                weather.Country = city.Country;

                return weather;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to fetch weather for {city.Name}, {city.Country}.");
                return Error.WeatherRetrievalFailed;
            }
        }
    }
}
