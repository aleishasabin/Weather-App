using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherApp.Data.Repositories;
using WeatherApp.Models.DTOs;
using WeatherApp.Services;
using WeatherApp.Services.Clients;
using WeatherApp.Services.Shared;

namespace WeatherApp.Tests.Services
{
    public class WeatherServiceTests
    {
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IWeatherApiClient> _mockWeatherApiClient;
        private readonly Mock<ILogger<WeatherService>> _mockLogger;

        private readonly WeatherService _weatherService;

        public WeatherServiceTests()
        {
            _mockCityRepository = new Mock<ICityRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockWeatherApiClient = new Mock<IWeatherApiClient>();
            _mockLogger = new Mock<ILogger<WeatherService>>();

            _weatherService = new WeatherService(_mockMapper.Object, _mockCityRepository.Object, _mockWeatherApiClient.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task WeatherService_GetWeatherForCityAsync_ReturnWeather()
        {
            // Arrange
            var cities = TestData.PerthCity;
            var weatherDto = TestData.WeatherDto;
            var weatherResponseDto = TestData.WeatherResponseDto;

            _mockCityRepository.Setup(r => r.GetByIdAsync(cities.Id))
                .ReturnsAsync(cities);

            _mockCityRepository.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            _mockWeatherApiClient.Setup(c => c.GetWeatherAsync(cities.Latitude, cities.Longitude))
                .ReturnsAsync(weatherResponseDto);

            _mockMapper.Setup(m => m.Map<WeatherDto>(weatherResponseDto))
                .Returns(weatherDto);

            // Act
            var result = await _weatherService.GetWeatherForCityAsync(cities.Id);

            // Assert
            result.Should().BeOfType<Result<WeatherDto>>();
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().BeEquivalentTo(TestData.WeatherDto);
        }

        [Fact]
        public async Task WeatherService_GetWeatherForCityAsync_ErrorCityNotFound()
        {
            // Arrange
            var cities = TestData.PerthCity;
            var weatherDto = TestData.WeatherDto;
            var weatherResponseDto = TestData.WeatherResponseDto;

            _mockCityRepository.Setup(r => r.GetByIdAsync(5));

            // Act
            var result = await _weatherService.GetWeatherForCityAsync(5);

            // Assert
            result.Should().BeOfType<Result<WeatherDto>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.CityNotFound);
        }

        [Fact]
        public async Task WeatherService_GetWeatherForCityAsync_ErrorWeatherRetrievalFailed()
        {
            // Arrange
            var cities = TestData.PerthCity;
            var weatherDto = TestData.WeatherDto;
            var weatherResponseDto = TestData.WeatherResponseDto;

            _mockCityRepository.Setup(r => r.GetByIdAsync(cities.Id))
                .ReturnsAsync(cities);

            _mockCityRepository.Setup(r => r.SaveChangesAsync())
                .ReturnsAsync(1);

            _mockWeatherApiClient.Setup(c => c.GetWeatherAsync(cities.Latitude, cities.Longitude));

            // Act
            var result = await _weatherService.GetWeatherForCityAsync(cities.Id);

            // Assert
            result.Should().BeOfType<Result<WeatherDto>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.WeatherRetrievalFailed);
        }
    }
}
