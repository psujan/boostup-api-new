using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface ITimesheetRepository : IBaseRepository<Timesheet>
    {
        Task<Timesheet?> Add(ClockInRequest request);
        Task<Timesheet?> Update(ClockoutRequest request);

        Task<PaginatedResponse<TimeSheetResponse>?> GetPaginated(int pageNumber, int pageSize);

    }

}
