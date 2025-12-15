namespace eCommerce.API.Middlewares;

public class ExceptionHandlingMiddlewarre
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddlewarre> _logger;

    public ExceptionHandlingMiddlewarre(RequestDelegate next, ILogger<ExceptionHandlingMiddlewarre> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
           await  _next(httpContext);
        }
        catch(Exception ex)
        {
            _logger.LogError($"{ex.GetType().ToString()}: {ex.Message}");
            if(ex.InnerException is not null)
            {
                _logger.LogError($"Inner Exception - {ex.InnerException.GetType().ToString()}: {ex.InnerException.Message}");
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new { Type = ex.GetType().ToString(), Message = ex.Message });
        }
    }
}

public static class ExceptionHandlingMiddlewarreExtensions
{
    public static IApplicationBuilder UseExceptionHandlingMiddlewarre(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddlewarre>();
    }
}
