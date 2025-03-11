using Boostup.API.Entities;

namespace Boostup.API.Interfaces
{
    public interface ILeaveTypeRepository
    {
        Task<IEnumerable<LeaveType>> GetAll();
        Task<LeaveType?> GetById(int id);
        Task<LeaveType?> Delete(int id);
        Task<LeaveType?> Add(LeaveType leaveType);
    }
}
