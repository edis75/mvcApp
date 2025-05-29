using Microsoft.EntityFrameworkCore;
using mvcApp.Models;

namespace mvcApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<User> Users { get; set; }

        
    }
}
