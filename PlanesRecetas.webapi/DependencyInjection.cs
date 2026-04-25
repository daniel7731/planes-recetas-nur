using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlanesRecetas.infraestructure.Extensions;
using System.Text;
namespace PlanesRecetas.webapi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddLogging();

            // REMARK: If you want to use Controllers, you'll need this.
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Map the boolean values
                        ValidateIssuer = jwtOptions.ValidateIssuer,
                        ValidateAudience = jwtOptions.ValidateAudience,
                        ValidateLifetime = jwtOptions.ValidateLifetime,
                        ValidateIssuerSigningKey = true,
                        // Map the strings
                        ValidIssuer = jwtOptions.ValidIssuer,
                        // Map the Secret key
                        IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                    };
                });


            return services;
        }
    }

}