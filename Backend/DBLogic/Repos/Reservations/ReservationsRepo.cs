using Backend.Context;
using Backend.DBLogic.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBLogic.Repos.Reservations
{
    public class ReservationsRepo : IReservationsRepo
    {
        AppDbContext  _appDbContext;

        public ReservationsRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Reservation> Add(Reservation element)
        {
            var newReservation = await _appDbContext.Reservations.AddAsync(element);
            await _appDbContext.SaveChangesAsync();

            return newReservation.Entity;
        }

        public async Task<Reservation> Delete(Guid id)
        {
            var element = await GetById(id);

            var deleted = _appDbContext.Remove(element);

            await _appDbContext.SaveChangesAsync();
            return deleted.Entity;
        }

        public async Task<Reservation> Delete(Reservation element)
        {
            var deleted = _appDbContext.Reservations.Remove(element);
            await _appDbContext.SaveChangesAsync();

            return deleted.Entity;

        }

        public async Task<List<Reservation>> GetAll()
        {
            return await _appDbContext.Reservations.ToListAsync();
        }

        public async Task<List<Reservation>> GetAllByCondition(Func<Reservation, bool> condition)
        {
            var reservations = await GetAll();

            return reservations.FindAll(x => condition(x));
        }

        public async Task<Reservation> GetByCondition(Func<Reservation, bool> condition)
        {
            var reservations = await GetAll();

            return reservations.Find(x => condition(x));
        }

        public async Task<Reservation> GetById(Guid id)
        {
            return await _appDbContext.Reservations.FindAsync(id);
        }

        public async Task<Reservation> Update(Reservation element)
        {
            var updated = _appDbContext.Reservations.Update(element);
            await _appDbContext.SaveChangesAsync();

            return updated.Entity;
        }
    }
}
