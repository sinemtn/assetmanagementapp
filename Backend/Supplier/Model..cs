using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Supplier;

public class Model
{
    [JsonPropertyName("id")]
    public required string SupplierId { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("address")]
    public required string Address { get; set; }
}
