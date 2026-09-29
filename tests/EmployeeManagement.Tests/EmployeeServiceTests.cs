using EmployeeManagement.Application.CQRS.Handlers;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Services;
using EmployeeManagement.Domain.Exceptions;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Tests;

public class EmployeeServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        DbInitializer.Initialize(_dbContext);

        var repo = new EmployeeRepository(_dbContext);
        var getByIdHandler = new GetEmployeeByIdQueryHandler(repo);
        var createHandler = new CreateEmployeeCommandHandler(repo);

        _employeeService = new EmployeeService(repo, getByIdHandler, createHandler);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ReturnsSeededEmployees()
    {
        // Act
        var employees = await _employeeService.GetAllEmployeesAsync();

        // Assert
        Assert.NotEmpty(employees);
        Assert.True(employees.Count() >= 3);
    }

    [Fact]
    public async Task CreateEmployeeAsync_ValidDto_AddsEmployee()
    {
        // Arrange
        var newEmp = new CreateEmployeeDto
        {
            FirstName = "Alice",
            LastName = "Wonderland",
            Email = "alice@company.com",
            Department = "IT",
            Salary = 95000,
            DateOfJoining = DateTime.UtcNow
        };

        // Act
        var result = await _employeeService.CreateEmployeeAsync(newEmp);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Id > 0);
        Assert.Equal("Alice", result.FirstName);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_NonExistingId_ThrowsNotFoundException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _employeeService.GetEmployeeByIdAsync(99999));
    }
}
