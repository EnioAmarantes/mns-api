namespace Application.Responses;

public record SaleResponse
{
    public Guid Id { get; init; }
    public DateTime Date { get; init; }
    public IList<SaleItemResponse> Items { get; init; } = new List<SaleItemResponse>();
    public string Status { get; init; }
    public decimal Total { get; init; }
}