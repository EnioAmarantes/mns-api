namespace Application.Requests;

public record StockMoveRequest
{
    public Guid ProductId { get; init; }
    public Guid WarehouseId { get; init; }
    public Guid? SupplierId { get; init; }
    public int Quantity { get; init; }
    public DateTime Date { get; init; }
    public string Type { get; init; }
}