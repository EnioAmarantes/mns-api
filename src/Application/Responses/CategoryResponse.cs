namespace Application.Responses;

public record CategoryResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}