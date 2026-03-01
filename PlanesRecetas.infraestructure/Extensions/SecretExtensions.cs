using Joseco.Communication.External.RabbitMQ.Services;
using Joseco.Secrets.Contrats;
using Joseco.Secrets.HashicorpVault;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nur.Store2025.Observability.Config;
using Nur.Store2025.Security.Config;
using PlanesRecetas.infraestructure.Persistence;


namespace PlanesRecetas.infraestructure.Extensions;

public static class SecretExtensions
{
    private const string JwtOptionsSecretName = "JwtOptions";
    private const string RabbitMqSettingsSecretName = "RabbitMqSettings";
    private const string DatabaseConnectionStringSecretName = "DatabaseSettings";
    private const string JaegerSettingsSecretName = "JaegerSettings";

    public static IServiceCollection AddSecrets(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        bool useSecretManager = configuration.GetValue("UseSecretManager", false);

        if (environment.IsDevelopment() && !useSecretManager)
        {
            configuration
                .LoadAndRegister<RabbitMqSettings>(services, RabbitMqSettingsSecretName)
                .LoadAndRegister<DataBaseSettings>(services, DatabaseConnectionStringSecretName)
                .LoadAndRegister<JwtOptions>(services, JwtOptionsSecretName)
                .LoadAndRegister<JeagerSettings>(services, JaegerSettingsSecretName);

            return services;
        }

        string? vaultUrl = Environment.GetEnvironmentVariable("VAULT_URL");
        string? vaultToken = Environment.GetEnvironmentVariable("VAULT_TOKEN");

        if (string.IsNullOrEmpty(vaultUrl) || string.IsNullOrEmpty(vaultToken))
        {
            throw new InvalidOperationException("Vault URL or Token is not set in environment variables.");
        }

        var settings = new VaultSettings()
        {
            VaultUrl = vaultUrl,
            VaultToken = vaultToken
        };

        services.AddHashicorpVault(settings)
            .LoadSecretsFromVault();

        return services;
    }

    private static void LoadSecretsFromVault(this IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

        using var scope = scopeFactory.CreateScope();
        var secretManager = scope.ServiceProvider.GetRequiredService<ISecretManager>();

        string vaultMountPoint = "secrets";

        Task[] tasks = [
            LoadAndRegisterAsync<JwtOptions>(secretManager, services, JwtOptionsSecretName, vaultMountPoint),
            LoadAndRegisterAsync<RabbitMqSettings>(secretManager, services, RabbitMqSettingsSecretName, vaultMountPoint),
            LoadAndRegisterAsync<DataBaseSettings>(secretManager, services, DatabaseConnectionStringSecretName, vaultMountPoint),
            LoadAndRegisterAsync<JeagerSettings>(secretManager, services, JaegerSettingsSecretName, vaultMountPoint)
        ];

        Task.WaitAll(tasks);
    }

    private static async Task LoadAndRegisterAsync<T>(ISecretManager secretManager, IServiceCollection services,
        string secretName, string mountPoint) where T : class, new()
    {
        T secret = await secretManager.Get<T>(secretName, mountPoint);
        services.AddSingleton(secret);
    }

    private static IConfiguration LoadAndRegister<T>(this IConfiguration configuration, IServiceCollection services,
        string secretName) where T : class, new()
    {
        T secret = new T();
        configuration.Bind(secretName, secret);
        services.AddSingleton(secret);
        return configuration;
    }
}
