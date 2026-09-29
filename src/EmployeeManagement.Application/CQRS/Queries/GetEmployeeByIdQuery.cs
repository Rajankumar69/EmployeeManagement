namespace EmployeeManagement.Application.CQRS.Queries;

/// <summary>
/// CQRS Read Query representation to fetch an employee by ID.
/// </summary>
public record GetEmployeeByIdQuery(int Id);
