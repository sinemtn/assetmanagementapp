using System.Text.Json.Serialization;

namespace Printer;

public class Model
{
    [JsonPropertyName("id")]
    public required string PrinterId { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("manufacture")]
    public required string Manufacture { get; set; }
    [JsonPropertyName("category")]
    public required string Category { get; set; }
    [JsonPropertyName("toner")]
    public required string Toner { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}