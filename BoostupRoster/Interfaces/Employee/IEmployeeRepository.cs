using Boostup.API.Entities;

namespace Boostup.API.Interfaces.Employee
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDetail?> AddEmployee(User user, string? phone);
    }
}
