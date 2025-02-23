using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces.Employee
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDetailResponse?> AddEmployee(User user, string? phone);
        Task<EmployeeDetailResponse?> GetById(int id);

    }
}
