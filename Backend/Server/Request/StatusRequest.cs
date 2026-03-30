using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Request;

public class StatusRequest
{
    [FromQuery(Name = "status")]
    [JsonPropertyName("status")]
    public required string Status { get; set; }
}