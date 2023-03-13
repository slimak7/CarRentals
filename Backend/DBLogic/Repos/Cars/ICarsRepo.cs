using Backend.DBLogic.DBModels;

namespace Backend.DBLogic.Repos.Cars
{
    public interface ICarsRepo : IRepo<Car>
    {
        public Task<List<Car>> GetAllByCondition(Func<Car, bool> condition);
    }
}
