namespace Application.Responses;

public record StockBalanceResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid WarehouseId { get; init; }
    public string WarehouseName { get; init; }
    public int Quantity { get; init; }
}