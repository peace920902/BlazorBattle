using BlazorBattle.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.data
{
    public class DataContext : DbContext
    {
        
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<UserUnit> UserUnits { get; set; }
    }
}