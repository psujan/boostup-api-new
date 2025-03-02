//using Boostup.API.Entities;

//using Roster = Boostup.API.Entities.Roster;

using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Interfaces.Roster
{
    public interface IRosterRepository
    {
        Task<List<Boostup.API.Entities.Roster>> AddRoster(List<RosterRequest> request);
        Task<Boostup.API.Entities.Roster> DeleteRoster(int Id);
        Task<IEnumerable<EmployeeWithRosterResponse>?> ListRoster(RosterFilterRequest request);
    }
}
