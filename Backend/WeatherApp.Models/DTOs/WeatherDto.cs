namespace WeatherApp.Models.DTOs
{
    public class WeatherDto
    {
        public string CityName { get; set; }
        public string Country { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public WindMetrics Wind { get; set; }
    }

    public class WindMetrics { 
        public double Speed { get; set; }
        public int Direction { get; set; }
    }
}
