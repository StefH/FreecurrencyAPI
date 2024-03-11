using FreecurrencyAPI.Internal;
using FreecurrencyAPI.Models;
using FreecurrencyAPI.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Stef.Validation;

namespace FreecurrencyAPI;

internal class Freecurrency : IFreecurrency
{
    private readonly IFreecurrencyApiInternal _api;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _getLatestExchangeRatesCacheExpirationInSeconds;
    private readonly TimeSpan _getCurrenciesCacheExpirationInHours;

    public Freecurrency(IOptions<FreecurrencyAPIOptions> options, IFreecurrencyApiInternal api, IMemoryCache cache)
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

        return _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getLatestExchangeRatesCacheExpirationInSeconds;
            return await _api.GetLatestExchangeRatesAsync(baseCurrency, cancellationToken);
        })!;
    }

    public Task<LatestExchangeRates> GetLatestExchangeRatesAsync(string baseCurrency, string[] currencies, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(baseCurrency);
        Guard.NotNullOrEmpty(currencies);

        var currenciesAsString = string.Join(",", currencies);
        var key = $"{nameof(GetLatestExchangeRatesAsync)}_{baseCurrency}_{currenciesAsString}";

        return _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getLatestExchangeRatesCacheExpirationInSeconds;
            return await _api.GetLatestExchangeRatesAsync(baseCurrency, currenciesAsString, cancellationToken);
        })!;
    }

    public Task<LatestExchangeRates> GetLatestExchangeRateAsync(string baseCurrency, string currency, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(baseCurrency);
        Guard.NotNullOrEmpty(currency);

        var key = $"{nameof(GetLatestExchangeRatesAsync)}_{baseCurrency}_{currency}";

        return _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getLatestExchangeRatesCacheExpirationInSeconds;
            return await _api.GetLatestExchangeRatesAsync(baseCurrency, currency, cancellationToken);
        })!;
    }

    public Task<Currencies> GetCurrenciesAsync(CancellationToken cancellationToken = default)
    {
        return _cache.GetOrCreate(nameof(GetCurrenciesAsync), async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getCurrenciesCacheExpirationInHours;
            return await _api.GetCurrenciesAsync(cancellationToken);
        })!;
    }

    public Task<Currencies> GetCurrenciesAsync(string[] currencies, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(currencies);

        var currenciesAsString = string.Join(",", currencies);
        var key = $"{nameof(GetCurrenciesAsync)}_{currenciesAsString}";

        return _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getCurrenciesCacheExpirationInHours;
            return await _api.GetCurrenciesAsync(currenciesAsString, cancellationToken);
        })!;
    }

    public async Task<Currency> GetCurrencyAsync(string currency, CancellationToken cancellationToken = default)
    {
        Guard.NotNullOrEmpty(currency);

        var key = $"{nameof(GetCurrenciesAsync)}_{currency}";

        return await _cache.GetOrCreate(key, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _getCurrenciesCacheExpirationInHours;
            return (await _api.GetCurrenciesAsync(cancellationToken)).Data.First().Value;
        })!;
    }

    public Task<Status> GetStatusAsync(CancellationToken cancellationToken = default)
    {
        return _api.GetStatusAsync(cancellationToken);
    }
}