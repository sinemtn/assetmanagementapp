namespace Complaint;

public class ComplaintModel
{
    public string ComplaintNo { get; set; } = string.Empty;
    public required string MPNo { get; set; }
    public required string Description { get; set; }
    public Customer.Model? Customer { get; set; }
    public required string Sales { get; set; }
    public required string Status { get; set; }
    public Assignment.AssignmentModel[] Assignment { get; set; } = [];
}

