using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using SecondMap.Services.SMS.API.MappingProfiles;
using SecondMap.Services.SMS.API.Middleware;
using SecondMap.Services.SMS.BLL.Extensions;
using SecondMap.Services.SMS.BLL.MappingProfiles;
using SecondMap.Services.SMS.DAL.Extensions;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;


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

			builder.Services.AddAuthentication(config =>
				{
					config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					config.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
					config.DefaultAuthenticateScheme = "Cookies";
				})
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, config =>
				{
					config.SignInScheme = "Cookies";
					config.Authority = "https://localhost:5001";
					config.ClientId = "test-client";
					config.ClientSecret = "test-secret";
					config.SaveTokens = true;
					config.ResponseType = "code";

					config.GetClaimsFromUserInfoEndpoint = true;
				});

			builder.Services.AddAuthorization();
			IdentityModelEventSource.ShowPII = true;

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
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseMiddleware<LoggingMiddleware>();

			app.MapControllers();

			app.UseCors(policy =>
			{
				policy.AllowAnyOrigin();
				policy.AllowAnyHeader();
				policy.AllowAnyMethod();
			});

			app.Run();
		}
	}
}