namespace WeatherApp.Models.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameAscii{ get; set; }
        public string Country { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime? LastSearched { get; set; }
    }
}
