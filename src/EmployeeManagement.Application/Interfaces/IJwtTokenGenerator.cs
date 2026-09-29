using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces;

/// <summary>
/// Interface for JWT token generation service.
/// </summary>
public interface IJwtTokenGenerator
{
    (string Token, DateTime ExpiresAt) GenerateToken(User user);
}
