using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models.DTOs;

namespace WeatherApp.Services.Shared
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Value);

            return MapErrorToActionResult(result.Error);
        }

        public static IActionResult ToActionResult(this Result result)
        {
            if (result.IsSuccess)
                return new OkResult();

            return MapErrorToActionResult(result.Error);
        }

        private static IActionResult MapErrorToActionResult(Error? error)
        {
            if (error == null)
                return new BadRequestResult();

            return new ObjectResult(new ErrorDto{ Error = error.CodeName, Message = error.Message })
            {
                StatusCode = error.StatusCode
            };
        }
    }
}
