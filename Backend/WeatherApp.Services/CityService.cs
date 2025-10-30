using AutoMapper;
using WeatherApp.Data.Repositories;
using WeatherApp.Models.DTOs;
using WeatherApp.Services.Shared;

namespace WeatherApp.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(IMapper mapper, ICityRepository cityRepository)
        { 
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CityDto>>> SearchCitiesAsync(string prefix)
        {
            if (prefix.Length < 3)
                return Error.InsufficientLength(3);

            var cities = (await _cityRepository.SearchCitiesAsync(prefix)).ToList();

            if (!cities.Any())
                return Error.CityNotFound;

            return _mapper.Map<List<CityDto>>(cities);
        }

        public async Task<Result<List<CityDto>>> GetRecentCitiesAsync(int count)
        {
            if (count > 30)
                return Error.MaxResultsExceeded(30);

            if (count < 1)
                return Error.MinResultsNotMet(1);

            var cities = (await _cityRepository.GetRecentCitiesAsync(count)).ToList();

            return _mapper.Map<List<CityDto>>(cities);
        }
    }
}
