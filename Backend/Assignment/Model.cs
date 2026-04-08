namespace Assignment;

public class AssignmentModel
{
    public required string MPNo { get; set; }
    public required string Status { get; set; }
    public Customer.Model? Customer { get; set; }
    public required string Task { get; set; }
    public string? AssignmentNo { get; set; }
    public required string PIC { get; set; }
    public string ComplaintNo { get; set; } = string.Empty;
    public ItemModel[] Items { get; set; } = [];
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
    public DateTime? ValidatedAt { get; set; }
    public string? ValidatedBy { get; set; }
    public DateTime? AuthorizedAt { get; set; }
    public string? AuthorizedBy { get; set; }
}


public class ItemModel
{
    public required string Type { get; set; }
    public required string ItemId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public int Quantity { get; set; }

}