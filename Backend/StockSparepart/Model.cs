using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace StockSparepart;

public class Model
{
    [JsonPropertyName("id")]
    public long? Id { get; set; }

    [FromQuery(Name = "sparepart")]
    [JsonPropertyName("sparepart")]
    public required string Sparepart { get; set; }

    [FromQuery(Name = "location")]
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    [FromQuery(Name = "branch")]
    [JsonPropertyName("branch")]
    public string? Branch { get; set; }

    [JsonPropertyName("qty")]
    public required int Qty { get; set; } = 0;

    [JsonPropertyName("customer")]
    public string? Customer { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
}
