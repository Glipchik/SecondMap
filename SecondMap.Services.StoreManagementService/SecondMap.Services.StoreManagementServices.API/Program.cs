
using Microsoft.EntityFrameworkCore;
using SecondMap.Services.StoreManagementService.API.MappingProfiles;
using SecondMap.Services.StoreManagementService.BLL.Constants;
using SecondMap.Services.StoreManagementService.BLL.Extensions;
using SecondMap.Services.StoreManagementService.DAL.Context;

namespace SecondMap.Services.StoreManagementService.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<StoreManagementDbContext>(optionsBuilder =>
				optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString(DbConnections.DEFAULT_CONNECTION)
				));

			builder.Services.AddServices();

			builder.Services.AddAutoMapper(typeof(ViewModelsToModelsProfile));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}