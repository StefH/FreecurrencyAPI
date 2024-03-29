﻿using Newtonsoft.Json;

namespace FreecurrencyAPI.Models;

[PublicAPI]
public class Currency
{
    public string Symbol { get; set; } = null!;

    public string Name { get; set; } = null!;

    [JsonProperty("symbol_native")]
    public string SymbolNative { get; set; } = null!;

    [JsonProperty("decimal_digits")]
    public int DecimalDigits { get; set; }
    
    public int Rounding { get; set; }
    
    public string Code { get; set; } = null!;

    [JsonProperty("name_plural")]
    public string NamePlural { get; set; } = null!;

    public string Type { get; set; } = null!;
}