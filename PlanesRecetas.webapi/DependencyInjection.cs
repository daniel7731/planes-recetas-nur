using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlanesRecetas.infraestructure.Extensions;
using PlanesRecetas.webapi.Infrastructure;
using System.Text;
namespace PlanesRecetas.webapi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

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
            //services.AddExceptionHandler<GlobalExceptionHandler>();
            //services.AddProblemDetails();


            /*if (!environment.IsDevelopment())
            {
                services.RegisterServiceToServiceDiscovery(configuration);
            }*/

            return services;
        }
        private static IServiceCollection RegisterServiceToServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
        {

            string? serviceDiscoveryAddress = configuration.GetValue<string?>("ServiceRegistration:ServiceDiscoveryAddress");

            services.AddSingleton(sp => new ConsulClient(c => c.Address = new Uri(serviceDiscoveryAddress)));
            services.AddHostedService<ServiceRegistration>();

            return services;
        }
    }

}
