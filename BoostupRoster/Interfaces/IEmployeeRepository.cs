using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<EmployeeBasicResponse> GetEmployeeFromUserId(string userId);
        Task<EmployeeDetailResponse?> AddEmployee(User user, string? phone);
        Task<EmployeeDetailResponse?> GetById(int id);
        Task<PaginatedResponse<EmployeeDetailResponse?>> GetPaginated(int pageNo, int pageSize);
        Task<EmployeeDetailResponse?> UpdateEmployee(EmployeeProfileUpdateRequest request);
        Task<EmployeeProfileImage> UpdateProfileImage(EmployeeProfileImageRequest request);
        Task<IEnumerable<EmployeeBasicResponse>?> GetAll();

    }
}
