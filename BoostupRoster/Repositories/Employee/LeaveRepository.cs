using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Employee;
using Boostup.API.Migrations;

namespace Boostup.API.Repositories.Employee
{
    public class LeaveRepository:ILeaveRepository
    {
        private readonly ApplicationDbContext dbContext;

        public LeaveRepository(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public async Task<Leave> AddLeave(LeaveRequest request)
        {
            var leave = new Leave()
            {
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                From = request.From,
                To = request.To,
                IsPaidLeave = request.IsPaidLeave,
                ForSingleDay = request.ForSingleDay,
                CreatedAt   = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            dbContext.Leave.Add(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<Leave> RemoveLeave(int Id)
        {
            var leave = await dbContext.Leave.FindAsync(Id);
            if (leave == null)
            {
                throw new Exception("Leave Not Found");
            }
            dbContext.Leave.Remove(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }
    }
}
