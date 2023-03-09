namespace Backend.DBLogic.Repos
{
    public interface IRepo<T> where T : class
    {
        public Task<List<T>> GetAll();
        public Task<T> GetById(Guid id);
        public Task<T> Add(T element);
        public Task<T> Update(T element);
        public Task<T> Delete(Guid id);
        public Task<T> Delete(T element);

        public Task<T> GetByCondition(Func<T, bool> condition);
    }
}
