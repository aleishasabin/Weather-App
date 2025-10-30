using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;

namespace WeatherApp.Tests
{
    public static class TestData
    {
        public static City MontrealCity => new City
        {
            Id = 1,
            Name = "Montréal",
            NameAscii = "Montreal",
            Country = "Canada",
            LastSearched = new DateTime(2025, 10, 27),
            Latitude = 45.5089,
            Longitude = -73.5617
        };

        public static City PerthCity => new City
        {
            Id = 2,
            Name = "Perth",
            NameAscii = "Perth",
            Country = "Australia",
            LastSearched = new DateTime(2024, 10, 23),
            Latitude = -31.9558,
            Longitude = 115.8597
        };

        public static CityDto MontrealCityDto => new CityDto
        {
            Id = 1,
            Name = "Montréal",
            NameAscii = "Montreal",
            Country = "Canada"
        };

        public static CityDto PerthCityDto => new CityDto
        {
            Id = 2,
            Name = "Perth",
            NameAscii = "Perth",
            Country = "Australia"
        };

        public static List<City> MultipleCities => new List<City> { MontrealCity, PerthCity };
        public static List<CityDto> MultipleCityDtos => new List<CityDto> { MontrealCityDto, PerthCityDto };

        public static WeatherResponseDto WeatherResponseDto => new WeatherResponseDto
        {
            Main = new MainInfo 
            { 
                Humidity = 78,
                Temp = 14.8
            },
            Weather = new[]
            {
                new WeatherDescription 
                {
                    Main = "Clouds",
                    Description = "broken clouds"
                }
                
            },
            Wind = new WindInfo
            { 
                Deg = 230,
                Speed = 3.09
            }
        };

        public static WeatherDto WeatherDto => new WeatherDto
        {
            CityName = PerthCity.Name,
            Country = PerthCity.Country,
            Description = WeatherResponseDto.Weather[0].Description,
            Summary = WeatherResponseDto.Weather[0].Main,
            Humidity = WeatherResponseDto.Main.Humidity,
            Temperature = WeatherResponseDto.Main.Temp,
            Wind = new WindMetrics 
            { 
                Direction = WeatherResponseDto.Wind.Deg,
                Speed = WeatherResponseDto.Wind.Speed
            }
        };
    }
}
