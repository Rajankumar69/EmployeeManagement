using EmployeeManagement.Application.CQRS.Commands;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Exceptions;

namespace EmployeeManagement.Application.CQRS.Handlers;

/// <summary>
/// CQRS Write Command Handler responsible ONLY for creating an employee (Single Responsibility Principle).
/// </summary>
public class CreateEmployeeCommandHandler
{
    private readonly IEmployeeRepository _repository;

    public CreateEmployeeCommandHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto> HandleAsync(CreateEmployeeCommand command)
    {
        var dto = command.EmployeeData;

        if (await _repository.EmailExistsAsync(dto.Email))
        {
            throw new ValidationException($"An employee with email '{dto.Email}' already exists.");
        }

        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Department = dto.Department,
            Salary = dto.Salary,
            DateOfJoining = dto.DateOfJoining
        };

        var created = await _repository.AddAsync(employee);

        return new EmployeeDto
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Email = created.Email,
            Department = created.Department,
            Salary = created.Salary,
            DateOfJoining = created.DateOfJoining
        };
    }
}
