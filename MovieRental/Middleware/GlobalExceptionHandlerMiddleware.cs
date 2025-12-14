using System.Net;
using System.Text.Json;

namespace MovieRental.Middleware
{
	public class GlobalExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

		public GlobalExceptionHandlerMiddleware(
			RequestDelegate next,
			ILogger<GlobalExceptionHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unhandled exception occurred");
				await HandleExceptionAsync(context, ex);
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			var statusCode = HttpStatusCode.InternalServerError;
			var message = "An internal server error occurred";

			if (exception is ArgumentNullException or ArgumentException)
			{
				statusCode = HttpStatusCode.BadRequest;
				message = exception.Message;
			}

			var response = new
			{
				error = message,
				statusCode = (int)statusCode
			};

			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			return context.Response.WriteAsync(JsonSerializer.Serialize(response));
		}
	}
}
