using System.Diagnostics;

namespace EmployeeManagement.Api.Middleware;

/// <summary>
/// Custom Middleware for logging details about incoming HTTP requests and outgoing HTTP responses.
/// </summary>
public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        _logger.LogInformation("HTTP Request Incoming: {Method} {Path}{QueryString}", 
            request.Method, request.Path, request.QueryString);

        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var response = context.Response;

            _logger.LogInformation("HTTP Response Outgoing: {StatusCode} for {Method} {Path} in {ElapsedMs}ms",
                response.StatusCode, request.Method, request.Path, stopwatch.ElapsedMilliseconds);
        }
    }
}
