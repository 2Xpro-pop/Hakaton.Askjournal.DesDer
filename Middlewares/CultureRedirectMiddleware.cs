using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Options;

namespace Destinationosh.Middlewares;

public class CultureRedirectMiddleware
{
    private static readonly string[] supportedCultures = new[] { "en", "ru", "kg" };
    private readonly RequestDelegate _next;
    private readonly SupportedCultureOptions _supportedCultureOptions;
    private readonly ILogger<CultureRedirectMiddleware> _logger;

    public CultureRedirectMiddleware(RequestDelegate next, IOptionsMonitor<SupportedCultureOptions> option, ILogger<CultureRedirectMiddleware> logger)
    {
        _next = next;
        _supportedCultureOptions = option.CurrentValue;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.Value;

        if (context.GetEndpoint() != null && context.GetEndpoint().DisplayName != "/PostsView")
        {
            await _next(context);
            return;
        }

        if(context.Request.PathBase.HasValue)
        {
            await _next(context);
            return;
        }

        if(context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        var first = !string.IsNullOrEmpty(path) && path != "/" ? path.Split('/', StringSplitOptions.RemoveEmptyEntries)[0] : "";
        
        if (!_supportedCultureOptions.SupportedCultures.ContainsKey(first))
        {
            context.Response.Redirect($"/{_supportedCultureOptions.DefaultCultureRoute}{context.Request.Path} ");
            return;
        }
        await _next(context);
    }
}