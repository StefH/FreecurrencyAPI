using Newtonsoft.Json;

namespace FreecurrencyAPI.Models;

public class Currencies
{
    public IDictionary<string, Currency> Data { get; set; } = null!;
}

public class Currency
{
    public string Symbol { get; set; }
    
    public string Name { get; set; }

    [JsonProperty("symbol_native")]
    public string SymbolNative { get; set; }

    [JsonProperty("decimal_digits")]
    public int DecimalDigits { get; set; }
    
    public int Rounding { get; set; }
    
    public string Code { get; set; }

    [JsonProperty("name_plural")]
    public string NamePlural { get; set; }

    public string Type { get; set; }
}