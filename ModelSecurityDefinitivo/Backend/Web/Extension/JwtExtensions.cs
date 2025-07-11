using Business.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Extension;

public static class JwtServiceExtensions
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]!))
            };
        });

        // .AddGoogle(googleOptions =>
        // {
        //     googleOptions.ClientId = configuration["Google:ClientId"]!;
        //     googleOptions.ClientSecret = configuration["Google:ClientSecret"]!;
        //     googleOptions.CallbackPath = "/signin-google"; // asegúrate que coincida con el de Google Console

        //     googleOptions.Events.OnCreatingTicket = async context =>
        //     {
        //         // Aquí puedes acceder al token y claims
        //         var email = context.Identity!.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        //         // Buscar o registrar al usuario en tu sistema
        //         // Ejemplo ficticio:
        //         var userService = context.HttpContext.RequestServices.GetRequiredService<AuthServices>();
        //         await userService.LoginWithGoogle(email);
        //     };
        // });

        return services;
    }
}
