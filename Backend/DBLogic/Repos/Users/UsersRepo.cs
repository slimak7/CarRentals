using Backend.Context;
using Backend.DBLogic.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Backend.DBLogic.Repos.Users
{
    public class UsersRepo : IUsersRepo
    {
        private AppDbContext _appDbContext;

        public UsersRepo(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User> Add(User element)
        {
            element.UserTypeIDFK = await GetUserType("Client");
            var entry = await _appDbContext.Users.AddAsync(element);

            _appDbContext.SaveChanges();

            return entry.Entity;
        }

        public async Task<User> Delete(Guid id)
        {
            var user = await GetById(id);

            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();

            return user;
        }

        public async Task<User> Delete(User element)
        {
            var entry = _appDbContext.Users.Remove(element);

            _appDbContext.SaveChanges();

            return entry.Entity;
        }

        public async Task<List<User>> GetAll()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<User> GetByCondition(Func<User, bool> condition)
        {
            var users = await GetAll();

            return users.Find(x => condition(x));
        }

        public async Task<User> GetById(Guid id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

        public async Task<User> Update(User element)
        {
            var entry = _appDbContext.Users.Update(element);

            return entry.Entity;
        }

        private async Task<Guid> GetUserType (string type)
        {
            var types = await _appDbContext.UserTypes.ToListAsync();

            return types.Find(x => x.TypeName == type).UserTypeID;  
        }
    }
}
