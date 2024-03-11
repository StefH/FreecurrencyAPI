namespace FreecurrencyAPI.Models;

public class Quotas
{
    public QuotaDetails Month { get; set; } = null!;

    public QuotaDetails Grace { get; set; } = null!;
}