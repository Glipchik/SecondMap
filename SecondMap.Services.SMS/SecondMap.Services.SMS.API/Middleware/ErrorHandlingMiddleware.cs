using SecondMap.Services.SMS.BLL.Exceptions;
using System.Net;

namespace SecondMap.Services.SMS.API.Middleware
{
	public class ErrorHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ErrorHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleException(context, ex);
			}
		}

		private static Task HandleException(HttpContext context, Exception exception)
		{
			HttpStatusCode httpStatusCode;

			switch (exception)
			{
				case NotFoundException:
					httpStatusCode = HttpStatusCode.NotFound;
					break;

				case AlreadyExistsException:
					httpStatusCode = HttpStatusCode.Conflict;
					break;

				default:
					httpStatusCode = HttpStatusCode.InternalServerError;
					break;
			}

			var errorMessage = exception.Message;

			context.Response.ContentType = "text/plain";
			context.Response.StatusCode = (int)httpStatusCode;

			return context.Response.WriteAsync(errorMessage);
		}
	}
}