using System.Text.Json.Serialization;
namespace Sparepart;

public class Model
{
    [JsonPropertyName("id")]
    public required string SparepartID { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}
