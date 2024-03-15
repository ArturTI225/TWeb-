using System.Data.Entity;
using ArturTI225.Domain.Entities;

namespace ArturTI225.Infrastructure
{
    public class CarsShopContext : DbContext
    {
        public CarsShopContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarDetail> CarDetails { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}