using SecondMap.Services.Auth.API.Extensions;
using SecondMap.Services.Auth.Application.Services;
using SecondMap.Services.Auth.Application.Services.Abstract;
using SecondMap.Services.Auth.Infrastructure.Extensions;

namespace SecondMap.Services.Auth.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllersWithViews();

			builder.Services.AddAuthDbContext(builder.Configuration);
			builder.Services.SetupIdentity();
			builder.Services.SetupIdentityServer(builder.Configuration);
			builder.Services.SetupIdentityServerCookie();
			builder.Services.InitializeDatabase();
			builder.Services.AddValidations();

			builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
			builder.Services.AddScoped<IUserService, UserService>();

			var app = builder.Build();

			if (!app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseHsts();
			}

			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseIdentityServer();

			app.UseAuthorization();

			app.MapDefaultControllerRoute();

			app.Run();
		}
	}
}