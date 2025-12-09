using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace StockPrinter;

public class Model
{
    [FromQuery(Name = "mpNo")]
    [JsonPropertyName("mpNo")]
    public required string MPNo { get; set; }

    [FromQuery(Name = "printer")]
    [JsonPropertyName("printer")]
    public required string Printer { get; set; }

    [FromQuery(Name = "serialNo")]
    [JsonPropertyName("serialNo")]
    public required string SerialNo { get; set; }

    [FromQuery(Name = "feature")]
    [JsonPropertyName("feature")]
    public string? Feature { get; set; }

    [FromQuery(Name = "buyDate")]
    [JsonPropertyName("buyDate")]
    public required DateTime BuyDate { get; set; }

    [FromQuery(Name = "location")]
    [JsonPropertyName("location")]
    public required string Location { get; set; }

    [FromQuery(Name = "branch")]
    [JsonPropertyName("branch")]
    public required string Branch { get; set; }

    [FromQuery(Name = "status")]
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [FromQuery(Name = "active")]
    [JsonPropertyName("active")]
    public required bool Active { get; set; } = true;

    [JsonPropertyName("note")]
    public string? Note { get; set; }
}
