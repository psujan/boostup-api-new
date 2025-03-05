using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;

namespace Boostup.API.Interfaces.Employee
{
    public interface ILeaveRepository
    {
        Task<Leave> AddLeave(LeaveRequest request);
        Task<Leave?> RemoveLeave(int Id);
    }
}
