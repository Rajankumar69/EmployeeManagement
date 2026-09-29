using EmployeeManagement.Application.CQRS.Queries;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Exceptions;

namespace EmployeeManagement.Application.CQRS.Handlers;

/// <summary>
/// CQRS Read Query Handler responsible ONLY for executing the read query (Single Responsibility Principle).
/// </summary>
public class GetEmployeeByIdQueryHandler
{
    private readonly IEmployeeRepository _repository;

    public GetEmployeeByIdQueryHandler(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<EmployeeDto> HandleAsync(GetEmployeeByIdQuery query)
    {
        var employee = await _repository.GetByIdAsync(query.Id);
        if (employee == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.Employee), query.Id);
        }

        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            Department = employee.Department,
            Salary = employee.Salary,
            DateOfJoining = employee.DateOfJoining
        };
    }
}
