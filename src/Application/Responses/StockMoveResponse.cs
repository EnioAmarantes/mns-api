namespace Application.Responses;

public record StockMoveResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public Guid WarehouseId { get; init; }
    public Guid? SupplierId { get; init; }
    public int Quantity { get; init; }
    public DateTime Date { get; init; } = DateTime.Now;
    public string Type { get; init; }
}