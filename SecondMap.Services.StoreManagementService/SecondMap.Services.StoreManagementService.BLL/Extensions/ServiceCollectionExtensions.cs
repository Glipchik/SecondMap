using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Services;
using SecondMap.Services.StoreManagementService.DAL.Extensions;
using System.Reflection;

namespace SecondMap.Services.StoreManagementService.BLL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddRepositories();

			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IStoreService, StoreService>();
			services.AddScoped<IUserService, UserService>();
		}
	}
}
