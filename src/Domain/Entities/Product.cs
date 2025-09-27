namespace Domain.Entities;

public record Product
{
    public Guid Id { get; init; }
    public Guid CompanyId { get; init; }
    public Company Company { get; init; }
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int MinStockQuantity { get; init; }
}