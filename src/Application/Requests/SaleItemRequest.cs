namespace Application.Requests;

public record SaleItemRequest
{
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}