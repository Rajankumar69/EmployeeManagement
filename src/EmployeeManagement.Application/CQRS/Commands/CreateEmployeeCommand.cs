using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.CQRS.Commands;

/// <summary>
/// CQRS Write Command representation to create a new employee.
/// </summary>
public record CreateEmployeeCommand(CreateEmployeeDto EmployeeData);
