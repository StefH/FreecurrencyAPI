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
            var result = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, CurrencyCodes.EUR, cancellationToken);
            var content = result.GetContent();
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(content));

            var resultMultiple = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new[] { CurrencyCodes.EUR, CurrencyCodes.CAD }, cancellationToken);
            var contentMultiple = resultMultiple.GetContent();
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(contentMultiple));

            var resultAll = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, cancellationToken);
            var contentAll = resultAll.GetContent();
            _logger.LogInformation("{method}|{result}", nameof(IFreecurrencyAPI.GetLatestExchangeRatesAsync), JsonConvert.SerializeObject(contentAll));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}