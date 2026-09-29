namespace EmployeeManagement.Application.DTOs;

/// <summary>
/// Data Transfer Object for sending Employee response details to the API client.
/// </summary>
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public decimal Salary { get; set; }
    public DateTime DateOfJoining { get; set; }
}
