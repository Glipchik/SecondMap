using Microsoft.EntityFrameworkCore;
using SecondMap.Services.StoreManagementService.DAL.Models;

namespace SecondMap.Services.StoreManagementService.DAL.Context
{
	public class StoreManagementDbContext : DbContext
    {
	    public StoreManagementDbContext(DbContextOptions<StoreManagementDbContext> options) : base(options)
	    {

	    }

        public DbSet<Store> Stores { get; set; }

        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreManagementDbContext).Assembly);
        }
    }
}