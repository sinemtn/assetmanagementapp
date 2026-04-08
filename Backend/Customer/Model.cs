namespace Customer;

public class Model
{
    public required string CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int? BillingAccount { get; set; }
}