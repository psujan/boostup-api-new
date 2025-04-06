using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces;

namespace Boostup.API.Repositories
{
    public class TimeSheetRepository :BaseRepository<Timesheet> , ITimesheetRepository
    {
        public TimeSheetRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            
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

            // Do Not clock Out if already done
            if(timesheet.UpdatedAt != null)
            {
                throw new Exception("Timesheet Record Is Already Updated. Can't Clock Out Multiple Times");
            }

            timesheet.ClockOut = request.ClockOut;
            timesheet.TotalHous = request.TotalHours;
            timesheet.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
            return timesheet;
        }
    }
}
