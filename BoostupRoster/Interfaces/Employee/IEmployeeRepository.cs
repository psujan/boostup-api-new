using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces.Employee
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDetailResponse?> AddEmployee(User user, string? phone);
        Task<EmployeeDetailResponse?> GetById(int id);

        Task<PaginatedResponse<EmployeeDetailResponse?>> GetPaginated(int pageNo, int pageSize);

    }
}
