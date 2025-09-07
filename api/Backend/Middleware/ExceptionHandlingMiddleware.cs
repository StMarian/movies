using Backend.Models;
using System.Net;
using System.Text.Json;

namespace Backend.Middleware
{
	public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
	{
		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An unhandled exception occurred");
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			context.Response.ContentType = "application/json";

			var statusCode = GetStatusCode(exception);
			context.Response.StatusCode = (int)statusCode;

			var errorResponse = new ErrorResponse
			{
				Status = (int)statusCode,
				TraceId = context.TraceIdentifier,
			};

			var json = JsonSerializer.Serialize(errorResponse);

			await context.Response.WriteAsync(json);
		}

		private static HttpStatusCode GetStatusCode(Exception exception)
		{
			return exception switch
			{
				KeyNotFoundException => HttpStatusCode.NotFound,
				InvalidOperationException => HttpStatusCode.BadRequest,
				HttpRequestException => HttpStatusCode.BadGateway,
				_ => HttpStatusCode.InternalServerError
			};
		}
	}

	public static class ExceptionHandlingMiddlewareExtensions
	{
		public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlingMiddleware>();
		}
	}
}