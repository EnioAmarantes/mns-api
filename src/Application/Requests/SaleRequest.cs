namespace Application.Requests;

public record SaleRequest
{
    public DateTime Date { get; init; }
    public IList<SaleItemRequest> Items { get; init; } = new List<SaleItemRequest>();
    public string Status { get; init; }
    public decimal Total { get; init; }
}