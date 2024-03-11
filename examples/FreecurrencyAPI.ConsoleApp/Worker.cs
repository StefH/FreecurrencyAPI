using System;
using System.Threading;
using System.Threading.Tasks;
using FreecurrencyAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FreecurrencyAPI.ConsoleApp;

internal class Worker
{
    private readonly IFreecurrencyAPI _api;
    private readonly ILogger<Worker> _logger;

    public Worker(IFreecurrencyAPI api, ILogger<Worker> logger)
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
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetStatusAsync), JsonConvert.SerializeObject(status));

            var allCurrencies = await _api.GetCurrenciesAsync(cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetCurrenciesAsync), JsonConvert.SerializeObject(allCurrencies));

            var result = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, CurrencyCodes.EUR, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(result));

            var resultMultiple = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new[] { CurrencyCodes.EUR, CurrencyCodes.CAD }, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultMultiple));

            var resultAll = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultAll));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}