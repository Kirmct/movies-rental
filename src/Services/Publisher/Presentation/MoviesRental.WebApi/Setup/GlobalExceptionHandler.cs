using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using MoviesRental.Core.DomainObjects;

namespace MoviesRental.WebApi.Setup;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string erroMessage) = exception switch 
        {
            ArgumentNullException argumentNullException => (500, argumentNullException.Message),
            DomainException domainException => (500, domainException.Message),
            SqlException sqlException => (500, sqlException.Message),
            ValidationException validationException => (500, validationException.Message),
            _ => (500, "Something Went Wrong")
        };

        _logger.LogError(exception, exception.Message);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(erroMessage, cancellationToken);
        return true;
    }
}
