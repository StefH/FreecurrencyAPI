namespace FreecurrencyAPI.Models;

[PublicAPI]
public class LatestExchangeRates
{
    public IDictionary<string, decimal> Data { get; set; } = null!;

    public decimal Rate => Data.First().Value;
}