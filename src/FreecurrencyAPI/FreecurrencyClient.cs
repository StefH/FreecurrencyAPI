using FreecurrencyAPI.Internal;
using FreecurrencyAPI.Models;
using FreecurrencyAPI.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace FreecurrencyAPI;

internal class FreecurrencyClient : IFreecurrencyClient
{
    private readonly IFreecurrencyApiInternal _api;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _getLatestExchangeRatesCacheExpirationInSeconds;
    private readonly TimeSpan _getCurrenciesCacheExpirationInHours;

    public FreecurrencyClient(IOptions<FreecurrencyAPIOptions> options, IFreecurrencyApiInternal api, IMemoryCache cache)
    {
        Guard.NotNull(options);
        _api = Guard.NotNull(api);
        _cache = Guard.NotNull(cache);

        _getLatestExchangeRatesCacheExpirationInSeconds = TimeSpan.FromSeconds(options.Value.GetLatestExchangeRatesCacheExpirationInMinutes);
        _getCurrenciesCacheExpirationInHours = TimeSpan.FromSeconds(options.Value.GetCurrenciesCacheExpirationInHours);
    }

    public Task<LatestExchangeRates> GetLatestExchangeRatesAsync(string baseCurrency, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(baseCurrency);

        var key = $"{nameof(GetLatestExchangeRatesAsync)}_{baseCurrency}";

        return GetAsync((api, ct) => api.GetLatestExchangeRatesAsync(baseCurrency, ct), key, cancellationToken);
    }

    public Task<LatestExchangeRates> GetLatestExchangeRatesAsync(string baseCurrency, string[] currencies, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(baseCurrency);
        Guard.NotNullOrEmpty(currencies);

        var currenciesAsString = string.Join(",", currencies);
        var key = $"{nameof(GetLatestExchangeRatesAsync)}_{baseCurrency}_{currenciesAsString}";

        return GetAsync((api, ct) => api.GetLatestExchangeRatesAsync(baseCurrency, currenciesAsString, ct), key, cancellationToken);
    }

    public Task<LatestExchangeRates> GetLatestExchangeRateAsync(string baseCurrency, string currency, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(baseCurrency);
        Guard.NotNullOrEmpty(currency);

        var key = $"{nameof(GetLatestExchangeRatesAsync)}_{baseCurrency}_{currency}";

        return GetAsync((api, ct) => api.GetLatestExchangeRatesAsync(baseCurrency, currency, ct), key, cancellationToken);
    }

    public Task<Currencies> GetCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        const string key = nameof(GetCurrenciesAsync);

        return GetAsync((api, ct) => api.GetCurrenciesAsync(ct), key, cancellationToken);
    }

    public Task<Currencies> GetCurrenciesAsync(string[] currencies, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(currencies);

        var currenciesAsString = string.Join(",", currencies);
        var key = $"{nameof(GetCurrenciesAsync)}_{currenciesAsString}";

        return GetAsync((api, ct) => api.GetCurrenciesAsync(currenciesAsString, ct), key, cancellationToken);
    }

    public async Task<Currency> GetCurrencyAsync(string currency, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(currency);

        var key = $"{nameof(GetCurrenciesAsync)}_{currency}";

        return (await GetAsync((api, ct) => api.GetCurrenciesAsync(ct), key, cancellationToken)).Data.First().Value;
    }

    public Task<Status> GetStatusAsync(CancellationToken cancellationToken = default) => _api.GetStatusAsync(cancellationToken);

    private Task<LatestExchangeRates> GetAsync(Func<IFreecurrencyApiInternal, CancellationToken, Task<LatestExchangeRates>> func, string key, CancellationToken cancellationToken) => 
        GetAsync(func, key, _getLatestExchangeRatesCacheExpirationInSeconds, cancellationToken);

    private Task<Currencies> GetAsync(Func<IFreecurrencyApiInternal, CancellationToken, Task<Currencies>> func, string key, CancellationToken cancellationToken) => 
        GetAsync(func, key, _getCurrenciesCacheExpirationInHours, cancellationToken);

    private Task<T> GetAsync<T>(Func<IFreecurrencyApiInternal, CancellationToken, Task<T>> func, string key, TimeSpan absoluteExpirationRelativeToNow, CancellationToken cancellationToken) =>
        _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            return await func(_api, cancellationToken);
        })!;
}