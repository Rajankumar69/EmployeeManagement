using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Exceptions;
using EmployeeManagement.Infrastructure.Authentication;
using EmployeeManagement.Infrastructure.Configuration;
using EmployeeManagement.Infrastructure.Data;
using EmployeeManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EmployeeManagement.Tests;

public class AuthServiceTests
{
    private readonly ApplicationDbContext _dbContext;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _dbContext = new ApplicationDbContext(options);
        DbInitializer.Initialize(_dbContext);

        var userRepository = new UserRepository(_dbContext);
        
        var jwtSettings = Options.Create(new JwtSettings
        {
            SecretKey = "SuperSecretJwtSigningKeyWithLengthGreaterThan32Bytes!",
            Issuer = "EmployeeManagementApi",
            Audience = "EmployeeManagementClient",
            ExpirationInMinutes = 60
        });

        var tokenGenerator = new JwtTokenGenerator(jwtSettings);
        _authService = new AuthService(userRepository, tokenGenerator);
    }

    [Fact]
    public async Task LoginAsync_ValidAdminCredentials_ReturnsJwtToken()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Username = "admin",
            Password = "Admin@123"
        };

        // Act
        var response = await _authService.LoginAsync(request);

        // Assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Token);
        Assert.Equal("admin", response.Username);
        Assert.Equal("Admin", response.Role);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ThrowsValidationException()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Username = "admin",
            Password = "WrongPassword"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() => _authService.LoginAsync(request));
    }
}
