namespace Domain.Entities;

public record Category
{
    public Guid Id { get; init; }
    public Guid CompanyId { get; init; }
    public Company Company { get; init; }
    public string Name { get; init; }
}