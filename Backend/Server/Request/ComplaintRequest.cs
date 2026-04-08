namespace Server.Request;

public class ComplaintRequest
{
    public required string ComplaintNo { get; set; }
    public required string MPNo { get; set; }
    public required string Description { get; set; }
    public required string Customer { get; set; }
    public required string Sales { get; set; }
    public required string Status { get; set; }
}