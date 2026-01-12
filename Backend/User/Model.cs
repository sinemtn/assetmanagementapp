using System.Text.Json.Serialization;
namespace User;

public class Model
{
    [JsonPropertyName("id")]
    public required string UserId { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("role")]
    public required int Role { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
    [JsonPropertyName("active")]
    public required bool Active { get; set; }
}