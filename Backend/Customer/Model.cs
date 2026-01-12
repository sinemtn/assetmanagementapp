using System.Text.Json.Serialization;

namespace Customer;

public class Model
{
    [JsonPropertyName("id")]
    public required string CustomerId { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("address")]
    public required string Address { get; set; }
    [JsonPropertyName("billingAccount")]
    public required int BillingAccount { get; set; }
}