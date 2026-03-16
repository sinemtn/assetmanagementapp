using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Assignment;

public class AssignmentModel
{
    [FromQuery(Name = "mpNo")]
    [JsonPropertyName("mpNo")]
    public required string AssigmentNo { get; set; }
    
    [FromQuery(Name = "customer")]
    [JsonPropertyName("customer")]
    public required string Customer { get; set; }
    
    [FromQuery(Name = "task")]
    [JsonPropertyName("task")]
    public required string Task { get; set; }
    
    [FromQuery(Name = "pic")]
    [JsonPropertyName("pic")]
    public required string PIC { get; set; }
    
    [FromQuery(Name = "status")]
    [JsonPropertyName("status")]
    public required string Status { get; set; }
}


public class AssignmentDetailModel
{
    [JsonPropertyName("mpNo")]
    public required string MPNo { get; set; }
    [JsonPropertyName("status")]
    public required string Status { get; set; }
    [JsonPropertyName("customer")]
    public required string Customer { get; set; }
    [JsonPropertyName("task")]
    public required string Task { get; set; }
    [JsonPropertyName("assignmentNo")]
    public required string AssignmentNo { get; set; }
    [JsonPropertyName("pic")]
    public required string PIC { get; set; }
    [JsonPropertyName("complaintNo")]
    public string ComplaintNo { get; set; } = string.Empty;
    [JsonPropertyName("items")]
    public ItemModel[] Items { get; set; } = [];
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")]
    public DateTime UpdatedAt { get; set; } 
    [JsonPropertyName("validatedAt")]
    public DateTime? ValidatedAt { get; set; }
    [JsonPropertyName("validatedBy")]
    public string? ValidatedBy { get; set; }
    [JsonPropertyName("authorizedAt")]
    public DateTime? AuthorizedAt { get; set; }
    [JsonPropertyName("authorizedBy")]
    public string? AuthorizedBy { get; set; }
}


public class ItemModel
{
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    [JsonPropertyName("itemId")]
    public required string ItemId { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
    [JsonPropertyName("serialNumber")]
    public string SerialNumber { get; set; } = string.Empty;
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

}