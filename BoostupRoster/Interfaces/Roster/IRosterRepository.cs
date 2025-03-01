//using Boostup.API.Entities;

//using Roster = Boostup.API.Entities.Roster;

using Boostup.API.Entities.Dtos.Request;

namespace Boostup.API.Interfaces.Roster
{
    public interface IRosterRepository
    {
        Task<List<Boostup.API.Entities.Roster>> AddRoster(List<RosterRequest> request);
        Task<Boostup.API.Entities.Roster> DeleteRoster(int Id);
    }
}
