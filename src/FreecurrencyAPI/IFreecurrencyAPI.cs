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
    Task<Response<LatestExchangeRates>> GetLatestExchangeRatesAsync([Query("base_currency")] string baseCurrency, [Query] string currencies, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get Latest Exchange Rates
    /// </summary>
    /// <param name="baseCurrency">The base currency to which all results are behaving relative to.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
    /// <returns><see cref="LatestExchangeRates"/></returns>
    [Get("/latest")]
    [PublicAPI]
    Task<Response<LatestExchangeRates>> GetLatestExchangeRatesAsync([Query("base_currency")] string baseCurrency, CancellationToken cancellationToken = default);
}

/// <summary>
/// Some extensions for the <see cref="IFreecurrencyAPI"/>
/// </summary>
// ReSharper disable once InconsistentNaming
[PublicAPI]
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
    public static Task<Response<LatestExchangeRates>> GetLatestExchangeRatesAsync(this IFreecurrencyAPI api, string baseCurrency, string[] currencies, CancellationToken cancellationToken = default) =>
        api.GetLatestExchangeRatesAsync(baseCurrency, string.Join(",", Guard.NotNullOrEmpty(currencies)), cancellationToken);
}