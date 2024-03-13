using FreecurrencyAPI.Models;
using RestEase;

namespace FreecurrencyAPI.Internal;

[PublicAPI]
internal interface IFreecurrencyApiInternal
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
    /// Returns all supported currencies
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