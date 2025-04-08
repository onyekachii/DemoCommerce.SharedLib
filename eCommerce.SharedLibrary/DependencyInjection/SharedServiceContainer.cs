﻿

using eCommerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace eCommerce.SharedLibrary.DependencyInjection
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string fileName) where TContext : DbContext
        {
            services.AddDbContext<TContext>(o => o.UseSqlServer(
                config.GetConnectionString("eCommConnString"), sqlserverOption => sqlserverOption.EnableRetryOnFailure()));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}", restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{level:u3}] {message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

            JWTAuthenticationScheme.AddJWTAuthenticationScheme(services,config);
            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalException>();
            app.UseMiddleware<ApiGateWayListener>();
            return app;
        }
    }   
}
