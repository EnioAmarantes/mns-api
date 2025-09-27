namespace Domain.Entities;

public class Warehouse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
}