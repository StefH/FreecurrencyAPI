namespace FreecurrencyAPI.Models;

[PublicAPI]
public class Currencies
{
    public IDictionary<string, Currency> Data { get; set; } = null!;
}