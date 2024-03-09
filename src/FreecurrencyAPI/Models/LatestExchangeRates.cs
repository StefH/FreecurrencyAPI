namespace FreecurrencyAPI.Models;

public class LatestExchangeRates
{
    public IDictionary<string, double> Data { get; set; } = null!;

    public double Rate => Data.First().Value;
}