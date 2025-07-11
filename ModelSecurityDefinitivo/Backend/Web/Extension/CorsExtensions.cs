using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extension;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.WithOrigins(allowedOrigins!)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });

        return services;
    }
}
