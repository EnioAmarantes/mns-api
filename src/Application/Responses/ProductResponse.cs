using Domain.Entities;

namespace Application.Responses;

public record ProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public int MinStockQuantity { get; init; }
    public CategoryResponse? Category { get; init; }
    public required List<StockBalanceResponse> StockBalances { get; init; }
}