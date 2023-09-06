using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.SMS.BLL.Interfaces;
using SecondMap.Services.SMS.BLL.Services;
using SecondMap.Services.SMS.DAL.Extensions;

namespace SecondMap.Services.SMS.BLL.Extensions
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

		public static void AddRabbitMq(this IServiceCollection services)
		{
			services.AddMassTransit(mtConfig =>
			{
				mtConfig.SetKebabCaseEndpointNameFormatter();
				mtConfig.SetInMemorySagaRepositoryProvider();

				var assembly = typeof(ServiceCollectionExtensions).Assembly;

				mtConfig.AddConsumers(assembly);
				mtConfig.AddSagaStateMachines(assembly);
				mtConfig.AddSagas(assembly);
				mtConfig.AddActivities(assembly);

				mtConfig.UsingRabbitMq((context, rbConfig) =>
				{
					rbConfig.Host("localhost", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
					rbConfig.ConfigureEndpoints(context);
				});
			});
		}
	}
}
