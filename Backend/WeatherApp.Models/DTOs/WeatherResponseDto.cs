namespace WeatherApp.Models.DTOs
{
    public class WeatherResponseDto
    {
        public MainInfo Main { get; set; }
        public WeatherDescription[] Weather { get; set; }
        public WindInfo Wind { get; set; }
    }

    public class MainInfo { 
        public double Temp { get; set; }
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class WindInfo { 
        public double Speed { get; set; }
        public int Deg {  get; set; }
    }
}
