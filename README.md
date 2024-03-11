# FreecurrencyAPI
Unofficial [RestEase](https://github.com/canton7/RestEase) C# Client for [freecurrencyapi](https://app.freecurrencyapi.com).

## Configuration

You will need your apikey to use freecurrencyapi, you can get one [https://app.freecurrencyapi.com/register](https://app.fxapi.com/register).

Register the api via Dependency Injection:

``` csharp
services.AddFreecurrencyAPI(o => 
    o.ApiKey = "[YOUR_API_KEY]"
);
```

## Usage & Endpoints

Use the instance to call the endpoints

### Status

Returns your current quota
``` csharp
    var statusResponse = await api.GetStatusAsync();
```

### Currencies

Returns all supported currency/currencies
``` csharp
    var currencyResponse = await api.GetCurrency("EUR");

    var currenciesResponse = await api.GetCurrencies(new [] { "EUR", "USD" });

    var allCurrenciesResponse = await api.GetCurrencies();
```

### Latest Exchange Rates

Returns the latest exchange rates. The default base currency is USD.
``` csharp
var rates = await _api.GetLatestExchangeRatesAsync(CurrencyCodes.USD, new [ CurrencyCodes.EUR, CurrencyCodes.AUD ]);

var rate = await _api.GetLatestExchangeRateAsync(CurrencyCodes.USD, CurrencyCodes.EUR);
```

### Historical Exchange Rates

Returns the latest exchange rates. The default base currency is USD.

``` csharp
// todo
```

## References
- https://freecurrencyapi.com/docs/