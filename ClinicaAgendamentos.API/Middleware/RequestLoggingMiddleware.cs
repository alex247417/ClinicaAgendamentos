namespace ClinicaAgendamentos.API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request: {Method} {Path} - {Time}",
            context.Request.Method,
            context.Request.Path,
            DateTime.UtcNow);
        try
        {
            await _next(context);

            if (context.Response.StatusCode >= 400)
            {
                _logger.LogWarning("Response: {StatusCode} em {Method} {Path}",
                    context.Response.StatusCode,
                    context.Request.Method,
                    context.Request.Path);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado em {Method} {Path}",
                context.Request.Method,
                context.Request.Path);
            throw;
        }
    }
}