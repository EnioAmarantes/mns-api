public record FinanceResponse
{
    public Guid Id { get; init; }
    public string Type { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Amount { get; init; } = 0;
    public DateTime DueDate { get; init; } = DateTime.Now;
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; } = DateTime.Now;
}

