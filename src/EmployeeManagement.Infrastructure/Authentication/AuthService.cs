using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Exceptions;
using EmployeeManagement.Infrastructure.Data;

namespace EmployeeManagement.Infrastructure.Authentication;

/// <summary>
/// Implementation of Authentication Service handling login and token generation.
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthService(IUserRepository userRepository, IJwtTokenGenerator tokenGenerator)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        
        if (user == null || user.PasswordHash != DbInitializer.HashPassword(loginRequest.Password))
        {
            throw new ValidationException("Invalid username or password.");
        }

        var (token, expiresAt) = _tokenGenerator.GenerateToken(user);

        return new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role,
            ExpiresAt = expiresAt
        };
    }
}
