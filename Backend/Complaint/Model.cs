using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Complaint;

public class ComplaintModel
{
    [FromQuery(Name = "complaintNo")]
    [JsonPropertyName("complaintNo")]
    public required string ComplaintNo { get; set; }

    [FromQuery(Name = "mpNo")]
    [JsonPropertyName("mpNo")]
    public required string MPNo { get; set; }

    [FromQuery(Name = "description")]
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [FromQuery(Name = "customer")]
    [JsonPropertyName("customer")]
    public required string Customer { get; set; }

    [FromQuery(Name = "sales")]
    [JsonPropertyName("sales")]
    public required string Sales { get; set; }

    [FromQuery(Name = "status")]
    [JsonPropertyName("status")]
    public required string Status { get; set; }
}

public class ComplaintDetailModel
{
    [JsonPropertyName("complaintNo")]
    public required string ComplaintNo { get; set; }

    [JsonPropertyName("mpNo")]
    public required string MPNo { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("customer")]
    public required ComplaintCustomerModel Customer { get; set; }

    [JsonPropertyName("sales")]
    public required string Sales { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("assignment")]
    public ComplaintAssignmentModel[] Assignment { get; set; } = [];
}


public class ComplaintCustomerModel
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("address")]
    public required string Address { get; set; }
}


public class ComplaintAssignmentModel
{
    [JsonPropertyName("assignmentNo")]
    public required string AssignmentNo { get; set; }

    [JsonPropertyName("pic")]
    public required string PIC { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }
}
