namespace Application.Requests;

public record ProductRequest
{
    public string Name { get; init; }
    public decimal Price { get; init; }
    public int MinStockQuantity { get; init; }
    public Guid? CategoryId { get; init; }
}