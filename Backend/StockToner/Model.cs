using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace StockToner;

public class Model
{
    [FromQuery(Name = "toner")]
    [JsonPropertyName("toner")]
    public required string Toner { get; set; }

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

    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime? CreatedAt { get; set; }
}
