using Entity.Context;
using Microsoft.EntityFrameworkCore;

namespace Web.Extension
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddCustomDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseProvider = configuration["DatabaseProvider"];

            switch (databaseProvider)
            {
                case "SqlServer":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
                    break;
                case "MySql":
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseMySql(configuration.GetConnectionString("MySqlConnection"),
                                         ServerVersion.AutoDetect(configuration.GetConnectionString("MySqlConnection"))));
                    break;
                default:
                    throw new InvalidOperationException($"Proveedor de base de datos '{databaseProvider}' no soportado.");
            }
            return services;
        }
    }
}
