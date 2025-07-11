using Business.Interface;
using Business.JWT;
using Business.Services;
using Business.Strategies;
using Data.Core;
using Data.Interface;
using Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using Utilities.Notification.Email;

namespace Microsoft.Extensions.DependencyInjection;

public static  class ServiceExtensions
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services)
    {
        // Repositorios
        services.AddScoped<PersonRepository>();
        services.AddScoped<UserRepository>();
        services.AddScoped<ModuleRepository>();
        services.AddScoped<FormRepository>();
        services.AddScoped<RolRepository>();
        services.AddScoped<PermissionRepository>();
        services.AddScoped<RolFormPermissionRepository>();
        services.AddScoped<FormModuleRepository>();
        services.AddScoped<RolUserRepository>();
        services.AddScoped<AuthRepository>();

        // Servicios de negocio
        services.AddScoped<PersonServices>();
        services.AddScoped<UserServices>();
        services.AddScoped<ModuleServices>();
        services.AddScoped<FormServices>();
        services.AddScoped<RolServices>();
        services.AddScoped<PermissionServices>();
        services.AddScoped<RolFormPermissionServices>();
        services.AddScoped<FormModuleServices>();
        services.AddScoped<RolUserServices>();
        services.AddScoped<AuthServices>();

        // Utilidades
        services.AddScoped<JWTGenerate>();
        services.AddScoped<NotificationEmail>();

        // Genéricos
        services.AddScoped(typeof(DataBase<>), typeof(DataBase<>));
        services.AddScoped(typeof(DeleteStrategyFactory<>)); 

        return services;
    }
}
