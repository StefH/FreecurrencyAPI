# ![icon](./resources/icon_32x32.png) Freecurrency-API
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [freecurrencyapi](https://app.freecurrencyapi.com) which uses [IMemoryCache](https://learn.microsoft.com/en-us/aspnet/core/performance/caching/memory) to cache the results.

## NuGet
[![NuGet Badge](https://img.shields.io/nuget/v/Freecurrency-API)](https://www.nuget.org/packages/Freecurrency-API) 

## Configuration

You will need your ApiKey to use freecurrencyapi, you can get one [https://app.freecurrencyapi.com/register](https://app.fxapi.com/register).

Register the client via Dependency Injection:

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

Returns all supported currencies
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
    [Range(0, int.MaxValue)]
    public int GetLatestExchangeRatesCacheExpirationInMinutes { get; set; } = 60;

    /// <summary>
    /// The cache expiration time in hours for the currencies.
    /// Default value is 24 hours.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int GetCurrenciesCacheExpirationInHours { get; set; } = 24;
}
```

## References
- https://freecurrencyapi.com/docs/


## Sponsors

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=StefH) and [Dapper Plus](https://dapper-plus.net/?utm_source=StefH) are major sponsors and proud to contribute to the development of **Freecurrency-API**.

[![Entity Framework Extensions](https://raw.githubusercontent.com/StefH/resources/main/sponsor/entity-framework-extensions-sponsor.png)](https://entityframework-extensions.net/bulk-insert?utm_source=StefH)

[![Dapper Plus](https://raw.githubusercontent.com/StefH/resources/main/sponsor/dapper-plus-sponsor.png)](https://dapper-plus.net/bulk-insert?utm_source=StefH)