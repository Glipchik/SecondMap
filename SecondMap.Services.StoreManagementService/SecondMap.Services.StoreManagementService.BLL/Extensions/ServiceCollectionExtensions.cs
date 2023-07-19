using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.StoreManagementService.BLL.Interfaces;
using SecondMap.Services.StoreManagementService.BLL.Services;

namespace SecondMap.Services.StoreManagementService.BLL.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddScoped<IReviewService, ReviewService>();
			services.AddScoped<IScheduleService, ScheduleService>();
			services.AddScoped<IStoreService, StoreService>();
			services.AddScoped<IUserService, UserService>();
		}
	}
}
