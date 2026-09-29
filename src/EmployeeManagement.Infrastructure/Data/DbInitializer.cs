using EmployeeManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Infrastructure.Data;

/// <summary>
/// Database Initializer for Seeding sample data upon startup.
/// </summary>
public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        // Seed Employees if empty
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@company.com",
                    Department = "Engineering",
                    Salary = 85000,
                    DateOfJoining = new DateTime(2022, 1, 15)
                },
                new Employee
                {
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@company.com",
                    Department = "Human Resources",
                    Salary = 75000,
                    DateOfJoining = new DateTime(2021, 5, 20)
                },
                new Employee
                {
                    FirstName = "Robert",
                    LastName = "Johnson",
                    Email = "robert.j@company.com",
                    Department = "Finance",
                    Salary = 92000,
                    DateOfJoining = new DateTime(2020, 11, 1)
                }
            );
        }

        // Seed Users if empty
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("Admin@123"),
                    Email = "admin@company.com",
                    Role = "Admin",
                    Department = "Management"
                },
                new User
                {
                    Username = "user",
                    PasswordHash = HashPassword("User@123"),
                    Email = "user@company.com",
                    Role = "User",
                    Department = "Engineering"
                },
                new User
                {
                    Username = "manager",
                    PasswordHash = HashPassword("Manager@123"),
                    Email = "manager@company.com",
                    Role = "Manager",
                    Department = "HR"
                }
            );
        }

        context.SaveChanges();
    }

    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
