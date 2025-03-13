//using Boostup.API.Entities;

//using Roster = Boostup.API.Entities.Roster;

using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces
{
    public interface IRosterRepository
    {
        Task<List<Roster>> AddRoster(List<RosterRequest> request);
        Task<Roster> DeleteRoster(int Id);
        Task<IEnumerable<EmployeeWithRosterResponse>?> ListRoster(RosterFilterRequest request);
    }
}
