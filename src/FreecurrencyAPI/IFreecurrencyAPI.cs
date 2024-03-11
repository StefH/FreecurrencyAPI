using FreecurrencyAPI.Models;
using JetBrains.Annotations;
using RestEase;
using Stef.Validation;

namespace FreecurrencyAPI;

[PublicAPI]
public interface IFreecurrencyAPI
{
    [Query("apikey")]
    string ApiKey { get; set; }

    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="currencies">A list of comma-separated currency codes which you want to get (EUR,USD,CAD).</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/latest")]
    [PublicAPI]
    Task<LatestExchangeRates> GetLatestExchangeRatesAsync([Query("base_currency")] string baseCurrency, [Query] string currencies, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/latest")]
    [PublicAPI]
    Task<LatestExchangeRates> GetLatestExchangeRatesAsync([Query("base_currency")] string baseCurrency, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all supported currencies
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/currencies")]
    [PublicAPI]
    Task<Currencies> GetCurrenciesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="currencies">A list of comma-separated currency codes which you want to get (EUR,USD,CAD).</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/currencies")]
    [PublicAPI]
    Task<Currencies> GetCurrenciesAsync([Query] string currencies, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns your current quota
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/status")]
    [PublicAPI]
    Task<Status> GetStatusAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Some extensions for the <see cref="IFreecurrencyAPI"/>
/// </summary>
[PublicAPI]
// ReSharper disable once InconsistentNaming
public static class IFreecurrencyAPIExtensions
{
    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="api">The <see cref="IFreecurrencyAPI"/></param>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="currencies">A list of currency codes which you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    public static Task<LatestExchangeRates> GetLatestExchangeRatesAsync(this IFreecurrencyAPI api, string baseCurrency, string[] currencies, CancellationToken cancellationToken = default) =>
        api.GetLatestExchangeRatesAsync(baseCurrency, string.Join(",", Guard.NotNullOrEmpty(currencies)), cancellationToken);

    /// <summary>
    /// Get Latest Exchange Rate
    /// </summary>
    /// <param name="api">The <see cref="IFreecurrencyAPI"/></param>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="currency">The currency you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    public static Task<LatestExchangeRates> GetLatestExchangeRateAsync(this IFreecurrencyAPI api, string baseCurrency, string currency, CancellationToken cancellationToken = default) =>
        api.GetLatestExchangeRatesAsync(baseCurrency, currency, cancellationToken);

    /// <summary>
    /// Returns currencies
    /// </summary>
    /// <param name="api">The <see cref="IFreecurrencyAPI"/></param>
    /// <param name="currencies">A list of currency codes which you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    public static Task<Currencies> GetCurrenciesAsync(this IFreecurrencyAPI api, string[] currencies, CancellationToken cancellationToken = default) =>
        api.GetCurrenciesAsync(string.Join(",", Guard.NotNullOrEmpty(currencies)), cancellationToken);

    /// <summary>
    /// Returns currency
    /// </summary>
    /// <param name="api">The <see cref="IFreecurrencyAPI"/></param>
    /// <param name="currency">The currency you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    public static async Task<Currency> GetCurrencyAsync(this IFreecurrencyAPI api, string currency, CancellationToken cancellationToken = default)
    {
        return (await api.GetCurrenciesAsync(currency, cancellationToken)).Data.First().Value;
    }
}