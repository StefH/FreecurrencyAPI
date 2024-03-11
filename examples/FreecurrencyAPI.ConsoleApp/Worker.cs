using System;
using System.Threading;
using System.Threading.Tasks;
using FreecurrencyAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly.Caching;

namespace FreecurrencyAPI.ConsoleApp;

internal class Worker
{
    private readonly IFreecurrency _api;
    private readonly ILogger<Worker> _logger;

    public Worker(IFreecurrency api, ILogger<Worker> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var isSupportedX = CurrencyCodes.IsSupported("x");

            var status = await _api.GetStatusAsync(cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetStatusAsync), JsonConvert.SerializeObject(status));

            var allCurrencies = await _api.GetCurrenciesAsync(cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetCurrenciesAsync), JsonConvert.SerializeObject(allCurrencies));

            var result = await _api.GetLatestExchangeRateAsync(CurrencyCodes.USD, CurrencyCodes.EUR, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetLatestExchangeRateAsync), JsonConvert.SerializeObject(result));

            var resultMultiple = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new[] { CurrencyCodes.EUR, CurrencyCodes.CAD }, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultMultiple));

            var resultAll1 = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultAll1));

            var resultAll2 = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrency.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultAll2));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}