using Newtonsoft.Json;

namespace FreecurrencyAPI.Models;

public class Status
{
    [JsonProperty("account_id")]
    public long AccountId { get; set; }

    public Quotas Quotas { get; set; } = null!;
}

public class Quotas
{
    public QuotaDetails Month { get; set; } = null!;

    public QuotaDetails Grace { get; set; } = null!;
}

public class QuotaDetails
{
    public int Total { get; set; }

    public int Used { get; set; }

    public int Remaining { get; set; }
}