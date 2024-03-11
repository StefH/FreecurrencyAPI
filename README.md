# FreecurrencyAPI
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [freecurrencyapi](https://app.freecurrencyapi.com) which uses [IMemoryCache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory) to cache the results.

## Configuration

You will need your ApiKey to use freecurrencyapi, you can get one [https://app.freecurrencyapi.com/register](https://app.fxapi.com/register).

Register the api via Dependency Injection:

``` csharp
services.AddFreecurrencyAPI(o =>
    o.ApiKey = "[YOUR_API_KEY]"
);
```

## Usage

### Status

Returns your current quota
``` csharp
IFreecurrencyClient client = // get from DI
var statusResponse = await client.GetStatusAsync();
```

### Latest Exchange Rates

Returns the latest exchange rates. The default base currency is USD.
``` csharp
IFreecurrencyClient client = // get from DI
var rates = await _client.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new [ CurrencyCodes.EUR, CurrencyCodes.AUD ]);

var rate = await _client.GetLatestExchangeRateAsync(CurrencyCodes.USD, CurrencyCodes.EUR);
```

### Historical Exchange Rates

Returns the latest exchange rates. The default base currency is USD.

``` csharp
// todo
```

### Currencies

Returns all supported currency/currencies
``` csharp
IFreecurrencyClient client = // get from DI
var currencyResponse = await client.GetCurrency("EUR");

var currenciesResponse = await client.GetCurrencies(new [] { "EUR", "USD" });

var allCurrenciesResponse = await client.GetCurrencies();
```

### Options
``` csharp
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
    public int GetLatestExchangeRatesCacheExpirationInMinutes { get; set; } = 60;

    /// <summary>
    /// The cache expiration time in hours for the currencies.
    /// Default value is 24 hours.
    /// </summary>
    public int GetCurrenciesCacheExpirationInHours { get; set; } = 24;
}
```

## References
- https://freecurrencyapi.com/docs/