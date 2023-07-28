using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.SMS.DAL.Constants;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.DAL.Interfaces;
using SecondMap.Services.SMS.DAL.Repositories;

namespace SecondMap.Services.SMS.DAL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IStoreRepository, StoreRepository>();

			services.AddScoped<IReviewRepository, ReviewRepository>();

			services.AddScoped<IScheduleRepository, ScheduleRepository>();

			services.AddScoped<IUserRepository, UserRepository>();
		}

		public static void AddDbConfig(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<StoreManagementDbContext>(optionsBuilder =>
			{
				optionsBuilder.UseNpgsql(
					 configuration.GetConnectionString(DbConnections.DEFAULT_CONNECTION));
				optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
		}
	}
}
