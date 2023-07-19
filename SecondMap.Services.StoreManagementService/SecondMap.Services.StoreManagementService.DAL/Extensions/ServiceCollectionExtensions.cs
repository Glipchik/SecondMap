using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.DAL.Interfaces;
using SecondMap.Services.StoreManagementService.DAL.Models;
using SecondMap.Services.StoreManagementService.DAL.Repositories;

namespace SecondMap.Services.StoreManagementService.DAL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDataAccessLayerServiceCollection(this IServiceCollection services)
		{
			services.AddScoped<IStoreRepository, StoreRepository>();
			services.AddScoped<IGenericRepository<Store>>(provider => provider.GetService<IStoreRepository>()!);

			services.AddScoped<IReviewRepository, ReviewRepository>();
			services.AddScoped<IGenericRepository<Review>>(provider => provider.GetService<IReviewRepository>()!);

			services.AddScoped<IScheduleRepository, ScheduleRepository>();
			services.AddScoped<IGenericRepository<Schedule>>(provider => provider.GetService<IScheduleRepository>()!);

			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IGenericRepository<User>>(provider => provider.GetService<IUserRepository>()!);

			return services;
		}
	}
}
