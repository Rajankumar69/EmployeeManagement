using EmployeeManagement.Application.CQRS.Commands;
using EmployeeManagement.Application.CQRS.Handlers;
using EmployeeManagement.Application.CQRS.Queries;
using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Domain.Exceptions;

namespace EmployeeManagement.Application.Services;

/// <summary>
/// Service implementation for Employee Management. Demonstrates Service Pattern and integration with CQRS handlers.
/// </summary>
public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly GetEmployeeByIdQueryHandler _getByIdQueryHandler;
    private readonly CreateEmployeeCommandHandler _createCommandHandler;

    public EmployeeService(
        IEmployeeRepository repository,
        GetEmployeeByIdQueryHandler getByIdQueryHandler,
        CreateEmployeeCommandHandler createCommandHandler)
    {
        _repository = repository;
        _getByIdQueryHandler = getByIdQueryHandler;
        _createCommandHandler = createCommandHandler;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();
        return employees.Select(e => new EmployeeDto
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            Department = e.Department,
            Salary = e.Salary,
            DateOfJoining = e.DateOfJoining
        });
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
        // Delegates to CQRS Read Handler example
        return await _getByIdQueryHandler.HandleAsync(new GetEmployeeByIdQuery(id));
    }

    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
    {
        // Delegates to CQRS Write Handler example
        return await _createCommandHandler.HandleAsync(new CreateEmployeeCommand(createDto));
    }

    public async Task UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        if (await _repository.EmailExistsAsync(updateDto.Email, id))
        {
            throw new ValidationException($"An employee with email '{updateDto.Email}' already exists.");
        }

        employee.FirstName = updateDto.FirstName;
        employee.LastName = updateDto.LastName;
        employee.Email = updateDto.Email;
        employee.Department = updateDto.Department;
        employee.Salary = updateDto.Salary;
        employee.DateOfJoining = updateDto.DateOfJoining;

        await _repository.UpdateAsync(employee);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
        {
            throw new NotFoundException(nameof(Employee), id);
        }

        await _repository.DeleteAsync(id);
    }
}
