using Microsoft.EntityFrameworkCore;
using WeatherApp.Models.Entities;

namespace WeatherApp.Data
{
    public class WeatherAppContext : DbContext
    {
        public WeatherAppContext(DbContextOptions<WeatherAppContext> options) : base(options) 
        { 
        }

        public DbSet<City> Cities { get; set; }
    }
}
