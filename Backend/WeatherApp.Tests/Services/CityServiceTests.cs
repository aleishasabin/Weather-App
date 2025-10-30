using AutoMapper;
using FluentAssertions;
using Moq;
using WeatherApp.Data.Repositories;
using WeatherApp.Models.DTOs;
using WeatherApp.Models.Entities;
using WeatherApp.Services;
using WeatherApp.Services.Shared;

namespace WeatherApp.Tests.Services
{
    public class CityServiceTests
    {
        private readonly Mock<ICityRepository> _mockCityRepository;
        private readonly Mock<IMapper> _mockMapper;

        private readonly CityService _cityService;

        public CityServiceTests()
        {
            _mockCityRepository = new Mock<ICityRepository>();
            _mockMapper = new Mock<IMapper>();

            _cityService = new CityService(_mockMapper.Object, _mockCityRepository.Object);
        }

        [Fact]
        public async Task CityService_SearchCitiesAsync_ReturnCities()
        {
            // Arrange
            var cities = new List<City> { TestData.MontrealCity };
            var cityDtos = new List<CityDto> { TestData.MontrealCityDto };

            _mockCityRepository.Setup(r => r.SearchCitiesAsync("mon"))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos);

            // Act
            var result = await _cityService.SearchCitiesAsync("mon");
          
            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().Contain(c => c.Name.StartsWith("Mon"));
            result.Value.Should().BeEquivalentTo(new List<CityDto> { TestData.MontrealCityDto });
        }

        [Fact]
        public async Task CityService_SearchCitiesAsync_ErrorCityNotFound()
        {
            // Arrange
            var cities = new List<City>();
            var cityDtos = new List<CityDto>();

            _mockCityRepository.Setup(r => r.SearchCitiesAsync("xyz"))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos);

            // Act
            var result = await _cityService.SearchCitiesAsync("xyz");

            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.CityNotFound);
        }

        [Fact]
        public async Task CityService_SearchCitiesAsync_InsufficientLength()
        {
            // Arrange
            var cities = new List<City>();
            var cityDtos = new List<CityDto>();

            _mockCityRepository.Setup(r => r.SearchCitiesAsync("xy"))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos);

            // Act
            var result = await _cityService.SearchCitiesAsync("xy");

            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.InsufficientLength(3));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(30)]
        public async Task CityService_GetRecentCitiesAsync_ReturnCities(int count)
        {
            // Arrange
            var cities = TestData.MultipleCities.Take(count);
            var cityDtos = TestData.MultipleCityDtos.Take(count);

            _mockCityRepository.Setup(r => r.GetRecentCitiesAsync(count))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos.ToList());

            // Act
            var result = await _cityService.GetRecentCitiesAsync(count);

            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value.Should().HaveCount(c => c <= count);

        }

        [Fact]
        public async Task CityService_GetRecentCitiesAsync_ErrorMaxResultsExceeded()
        {
            // Arrange
            var cities = TestData.MultipleCities;
            var cityDtos = TestData.MultipleCityDtos;

            _mockCityRepository.Setup(r => r.GetRecentCitiesAsync(31))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos);

            // Act
            var result = await _cityService.GetRecentCitiesAsync(31);

            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.MaxResultsExceeded(30));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task CityService_GetRecentCitiesAsync_ErrorMinResultsNotMet(int count)
        {
            // Arrange
            var cities = TestData.MultipleCities;
            var cityDtos = TestData.MultipleCityDtos;

            _mockCityRepository.Setup(r => r.GetRecentCitiesAsync(count))
                .ReturnsAsync(cities);

            _mockMapper.Setup(m => m.Map<List<CityDto>>(cities))
                .Returns(cityDtos);

            // Act
            var result = await _cityService.GetRecentCitiesAsync(count);

            // Assert
            result.Should().BeOfType<Result<List<CityDto>>>();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
            result.Error.Should().BeEquivalentTo(Error.MinResultsNotMet(1));
        }
    }
}
