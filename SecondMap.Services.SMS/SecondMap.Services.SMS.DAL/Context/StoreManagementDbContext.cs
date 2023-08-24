using Microsoft.EntityFrameworkCore;
using SecondMap.Services.SMS.DAL.Entities;

namespace SecondMap.Services.SMS.DAL.Context
{
	public class StoreManagementDbContext : DbContext
	{
		public StoreManagementDbContext(DbContextOptions<StoreManagementDbContext> options) : base(options)
		{
			if (Database.IsRelational())
			{
				Database.Migrate();
			}
		}

		public DbSet<StoreEntity> Stores { get; set; }

		public DbSet<ScheduleEntity> Schedules { get; set; }

		public DbSet<UserEntity> Users { get; set; }

		public DbSet<ReviewEntity> Reviews { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreManagementDbContext).Assembly);
		}
	}
}