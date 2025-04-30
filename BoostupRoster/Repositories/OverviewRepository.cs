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
        public Task<object> GetAdminDashboardOverview()
        {
            throw new NotImplementedException();
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
