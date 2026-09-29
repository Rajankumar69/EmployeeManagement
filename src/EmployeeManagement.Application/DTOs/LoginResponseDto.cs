namespace EmployeeManagement.Application.DTOs;

/// <summary>
/// Data Transfer Object for authentication login response containing JWT token details.
/// </summary>
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
