using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IRosterRepository
    {
        Task<List<Roster>> AddRoster(List<RosterRequest> request);
        Task<Roster?> DeleteRoster(int Id);
        //Task<IEnumerable<EmployeeWithRosterResponse>?> ListRoster(RosterFilterRequest request);
        Task<Roster?> SwapRoster(RosterSwapRequest request);
        Task<PaginatedResponse<EmployeeWithRosterResponse?>?> ListRoster(RosterFilterRequest request);
        Task<RosterResponse> GetById(int id);
        Task<PaginatedResponse<RosterResponse?>?> GetByEmployeeId(int id, int pageNumber , int pageSize, string from, string to);
    }
}
