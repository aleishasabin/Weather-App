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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService) 
        { 
            _cityService = cityService;
        }

        /// <summary>
        /// Search cities that start with a given prefix.
        /// </summary>
        /// <param name="prefix">The starting characters of the city name (case-insensitive).</param>
        /// <returns>A list of matching cities.</returns>
        [HttpGet("Search")]
        [ProducesResponseType(typeof(List<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchCities([FromQuery] string prefix)
        {
            var result = await _cityService.SearchCitiesAsync(prefix);
            return result.ToActionResult();
        }

        /// <summary>
        /// Get recently viewed cities, ordered by last searched date descending.
        /// </summary>
        /// <param name="count">Number of cities to return.</param>
        /// <returns>A list of recently viewed cities.</returns>
        [HttpGet("Recent")]
        [ProducesResponseType(typeof(List<CityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecentCities([FromQuery] int count = 5)
        {
            var result = await _cityService.GetRecentCitiesAsync(count);
            return result.ToActionResult();
        }
    }
}
