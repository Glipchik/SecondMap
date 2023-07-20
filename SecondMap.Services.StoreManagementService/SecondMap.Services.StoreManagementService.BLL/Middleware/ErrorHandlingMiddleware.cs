﻿using Microsoft.AspNetCore.Http;
using System.Net;
using SecondMap.Services.StoreManagementService.BLL.Exceptions;

namespace SecondMap.Services.StoreManagementService.BLL.Middleware
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

				case ValidationFailException:
					httpStatusCode = HttpStatusCode.UnprocessableEntity;
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