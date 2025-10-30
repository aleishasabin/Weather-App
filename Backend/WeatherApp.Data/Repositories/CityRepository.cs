using Microsoft.EntityFrameworkCore;
using WeatherApp.Models.Entities;

namespace WeatherApp.Data.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(WeatherAppContext context) : base(context) { }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string prefix)
        {
            return await FindAsync(c => c.NameAscii.ToLower().StartsWith(prefix.ToLower()));
        }

        public async Task<IEnumerable<City>> GetRecentCitiesAsync(int count)
        {
            return await _dbSet.Where(c => c.LastSearched != null)
                .OrderByDescending(c => c.LastSearched)
                .Take(count)
                .ToListAsync();
        }
    }
}
