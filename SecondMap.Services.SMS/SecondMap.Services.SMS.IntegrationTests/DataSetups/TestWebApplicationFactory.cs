namespace SecondMap.Services.SMS.IntegrationTests.DataSetups
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
                }
            });
        }
    }
}