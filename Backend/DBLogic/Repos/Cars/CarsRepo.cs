using Backend.Context;
using Backend.DBLogic.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBLogic.Repos.Cars
{
    public class CarsRepo : ICarsRepo
    {
        AppDbContext _appDbContext;

        public CarsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Car> Add(Car element)
        {
            var newCar = await _appDbContext.Cars.AddAsync(element);
            await _appDbContext.SaveChangesAsync();

            return newCar.Entity;
        }

        public async Task<Car> Delete(Guid id)
        {
            var car = await GetById(id);
            var deleted = _appDbContext.Cars.Remove(car);
            await _appDbContext.SaveChangesAsync();

            return deleted.Entity;
        }

        public async Task<Car> Delete(Car element)
        {
            var deleted = _appDbContext.Cars.Remove(element);
            await _appDbContext.SaveChangesAsync();
            return deleted.Entity;
        }

        public async Task<List<Car>> GetAll()
        {
            return await _appDbContext.Cars.ToListAsync();
        }

        public async Task<List<Car>> GetAllByCondition(Func<Car, bool> condition)
        {
            var cars = await GetAll();

            return cars.FindAll(x => condition(x));
        }

        public async Task<Car> GetByCondition(Func<Car, bool> condition)
        {
            var cars = await GetAll();

            return cars.Find(x => condition(x));
        }

        public async Task<Car> GetById(Guid id)
        {
            return await _appDbContext.Cars.FindAsync(id);
        }

        public async Task<Car> Update(Car element)
        {
            var updated = _appDbContext.Cars.Update(element);
            await _appDbContext.SaveChangesAsync();

            return updated.Entity;
        }
    }
}
