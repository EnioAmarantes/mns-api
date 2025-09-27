namespace Application.Requests;

public record ChangePasswordRequest(string Email, string NewPassword, string ConfirmPassword);