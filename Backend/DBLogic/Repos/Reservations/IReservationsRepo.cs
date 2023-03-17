using Backend.DBLogic.DBModels;

namespace Backend.DBLogic.Repos.Reservations
{
    public interface IReservationsRepo : IRepo<Reservation>
    {
        public Task<List<Reservation>> GetAllByCondition(Func<Reservation, bool> condition);
    }
}
