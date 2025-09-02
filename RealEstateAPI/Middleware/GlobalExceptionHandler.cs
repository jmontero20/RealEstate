using FluentValidation;
using RealEstate.SharedKernel.Result;
using System.Net;
using System.Text.Json;

namespace RealEstateAPI.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ValidationException validationEx => CreateValidationExceptionResponse(validationEx),
                ArgumentException argEx => CreateArgumentExceptionResponse(argEx),
                KeyNotFoundException notFoundEx => CreateNotFoundResponse(notFoundEx),
                UnauthorizedAccessException unauthorizedEx => CreateUnauthorizedResponse(unauthorizedEx),
                InvalidOperationException invalidOpEx => CreateInvalidOperationResponse(invalidOpEx),
                TimeoutException timeoutEx => CreateTimeoutResponse(timeoutEx),
                _ => CreateGenericExceptionResponse(exception)
            };

            context.Response.StatusCode = response.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(response.Body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }

        private static ExceptionResponse CreateValidationExceptionResponse(ValidationException ex)
        {
            var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
            var Result = Result<object>.Failure(errors, "Validation failed");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = Result
            };
        }

        private static ExceptionResponse CreateArgumentExceptionResponse(ArgumentException ex)
        {
            var Result = Result<object>.Failure(
                new List<string> { ex.ParamName ?? "Invalid argument" }, ex.Message);

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = Result
            };
        }

        private static ExceptionResponse CreateNotFoundResponse(KeyNotFoundException ex)
        {
            var Result = Result<object>.Failure(
              
                new List<string> { ex.Message }, "Resource not found");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Body = Result
            };
        }

        private static ExceptionResponse CreateUnauthorizedResponse(UnauthorizedAccessException ex)
        {
            var Result = Result<object>.Failure(
                new List<string> { ex.Message }, "Unauthorized access");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Body = Result
            };
        }

        private static ExceptionResponse CreateInvalidOperationResponse(InvalidOperationException ex)
        {
            var Result = Result<object>.Failure(
                new List<string> { ex.Message },
                  "Invalid operation");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = Result
            };
        }

        private static ExceptionResponse CreateTimeoutResponse(TimeoutException ex)
        {
            var Result = Result<object>.Failure(
                
                new List<string> { ex.Message }, "Request timeout");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.RequestTimeout,
                Body = Result
            };
        }

        private static ExceptionResponse CreateGenericExceptionResponse(Exception ex)
        {
            var Result = Result<object>.Failure(
               
                new List<string> { "Please try again later or contact support if the problem persists" }, "An error occurred while processing your request");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = Result
            };
        }

        private class ExceptionResponse
        {
            public int StatusCode { get; set; }
            public Result<object> Body { get; set; } = null!;
        }
    }
}
