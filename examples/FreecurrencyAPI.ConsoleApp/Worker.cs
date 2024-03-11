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
    private readonly IFreecurrencyClient _client;
    private readonly ILogger<Worker> _logger;

    public Worker(IFreecurrencyClient client, ILogger<Worker> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var isSupportedX = CurrencyCodes.IsSupported("x");

            var status = await _client.GetStatusAsync(cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetStatusAsync), JsonConvert.SerializeObject(status));

            var allCurrencies = await _client.GetCurrenciesAsync(cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetCurrenciesAsync), JsonConvert.SerializeObject(allCurrencies));

            var result = await _client.GetLatestExchangeRateAsync(CurrencyCodes.USD, CurrencyCodes.EUR, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetLatestExchangeRateAsync), JsonConvert.SerializeObject(result));

            var resultMultiple = await _client.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new[] { CurrencyCodes.EUR, CurrencyCodes.CAD }, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultMultiple));

            var resultAll1 = await _client.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultAll1));

            var resultAll2 = await _client.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyClient.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(resultAll2));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}