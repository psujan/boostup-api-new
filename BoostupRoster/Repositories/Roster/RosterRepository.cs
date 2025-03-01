using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Roster;

namespace Boostup.API.Repositories.Roster
{
    public class RosterRepository : IRosterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public RosterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Entities.Roster>> AddRoster(List<RosterRequest> request)
        {
            var roster = new List<Boostup.API.Entities.Roster>();
            for (int i = 0; i < request.Count; i++)
            {
                var rosterRow = new Entities.Roster()
                {
                    Date = request[i].Date,
                    WorkHours = request[i].WorkHours,
                    StartTime = request[i].StartTime,
                    EndTime = request[i].EndTime,
                    JobId = request[i].JobId,
                    EmployeeId = request[i].EmployeeId,
                    Notes = request[i].Notes,
                };
                dbContext.Roster.Add(rosterRow);
                await dbContext.SaveChangesAsync();
                roster.Add(rosterRow);
            }
            return roster;
        }

        public async Task<Entities.Roster> DeleteRoster(int Id)
        {
            var roster = await dbContext.Roster.FindAsync(Id);
            if (roster == null)
            {
                throw new Exception("Roster Not Found");
            }
            dbContext.Roster.Remove(roster);
            await dbContext.SaveChangesAsync();
            return roster;
        }
    }
}
