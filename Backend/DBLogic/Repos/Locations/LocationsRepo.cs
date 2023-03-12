using Backend.Context;
using Backend.DBLogic.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBLogic.Repos.Locations
{
    public class LocationsRepo : ILocationsRepo
    {
        AppDbContext _appDbContext;

        public LocationsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Location> Add(Location element)
        {
            var location = await _appDbContext.Locations.AddAsync(element);
            await _appDbContext.SaveChangesAsync();

            return location.Entity;
        }

        public async Task<Location> Delete(Guid id)
        {
            var entry = await GetById(id);

            var deleted = _appDbContext.Locations.Remove(entry);
            await _appDbContext.SaveChangesAsync();

            return deleted.Entity;
        }

        public async Task<Location> Delete(Location element)
        {
            var deleted = _appDbContext.Locations.Remove(element);

            await _appDbContext.SaveChangesAsync();
            return deleted.Entity;
        }

        public async Task<List<Location>> GetAll()
        {
            return await _appDbContext.Locations.ToListAsync();
        }

        public async Task<Location> GetByCondition(Func<Location, bool> condition)
        {
            var locations = await GetAll();

            return locations.Find(x => condition(x));
        }

        public async Task<Location> GetById(Guid id)
        {
            return await _appDbContext.Locations.FindAsync(id);
        }

        public async Task<Location> Update(Location element)
        {
            var location = _appDbContext.Locations.Update(element);
            await _appDbContext.SaveChangesAsync();

            return location.Entity;
        }
    }
}
