using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MediaReview.Review.Api.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Instance = httpContext.Request.Path
        };

        if (exception is FluentValidation.ValidationException fluentException)
        {
            problemDetails.Title = "one or more validation errors occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var validationErrors = fluentException.Errors.Select(error => error.ErrorMessage).ToList();
            problemDetails.Extensions.Add("errors", validationErrors);
            logger.LogError("Validation errors occurred: {ValidationErrors}", string.Join(", ", validationErrors));
        }
        else if (exception is UnauthorizedAccessException)
        {
            problemDetails.Title = "Access denied.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            logger.LogWarning("Access denied: {Path}", httpContext.Request.Path);
        }

        else if (exception is KeyNotFoundException)
        {
            problemDetails.Title = "The requested resource was not found.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            logger.LogWarning("Resource not found: {Path}", httpContext.Request.Path);
        }

        else if (exception is InvalidOperationException)
        {
            problemDetails.Title = "Invalid operation.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            logger.LogError("Invalid operation: {Message}", exception.Message);
        }

        else if (exception is TaskCanceledException || exception is TimeoutException)
        {
            problemDetails.Title = "Request timed out.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.7";
            httpContext.Response.StatusCode = StatusCodes.Status408RequestTimeout;
            logger.LogWarning("Request timed out: {Path}", httpContext.Request.Path);
        }

        else if (exception is Microsoft.EntityFrameworkCore.DbUpdateException dbException)
        {
            problemDetails.Title = "A database error occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            logger.LogError("Database error: {Message}", dbException.InnerException?.Message ?? dbException.Message);
        }

        else
        {
            problemDetails.Title = "An unexpected error occurred.";
            problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            logger.LogError("Unexpected error: {Message}", exception.Message);
        }

        logger.LogError("{ProblemDetailsTitle}", problemDetails.Title);

        problemDetails.Status = httpContext.Response.StatusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);
        return true;
    }
}