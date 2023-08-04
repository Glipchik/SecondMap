using FluentValidation;
using FluentValidation.AspNetCore;
using SecondMap.Services.SMS.API.MappingProfiles;
using SecondMap.Services.SMS.API.Middleware;
using SecondMap.Services.SMS.BLL.Extensions;
using SecondMap.Services.SMS.BLL.MappingProfiles;
using SecondMap.Services.SMS.DAL.Extensions;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SecondMap.Services.SMS.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers()
				.AddJsonOptions(options => 
					options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
				.AddNewtonsoftJson(options =>
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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