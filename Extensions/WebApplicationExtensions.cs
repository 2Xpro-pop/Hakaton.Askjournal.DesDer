using Destinationosh.Middlewares;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;

namespace Destinationosh.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void UseSpaAdmin(this WebApplication app)
        {
            var options = app.Configuration.GetSection("SpaAdmin").Get<SpaAdminOptions>();

            app.UseStaticFiles($"/{options.UrlPath}");

            app.UsePathBase($"/{options.UrlPath}");
            app.MapBlazorHub($"/{options.UrlPath}/_blazor", options =>
            {
                options.CloseOnAuthenticationExpiration = true;
            }).RequireAuthorization();
            app.MapFallbackToPage("/admin/{*path:nonfile}", "/Admin");
        }

        public static void UseDesDer2(this WebApplication app)
        {
            app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/DesDer2"), desDer2 =>
            {
                desDer2.UseBlazorFrameworkFiles("/DesDer2");
                desDer2.UseStaticFiles();

                desDer2.UseRouting();
                desDer2.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapFallbackToFile("DesDer2/{*path:nonfile}", "DesDer2/index.html");
                });
            });
        }

        public static void UseCultureRedirect(this WebApplication app)
        {
            app.UseMiddleware<CultureRedirectMiddleware>();

            app.UseRequestLocalization(options =>
            {
                var cultureOption = app.Services.GetRequiredService<IOptionsMonitor<SupportedCultureOptions>>();
                var cultures = cultureOption.CurrentValue.SupportedCultures.Values.ToArray();
                options.SetDefaultCulture(
                    cultureOption.CurrentValue.SupportedCultures[cultureOption.CurrentValue.DefaultCultureRoute]
                );
                options.AddSupportedCultures(
                    cultures
                );
                options.AddSupportedUICultures(
                    cultures
                );

                options.RequestCultureProviders.Clear();
                options.RequestCultureProviders.Add(
                    new RouteCultureProvider( 
                        app.Services.GetRequiredService<ILogger<RouteCultureProvider>>(),
                        cultureOption
                    )
                );
                options.RequestCultureProviders.Add(
                    new CookieRequestCultureProvider()
                );
            });
        }
    }
}