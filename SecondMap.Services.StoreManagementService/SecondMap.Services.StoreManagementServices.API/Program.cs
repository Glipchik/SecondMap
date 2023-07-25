
using FluentValidation;
using FluentValidation.AspNetCore;
using SecondMap.Services.StoreManagementService.API.MappingProfiles;
using SecondMap.Services.StoreManagementService.BLL.Extensions;
using SecondMap.Services.StoreManagementService.BLL.MappingProfiles;
using SecondMap.Services.StoreManagementService.BLL.Middleware;
using SecondMap.Services.StoreManagementService.DAL.Extensions;
using Serilog;
using System.Reflection;

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

			builder.Services.AddDbConfig(builder.Configuration);

			builder.Services.AddServices();

			builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
				.AddFluentValidationAutoValidation();

			builder.Services.AddAutoMapper(
				typeof(ViewModelsToModelsProfile).Assembly,
				typeof(ModelToEntityProfile).Assembly
			);

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Console()
				.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
				.CreateLogger();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseMiddleware<ErrorHandlingMiddleware>();

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseMiddleware<LoggingMiddleware>();

			app.MapControllers();

			app.Run();
		}
	}
}