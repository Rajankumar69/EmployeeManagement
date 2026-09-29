namespace EmployeeManagement.Domain.Entities;

/// <summary>
/// Domain Entity representing an Application User for Authentication & Authorization.
/// </summary>
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty; // e.g. "Admin", "User", "Manager"
    public string Department { get; set; } = string.Empty;
}
