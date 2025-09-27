namespace Application.Responses;

public record WarehouseResponse {
    public Guid Id { get; init; }
    public string Name { get; init; }
}