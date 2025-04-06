using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;

namespace Boostup.API.Interfaces
{
    public interface ITimesheetRepository : IBaseRepository<Timesheet>
    {
        Task<Timesheet?> Add(ClockInRequest request);
        Task<Timesheet?> Update(ClockoutRequest request);

    }

}
