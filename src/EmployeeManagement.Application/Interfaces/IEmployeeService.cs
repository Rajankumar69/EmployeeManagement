using EmployeeManagement.Application.DTOs;

namespace EmployeeManagement.Application.Interfaces;

/// <summary>
/// Application Service Interface for Employee Management operations.
/// </summary>
public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto> GetEmployeeByIdAsync(int id);
    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
    Task UpdateEmployeeAsync(int id, UpdateEmployeeDto updateDto);
    Task DeleteEmployeeAsync(int id);
}
