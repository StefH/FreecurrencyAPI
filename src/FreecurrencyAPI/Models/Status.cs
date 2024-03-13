using Newtonsoft.Json;

namespace FreecurrencyAPI.Models;

[PublicAPI]
public class Status
{
    [JsonProperty("account_id")]
    public long AccountId { get; set; }

    public Quotas Quotas { get; set; } = null!;
}