using Serilog;

namespace SecondMap.Services.SMS.API.Middleware
{
	public class LoggingMiddleware
	{
		private readonly RequestDelegate _next;

		public LoggingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			Log.Information("Received request: {Method} {Path}", context.Request.Method, context.Request.Path);

			await _next(context);

			Log.Information("Sending response: {StatusCode}", context.Response.StatusCode);
		}
	}
}
