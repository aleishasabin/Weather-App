namespace WeatherApp.Services.Shared
{
    public class Error
    {
        public int StatusCode { get; }
        public string CodeName { get; }
        public string Message { get; }

        private Error(int statusCode, string codeName, string message)
        {
            StatusCode = statusCode;
            CodeName = codeName;
            Message = message;
        }

        private static Error Create(int statusCode, string message, [System.Runtime.CompilerServices.CallerMemberName] string codeName = "")
            => new Error(statusCode, codeName, message);

        public static Error CityNotFound => Create(404, "No city found.");
        public static Error WeatherRetrievalFailed => Create(503, "Unable to retrieve weather data at the moment. Please try again later.");
        public static Error MaxResultsExceeded(int max) => Create(400, $"Cannot return more than {max} result{(max != 1 ? "s" : "")}.");
        public static Error MinResultsNotMet(int min) => Create(400, $"Cannot return less than {min} result{(min != 1 ? "s" : "")}.");
        public static Error InsufficientLength(int min) => Create(400, $"{min} or more characters required.");
    }
}
