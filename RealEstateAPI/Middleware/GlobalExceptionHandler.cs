using FluentValidation;
using RealEstate.Application.Common.Models;
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
            var applicationResponse = ApplicationResponse<object>.FailureResponse(errors, "Validation failed");

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateArgumentExceptionResponse(ArgumentException ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                ex.Message,
                new List<string> { ex.ParamName ?? "Invalid argument" });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateNotFoundResponse(KeyNotFoundException ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                "Resource not found",
                new List<string> { ex.Message });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateUnauthorizedResponse(UnauthorizedAccessException ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                "Unauthorized access",
                new List<string> { ex.Message });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateInvalidOperationResponse(InvalidOperationException ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                "Invalid operation",
                new List<string> { ex.Message });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateTimeoutResponse(TimeoutException ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                "Request timeout",
                new List<string> { ex.Message });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.RequestTimeout,
                Body = applicationResponse
            };
        }

        private static ExceptionResponse CreateGenericExceptionResponse(Exception ex)
        {
            var applicationResponse = ApplicationResponse<object>.FailureResponse(
                "An error occurred while processing your request",
                new List<string> { "Please try again later or contact support if the problem persists" });

            return new ExceptionResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = applicationResponse
            };
        }

        private class ExceptionResponse
        {
            public int StatusCode { get; set; }
            public ApplicationResponse<object> Body { get; set; } = null!;
        }
    }
}
