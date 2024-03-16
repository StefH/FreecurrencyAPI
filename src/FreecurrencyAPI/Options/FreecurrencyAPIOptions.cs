using System.ComponentModel.DataAnnotations;

namespace FreecurrencyAPI.Options;

[PublicAPI]
public class FreecurrencyAPIOptions
{
    /// <summary>
    /// The required BaseAddress.
    /// </summary>
    [Required]
    public Uri BaseAddress { get; set; } = new("https://api.freecurrencyapi.com/v1");

    [Required]
    public string ApiKey { get; set; } = null!;

    /// <summary>
    /// Optional HttpClient name to use.
    /// </summary>
    public string? HttpClientName { get; set; }

    /// <summary>
    /// This timeout in seconds defines the timeout on the HttpClient which is used to call the BaseAddress.
    /// Default value is 60 seconds.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TimeoutInSeconds { get; set; } = 60;

    /// <summary>
    /// The maximum number of retries.
    /// </summary>
    [Range(0, 99)]
    public int MaxRetries { get; set; } = 3;

    /// <summary>
    /// In addition to Network failures, TaskCanceledException, HTTP 5XX and HTTP 408. Also retry these <see cref="HttpStatusCode"/>s. [Optional]
    /// </summary>
    public HttpStatusCode[]? HttpStatusCodesToRetry { get; set; }

    /// <summary>
    /// The cache expiration time in minutes for the latest exchange rates.
    /// Default value is 60 minutes.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int GetLatestExchangeRatesCacheExpirationInMinutes { get; set; } = 60;

    /// <summary>
    /// The cache expiration time in hours for the currencies.
    /// Default value is 24 hours.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int GetCurrenciesCacheExpirationInHours { get; set; } = 24;
}