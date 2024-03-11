using FreecurrencyAPI.Models;
using JetBrains.Annotations;

namespace FreecurrencyAPI;

[PublicAPI]
public interface IFreecurrencyClient
{
    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [PublicAPI]
    Task<LatestExchangeRates> GetLatestExchangeRatesAsync(string baseCurrency, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="currencies">A list of currency codes which you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    Task<LatestExchangeRates> GetLatestExchangeRatesAsync(string baseCurrency, string[] currencies, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Latest Exchange Rate
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="currency">The currency you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    Task<LatestExchangeRates> GetLatestExchangeRateAsync(string baseCurrency, string currency, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns all supported currencies
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [PublicAPI]
    Task<Currencies> GetCurrenciesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns currencies
    /// </summary>
    /// <param name="currencies">A list of currency codes which you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    Task<Currencies> GetCurrenciesAsync(string[] currencies, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns currency
    /// </summary>
    /// <param name="currency">The currency you want to get.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    Task<Currency> GetCurrencyAsync(string currency, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns your current quota
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [PublicAPI]
    Task<Status> GetStatusAsync(CancellationToken cancellationToken = default);
}