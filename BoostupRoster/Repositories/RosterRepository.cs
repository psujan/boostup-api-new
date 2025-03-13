using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class RosterRepository : IRosterRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public RosterRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeWithRosterResponse>?> ListRoster(RosterFilterRequest request)
        {
            var startDate = DateOnly.Parse(request.From);
            var endDate = DateOnly.Parse(request.To);
            // var rows = await dbContext.EmployeeDetail.Where(emp => request.EmployeeIds.Contains(emp.Id)).ToListAsync();
            var rows = await dbContext.EmployeeDetail
                .Where(emp => request.EmployeeIds.Contains(emp.Id))
                .Include(emp => emp.User)
                .Include(emp => emp.Rosters.Where(
                    roster => roster.Date <= endDate && roster.Date >= startDate))
                .ThenInclude(roster => roster.Job)
                .AsNoTracking()
                .ToListAsync();
            var mappedData = mapper.Map<IEnumerable<EmployeeWithRosterResponse>>(rows);
            return mappedData;
        }

        public async Task<List<Roster>> AddRoster(List<RosterRequest> request)
        {
            var roster = new List<Roster>();
            for (int i = 0; i < request.Count; i++)
            {
                var rosterRow = new Roster()
                {
                    Date = DateOnly.Parse(request[i].Date),
                    WorkHours = request[i].WorkHours,
                    StartTime = request[i].StartTime,
                    EndTime = request[i].EndTime,
                    JobId = request[i].JobId,
                    EmployeeId = request[i].EmployeeId,
                    Notes = request[i].Notes,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                dbContext.Roster.Add(rosterRow);
                await dbContext.SaveChangesAsync();
                roster.Add(rosterRow);
            }
            return roster;
        }

        public async Task<Roster> DeleteRoster(int Id)
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
