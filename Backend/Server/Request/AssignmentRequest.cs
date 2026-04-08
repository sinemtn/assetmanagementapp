using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Server.Request;

public class AssignmentRequest
{
    [FromQuery(Name = "mpNo")]
    [JsonPropertyName("mpNo")]
    public required string MPNo { get; set; }
    
    [FromQuery(Name = "status")]
    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [FromQuery(Name = "customer")]
    [JsonPropertyName("customer")]
    public required string Customer { get; set; }

    [FromQuery(Name = "task")]
    [JsonPropertyName("task")]
    public required string Task { get; set; }

    [FromQuery(Name = "assignmentNo")]
    [JsonPropertyName("assignmentNo")]
    public string? AssignmentNo { get; set; }

    [FromQuery(Name = "pic")]
    [JsonPropertyName("pic")]
    public required string PIC { get; set; }

    [FromQuery(Name = "complaintNo")]
    [JsonPropertyName("complaintNo")]
    public string ComplaintNo { get; set; } = string.Empty;

    [FromQuery(Name = "items")]
    [JsonPropertyName("items")]
    public AssignmentItemRequest[] Items { get; set; } = [];
}

public class AssignmentItemRequest
{
    [FromQuery(Name = "type")]
    [JsonPropertyName("type")]
    public required string Type { get; set; }

    [FromQuery(Name = "itemId")]
    [JsonPropertyName("itemId")]
    public required string ItemId { get; set; }

    [FromQuery(Name = "serialNumber")]
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; set; } = string.Empty;
    
    [FromQuery(Name = "qty")]
    [JsonPropertyName("qty")]
    public required int Quantity { get; set; } = 0;

}