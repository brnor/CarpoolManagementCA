using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class CarpoolDbContext : DbContext, ICarpoolDbContext
    {
        public CarpoolDbContext(DbContextOptions<CarpoolDbContext> options)
            : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rideshare> Rideshares { get; set; }
        public DbSet<EmployeeRideshare> EmployeeRideshares { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RideshareConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeRideshareConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
