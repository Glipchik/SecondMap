using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SecondMap.Services.SMS.DAL.Context;
using SecondMap.Services.SMS.IntegrationTests.Constants;

namespace SecondMap.Services.SMS.IntegrationTests.Utilities
{
	public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
	{
		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var dbContextDescriptor = services.SingleOrDefault(d =>
					d.ServiceType == typeof(DbContextOptions<StoreManagementDbContext>));

				if (dbContextDescriptor != null)
					services.Remove(dbContextDescriptor);

				services.AddDbContext<StoreManagementDbContext>(options =>
					options.UseInMemoryDatabase(TestConstants.IN_MEMORY_DB_NAME));

				var sp = services.BuildServiceProvider();

				using (var scope = sp.CreateScope())
				{
					var scopedServices = scope.ServiceProvider;
					var db = scopedServices.GetRequiredService<StoreManagementDbContext>();

					db.Database.EnsureCreated(); // Call EnsureCreated() here

					// You can add more setup steps here if needed
				}
			});
		}
	}
}