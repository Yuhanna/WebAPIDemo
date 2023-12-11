using Microsoft.EntityFrameworkCore;
using WebAPIDemo.Models;

namespace WebAPIDemo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //data seeding
            modelBuilder.Entity<Shirt>().HasData(
            new Shirt { ShirtId = 1, Brand = "My Brand", Color = "Blue", Gender = "Man", Price = 30, Size = 10 },
            new Shirt { ShirtId = 2, Brand = "My Brand1", Color = "Blue1", Gender = "Man1", Price = 130, Size = 110 },
            new Shirt { ShirtId = 3, Brand = "My Brand2", Color = "Blue2", Gender = "Man2", Price = 230, Size = 210 },
            new Shirt { ShirtId = 4, Brand = "My Brand3", Color = "Blue3", Gender = "Man3", Price = 330, Size = 310 },
                );
        }
    }
}
