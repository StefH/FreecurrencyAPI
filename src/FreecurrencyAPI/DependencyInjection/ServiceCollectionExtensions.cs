using FreecurrencyAPI;
using FreecurrencyAPI.Internal;
using FreecurrencyAPI.Options;
using FreecurrencyAPI.RetryPolicies;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestEase.HttpClientFactory;

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

        services.AddMemoryCache();
        
        services
            .AddHttpClient(options.HttpClientName!, httpClient =>
            {
                httpClient.BaseAddress = options.BaseAddress;
                httpClient.Timeout = TimeSpan.FromSeconds(options.TimeoutInSeconds);
            })
            .AddPolicyHandler((serviceProvider, _) => HttpClientRetryPolicies.GetPolicy<IFreecurrencyApiInternal>(serviceProvider, options.MaxRetries, options.HttpStatusCodesToRetry))
            .UseWithRestEaseClient(new UseWithRestEaseClientOptions<IFreecurrencyApiInternal>
            {
                InstanceConfigurer = freecurrencyAPI =>
                {
                    freecurrencyAPI.ApiKey = options.ApiKey;
                }
            });

        // This regex pattern will match any part of a string that starts with "apikey=" (in a case-insensitive manner)
        // followed by any number of characters that are not an ampersand.
        services.UseSanitizedHttpLogger(o => o.RequestUriReplacements.Add("(?i)apikey=[^&]*", "apikey=***"));

        services.AddScoped<IFreecurrencyClient, FreecurrencyClient>();

        return services;
    }
}