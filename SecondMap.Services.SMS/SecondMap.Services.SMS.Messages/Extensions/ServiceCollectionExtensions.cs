using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace SecondMap.Services.SMS.Messages.Extensions
{
	public static class ServiceCollectionExtensions
	{
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
