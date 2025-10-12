using System.Text.Json.Serialization;
namespace Toner;

public class Model
{
    [JsonPropertyName("id")]
    public required string TonerId { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("category")]
    public required string Category { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}
