using FreecurrencyAPI;
using FreecurrencyAPI.Options;
using FreecurrencyAPI.RetryPolicies;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestEase.HttpClientFactory;
using Stef.Validation;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

[PublicAPI]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFreecurrencyAPI(this IServiceCollection services, IConfiguration configuration)
    {
        Guard.NotNull(services);
        Guard.NotNull(configuration);

        return services.AddFreecurrencyAPI(restEaseClientOptions =>
        {
            configuration.GetSection(nameof(FreecurrencyAPIOptions)).Bind(restEaseClientOptions);
        });
    }

    public static IServiceCollection AddFreecurrencyAPI(this IServiceCollection services, IConfigurationSection section, JsonSerializerSettings? jsonSerializerSettings = null)
    {
        Guard.NotNull(services);
        Guard.NotNull(section);

        return services.AddFreecurrencyAPI(section.Bind);
    }

    public static IServiceCollection AddFreecurrencyAPI(this IServiceCollection services, Action<FreecurrencyAPIOptions> configureAction)
    {
        Guard.NotNull(services);
        Guard.NotNull(configureAction);

        var options = new FreecurrencyAPIOptions();
        configureAction(options);

        return services.AddFreecurrencyAPI(options);
    }

    public static IServiceCollection AddFreecurrencyAPI(this IServiceCollection services, FreecurrencyAPIOptions options)
    {
        Guard.NotNull(services);
        Guard.NotNull(options);

        if (string.IsNullOrEmpty(options.HttpClientName))
        {
            options.HttpClientName = nameof(FreecurrencyAPI);
        }

        services.AddOptionsWithDataAnnotationValidation(options);

        services
            .AddHttpClient(options.HttpClientName!, httpClient =>
            {
                httpClient.BaseAddress = options.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IFreecurrencyAPI>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .UseWithRestEaseClient(new UseWithRestEaseClientOptions<IFreecurrencyAPI>
            {
                InstanceConfigurer = freecurrencyAPI =>
                {
                    freecurrencyAPI.ApiKey = options.ApiKey;
                }
            });

        return services;
    }
}