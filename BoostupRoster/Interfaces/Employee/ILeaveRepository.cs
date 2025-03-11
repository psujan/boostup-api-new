using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces.Employee
{
    public interface ILeaveRepository
    {
        Task<Leave> AddLeave(LeaveRequest request);
        Task<Leave?> RemoveLeave(int Id);
        Task<LeaveResponse?> GetLeaveById(int Id);
        Task<PaginatedResponse<LeaveResponse?>> GetLeave(LeaveFilterRequest request);
        Task<LeaveResponse?> UpdateLeave(int Id, LeaveUpdateRequest request);
    }
}
