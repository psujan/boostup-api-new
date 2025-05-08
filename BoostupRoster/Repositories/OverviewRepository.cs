using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Boostup.API.Repositories
{
    public class OverviewRepository : IOverviewRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OverviewRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<object> GetAdminDashboardOverview()
        {
           var totalEmployee = await dbContext.Set<EmployeeDetail>().CountAsync();
           var totalShift = await dbContext.Set<Roster>().CountAsync();
           var totalClockIns = await dbContext.Set<Timesheet>().CountAsync();
           var totalJobs = await dbContext.Set<Jobs>().CountAsync();
           var totalLeaves = await dbContext.Set<Leave>().CountAsync();
           
            return new
            {
                TotalEmployee = totalEmployee,
                TotalShift = totalShift,
                TotalClockIns = totalClockIns,
                TotalJobs = totalJobs,
                TotalLeaves = totalLeaves
            };
        }

        public async Task<EmployeeOverviewResponse> GetEmployeeDashboardOverview(int id)
        {
            var totalShifts = await dbContext.Set<Roster>().Where(r => r.EmployeeId == id).CountAsync();
            var totalHours = await dbContext.Set<Timesheet>().Where(t => t.EmployeeId == id).SumAsync(t => t.TotalHours);
            return new EmployeeOverviewResponse()
            {
                TotalHours = (decimal)totalHours,
                TotalShifts = totalShifts
            };
            
        }
    }
}
