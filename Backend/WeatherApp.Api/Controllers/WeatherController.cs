using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using WeatherApp.Models.DTOs;
using WeatherApp.Services;
using WeatherApp.Services.Shared;

namespace WeatherApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService) 
        { 
            _weatherService = weatherService;
        }

        /// <summary>
        /// Get the current weather for a city.
        /// </summary>
        /// <param name="cityId">The ID of the city.</param>
        /// <returns>Weather information for the city.</returns>
        [HttpGet("{cityId}")]
        [ProducesResponseType(typeof(WeatherDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWeather(int cityId)
        {
            var result = await _weatherService.GetWeatherForCityAsync(cityId);
            return result.ToActionResult();
        }
    }
}
