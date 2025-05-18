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
    public class TimeSheetRepository :BaseRepository<Timesheet> , ITimesheetRepository
    {
        private readonly IMapper mapper;

        public TimeSheetRepository(ApplicationDbContext dbContext , IMapper mapper):base(dbContext) 
        {
            this.mapper = mapper;
        }

        public async new Task<PaginatedResponse<TimeSheetResponse>?> GetPaginated(int pageNumber, int pageSize)
        {
            var rows = await dbContext.Set<Timesheet>()
                .Include(t => t.Employee)
                .ThenInclude(emp => emp.User)
                .Include(t=>t.Job)
                 .OrderByDescending(x => EF.Property<object>(x, "Id")).Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            var totalCount = await dbContext.Set<Timesheet>().CountAsync();
            var resultCount = rows.Count();
            var mappedRows = mapper.Map<IEnumerable<TimeSheetResponse>>(rows);
            return new PaginatedResponse<TimeSheetResponse>(mappedRows, totalCount, resultCount, pageNumber, pageSize);
        }

        public async Task<Timesheet?> Add(ClockInRequest request)
        {
            return  await base.Add(new Timesheet()
            {
                EmployeeId = request.EmployeeId,
                RosterId = request.RosterId,
                JobId = request.JobId,
                ClockIn = request.ClockIn,
                CreatedAt = DateTime.UtcNow,
                Date = DateOnly.Parse(request.Date),
            });
        }

        public async Task<Timesheet?> Update(ClockoutRequest request)
        {
            var timesheet =  await base.GetById(request.TimeSheetId);
            if (timesheet == null) {
                throw new Exception("Timesheet Record Not Found");
            }

            //// Do Not clock Out if already done
            //if(timesheet.UpdatedAt != null)
            //{
            //    throw new Exception("Timesheet Record Is Already Updated. Can't Clock Out Multiple Times");
            //}

            timesheet.ClockOut = request.ClockOut;
            timesheet.TotalHours = Decimal.Parse(request.TotalHours);
            timesheet.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return timesheet;
        }
    }
}
