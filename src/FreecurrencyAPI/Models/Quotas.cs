namespace FreecurrencyAPI.Models;

[PublicAPI]
public class Quotas
{
    public QuotaDetails Month { get; set; } = null!;

    public QuotaDetails Grace { get; set; } = null!;
}