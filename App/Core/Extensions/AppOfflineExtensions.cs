using App.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace App.Core.Extensions
{
    public static class AppOfflineExtensions
    {
        public static IApplicationBuilder UseAppOffline(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AppOfflineMiddleware>();
        }
    }
}
