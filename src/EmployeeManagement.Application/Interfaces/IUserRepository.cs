using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Interfaces;

/// <summary>
/// Repository Interface defining data access contracts for User Accounts.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}
