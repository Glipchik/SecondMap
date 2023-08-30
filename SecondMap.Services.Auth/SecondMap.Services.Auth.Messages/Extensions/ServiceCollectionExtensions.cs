using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace SecondMap.Services.Auth.Messages.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static void AddRabbitMq(this IServiceCollection services)
		{
			services.AddMassTransit(mtConfig =>
			{
				mtConfig.SetKebabCaseEndpointNameFormatter();

				mtConfig.UsingRabbitMq((context, config) =>
				{
					config.Host("localhost", h =>
					{
						h.Username("guest");
						h.Password("guest");
					});
					config.ConfigureEndpoints(context);
				});
			});
		}
	}
}
