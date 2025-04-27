using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class RosterRepository : BaseRepository<Roster> , IRosterRepository
    {
        private readonly IMapper mapper;

        public RosterRepository(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task<PaginatedResponse<EmployeeWithRosterResponse?>?> ListRoster(RosterFilterRequest request)
        {
            var startDate = DateOnly.Parse(request.From);
            var endDate = DateOnly.Parse(request.To);
            int pageNumber =  request.PageNumber;
            int pageSize = request.PageSize;

            IQueryable<EmployeeDetail> query = dbContext.Set<EmployeeDetail>();

            if(request.EmployeeIds != null)
            {
                query = query.Where(emp => request.EmployeeIds.Contains(emp.Id));
            }

            query = query.Include(emp => emp.User)
                        .Include(emp => emp.Rosters.Where(
                            roster => roster.Date <= endDate && roster.Date >= startDate))
                        .ThenInclude(roster => roster.Job);

            query = query.OrderByDescending(x => x.User.FullName);
            query = query.Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);
                        
            var rows = await query.AsNoTracking().ToListAsync();
            //// var rows = await dbContext.EmployeeDetail.Where(emp => request.EmployeeIds.Contains(emp.Id)).ToListAsync();
            //var rows = await dbContext.EmployeeDetail
            //    .Where(emp => request.EmployeeIds.Contains(emp.Id))
            //    .Include(emp => emp.User)
            //    .Include(emp => emp.Rosters.Where(
            //        roster => roster.Date <= endDate && roster.Date >= startDate))
            //    .ThenInclude(roster => roster.Job)
            //    .AsNoTracking()
            //    .ToListAsync();
            var mappedData = mapper.Map<IEnumerable<EmployeeWithRosterResponse>>(rows);
            var totalCount = await dbContext.EmployeeDetail.CountAsync();
            var resultCount = rows.Count();
            return new PaginatedResponse<EmployeeWithRosterResponse?>(mappedData, totalCount, resultCount, pageNumber, pageSize);
            //return mappedData;
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

        public async Task<Roster?> DeleteRoster(int Id)
        {
            var roster = await dbContext.Roster.FindAsync(Id);
            if (roster == null)
            {
                throw new Exception("Roster Not Found");
            }
            return await base.Delete(Id);
        }

        public async Task<Roster?> SwapRoster(RosterSwapRequest request)
        {
            var roster = await dbContext.Roster.FindAsync(request.RosterId);
            if (roster == null)
            {
                throw new Exception("Roster Not Found");
            }
            roster.EmployeeId = request.EmployeeId;
            await dbContext.SaveChangesAsync();
            return roster;
        }

        public async Task<RosterResponse> GetById(int id)
        {
            var row = await dbContext.Roster
                            .Include(r => r.Employee)
                            .ThenInclude(emp => emp.User)
                            .Include(r => r.Leaves)
                            .ThenInclude(l => l.LeaveType)
                            .Include(r => r.Job)
                            .FirstOrDefaultAsync(x => x.Id == id);
            if (row == null)
            {
                throw new Exception("Roster Not Found");
            }
            return mapper.Map<RosterResponse>(row);
        }

        public async Task<PaginatedResponse<RosterResponse?>?> GetByEmployeeId(int id, int pageNumber, int pageSize, string from, string to)
        {
            var rosterFrom = DateOnly.Parse(from);
            var rosterTo = DateOnly.Parse(to);
            IQueryable<Roster> query = dbContext.Set<Roster>();
            query = query.Where(r => r.EmployeeId == id);
            query = query.Where(r => r.Date >= rosterFrom && r.Date <= r.Date);
            query = query.Include(r => r.Employee).ThenInclude(e => e.User);
            query = query.Include(r => r.Job);
            query = query.Include(r => r.Leaves).ThenInclude(l => l.LeaveType);
            query = query.Include(r => r.Timesheets);
            var totalCount = await query.CountAsync();
            query = query.Skip((pageNumber - 1) * pageSize)
                       .Take(pageSize);
            var rows = await query.AsNoTracking().ToListAsync();
            var mappedData = mapper.Map<IEnumerable<RosterResponse>>(rows);
            var resultCount = rows.Count();
            return new PaginatedResponse<RosterResponse?>(mappedData, totalCount, resultCount, pageNumber, pageSize);

        }
    }
}
