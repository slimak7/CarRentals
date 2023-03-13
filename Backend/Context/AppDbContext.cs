using Backend.DBLogic.DBModels;
using Microsoft.EntityFrameworkCore;


namespace Backend.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CarModel> CarsModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
