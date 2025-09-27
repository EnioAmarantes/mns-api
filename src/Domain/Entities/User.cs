using Application.Requests;

namespace Domain.Entities;

public record User
{
    public Guid Id { get; init; }
    public Guid CompanyId { get; init; }
    public Company Company { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PasswordHash { get; init; }
    public bool MustChangePassword { get; init; }
}