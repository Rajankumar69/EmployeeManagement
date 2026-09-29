namespace EmployeeManagement.Domain.Exceptions;

/// <summary>
/// Domain exception thrown when a requested resource or entity is not found.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" with key ({key}) was not found.")
    {
    }
}
