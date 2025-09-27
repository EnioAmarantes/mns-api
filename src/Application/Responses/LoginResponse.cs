namespace Application.Responses;

public record LoginResponse(string Token, string CompanyId, string UserId, string Email);