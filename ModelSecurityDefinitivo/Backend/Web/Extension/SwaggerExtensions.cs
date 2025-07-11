using Microsoft.OpenApi.Models;
using Web.Filter;

namespace Web.Extension
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<EnumSchemaFilter>();

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
