using WeatherApp.Models.Entities;

namespace WeatherApp.Data.Repositories
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IEnumerable<City>> SearchCitiesAsync(string prefix);
        Task<IEnumerable<City>> GetRecentCitiesAsync(int count);
    }
}
